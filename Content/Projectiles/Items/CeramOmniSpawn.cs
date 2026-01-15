using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using HendecamMod.Content.DamageClasses;


namespace HendecamMod.Content.Projectiles.Items
{
    public class CeramOmniSpawn : ModProjectile
    {

        
        public override void SetDefaults()
        {
            Projectile.width = 1; // The width of projectile hitbox
            Projectile.height = 1; // The height of projectile hitbox
            Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = ModContent.GetInstance<OmniDamage>(); // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 1; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255;
            Projectile.light = 0f; // How much light emit around the projectile
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
            Projectile.extraUpdates = 0; // Set to above 0 if you want the projectile to update multiple time in a frame

            AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        }

       

       
       
       

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);
            

            for (int i = 0; i < 10; i++)
            {
                float rotation = MathHelper.ToRadians(i * 36f);
                Vector2 velocity = Projectile.velocity.RotatedBy(rotation);
                Vector2 position = Projectile.Center;

                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    position,
                    velocity,
                    ModContent.ProjectileType<CeramOmni>(),
                    Projectile.damage,
                    Projectile.knockBack,
                    Projectile.owner
                );
            }


        }

    }

}



