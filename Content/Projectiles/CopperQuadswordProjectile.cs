namespace HendecamMod.Content.Projectiles;

public class CopperQuadswordProjectile : ModProjectile
{
    public override void SetStaticDefaults()
    {
    }

    public override void SetDefaults()
    {
        Projectile.width = 64;
        Projectile.height = 64;
        Projectile.tileCollide = false;
        Projectile.arrow = false;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.MeleeNoSpeed;
        Projectile.timeLeft = 44;
        Projectile.penetrate = 4;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 8;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        for (int i = 0; i < 3; i++)
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Blood);
            dust.noGravity = true;
            dust.velocity *= 3.65f;
            dust.scale *= 0.85f;
        }
    }

    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        Projectile.rotation -= 0.375f;
       
        if (Projectile.ai[0] == 0f)
        {
            if (Projectile.timeLeft <= 22)
            {
                Projectile.ai[0] = 1f;
                Projectile.tileCollide = false;
            }
        }

        else
        {
            Projectile.timeLeft = 10;

            Vector2 toPlayer = player.Center - Projectile.Center;
            float distance = toPlayer.Length();
            if (distance < 24f)
            {
                Projectile.Kill();
                return;
            }

            toPlayer.Normalize();

            float returnSpeed = 17f;
            float acceleration = 1.15f;

            Projectile.velocity = (Projectile.velocity * acceleration + toPlayer * returnSpeed) / (acceleration + 1f);
            if (Projectile.velocity.Length() < returnSpeed)
                Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero) * returnSpeed;
        }
    }
}