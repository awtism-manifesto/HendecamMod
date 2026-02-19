using HendecamMod.Content.Buffs;
using HendecamMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent;

namespace HendecamMod.Content.Projectiles.Items;

public class ParagonMeteorBoom : ModProjectile
{
    private Vector2 _initialCenter;
    private Vector2 _spriteOffset; 

    public override void SetDefaults()
    {
        Projectile.width = 50;
        Projectile.height = 50;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Summon;
        Projectile.light = 5f;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 15;
        Projectile.timeLeft = 60;
        Projectile.alpha = 135;
        Projectile.tileCollide = false;
        Projectile.scale = 0.5f;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(ModContent.BuffType<MoonBurn>(), 960);
        target.AddBuff(BuffID.Oiled, 960);
        target.AddBuff(BuffID.OnFire, 960);
        target.AddBuff(BuffID.OnFire3, 960);
        target.AddBuff(BuffID.Frostburn, 960);
        target.AddBuff(BuffID.Frostburn2, 960);
    }

    public override void AI()
    {
        // Store the initial center position on the first tick
        if (_initialCenter == Vector2.Zero)
        {
            _initialCenter = Projectile.Center;
        }

        // Scale both hitbox and visual size proportionally
        Projectile.scale += 0.185f;

        // Update the hitbox based on scale
        int newWidth = (int)(50 * Projectile.scale);
        int newHeight = (int)(50 * Projectile.scale);

        // Calculate the difference in size
        int widthDiff = newWidth - Projectile.width;
        int heightDiff = newHeight - Projectile.height;

        // Update the hitbox
        Projectile.width = newWidth;
        Projectile.height = newHeight;

        // Keep the hitbox centered at the initial position
        Projectile.Center = _initialCenter;

        // Add a slow upward drift to the VISUAL ONLY
        // This value controls how fast the sprite drifts upward
        float driftPerFrame = 0.25f; // Adjust this to control drift speed (pixels per frame)
        _spriteOffset.Y -= driftPerFrame;

        Projectile.velocity.X = 0;
        Projectile.velocity.Y = 0;

        Projectile.alpha = (int)MathHelper.Max(0, Projectile.alpha - 1);
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = TextureAssets.Projectile[Type].Value;

        // Calculate the center of the texture for drawing
        Vector2 textureCenter = new Vector2(texture.Width / 2f, texture.Height / 2f);

        // Calculate draw position - use the hitbox center plus visual offset
        Vector2 drawPosition = Projectile.Center - Main.screenPosition + _spriteOffset;

        // Calculate alpha color
        Color drawColor = Projectile.GetAlpha(lightColor);

        // Draw the projectile
        Main.EntitySpriteDraw(
            texture,
            drawPosition,
            null,
            drawColor,
            Projectile.rotation,
            textureCenter,
            Projectile.scale,
            SpriteEffects.None,
            0
        );

        return false; // Return false to prevent default drawing
    }

    public override void OnKill(int timeLeft)
    {
        for (int j = 0; j < 30; j++)
        {
            Dust fireDust = Dust.NewDustDirect(
                Projectile.position,
                Projectile.width,
                Projectile.height,
                DustID.BlueTorch,
                0f, 0f, 100, default, 2.25f
            );
            fireDust.noGravity = true;
            fireDust.velocity *= 7f;

            fireDust = Dust.NewDustDirect(
                Projectile.position,
                Projectile.width,
                Projectile.height,
                DustID.IceTorch,
                0f, 0f, 100, default, 2f
            );
            fireDust.velocity *= 6f;
            fireDust.noGravity = true;
        }
    }
}