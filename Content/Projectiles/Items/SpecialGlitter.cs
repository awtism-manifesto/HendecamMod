using System;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles.Items;

// This example is similar to the Wooden Arrow projectile
public class SpecialGlitter : ModProjectile
{
    private NPC HomingTarget
    {
        get => Projectile.ai[0] == 0 ? null : Main.npc[(int)Projectile.ai[0] - 1];
        set { Projectile.ai[0] = value == null ? 0 : value.whoAmI + 1; }
    }

    public ref float DelayTimer => ref Projectile.ai[1];

    public override void SetDefaults()
    {
        Projectile.width = 4; // The width of projectile hitbox
        Projectile.height = 4; // The height of projectile hitbox

        Projectile.extraUpdates = 4;
        Projectile.friendly = true;
        Projectile.penetrate = 9999;
        Projectile.DamageType = ModContent.GetInstance<SummonStupidDamage>();
        Projectile.timeLeft = 90;
        Projectile.aiStyle = ProjAIStyleID.Arrow;
        AIType = ProjectileID.Bullet;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 3;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(ModContent.BuffType<AlpineTagBuff>(), 300);

        Main.player[Projectile.owner].MinionAttackTargetNPC = target.whoAmI;
    }

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        modifiers.SourceDamage *= 66.77f;

        modifiers.SetMaxDamage(limit: 1);
    }

    public override void AI()
    {
        if (Math.Abs(Projectile.velocity.X) <= 22.9f && Math.Abs(Projectile.velocity.Y) <= 22.9f)
        {
            Projectile.velocity *= 1.05f;
        }

        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

        for (int i = 0; i < 2; i++)
        {
            float posOffsetX = 0f;
            float posOffsetY = 0f;
            if (i == 1)
            {
                posOffsetX = Projectile.velocity.X * 2.5f;
                posOffsetY = Projectile.velocity.Y * 2.5f;
            }

            Dust chudDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 12, Projectile.height - 12, DustID.PurpleTorch, 0f, 0f, 100, default, 0.2f);
            chudDust.fadeIn = 0.2f + Main.rand.Next(5) * 0.1f;
            chudDust.velocity *= 0.05f;
        }

        float maxDetectRadius = 1999f; // The maximum radius at which a projectile can detect a target

        // A short delay to homing behavior after being fired
        if (DelayTimer < 2)
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
        Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(359f)).ToRotationVector2() * length;
    }

    // Finding the closest NPC to attack within maxDetectDistance range
    // If not found then returns null
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
        return target.CanBeChasedBy() && Collision.CanHit(Projectile.Center, 1, 1, target.position, target.width, target.height);
    }
}