using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles;

// This example is similar to the Wooden Arrow projectile
public class GalaxyProj2 : ModProjectile
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
        Projectile.width = 18; // The width of projectile hitbox
        Projectile.height = 18; // The height of projectile hitbox
        Projectile.aiStyle = -1;
        Projectile.arrow = true;
        Projectile.friendly = true;
        Projectile.DamageType = ModContent.GetInstance<OmniDamage>();
        Projectile.timeLeft = 480;

    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(ModContent.BuffType<GalaxyTag>(), 330);
        Main.player[Projectile.owner].MinionAttackTargetNPC = target.whoAmI;

    }
    public override void AI()
    {
        if (Math.Abs(Projectile.velocity.X) <= 22.9f && Math.Abs(Projectile.velocity.Y) <= 22.9f)
        {
            Projectile.velocity *= 1.1f;

        }
        // The code below was adapted from the ProjAIStyleID.Arrow behavior. Rather than copy an existing aiStyle using Projectile.aiStyle and AIType,
        // like some examples do, this example has custom AI code that is better suited for modifying directly.
        // See https://github.com/tModLoader/tModLoader/wiki/Basic-Projectile#what-is-ai for more information on custom projectile AI.

        // Apply gravity after a quarter of a second
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 13f)
        {
            Projectile.ai[0] = 8f;
            Projectile.velocity.Y += 0.23f;
        }

        // The projectile is rotated to face the direction of travel
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

        // Cap downward velocity
        if (Projectile.velocity.Y > 13f)
        {
            Projectile.velocity.Y = 19f;
        }

        float maxDetectRadius = 2250f; // The maximum radius at which a projectile can detect a target

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
        Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(60f)).ToRotationVector2() * length;

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


        Vector2 Peanits = Projectile.Center - new Vector2(Main.rand.Next(-4, 4), 2);
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits,
        new Vector2(11, 5).RotatedBy((Peanits).DirectionTo(Projectile.Center).ToRotation()),
        ModContent.ProjectileType<GalaxyBoom>(), Projectile.damage = (int)(Projectile.damage * 0.75f), Projectile.knockBack, Projectile.owner);
        Vector2 JorkinMy = Projectile.Center - new Vector2(Main.rand.Next(-4, 4), 2);
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), JorkinMy,
        new Vector2(-3, 8).RotatedBy((JorkinMy).DirectionTo(Projectile.Center).ToRotation()),
        ModContent.ProjectileType<PearlProj>(), Projectile.damage = (int)(Projectile.damage * 0.5f), Projectile.knockBack, Projectile.owner);
        Vector2 InDaClerb = Projectile.Center - new Vector2(Main.rand.Next(-4, 4), 2);
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), InDaClerb,
        new Vector2(6, -10).RotatedBy((InDaClerb).DirectionTo(Projectile.Center).ToRotation()),
        ModContent.ProjectileType<PearlProj>(), Projectile.damage = (int)(Projectile.damage * 1.025f), Projectile.knockBack, Projectile.owner);
        Vector2 JorkinMy2 = Projectile.Center - new Vector2(Main.rand.Next(-4, 4), 2);
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), JorkinMy2,
        new Vector2(3, -8).RotatedBy((JorkinMy).DirectionTo(Projectile.Center).ToRotation()),
        ModContent.ProjectileType<GalaxyShard>(), Projectile.damage = (int)(Projectile.damage * 0.66f), Projectile.knockBack, Projectile.owner);
        Vector2 InDaClerb2 = Projectile.Center - new Vector2(Main.rand.Next(-4, 4), 2);
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), InDaClerb2,
        new Vector2(-6, 10).RotatedBy((InDaClerb).DirectionTo(Projectile.Center).ToRotation()),
        ModContent.ProjectileType<GalaxyShard>(), Projectile.damage = (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);



        SoundEngine.PlaySound(SoundID.Shatter, Projectile.position); // Plays the basic sound most projectiles make when hitting blocks.

    }
}