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
    public class PlutoAura : ModProjectile
    {
       
        public override void SetDefaults()
        {
            Projectile.width = 448;
            Projectile.height = 448;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 38;
            Projectile.timeLeft = 38;
            Projectile.alpha = 230;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = false;
            
            Projectile.scale = 1f;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 6;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

            target.AddBuff(ModContent.BuffType<RadPoisoning2>(), 90);



        }
        public override void AI()
        {

            Player player = Main.player[Projectile.owner];

           
            Projectile.Center = player.Center;

            if (Main.rand.NextBool(2))
            {
                Projectile.rotation += 0.2f;
            }
            else 
            {
                Projectile.rotation += -0.2f;
            }





                Lighting.AddLight(Projectile.Center, 0.81f, 0.25f, 0.95f);

          
            Projectile.velocity = Vector2.Zero;


        }


    }
}


