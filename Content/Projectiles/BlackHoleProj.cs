using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles;


public class BlackHoleProj : ModProjectile
{

    public override void SetDefaults()
    {


        Projectile.width = 330; // The width of projectile hitbox
        Projectile.height = 330; // The height of projectile hitbox


        Projectile.timeLeft = 240;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
        Projectile.light = -100f;
        Projectile.tileCollide = false;
        Projectile.friendly = true;
        Projectile.DamageType = ModContent.GetInstance<OmniDamage>();
        Projectile.penetrate = 500;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 1;
        Projectile.alpha = 255;
        Projectile.extraUpdates = 1;

    }


    public override void AI()
    {
        Projectile.scale = Projectile.scale * 1.08f;
        Projectile.width = (int)(Projectile.width * 1.075f);
        Projectile.height = (int)(Projectile.height * 1.075f);
        Projectile.velocity.X = -13.5f;
        Projectile.velocity.Y = -13.5f;

    }

    public override void OnKill(int timeLeft)
    {







    }
}