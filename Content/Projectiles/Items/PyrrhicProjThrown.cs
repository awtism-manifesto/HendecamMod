namespace HendecamMod.Content.Projectiles.Items;

public class PyrrhicProjThrown : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 35;
        Projectile.height = 35;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = 4;
        Projectile.timeLeft = 600;
        Projectile.tileCollide = true;
        Projectile.ignoreWater = false;
        Projectile.extraUpdates = 1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 15;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.Ichor, 180);
    }

    public override void AI()
    {
        if (Projectile.timeLeft < 589)
        {
            Projectile.Resize(66, 66);
        }

        Projectile.rotation += 0.235f;
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 28f)
        {
            Projectile.ai[0] = 28f;
            Projectile.velocity.Y += 0.21f;
        }

        if (Projectile.velocity.Y > 15f)
        {
            Projectile.velocity.Y = 17f;
        }
    }
}