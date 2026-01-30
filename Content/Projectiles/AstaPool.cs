using HendecamMod.Content.Buffs;
using Terraria;
using Terraria.ModLoader;
namespace HendecamMod.Content.Projectiles;

public class AstaPool : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 3;
    }
    public override void SetDefaults()
    {
        Projectile.width = 66;
        Projectile.height = 66;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.penetrate = 7;
        Projectile.timeLeft = 60;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = false;
        Projectile.extraUpdates = 1;
        Projectile.scale = 1.7f;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 17;
        Projectile.alpha = 145;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {

        target.AddBuff(ModContent.BuffType<RadPoisoning3>(), 255);
    }
    public override void AI()
    {
        int frameSpeed = 5;

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

        Projectile.velocity *= 0f;

        if (Projectile.timeLeft < 53)
        {
            Projectile.scale = 1.85f;

        }
        if (Projectile.timeLeft < 47)
        {
            Projectile.scale = 2f;
            Projectile.Resize(90, 90);
        }
        if (Projectile.timeLeft < 40)
        {
            Projectile.scale = 2.15f;

        }
        if (Projectile.timeLeft < 33)
        {
            Projectile.scale = 2.3f;
            Projectile.Resize(115, 115);
        }
        if (Projectile.timeLeft < 27)
        {
            Projectile.scale = 2f;

        }
        if (Projectile.timeLeft < 20)
        {
            Projectile.scale = 1.8f;
            Projectile.Resize(95, 95);
        }
        if (Projectile.timeLeft < 13)
        {
            Projectile.scale = 1.66f;

        }
        if (Projectile.timeLeft < 7)
        {
            Projectile.scale = 1.45f;
            Projectile.Resize(65, 65);
        }

    }
}
