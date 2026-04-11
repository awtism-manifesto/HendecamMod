using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent;

namespace HendecamMod.Common.Utils;

/// <summary>
/// Contains a lot of utillities and global usings
/// </summary>
public static partial class LemonUtils
{
    /// <summary>
    /// Checks if either master mode or FTW are enabled.
    /// </summary>
    /// <returns></returns>
    public static bool IsHard() => Main.masterMode || Main.getGoodWorld;

    /// <summary>
    /// Prevents the NPC from dropping anything.
    /// Should be used in SetStaticDefaults().
    /// </summary>
    /// <param name="npc"></param>
    public static void DontDropAnything(this NPC npc)
    {
        NPCID.Sets.CantTakeLunchMoney[npc.type] = true;
        NPCID.Sets.CannotDropSouls[npc.type] = true;
        NPCID.Sets.NeverDropsResourcePickups[npc.type] = true;
    }

    /// <summary>
    /// Attempts to find a safe position to teleport.
    /// </summary>
    /// <returns>Chosen position. Returns Vector2.Zero if no safe position is found.</returns>
    public static Vector2 FindSafeTeleportPosition(this NPC npc, Vector2 target, float maxDistanceToTarget, float minDistanceToTarget, int maxAttemptCount = 100)
    {
        int attemptCount = 0;
        while (attemptCount < maxAttemptCount)
        {
            Vector2 chosenPos = Vector2.Zero;
            chosenPos = target + RandomVector2Circular(maxDistanceToTarget, maxDistanceToTarget, minDistanceToTarget, minDistanceToTarget);

            Point16 topLeftTile = chosenPos.ToTileCoordinates16();
            Point16 bottomRightTile = (chosenPos + new Vector2(npc.width, npc.height)).ToTileCoordinates16();
            bool badPos = false;
            for (int x = topLeftTile.X; x < bottomRightTile.X; x++)
            {
                for (int y = topLeftTile.Y; y < bottomRightTile.Y; y++)
                {
                    Tile tile = Main.tile[x, y];
                    if (tile.HasTile || tile.LiquidAmount >= 200)
                    {
                        badPos = true;
                        break;
                    }

                }
                if (badPos)
                {
                    break;
                }
            }

            if (!badPos)
            {
                return chosenPos;
            }

            attemptCount++;
        }

        return Vector2.Zero;
    }

    /// <summary>
    /// Basic DOT debuff effect.
    /// Should be called in ModNPC.UpdateLifeRegen().
    /// </summary>
    /// <param name="npc"></param>
    /// <param name="damagePerSecond"></param>
    /// <param name="damage"></param>
    public static void DOTDebuff(this NPC npc, float damagePerSecond, ref int damage)
    {
        if (npc.lifeRegen > 0) npc.lifeRegen = 0;
        npc.lifeRegen -= (int)(damagePerSecond * 2);
        if (damage < damagePerSecond)
        {
            damage = (int)damagePerSecond;
        }
    }

    /// <summary>
    /// Gets random position on the NPC's hitbox.
    /// Increase fluffX and fluffY to expand the range.
    /// Useful for spawning dusts on the NPC.
    /// </summary>
    /// <param name="npc"></param>
    /// <param name="fluffX"></param>
    /// <param name="fluffY"></param>
    /// <returns></returns>
    public static Vector2 RandomPos(this NPC npc, float fluffX = 0, float fluffY = 0)
    {
        Vector2 pos = npc.position + new Vector2(Main.rand.NextFloat(-fluffX, npc.width + fluffX), Main.rand.NextFloat(-fluffY, npc.height + fluffY));
        return pos;
    }

    /// <summary>
    /// Draw basic afterimages.
    /// Should be used in PreDraw() or PostDraw().
    /// </summary>
    /// <param name="npc"></param>
    /// <param name="lightColor"></param>
    /// <param name="opacityMultiplier"></param>
    public static void DrawAfterimages(this NPC npc, Color lightColor, float opacityMultiplier = 1f)
    {
        Texture2D texture = TextureAssets.Npc[npc.type].Value;
        Rectangle sourceRect = npc.frame;
        Vector2 drawOrigin = sourceRect.Size() * 0.5f;
        for (int k = npc.oldPos.Length - 1; k > 0; k--)
        {
            Vector2 drawPos = (npc.oldPos[k] - Main.screenPosition) + drawOrigin;
            Color color = (npc.GetAlpha(lightColor) * ((npc.oldPos.Length - k) / (float)npc.oldPos.Length)) * opacityMultiplier;
            Main.EntitySpriteDraw(texture, drawPos, sourceRect, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0);
        }
    }
}
