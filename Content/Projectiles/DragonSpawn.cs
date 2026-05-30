using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;

namespace HendecamMod.Content.Projectiles;

public class DragonSpawn : ModProjectile
{
  

    public override void SetDefaults()
    {
        Projectile.width = 1; // The width of projectile hitbox
        Projectile.height = 1; // The height of projectile hitbox
        Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
        Projectile.friendly = false; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
        Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
        Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
        Projectile.timeLeft = 1; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)

        Projectile.light = 0f; // How much light emit around the projectile
        Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
        Projectile.tileCollide = true; // Can the projectile collide with tiles?
        Projectile.extraUpdates = 0; // Set to above 0 if you want the projectile to update multiple time in a frame

        AIType = ProjectileID.WoodenArrowFriendly; // Act exactly like default Bullet
    }

    
   

    public override void OnKill(int timeLeft)
    {
        for (int i = 0; i < 15; i++)
        {
            Vector2 velocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(12));
            Vector2 Peanits = Projectile.Center;
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits, velocity,
                ProjectileType<DragonBreath>(), (int)(Projectile.damage * 0.375f), Projectile.knockBack, Projectile.owner);
        }

        SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
    }
}