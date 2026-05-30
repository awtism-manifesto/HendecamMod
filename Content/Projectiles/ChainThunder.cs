using HendecamMod.Content.Dusts;

namespace HendecamMod.Content.Projectiles;

public class ChainThunder : ModProjectile
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
        Projectile.width = 9; // The width of projectile hitbox
        Projectile.height = 9; // The height of projectile hitbox

        Projectile.friendly = true; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
        Projectile.DamageType = DamageClass.Melee; // Is the projectile shoot by a ranged weapon?
        Projectile.penetrate = 5; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
        Projectile.timeLeft = 1500;

        Projectile.light = 0.1f;
        Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
        Projectile.tileCollide = false; // Can the projectile collide with tiles?
        Projectile.extraUpdates = 200; // Set to above 0 if you want the projectile to update multiple time in a frame
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 20;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
        Projectile.alpha = 255;
    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        if (target.whoAmI == LastHitNPCIndex)
        {
            modifiers.SourceDamage *= 0.85f;
        }
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (Projectile.penetrate <= 0)
        {
            Projectile.Kill();
        }
        else
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            // If the projectile hits the left or right side of the tile, reverse the X velocity
            if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }

            // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
        }
        Projectile.penetrate--;
        return false;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        // Store the last hit NPC to avoid re-targeting it
        LastHitNPCIndex = target.whoAmI;

        // Reset homing target to force finding a new one
        HomingTarget = null;

        for (int i = 0; i < 5; i++)
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Electric);
            dust.noGravity = true;
            dust.velocity *= 3.75f;
            dust.scale *= 0.4f;
        }
       
        Projectile.damage = (int)(Projectile.damage * 0.75f);
        Projectile.velocity = Projectile.velocity * 1.1f;
    }

    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        Projectile.rotation -= 0.345f;
        if (Projectile.alpha < 220)
        {
            for (int i = 0; i < 1; i++)
            {
                float posOffsetX = 0f;
                float posOffsetY = 0f;
                if (i == 1)
                {
                    posOffsetX = Projectile.velocity.X * 2.5f;
                    posOffsetY = Projectile.velocity.Y * 2.5f;
                }

                Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 7, Projectile.height - 7, DustID.Electric, 0f, 0f, 100, default, 0.5f);
                fireDust.fadeIn = 0.2f + Main.rand.Next(4) * 0.1f;
                fireDust.noGravity = true;
                fireDust.velocity *= 0.15f;
            }
        }

        

        // HOMING PHASE - only active when not returning

        // A short delay to homing behavior after being fired
        if (DelayTimer < 10)
        {
            DelayTimer += 1;
            return;
        }

        float maxDetectRadius = 895f;

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
                Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(1.05f)).ToRotationVector2() * length;

            
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
        return target.CanBeChasedBy();
    }
}
   