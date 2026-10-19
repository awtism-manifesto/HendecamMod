using HendecamMod.Content.Dusts;

namespace HendecamMod.Content.Projectiles.Items;

public class MintalShurikenProj : ModProjectile
{
    private NPC HomingTarget
    {
        get => Projectile.ai[0] == 0 ? null : Main.npc[(int)Projectile.ai[0] - 1];
        set { Projectile.ai[0] = value == null ? 0 : value.whoAmI + 1; }
    }

    public ref float DelayTimer => ref Projectile.ai[1];

    // Store the last hit NPC's index to avoid re-targeting it immediately
    private int LastHitNPCIndex = -1;

    public override void SetDefaults()
    {
        Projectile.width = 42;
        Projectile.height = 42;
        Projectile.tileCollide = true;
        Projectile.arrow = false;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.MeleeNoSpeed;
        Projectile.timeLeft = 135;
        Projectile.penetrate = 5;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 8;
        Projectile.extraUpdates = 1;
    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        if (target.whoAmI == LastHitNPCIndex)
        {
            modifiers.SourceDamage *= 0.75f;
        }
    }
    
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        // Store the last hit NPC to avoid re-targeting it
        LastHitNPCIndex = target.whoAmI;

        // Reset homing target to force finding a new one
        HomingTarget = null;

        for (int i = 0; i < 5; i++)
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Blood);
            dust.noGravity = true;
            dust.velocity *= 3f;
            dust.scale *= 1.2f;
        }
        for (int i = 0; i < 5; i++)
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustType<MintalDust>());
            dust.noGravity = true;
            dust.velocity *= 2.66f;
            dust.scale *= 1f;
        }
        Projectile.damage = (int)(Projectile.damage * 0.9f);
    }

    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        Projectile.rotation -= 0.345f;

        
      

        // HOMING PHASE - only active when not returning

        // A short delay to homing behavior after being fired
        if (DelayTimer < 10)
        {
            DelayTimer += 1;
            return;
        }

        float maxDetectRadius = 1500f;

        // Find a new homing target if we don't have one or current target is invalid
        if (HomingTarget == null || !IsValidTarget(HomingTarget))
        {
            HomingTarget = FindClosestNPCWithPriority(maxDetectRadius);
        }

        // If we have a homing target, adjust trajectory
        if (HomingTarget != null && IsValidTarget(HomingTarget))
        {
            
                float length = Projectile.velocity.Length();
                float targetAngle = Projectile.AngleTo(HomingTarget.Center);
                Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(8f)).ToRotationVector2() * length;
               
            
        }
    }

    public NPC FindClosestNPCWithPriority(float maxDetectDistance)
    {
        NPC closestNPC = null;
        float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

        // First pass: try to find any valid NPC that ISN'T the last hit one
        foreach (var target in Main.ActiveNPCs)
        {
            if (IsValidTarget(target) && target.whoAmI != LastHitNPCIndex)
            {
                float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                if (sqrDistanceToTarget < sqrMaxDetectDistance)
                {
                    sqrMaxDetectDistance = sqrDistanceToTarget;
                    closestNPC = target;
                }
            }
        }

        // Second pass: if no other NPCs found, allow targeting the last hit one again
        if (closestNPC == null)
        {
            sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            foreach (var target in Main.ActiveNPCs)
            {
                if (IsValidTarget(target))
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }
        }

        return closestNPC;
    }

    public NPC FindClosestNPC(float maxDetectDistance)
    {
        NPC closestNPC = null;
        float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

        foreach (var target in Main.ActiveNPCs)
        {
            if (IsValidTarget(target))
            {
                float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

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
        return target.CanBeChasedBy() && Collision.CanHit(Projectile.Center, 1, 1, target.position, target.width, target.height);
    }
}