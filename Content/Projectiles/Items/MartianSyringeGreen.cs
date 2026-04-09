using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using Terraria.Audio;

namespace HendecamMod.Content.Projectiles.Items;


public class MartianSyringeGreen : ModProjectile
{
    

    public override void SetDefaults()
    {
        Projectile.width = 20; // The width of projectile hitbox
        Projectile.height = 20; // The height of projectile hitbox
        Projectile.aiStyle = -1; // The ai style of the projectile, please reference the source code of Terraria
        Projectile.friendly = true; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
        Projectile.DamageType = ModContent.GetInstance<StupidDamage>(); // Is the projectile shoot by a ranged weapon?
        Projectile.penetrate = 3; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
        Projectile.timeLeft = 333; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)

        Projectile.light = 0.25f; // How much light emit around the projectile
        Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
        Projectile.tileCollide = true; // Can the projectile collide with tiles?
        Projectile.extraUpdates = 1; // Set to above 0 if you want the projectile to update multiple time in a frame

        AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 18;
    }

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
       
        modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) =>
        {
            if (hitInfo.Crit)
            {
                hitInfo.Damage = (int)(hitInfo.Damage * 1.125f);
            }
        };
    }

    public override void AI()
    {
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 12f)
        {
            Projectile.ai[0] = 12f;
            Projectile.velocity.Y += 0.17f;
        }

        // The projectile is rotated to face the direction of travel
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

        // Cap downward velocity
        if (Projectile.velocity.Y > 21f)
        {
            Projectile.velocity.Y = 24f;
        }

       
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Main.rand.NextBool(2))
        {
            target.AddBuff(BuffID.Poisoned, 300);
        }
        if (Main.rand.NextBool(3))
        {
            target.AddBuff(BuffID.Frostburn, 300);
        }
        if (Main.rand.NextBool(4))
        {
            target.AddBuff(ModContent.BuffType<RadPoisoning>(), 300);
        }

        if (Main.rand.NextBool(5))
        {
            target.AddBuff(BuffID.CursedInferno, 300);
        }
        if (Main.rand.NextBool(6))
        {
            target.AddBuff(BuffID.Oiled, 300);
        }
        if (Main.rand.NextBool(7))
        {
            target.AddBuff(BuffID.Ichor, 300);
        }
        if (Main.rand.NextBool(50))
        {
            target.AddBuff(ModContent.BuffType<MoonBurn>(), 300);
        }
    }
}