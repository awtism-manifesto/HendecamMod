using HendecamMod.Common.Systems.Particles;

namespace HendecamMod.Content.Projectiles;

public class VileFlame : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 30; // The width of projectile hitbox
        Projectile.height = 30; // The height of projectile hitbox

        Projectile.friendly = true; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
        Projectile.DamageType = DamageClass.Magic; // Is the projectile shoot by a ranged weapon?
        Projectile.penetrate = 5; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
        Projectile.timeLeft = 59;

        Projectile.light = 0.5f;
        Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
        Projectile.tileCollide = true; // Can the projectile collide with tiles?
        Projectile.extraUpdates = 3; // Set to above 0 if you want the projectile to update multiple time in a frame
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        Projectile.aiStyle = 1;
        Projectile.alpha = 255;
    }

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        modifiers.SourceDamage *= 1.1f;
    }

    public override void AI()
    {
        if (Projectile.alpha < 182)
        {
            for (int i = 0; i < 2; i++)
            {
                Color randColor = Main.rand.NextFromList(Color.Lime, Color.LimeGreen, Color.Green, Color.GreenYellow);




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
        Projectile.damage = (int)(Projectile.damage * 0.8f);

        target.AddBuff(BuffID.CursedInferno, 240);
    }
}