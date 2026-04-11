using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Dusts;

namespace HendecamMod.Content.Projectiles.Items;

public class FaradaySunShot : ModProjectile
{
    

    public override void SetDefaults()
    {
        Projectile.width = 13;
        Projectile.height = 13;
        Projectile.scale = 1.67f;
        Projectile.friendly = true;
        Projectile.penetrate = 1; // Infinite penetration so that the blast can hit all enemies within its radius.
        Projectile.DamageType = GetInstance<StupidDamage>();
        Projectile.light = 0.4f; // How much light emit around the projectile
        Projectile.usesLocalNPCImmunity = true;
        Projectile.timeLeft = 210;
        Projectile.extraUpdates = 33;
        AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        Projectile.aiStyle = 1;
        Projectile.alpha = 255;
        Projectile.localNPCHitCooldown = -1;
        Projectile.tileCollide = false;

    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffType<MoonBurn>(), 300);
        target.AddBuff(BuffID.OnFire, 600);
        target.AddBuff(BuffID.OnFire3, 600);
    }

    public override void AI()
    {



        if (Projectile.alpha < 235)
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

                Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 15, Projectile.height - 15, DustID.HallowedWeapons, 0f, 0f, 100);
                fireDust.fadeIn = 0.2f + Main.rand.Next(3) * 0.2f;
                fireDust.noGravity = true;
                fireDust.velocity *= 0.66f;
            }
            for (int i = 0; i < 2; i++)
            {
                float posOffsetX = 0f;
                float posOffsetY = 0f;
                if (i == 1)
                {
                    posOffsetX = Projectile.velocity.X * 2.5f;
                    posOffsetY = Projectile.velocity.Y * 2.5f;
                }

                Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 15, Projectile.height - 15, DustID.SolarFlare, 0f, 0f, 100);
                fireDust.fadeIn = 0.1f + Main.rand.Next(1) * 0.1f;
                fireDust.noGravity = true;
                fireDust.velocity *= 0.97f;
            }
        }


       
    }

}