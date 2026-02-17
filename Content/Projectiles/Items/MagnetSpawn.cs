namespace HendecamMod.Content.Projectiles.Items;

public class MagnetSpawn : ModProjectile
{
   

    public override void SetDefaults()
    {
        Projectile.width = 1; // The width of projectile hitbox
        Projectile.height = 1; // The height of projectile hitbox
        Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
        Projectile.friendly = false; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
      
        Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
        Projectile.timeLeft = 1; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
      
    }

    public override void OnKill(int timeLeft)
    {
       

        for (int i = 0; i < 8; i++)
        {
            float rotation = MathHelper.ToRadians(i * 45f);
            Vector2 velocity = Projectile.velocity.RotatedBy(rotation);
            Vector2 position = Projectile.Center;

            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                position,
                velocity,
                ProjectileID.MagnetSphereBall,
                (int)(Projectile.damage*1f),
                Projectile.knockBack,
                Projectile.owner
            );
        }
    }
}