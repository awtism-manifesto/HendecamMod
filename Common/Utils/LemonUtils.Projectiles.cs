using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria.DataStructures;
using Terraria.GameContent;

namespace HendecamMod.Common.Utils;

/// <summary>
/// Contains a lot of utillities and global usings
/// </summary>
public static partial class LemonUtils
{
    /// <param name="projectile"></param>
    /// <returns>Main.player[projectile.owner]</returns>
    public static Player GetOwner(this Projectile projectile)
    {
        return Main.player[projectile.owner];
    }

    /// <summary>
    /// Used as a shorthand for spawning projectiles.
    /// Automatically checks whether the entity spawning is a Projectile or NPC and adjusts params accordingly.
    /// If entity is a projectile, the new projectile will inherit its owner and damage.
    /// If entity is an NPC, the new projectile will inherit its damage, which will be adjusted for difficulty.
    /// Should not be used for player-spawned projectiles.
    /// The source is always whateverEntity.GetSource_FromThis(), so use Projectile.NewProjectile() for something more specific.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="position"></param>
    /// <param name="velocity"></param>
    /// <param name="type"></param>
    /// <param name="damage"></param>
    /// <param name="knockback"></param>
    /// <param name="owner"></param>
    /// <param name="ai0"></param>
    /// <param name="ai1"></param>
    /// <param name="ai2"></param>
    /// <returns>The spawned projectile</returns>
    public static Projectile QuickProj(Entity entity, Vector2 position, Vector2 velocity, int type, float damage = -1, float knockback = 1, int owner = -1, float ai0 = 0, float ai1 = 0, float ai2 = 0)
    {
        IEntitySource source = null;
        if (entity is Projectile proj)
        {
            if (damage == -1) damage = proj.damage;
            if (owner == -1) owner = proj.owner;
            source = proj.GetSource_FromThis();
        }
        else if (entity is NPC npc)
        {
            if (damage == -1) damage = npc.damage;

            if (!Main.expertMode)
            {
                damage *= 0.5f;
            }
            else if (Main.expertMode)
            {
                damage *= 0.25f;
            }
            else if (Main.masterMode)
            {
                damage *= 1 / 6f;
            }

            source = npc.GetSource_FromThis();
        }

        return Projectile.NewProjectileDirect(source, position, velocity, type, (int)damage, knockback, owner, ai0, ai1, ai2);
    }

    /// <summary>
    /// Gets the projectile's texture from TextureAssets.Projectile.
    /// </summary>
    /// <param name="projectile"></param>
    /// <returns></returns>
    public static Texture2D GetTexture(this Projectile projectile)
    {
        return TextureAssets.Projectile[projectile.type].Value;
    }

    /// <summary>
    /// Manually draws the projectile with the correct origin.
    /// </summary>
    /// <param name="projectile"></param>
    /// <param name="color"></param>
    public static void DrawProjectile(this Projectile projectile, Color color)
    {
        Texture2D texture = TextureAssets.Projectile[projectile.type].Value;
        Rectangle sourceRect = texture.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
        Vector2 drawPos = projectile.Center - Main.screenPosition;
        Vector2 drawOrigin = new Vector2(sourceRect.Width, sourceRect.Height) * 0.5f;
        Main.EntitySpriteDraw(texture, drawPos, sourceRect, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0);
    }

    /// <summary>
    /// Simple afterimage drawing.
    /// </summary>
    /// <param name="Projectile"></param>
    /// <param name="lightColor"></param>
    /// <param name="opacityMultiplier"></param>
    public static void DrawAfterimages(this Projectile Projectile, Color lightColor, float opacityMultiplier = 1f)
    {
        Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
        Rectangle sourceRect = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
        Vector2 drawOrigin = new Vector2(sourceRect.Width, sourceRect.Height) * 0.5f;
        for (int k = Projectile.oldPos.Length - 1; k > 0; k--)
        {
            Vector2 drawPos = Projectile.oldPos[k] + new Vector2(Projectile.width, Projectile.height) * 0.5f - Main.screenPosition;
            Color color = (Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length)) * opacityMultiplier;
            Main.EntitySpriteDraw(texture, drawPos, sourceRect, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
        }
    }

    /// <summary>
    /// Whether a projectile is a true melee projectile.
    /// Is not 100% guaranteed to be correct.
    /// </summary>
    /// <param name="proj"></param>
    /// <returns></returns>
    public static bool CountsAsTrueMelee(this Projectile proj)
    {
        if (proj.TryGetOwner(out Player owner))
        {
            if (proj.friendly && proj.CountsAsClass(DamageClass.Melee) && owner.heldProj == proj.whoAmI)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Gets closest chaseable NPC to a position under minDistance, returns null if none found.
    /// Pass in an NPC's whoAmI to exlude them from being returned.
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="maxDistance"></param>
    /// <returns></returns>
    public static NPC GetClosestNPC(Vector2 pos, float maxDistance = 0, params int[] excludeWhoAmIs)
    {
        NPC closestEnemy = null;
        if (maxDistance == 0) maxDistance = 99999;
        foreach (var npc in Main.ActiveNPCs)
        {
            if (excludeWhoAmIs.Contains(npc.whoAmI))
            {
                continue;
            }
            if (npc.CanBeChasedBy() && (npc.Distance(pos) < maxDistance))
            {
                if (closestEnemy == null)
                {
                    closestEnemy = npc;
                }
                float distanceToNPC = pos.Distance(npc.Center);
                if (distanceToNPC < pos.Distance(closestEnemy.Center))
                {
                    closestEnemy = npc;
                }
            }
        }
        return closestEnemy;
    }

    /// <summary>
    /// Gets closest alive player.
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="maxDistance"></param>
    /// <returns></returns>
    public static Player GetClosestPlayer(Vector2 pos, float maxDistance = 0)
    {
        Player closestPlayer = null;
        if (maxDistance == 0) maxDistance = 99999;
        foreach (var player in Main.ActivePlayers)
        {
            if (player.IsAlive() && (player.Distance(pos) < maxDistance))
            {
                if (closestPlayer == null)
                {
                    closestPlayer = player;
                }
                float distanceToCurrentPlayer = pos.Distance(player.Center);
                if (distanceToCurrentPlayer < pos.Distance(closestPlayer.Center))
                {
                    closestPlayer = player;
                }
            }
        }
        return closestPlayer;
    }

    /// <summary>
    /// Animates the projectile with equal frame duration.
    /// </summary>
    /// <param name="proj"></param>
    /// <param name="frameDuration"></param>
    /// <param name="maxFrames"></param>
    public static void StandardAnimation(this Projectile proj, int frameDuration, int maxFrames)
    {
        proj.frameCounter++;
        if (proj.frameCounter == frameDuration)
        {
            proj.frame++;
            proj.frameCounter = 0;
            if (proj.frame >= maxFrames)
            {
                proj.frame = 0;
            }
        }
    }

    /// <summary>
    /// Gets random position on the Projectile's hitbox.
    /// Increase fluffX and fluffY to expand the range.
    /// Useful for spawning dusts on the Projectile.
    /// </summary>
    /// <param name="proj"></param>
    /// <param name="fluffX"></param>
    /// <param name="fluffY"></param>
    /// <returns></returns>
    public static Vector2 RandomPos(this Projectile proj, float fluffX = 0, float fluffY = 0)
    {
        Vector2 pos = proj.position + new Vector2(Main.rand.NextFloat(-fluffX, proj.width + fluffX), Main.rand.NextFloat(-fluffY, proj.height + fluffY));
        return pos;
    }
}
