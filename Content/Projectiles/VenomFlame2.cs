using HendecamMod.Common.Systems.Particles;

namespace HendecamMod.Content.Projectiles;

public class VenomFlame2 : ModProjectile
{
   
    public override void SetDefaults()
    {
        Projectile.width = 27; // The width of projectile hitbox
        Projectile.height = 27; // The height of projectile hitbox

        Projectile.friendly = true; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
        Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
        Projectile.penetrate = 5; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
        Projectile.timeLeft = 48;

        Projectile.light = 0.5f;
        Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
        Projectile.tileCollide = true; // Can the projectile collide with tiles?
        Projectile.extraUpdates = 1; // Set to above 0 if you want the projectile to update multiple time in a frame
        Projectile.usesLocalNPCImmunity = true;
        AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        Projectile.aiStyle = 1;
        Projectile.alpha = 255;
    }

    public override void AI()
    {
        if (Projectile.alpha < 190)
        {
            for (int i = 0; i < 2; i++)
            {
                Color randColor = Main.rand.NextFromList(Color.Violet, Color.Purple, Color.DarkViolet, Color.MediumPurple);




                ParticleSystem.SpawnParticle(
                    ParticleID.Fire,
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
        Projectile.damage = (int)(Projectile.damage * 0.82f);
        target.AddBuff(BuffID.Venom, 240);
        target.immune[Projectile.owner] = 7;
    }
}