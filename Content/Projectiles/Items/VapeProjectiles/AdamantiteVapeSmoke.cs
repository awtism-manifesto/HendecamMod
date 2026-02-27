using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Global;

namespace HendecamMod.Content.Projectiles.Items.VapeProjectiles;

public class AdamantiteVapeSmoke : ModProjectile
{
    public override void SetStaticDefaults()
    {
       
    }

    public override void SetDefaults()
    {
        Projectile.width = 22; // The width of projectile hitbox
        Projectile.height = 22; // The height of projectile hitbox
        Projectile.usesLocalNPCImmunity = true;
        Projectile.penetrate = 7;
        Projectile.localNPCHitCooldown = 20;
       
        Projectile.friendly = true;
        Projectile.DamageType = ModContent.GetInstance<StupidDamage>();
        Projectile.timeLeft = 75;
        Projectile.GetGlobalProjectile<VapeMark>().VapeProj = true;
    }

    public override void AI()
    {
       

        // Apply gravity after a quarter of a second
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 8f)
        {
            Projectile.ai[0] = 8f;
            Projectile.velocity.Y -= 0.125f;
        }

        // The projectile is rotated to face the direction of travel
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

       

        for (int i = 0; i < 2; i++)
        {
            float posOffsetX = 0f;
            float posOffsetY = 0f;
            if (i == 1)
            {
                posOffsetX = Projectile.velocity.X * 2.5f;
                posOffsetY = Projectile.velocity.Y * 2.5f;
            }

            Dust fire2Dust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 10, Projectile.height - 10, DustID.Smoke, 0f, 0f, 166, default, 3.33f);
            fire2Dust.fadeIn = 0.2f + Main.rand.Next(4) * 0.1f;
            fire2Dust.noGravity = true;
            fire2Dust.velocity *= 1.33f;
            Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 10, Projectile.height - 10, DustID.Adamantite, 0f, 0f, 100, default, 1.5f);
            fireDust.fadeIn = 0.1f + Main.rand.Next(2) * 0.1f;
            fireDust.noGravity = true;
            fireDust.velocity *= 1.33f;
        }

    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
       

        Projectile.damage = (int)(Projectile.damage * 0.92f);
    }

   
}