using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Dusts;

namespace HendecamMod.Content.Projectiles;

public class LimestoneDustCloud : ModProjectile
{
   

    public override void SetDefaults()
    {
        Projectile.width = 95;
        Projectile.height = 95;
        Projectile.friendly = true;
        Projectile.DamageType = GetInstance<RangedStupidDamage>();
        Projectile.penetrate = 10;
        Projectile.timeLeft = 61;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = false;
        Projectile.extraUpdates = 1;
        Projectile.scale = 1f;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 11;
        Projectile.alpha = 145;
    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        modifiers.SourceDamage *= 66.77f;

        modifiers.SetMaxDamage(limit: 1);
    }
   

    public override void AI()
    {
        Projectile.velocity = Vector2.Zero;
        if (Main.rand.NextBool(6))
        {
            for (int i = 0; i < 10; i++) // Creates a splash of dust around the position the projectile dies.
            {
                Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustType<LimestoneDust>());
                dust2.noGravity = true;
                dust2.velocity *= 1.75f;
                dust2.scale *= 1f;
            }
        }
       
    }
}