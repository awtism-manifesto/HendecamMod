using HendecamMod.Content.DamageClasses;

namespace HendecamMod.Content.Projectiles;

/// <summary>
///     This the class that clones the vanilla Meowmere projectile using CloneDefaults().
///     Make sure to check out <see cref="ExampleCloneWeapon" />, which fires this projectile; it itself is a cloned
///     version of the Meowmere.
/// </summary>
public class TheSun : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 536; // The width of projectile hitbox
        Projectile.height = 536; // The height of projectile hitbox
        Projectile.timeLeft = 500;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
        Projectile.light = 100f;
        Projectile.tileCollide = false;
        Projectile.friendly = true;
        Projectile.DamageType = ModContent.GetInstance<StupidDamage>();
        Projectile.penetrate = 500;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.alpha = 255;
        Projectile.extraUpdates = 1;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.OnFire, 1250);
        target.AddBuff(BuffID.OnFire3, 1250);
        target.AddBuff(BuffID.CursedInferno, 1250);
        target.AddBuff(BuffID.Frostburn, 1250);
        target.AddBuff(BuffID.Frostburn2, 1250);
        target.AddBuff(BuffID.ShadowFlame, 1250);
    }

    public override void AI()
    {
        Projectile.scale = Main.rand.NextFloat(1.25f, 1.275f);
    }

    public override void OnKill(int timeLeft)
    {
    }
}