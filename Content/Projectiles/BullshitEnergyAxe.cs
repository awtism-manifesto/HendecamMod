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
public class BullshitEnergyAxe : ModProjectile
{
    private NPC HomingTarget
    {
        get => Projectile.ai[0] == 0 ? null : Main.npc[(int)Projectile.ai[0] - 1];
        set
        {
            Projectile.ai[0] = value == null ? 0 : value.whoAmI + 1;
        }
    }

    public ref float DelayTimer => ref Projectile.ai[1];
   
    public override void SetDefaults()
    {
        // This method right here is the backbone of what we're doing here; by using this method, we copy all of
        // the Meowmere Projectile's SetDefault stats (such as projectile.friendly and projectile.penetrate) on to our projectile,
        // so we don't have to go into the source and copy the stats ourselves. It saves a lot of time and looks much cleaner;
        // if you're going to copy the stats of a projectile, use CloneDefaults().
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.width = 128;
        Projectile.height = 128;
        // To further the Cloning process, we can also copy the ai of any given projectile using AIType, since we want
        // the projectile to essentially behave the same way as the vanilla projectile.
        Projectile.DamageType = ModContent.GetInstance<OmniDamage>();
        Projectile.extraUpdates = 1;
        Projectile.scale = 1f;
        Projectile.timeLeft = 480;
        Projectile.tileCollide = false;
        // After CloneDefaults has been called, we can now modify the stats to our wishes, or keep them as they are.
        // For the sake of example, lets make our projectile penetrate enemies a few more times than the vanilla projectile.
        // This can be done by modifying projectile.penetrate
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {

        Vector2 Peanits = Projectile.Center - new Vector2(Main.rand.Next(-1, 1), 2);
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits,
        new Vector2(1, 0).RotatedBy((Peanits).DirectionTo(Projectile.Center).ToRotation()),
        ModContent.ProjectileType<MoonProjFunnier>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
        target.AddBuff(ModContent.BuffType<JevilTag>(), 180);
        Projectile.Kill();
    }
    
    public override void AI()
    {
        Lighting.AddLight(Projectile.Center, 2.25f, 1.65f, 0.65f);
        Projectile.rotation += -0.215f;

        float maxDetectRadius = 1150f; // The maximum radius at which a projectile can detect a target

        // A short delay to homing behavior after being fired
        if (DelayTimer < 45)
        {
            DelayTimer += 1;
            return;
        }

        // First, we find a homing target if we don't have one
        if (HomingTarget == null)
        {
            HomingTarget = FindClosestNPC(maxDetectRadius);
        }

        // If we have a homing target, make sure it is still valid. If the NPC dies or moves away, we'll want to find a new target
        if (HomingTarget != null && !IsValidTarget(HomingTarget))
        {
            HomingTarget = null;
        }

        // If we don't have a target, don't adjust trajectory
        if (HomingTarget == null)
            return;

        // If found, we rotate the projectile velocity in the direction of the target.
        // We only rotate by 3 degrees an update to give it a smooth trajectory. Increase the rotation speed here to make tighter turns
        float length = Projectile.velocity.Length();
        float targetAngle = Projectile.AngleTo(HomingTarget.Center);
        Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(9f)).ToRotationVector2() * length;
        if (DelayTimer >= 29)
        {
            
                Projectile.friendly = true;
            
        }
    }
    public NPC FindClosestNPC(float maxDetectDistance)
    {
        NPC closestNPC = null;

        // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
        float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

        // Loop through all NPCs
        foreach (var target in Main.ActiveNPCs)
        {
            // Check if NPC able to be targeted. 
            if (IsValidTarget(target))
            {
                // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                // Check if it is within the radius
                if (sqrDistanceToTarget < sqrMaxDetectDistance)
                {
                    sqrMaxDetectDistance = sqrDistanceToTarget;
                    closestNPC = target;
                }
            }
        }

        return closestNPC;
    }

    public bool IsValidTarget(NPC target)
    {
        // This method checks that the NPC is:
        // 1. active (alive)
        // 2. chaseable (e.g. not a cultist archer)
        // 3. max life bigger than 5 (e.g. not a critter)
        // 4. can take damage (e.g. moonlord core after all it's parts are downed)
        // 5. hostile (!friendly)
        // 6. not immortal (e.g. not a target dummy)
        // 7. doesn't have solid tiles blocking a line of sight between the projectile and NPC
        return target.CanBeChasedBy();
    }

    public override void OnKill(int timeLeft)
    {
       

    }
}