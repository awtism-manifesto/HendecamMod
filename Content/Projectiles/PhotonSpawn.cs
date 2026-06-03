using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent;

namespace HendecamMod.Content.Projectiles;

public class PhotonSpawn : ModProjectile
{
   

    public override void SetDefaults()
    {
        Projectile.width = 10; // The width of projectile hitbox
        Projectile.height = 15; // The height of projectile hitbox
        Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
        Projectile.friendly = false; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
        Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
        Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
        Projectile.timeLeft = 1; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)

        Projectile.light = 0f; // How much light emit around the projectile
        Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
        Projectile.tileCollide = true; // Can the projectile collide with tiles?
        Projectile.extraUpdates = 1; // Set to above 0 if you want the projectile to update multiple time in a frame

        AIType = ProjectileID.Bullet; // Act exactly like default Bullet
    }

   
   

    public override void OnKill(int timeLeft)
    {
        Vector2 velocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(6));
        Vector2 Peanits = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits, velocity,
            ProjectileType<Photon>(), (int)(Projectile.damage * 0.52f), Projectile.knockBack, Projectile.owner);
        Vector2 velocity2 = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(6));
        Vector2 Peanits2 = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits2, velocity2,
            ProjectileType<Photon>(), (int)(Projectile.damage * 0.52f), Projectile.knockBack, Projectile.owner);
        Vector2 velocity3 = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(6));
        Vector2 Peanits3 = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits3, velocity3,
            ProjectileType<Photon>(), (int)(Projectile.damage * 0.52f), Projectile.knockBack, Projectile.owner);
        // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
        Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
    }
}