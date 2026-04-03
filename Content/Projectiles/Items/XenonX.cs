using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;

namespace HendecamMod.Content.Projectiles.Items;

public class XenonX : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 6;
    }

    public override void SetDefaults()
    {
        Projectile.width = 108;
        Projectile.height = 108;
        Projectile.friendly = true;
        Projectile.DamageType = ModContent.GetInstance<OmniDamage>();
        Projectile.penetrate = -1;
        Projectile.timeLeft = 60;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = false;
        Projectile.extraUpdates = 1;
       
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.alpha = 100;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        
        return Color.White;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(ModContent.BuffType<Stamped>(), 360);
    }

    public override void AI()
    {
        int frameSpeed = 10;

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

       
    }
}