using HendecamMod.Content.Global;
using Terraria.DataStructures;

namespace HendecamMod.Content.Projectiles;

public class ZazaSmoke : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1; // The length of old position to be recorded
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
    }

    public override void SetDefaults()
    {
        Projectile.width = 20; // The width of projectile hitbox
        Projectile.height = 20; // The height of projectile hitbox

        Projectile.friendly = true; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
        Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
        Projectile.penetrate = 2; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
        Projectile.timeLeft = 58;

        Projectile.light = 0.5f;
        Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
        Projectile.tileCollide = true; // Can the projectile collide with tiles?
        Projectile.extraUpdates = 0; // Set to above 0 if you want the projectile to update multiple time in a frame
        Projectile.usesLocalNPCImmunity = true;
        AIType = ProjectileID.ShimmerArrow; // Act exactly like default Bullet
        Projectile.aiStyle = 1;
        Projectile.alpha = 255;
    }

    public override void OnSpawn(IEntitySource source)
    {
        var vapeMark = Projectile.GetGlobalProjectile<VapeMark>();
        vapeMark.VapeProj = true;
        vapeMark.DustScale = 1.5f;


    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Main.rand.NextBool(4))
        {
            target.AddBuff(BuffID.Confused, 60);
        }

        target.immune[Projectile.owner] = 9;
        Projectile.damage = (int)(Projectile.damage * 0.875f);
    }
}