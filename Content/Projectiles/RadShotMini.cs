using HendecamMod.Content.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using HendecamMod.Content.Dusts;

namespace HendecamMod.Content.Projectiles;

// This example is similar to the Wooden Arrow projectile
public class RadShotMini : ModProjectile
{
    public override void SetStaticDefaults()
    {
        // If this arrow would have strong effects (like Holy Arrow pierce), we can make it fire fewer projectiles from Daedalus Stormbow for game balance considerations like this:
        //ProjectileID.Sets.FiresFewerFromDaedalusStormbow[Type] = true;
        
    }

    public override void SetDefaults()
    {
        Projectile.width = 6; // The width of projectile hitbox
        Projectile.height = 6; // The height of projectile hitbox
        Projectile.aiStyle = 1;
        Projectile.extraUpdates = 1;
        Projectile.friendly = true;
        Projectile.penetrate = 1;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.timeLeft = 120;
        AIType = ProjectileID.Bullet;
    }
   
    public override void AI()
    {
       


        if (Math.Abs(Projectile.velocity.X) >= 4f || Math.Abs(Projectile.velocity.Y) >= 4f)
        {
            for (int i = 0; i < 2; i++)
            {
                float posOffsetX = 0f;
                float posOffsetY = 0f;
                if (i == 1)
                {
                    posOffsetX = Projectile.velocity.X * 2.5f;
                    posOffsetY = Projectile.velocity.Y * 2.5f;
                }



                Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 8, Projectile.height - 8, ModContent.DustType<UraniumDust>(), 0f, 0f, 100, default, 0.1f);
                fireDust.fadeIn = 0.1f + Main.rand.Next(5) * 0.1f;
                fireDust.velocity *= 0.08f;
            }
        }
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {

        target.AddBuff(ModContent.BuffType<RadPoisoning>(), 60);
       


    }
   
}