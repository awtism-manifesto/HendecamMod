using HendecamMod.Content.Buffs;

namespace HendecamMod.Content.Projectiles;

public class RadAura2 : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 799;
        Projectile.height = 799;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = 99;
        Projectile.timeLeft = 16;
        Projectile.alpha = 205;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = false;

        Projectile.scale = 1f;
        Projectile.usesIDStaticNPCImmunity = true;
        Projectile.idStaticNPCHitCooldown = 5;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffType<RadPoisoning4>(), 150);
    }

    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        Projectile.Center = player.Center;

        if (Main.rand.NextBool(2))
        {
            Projectile.rotation += 0.2f;
        }
        else
        {
            Projectile.rotation += -0.2f;
        }

        Lighting.AddLight(Projectile.Center, 1f, 2.95f, 3.55f);
        Projectile.velocity = Vector2.Zero;
    }
}