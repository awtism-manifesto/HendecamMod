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


namespace HendecamMod.Content.Projectiles;

public class RadAura : ModProjectile
{
   
    public override void SetDefaults()
    {
        Projectile.width = 299;
        Projectile.height = 299;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = 10;
        Projectile.timeLeft = 12;
        Projectile.alpha = 205;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = false;
        
        Projectile.scale = 1f;
        Projectile.usesIDStaticNPCImmunity = true;
        Projectile.idStaticNPCHitCooldown = 7;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {

        target.AddBuff(ModContent.BuffType<RadPoisoning>(), 75);



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





            Lighting.AddLight(Projectile.Center, 0.25f, 0.95f, 0.05f);

      
        Projectile.velocity = Vector2.Zero;


    }


}


