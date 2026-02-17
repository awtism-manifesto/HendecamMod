namespace HendecamMod.Content.Projectiles;

public class KnightSpawn : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 1; // The width of projectile hitbox
        Projectile.height = 1; // The height of projectile hitbox
        Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
        Projectile.friendly = true; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
        Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
        Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
        Projectile.timeLeft = 1; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)

        Projectile.light = 0f; // How much light emit around the projectile
        Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
        Projectile.tileCollide = true; // Can the projectile collide with tiles?
        Projectile.extraUpdates = 0; // Set to above 0 if you want the projectile to update multiple time in a frame

        AIType = ProjectileID.Bullet; // Act exactly like default Bullet
    }

    public override void OnKill(int timeLeft)
    {
        Vector2 forward = Projectile.velocity.SafeNormalize(Vector2.UnitX);
        Vector2 perpendicular = forward.RotatedBy(MathHelper.PiOver2);
        float sideOffset = 72f;
       
        float forwardOffset = 0f;  
        Vector2 center = Projectile.Center + forward * forwardOffset;

        Vector2 above = center + perpendicular * sideOffset;
        Vector2 below = center - perpendicular * sideOffset;
        Vector2 velocity = forward * Projectile.velocity.Length();

        Projectile.NewProjectile(
            Projectile.GetSource_FromThis(),
            above,
            velocity,
            ModContent.ProjectileType<KnightSwordUp>(),
            (int)(Projectile.damage * 0.66f),
            Projectile.knockBack,
            Projectile.owner
        );

        Projectile.NewProjectile(
            Projectile.GetSource_FromThis(),
            below,
            velocity,
            ModContent.ProjectileType<KnightSwordDown>(),
            (int)(Projectile.damage * 0.66f),
            Projectile.knockBack,
            Projectile.owner
        );
    }
}