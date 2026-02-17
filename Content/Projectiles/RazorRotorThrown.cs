namespace HendecamMod.Content.Projectiles;

public class RazorRotorThrown : ModProjectile
{
    public override void SetStaticDefaults()
    {
    }

    public override void SetDefaults()
    {
        Projectile.width = 104;
        Projectile.height = 104;
        Projectile.tileCollide = false;
        Projectile.arrow = false;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.MeleeNoSpeed;
        Projectile.timeLeft = 74;
        Projectile.penetrate = 10;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 4;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        for (int i = 0; i < 8; i++)
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Blood);
            dust.noGravity = true;
            dust.velocity *= 6.5f;
            dust.scale *= 1.5f;
        }
    }

    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        Projectile.rotation += 0.425f;
        Lighting.AddLight(Projectile.Center, 0.5f, 0.05f, 0.05f);
        if (Projectile.ai[0] == 0f)
        {
            if (Projectile.timeLeft <= 37)
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

            float returnSpeed = 25f;
            float acceleration = 1.33f;

            Projectile.velocity = (Projectile.velocity * acceleration + toPlayer * returnSpeed) / (acceleration + 1f);
            if (Projectile.velocity.Length() < returnSpeed)
                Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero) * returnSpeed;
        }
    }
}