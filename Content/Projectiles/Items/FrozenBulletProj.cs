using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace HendecamMod.Content.Projectiles.Items;

public class FrozenBulletProj : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8; // The length of old position to be recorded
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
    }

    public override void SetDefaults()
    {
        Projectile.width = 5;
        Projectile.height = 5;
        Projectile.extraUpdates = 1;
        Projectile.scale = 1.2f;

        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.timeLeft = 450;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
    }

    public override void AI()
    {
        if (Projectile.alpha < 200)
        {
            Dust fire2Dust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 0f, Projectile.position.Y + 0f) - Projectile.velocity * 0.1f, Projectile.width - 2, Projectile.height - 2, DustID.IceTorch, 0f, 0f, 100, default, 0.75f);
            fire2Dust.fadeIn = 0.1f + Main.rand.Next(3) * 0.1f;
            fire2Dust.velocity *= 0.15f;
        }
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

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        for (int j = 0; j < 4; j++)
        {
            Dust fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.IceTorch, 0f, 0f, 100);
            fireDust.noGravity = true;
            fireDust.velocity *= 6.55f;
            target.AddBuff(BuffID.Frostburn, 180);
        }
    }
}