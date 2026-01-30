using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles;

/// <summary>
/// This the class that clones the vanilla Meowmere projectile using CloneDefaults().
/// Make sure to check out <see cref="ExampleCloneWeapon" />, which fires this projectile; it itself is a cloned version of the Meowmere.
/// </summary>
public class IronWave : ModProjectile
{

    public override void SetDefaults()
    {


        Projectile.width = 145; // The width of projectile hitbox
        Projectile.height = 145; // The height of projectile hitbox

        Projectile.scale = 1f;
        Projectile.timeLeft = 20;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
        Projectile.extraUpdates = 1;
        Projectile.tileCollide = false;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = 8;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.alpha = 255;

    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        SoundEngine.PlaySound(SoundID.NPCHit42, Projectile.position);
        Projectile.damage = (int)(Projectile.damage * 0.75f);
    }

    public override void AI()
    {



        Projectile.scale = Main.rand.NextFloat(0.98f, 1.15f);


        if (Projectile.timeLeft < 17)
        {
            Projectile.alpha = 190;
        }
        if (Projectile.timeLeft < 13)
        {
            Projectile.alpha = 130;
        }
        if (Projectile.timeLeft < 9)
        {
            Projectile.alpha = 75;
        }
        if (Projectile.timeLeft < 5)
        {
            Projectile.alpha = 35;
        }


    }

    public override void OnKill(int timeLeft)
    {







    }
}