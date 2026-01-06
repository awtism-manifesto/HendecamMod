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
    public class PlasmaPool : ModProjectile
    {
        public override void SetStaticDefaults()
        {


            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 45;
            Projectile.height = 45;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 60;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = false;
            Projectile.extraUpdates = 1;
            Projectile.scale = 1.5f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 23;
            Projectile.alpha = 145;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

            target.AddBuff(ModContent.BuffType<RadPoisoning>(), 60);



        }
        public override void AI()
        {
            int frameSpeed = 5;

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



            Projectile.velocity *= 0f;

            if (Projectile.timeLeft < 53)
            {
                Projectile.scale = 1.65f;

            }
            if (Projectile.timeLeft < 47)
            {
                Projectile.scale = 1.2f;
                Projectile.Resize(65, 65);
            }
            if (Projectile.timeLeft < 40)
            {
                Projectile.scale = 1.85f;

            }
            if (Projectile.timeLeft < 33)
            {
                Projectile.scale = 2f;
                Projectile.Resize(85, 85);
            }
            if (Projectile.timeLeft < 27)
            {
                Projectile.scale = 1.85f;

            }
            if (Projectile.timeLeft < 20)
            {
                Projectile.scale = 1.66f;
                Projectile.Resize(60, 60);
            }
            if (Projectile.timeLeft < 13)
            {
                Projectile.scale = 1.45f;

            }
            if (Projectile.timeLeft < 7)
            {
                Projectile.scale = 1.25f;
                Projectile.Resize(45, 45);
            }

        }


    }
}


