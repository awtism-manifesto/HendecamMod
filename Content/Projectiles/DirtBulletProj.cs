using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles;

public class DirtBulletProj : ModProjectile
{
    public override void SetStaticDefaults()
    {
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
    }

    public override void AI()
    {
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 13f)
        {
            Projectile.ai[0] = 8f;
            Projectile.velocity.Y += 0.18f;
        }

        Projectile.rotation += 0.25f;

        if (Projectile.velocity.Y > 18f)
        {
            Projectile.velocity.Y = 19f;
        }

        Dust fire2Dust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 0f, Projectile.position.Y + 0f) - Projectile.velocity * 0.1f, Projectile.width - 2, Projectile.height - 2, DustID.Dirt, 0f, 0f, 100, default, 0.75f);
        fire2Dust.fadeIn = 0.1f + Main.rand.Next(3) * 0.1f;
        fire2Dust.velocity *= 0.15f;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        for (int j = 0; j < 3; j++)
        {
            Dust fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Dirt, 0f, 0f, 100);
            fireDust.noGravity = true;
            fireDust.velocity *= 5f;
        }
    }
}