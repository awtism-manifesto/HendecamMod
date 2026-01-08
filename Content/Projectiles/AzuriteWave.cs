
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles
{
    /// <summary>
    /// This the class that clones the vanilla Meowmere projectile using CloneDefaults().
    /// Make sure to check out <see cref="ExampleCloneWeapon" />, which fires this projectile; it itself is a cloned version of the Meowmere.
    /// </summary>
    public class AzuriteWave : ModProjectile
    {
        public override void SetStaticDefaults()
        {

            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            

            Projectile.width = 100; // The width of projectile hitbox
            Projectile.height = 100; // The height of projectile hitbox

            Projectile.scale = 1f;
            Projectile.timeLeft = 50;
            Projectile.aiStyle = 1;
            AIType = ProjectileID.Bullet;
           
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 3; 
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.alpha = 255;

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
           
            Projectile.damage = (int)(Projectile.damage * 0.75f); 
        }
       
        public override void AI()
        {
            Projectile.scale = Main.rand.NextFloat(0.85f, 1.15f);

            int frameSpeed = 8;

            Projectile.frameCounter++;
            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;


                }
            }
            if (Projectile.timeLeft < 42)
            {
                Projectile.alpha = 190;
            }
            if (Projectile.timeLeft < 37)
            {
                Projectile.alpha = 130;
            }
            if (Projectile.timeLeft < 33)
            {
                Projectile.alpha = 75;
            }
            if (Projectile.timeLeft < 29)
            {
                Projectile.alpha = 35;
            }
            if (Projectile.timeLeft > 178)
            {

               
            }
            Lighting.AddLight(Projectile.Center, 0.25f, 0.25f, 0.875f);
        }
       
        public override void OnKill(int timeLeft)
        {

            
           




        }
    }
}