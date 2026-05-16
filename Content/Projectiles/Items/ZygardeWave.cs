using Terraria.Audio;

namespace HendecamMod.Content.Projectiles.Items;

public class ZygardeWave : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 145; // The width of projectile hitbox
        Projectile.height = 145; // The height of projectile hitbox

        Projectile.scale = 1f;
        Projectile.timeLeft = 160;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
        Projectile.extraUpdates = 1;
        Projectile.tileCollide = false;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.penetrate = 7;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.alpha = 255;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      
        Projectile.damage = (int)(Projectile.damage * 0.8f);
    }

    public override void AI()
    {
        Projectile.scale = Main.rand.NextFloat(0.98f, 1.08f);
        Lighting.AddLight(Projectile.Center, 0.5f, 1.4f, 0.5f);
    }

    public override void OnKill(int timeLeft)
    {
    }
}