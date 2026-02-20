using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;

namespace HendecamMod.Content.Projectiles.Items;

public class ToyBlockYellow : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 30;
        Projectile.height = 30;
        Projectile.friendly = true;
        Projectile.DamageType = ModContent.GetInstance<StupidDamage>();
        Projectile.penetrate = 1;
        Projectile.timeLeft = 600;
        Projectile.tileCollide = true;
        Projectile.ignoreWater = false;
        Projectile.extraUpdates = 1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(ModContent.BuffType<Splinters>(), 180);
    }

    public override void AI()
    {
        Projectile.rotation += 0.21f;
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 12f)
        {
            Projectile.ai[0] = 12f;
            Projectile.velocity.Y += 0.29f;
        }

        if (Projectile.velocity.Y > 21f)
        {
            Projectile.velocity.Y = 26f;
        }
    }
}