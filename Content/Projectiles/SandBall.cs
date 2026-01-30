using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles;

public class SandBall : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 14; // The width of projectile hitbox
        Projectile.height = 14; // The height of projectile hitbox

        Projectile.friendly = true; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
        Projectile.DamageType = DamageClass.Magic;
        Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
        Projectile.timeLeft = 220; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
        Projectile.alpha = 2; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
        Projectile.light = 0.2f; // How much light emit around the projectile
        Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
        Projectile.tileCollide = true; // Can the projectile collide with tiles?
        Projectile.extraUpdates = 2;
    }

    public override void AI()
    {
        Projectile.rotation += -0.135f;
    }
}