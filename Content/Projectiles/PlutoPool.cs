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

public class PlutoPool : ModProjectile
{
    public override void SetStaticDefaults()
    {


        Main.projFrames[Projectile.type] = 3;
    }
    public override void SetDefaults()
    {
        Projectile.width = 55;
        Projectile.height = 55;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.penetrate = 6;
        Projectile.timeLeft = 60;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = false;
        Projectile.extraUpdates = 1;
        Projectile.scale = 1.55f;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 19;
        Projectile.alpha = 145;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(ModContent.BuffType<RadPoisoning2>(), 60);

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
            Projectile.scale = 1.75f;

        }
        if (Projectile.timeLeft < 47)
        {
            Projectile.scale = 1.2f;
            Projectile.Resize(80, 80);
        }
        if (Projectile.timeLeft < 40)
        {
            Projectile.scale = 1.95f;

        }
        if (Projectile.timeLeft < 33)
        {
            Projectile.scale = 2.1f;
            Projectile.Resize(100, 100);
        }
        if (Projectile.timeLeft < 27)
        {
            Projectile.scale = 1.95f;

        }
        if (Projectile.timeLeft < 20)
        {
            Projectile.scale = 1.76f;
            Projectile.Resize(70, 70);
        }
        if (Projectile.timeLeft < 13)
        {
            Projectile.scale = 1.55f;

        }
        if (Projectile.timeLeft < 7)
        {
            Projectile.scale = 1.35f;
            Projectile.Resize(45, 45);
        }

    }


}


