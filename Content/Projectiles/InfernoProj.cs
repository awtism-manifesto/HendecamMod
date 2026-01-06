using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
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
    public class InfernoProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {

            ProjectileID.Sets.SentryShot[Type] = true;
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 960;
            Projectile.height = 960;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = 666;
            Projectile.timeLeft = 60;
            Projectile.alpha = 93;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = false;
            
            Projectile.scale = 1f;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 3; 
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Oiled, 1360);
            target.AddBuff(BuffID.OnFire, 1360);
            target.AddBuff(BuffID.OnFire3, 1360);



        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 55; i++) // Creates a splash of dust around the position the projectile dies.
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
                dust.noGravity = true;
                dust.velocity *= 19.75f;
                dust.scale *= 4.5f;
            }

            Vector2 velocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(360));
            Vector2 Peanits = Projectile.Center;
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits, velocity,
            ModContent.ProjectileType<InfernoMeteor>(), (int)(Projectile.damage * 22.5f), Projectile.knockBack, Projectile.owner);



        }
        public override void AI()
        {

           

           
                Projectile.rotation += -0.14f;
            

            int frameSpeed = 6;

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



            Lighting.AddLight(Projectile.Center, 3.15f, 1.85f, 0.95f);

          
            Projectile.velocity = Vector2.Zero;


        }


    }
}


