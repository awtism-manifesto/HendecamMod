using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace HendecamMod.Content.Projectiles;

public class RingFireProj : ModProjectile
{
    public override void SetStaticDefaults()
    {

        ProjectileID.Sets.SentryShot[Type] = true;
        Main.projFrames[Projectile.type] = 3;
    }
    public override void SetDefaults()
    {
        Projectile.width = 480;
        Projectile.height = 480;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Summon;
        Projectile.penetrate = 66;
        Projectile.timeLeft = 30;
        Projectile.alpha = 93;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = false;

        Projectile.scale = 1f;
        Projectile.usesIDStaticNPCImmunity = true;
        Projectile.idStaticNPCHitCooldown = 5;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {

        target.AddBuff(BuffID.OnFire3, 360);

    }
    public override void OnKill(int timeLeft)
    {
        for (int i = 0; i < 36; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
            dust.noGravity = true;
            dust.velocity *= 10.5f;
            dust.scale *= 2.5f;
        }

    }
    public override void AI()
    {
        Projectile.rotation += 0.11f;
        int frameSpeed = 6;

        Projectile.frameCounter++;

        if (Projectile.frameCounter >= frameSpeed)
        {
            Projectile.frameCounter = 0;
            Projectile.frame++;

            if (Projectile.frame >= Main.projFrames[Projectile.type])
            {
                Projectile.frame = 0;
            }
        }

        Lighting.AddLight(Projectile.Center, 2.25f, 1.45f, 0.65f);
        Projectile.velocity = Vector2.Zero;
    }
}
