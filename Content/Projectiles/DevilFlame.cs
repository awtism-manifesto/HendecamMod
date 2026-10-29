using HendecamMod.Common.Systems.Particles;
using HendecamMod.Common.Systems.Particles.Renderers;
using HendecamMod.Common.Utilities;

namespace HendecamMod.Content.Projectiles;

public class DevilFlame : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1; // The length of old position to be recorded
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
    }

    public override void SetDefaults()
    {
        Projectile.width = 20; // The width of projectile hitbox
        Projectile.height = 20; // The height of projectile hitbox

        Projectile.friendly = true; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
        Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
        Projectile.penetrate = 5; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
        Projectile.timeLeft = 44;

        Projectile.light = 0.2f;
        Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
        Projectile.tileCollide = true; // Can the projectile collide with tiles?
        Projectile.extraUpdates = 0; // Set to above 0 if you want the projectile to update multiple time in a frame
        Projectile.usesLocalNPCImmunity = true;
        AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        Projectile.aiStyle = 1;
        Projectile.alpha = 255;
    }

    public override void AI()
    {
        if (Projectile.alpha < 166)
        {
            // Glowy particles
            for (int i = 0; i < 6; i++)
            {
                // It is important to use a renderer with an Additive blend state, otherwise it won't look great
                // AfterDustParticleRendererGlowy works
                ParticleSystem.SpawnParticle<AfterDustParticleRendererGlowy>(
                    ParticleID.Glowy,
                    Projectile.RandomPos(-8, -8),
                    Vector2.UnitY.RotatedBy(Main.rand.NextFloat(-6.28f, 6.28f)),
                    Main.rand.NextFromList(Color.Red, Color.DarkRed, Color.OrangeRed, Color.Black),
                    1f,
                    Main.rand.NextFloat(1f, 1.5f),
                    data0: 30, // Check docs for (hover over) ParticleID.Glowy to see what these represent
                    data1: 5f,
                    data2: 20f,
                    data3: 0.98f
                    );
            }

            for (int i = 0; i < 2; i++)
            {
                Color randColor = Main.rand.NextFromList(Color.Red, Color.DarkRed, Color.OrangeRed, Color.Black);

                ParticleSystem.SpawnParticle(
                    ParticleID.Gas,
                    Projectile.Center - new Vector2(Main.rand.NextFloat(-5f, 5f)),
                    Vector2.Zero,
                    randColor,
                    Main.rand.NextFloat(0.4f, 0.8f),
                    Main.rand.NextFloat(0.7f, 1f));
            }
        }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        Projectile.damage = (int)(Projectile.damage * 0.8f);
        target.immune[Projectile.owner] = 7;
    }
}