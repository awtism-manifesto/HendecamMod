using HendecamMod.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace HendecamMod.Content.Projectiles;

public class DaggerProjThrown : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 30;
        Projectile.height = 30;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = 2;
        Projectile.timeLeft = 600;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = false;
        Projectile.extraUpdates = 1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 15;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.ShadowFlame, 300);
        target.AddBuff(BuffID.Venom, 180);
        target.AddBuff(ModContent.BuffType<RadPoisoning2>(), 120);

    }
    public override void AI()
    {
        Projectile.rotation += 0.235f;
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 25f)
        {
            Projectile.ai[0] = 25f;
            Projectile.velocity.Y += 0.16f;
        }
        if (Projectile.velocity.Y > 15f)
        {
            Projectile.velocity.Y = 17f;
        }
    }
}