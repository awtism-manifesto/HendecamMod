namespace HendecamMod.Content.Projectiles;

public class RazorRotorsProj : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 312;
        Projectile.height = 312;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.MeleeNoSpeed;
        Projectile.penetrate = 50;
        Projectile.timeLeft = 30;
        Projectile.alpha = 65;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = false;

        Projectile.scale = 1f;
        Projectile.usesIDStaticNPCImmunity = true;
        Projectile.idStaticNPCHitCooldown = 5;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        for (int i = 0; i < 8; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Blood);
            dust.noGravity = true;
            dust.velocity *= 9.5f;
            dust.scale *= 1.5f;
        }
    }

    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        Projectile.Center = player.Center;

        if (player.direction == 1)
        {
            Projectile.rotation += 0.425f;
        }
        else

        {
            Projectile.rotation += -0.425f;
        }

        Lighting.AddLight(Projectile.Center, 0.5f, 0.05f, 0.05f);
        Projectile.velocity = Vector2.Zero;
    }
}