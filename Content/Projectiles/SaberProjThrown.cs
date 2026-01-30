using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles;

public class SaberProjThrown : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 30;
        Projectile.height = 30;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = 2;
        Projectile.timeLeft = 600;
        Projectile.light = 0.33f;
        Projectile.tileCollide = true;
        Projectile.ignoreWater = false;
        Projectile.extraUpdates = 1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 15;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        for (int i = 0; i < 4; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Marble);
            dust.noGravity = true;
            dust.velocity *= 1.66f;
            dust.scale *= 0.95f;
        }
    }

    public override void AI()
    {
        Projectile.rotation += 0.21f;
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