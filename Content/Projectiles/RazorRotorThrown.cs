using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles
{
    // This example is similar to the Wooden Arrow projectile
    public class RazorRotorThrown : ModProjectile
    {
        public override void SetStaticDefaults()
        {
           
        }

        public override void SetDefaults()
        {
            Projectile.width = 104; // The width of projectile hitbox
            Projectile.height = 104; // The height of projectile hitbox
            Projectile.tileCollide = false;
            Projectile.arrow = false;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.timeLeft = 74;
            Projectile.penetrate = 10;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 4;
        }


        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

            for (int i = 0; i < 8; i++) 
            {
                Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Blood);
                dust.noGravity = true;
                dust.velocity *= 6.5f;
                dust.scale *= 1.5f;
            }




        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (Projectile.timeLeft <= 37)
            {
                float length = Projectile.velocity.Length();
                float targetAngle = Projectile.AngleTo(player.Center);
                Vector2 position0 = Projectile.position;
                Vector2 targetPosition0 = player.Center;
                Vector2 direction = targetPosition0 - position0;
                Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(360f)).ToRotationVector2() * length;
            }
            if (Projectile.timeLeft <= 13)
            {
               
                    Projectile.velocity *= 1.07f;

                
            }

            

            Projectile.rotation += 0.425f;
           
        }

        
    }
}