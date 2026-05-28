using HendecamMod.Content.Buffs;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent;

namespace HendecamMod.Content.Projectiles.Items;

public class MantleStoneChunk : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 80; // The length of old position to be recorded
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
    }

    public override void SetDefaults()
    {
        Projectile.width = 13; // The width of projectile hitbox
        Projectile.height = 13; // The height of projectile hitbox
        Projectile.scale = 1.5f;
        Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
        Projectile.friendly = true; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
        Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
        Projectile.penetrate = 999; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
        Projectile.timeLeft = 200; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
        Projectile.alpha = 255; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
        Projectile.light = 0.5f; // How much light emit around the projectile
        Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
        Projectile.tileCollide = false; // Can the projectile collide with tiles?
        Projectile.extraUpdates = 30; // Set to above 0 if you want the projectile to update multiple time in a frame
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        AIType = ProjectileID.Bullet; // Act exactly like default Bullet
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = TextureAssets.Projectile[Type].Value;

        // Redraw the projectile with the color not influenced by light
        Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
        for (int k = 0; k < Projectile.oldPos.Length; k++)
        {
            Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
            Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None);
        }

        return true;
    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {

        modifiers.SourceDamage = modifiers.SourceDamage + (target.defense * 0.036f);
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
     
        Projectile.damage = (int)(Projectile.damage * 0.95f); 
    }

    public override void AI()
    {
        // dust, all dust
        if (Projectile.alpha < 187)
        {
            for (int i = 0; i < 2; i++)
            {
                float posOffsetX = 0f;
                float posOffsetY = 0f;
                if (i == 1)
                {
                    posOffsetX = Projectile.velocity.X * 2.5f;
                    posOffsetY = Projectile.velocity.Y * 2.5f;
                }

               // Dust fire2Dust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 6, Projectile.height - 6, DustID.RedTorch, 0f, 0f, 100, default, 1.25f);
               // fire2Dust.fadeIn = 0.1f + Main.rand.Next(2) * 0.05f;
               // fire2Dust.velocity *= 0.1f;
               // fire2Dust.noGravity = true;
            }
        }
    }

   
}