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

public class AdamantiteBolt : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0;

    }

    public override void SetDefaults()
    {
        Projectile.width = 15;
        Projectile.height = 15;

        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = 5;
        Projectile.timeLeft = 130;

        Projectile.light = 0.3f;
        Projectile.ignoreWater = false;
        Projectile.tileCollide = true;
        Projectile.extraUpdates = 1;
        Projectile.usesLocalNPCImmunity = true;
        AIType = ProjectileID.Bullet;
        Projectile.aiStyle = 1;
        Projectile.alpha = 255;
    }

    public override void AI()
    {

        if (Projectile.alpha < 180)
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


                Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 11, Projectile.height - 11, DustID.RedTorch, 0f, 0f, 100, default, 2.5f);
                fireDust.fadeIn = 0.2f + Main.rand.Next(6) * 0.1f;
                fireDust.noGravity = true;
                fireDust.velocity *= 0.25f;
                Dust fire2Dust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 11, Projectile.height - 11, DustID.Adamantite, 0f, 0f, 100, default, 2.5f);
                fire2Dust.fadeIn = 0.2f + Main.rand.Next(6) * 0.1f;
                fire2Dust.noGravity = true;
                fire2Dust.velocity *= 0.25f;
            }
        }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {

        target.immune[Projectile.owner] = 6;
        Projectile.damage = (int)(Projectile.damage * 0.9f);
    }


}


