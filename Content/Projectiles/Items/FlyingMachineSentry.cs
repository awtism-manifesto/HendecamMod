using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using Terraria.Audio;

namespace HendecamMod.Content.Projectiles.Items;

public class FlyingMachineSentry : ModProjectile
{
    
    private const float PreferredRange = 325f;
    
    private const float RangeTolerance = 75f;

    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 4;
        Main.projPet[Projectile.type] = true;

        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
    }

    public override void SetDefaults()
    {
        Projectile.netImportant = true;
        Projectile.width = 115;
        Projectile.height = 115;
        Projectile.timeLeft = 13000;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        Projectile.minion = true;
        Projectile.minionSlots = 1f;
        Projectile.DamageType = GetInstance<RangedSummonDamage>();

        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 17;
    }

    public override bool? CanCutTiles() => false;

    
    public override bool MinionContactDamage() => false;

    public override void AI()
    {
        Player owner = Main.player[Projectile.owner];

        if (!CheckActive(owner))
            return;

        GeneralBehavior(owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
        SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
        Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);
        ShootAtTarget(foundTarget, targetCenter);
        Visuals(foundTarget, targetCenter);
    }

    private bool CheckActive(Player owner)
    {
        if (owner.dead || !owner.active)
        {
            owner.ClearBuff(BuffType<FlyingMachineBuff>());
            return false;
        }

        if (owner.HasBuff(BuffType<FlyingMachineBuff>()))
            Projectile.timeLeft = 3;

        return true;
    }

    private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition)
    {
        Vector2 idlePosition = owner.Center;
        idlePosition.Y -= 48f;

        float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -owner.direction;
        idlePosition.X += minionPositionOffsetX;

        vectorToIdlePosition = idlePosition - Projectile.Center;
        distanceToIdlePosition = vectorToIdlePosition.Length();

        if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f)
        {
            Projectile.position = idlePosition;
            Projectile.velocity *= 0.1f;
            Projectile.netUpdate = true;
        }

        float overlapVelocity = 0.04f;

        for (int i = 0; i < Main.maxProjectiles; i++)
        {
            Projectile other = Main.projectile[i];

            if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner &&
                Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width)
            {
                if (Projectile.position.X < other.position.X)
                    Projectile.velocity.X -= overlapVelocity;
                else
                    Projectile.velocity.X += overlapVelocity;

                if (Projectile.position.Y < other.position.Y)
                    Projectile.velocity.Y -= overlapVelocity;
                else
                    Projectile.velocity.Y += overlapVelocity;
            }
        }
    }

    private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
    {
        distanceFromTarget = 700f;
        targetCenter = Projectile.position;
        foundTarget = false;

        if (owner.HasMinionAttackTargetNPC)
        {
            NPC npc = Main.npc[owner.MinionAttackTargetNPC];
            float between = Vector2.Distance(npc.Center, Projectile.Center);

            if (between < 2000f)
            {
                distanceFromTarget = between;
                targetCenter = npc.Center;
                foundTarget = true;
            }
        }

        if (!foundTarget)
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (npc.CanBeChasedBy())
                {
                    float between = Vector2.Distance(npc.Center, Projectile.Center);
                    bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                    bool inRange = between < distanceFromTarget;
                    bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
                    bool closeThroughWall = between < 100f;

                    if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                    {
                        distanceFromTarget = between;
                        targetCenter = npc.Center;
                        foundTarget = true;
                    }
                }
            }
        }

        Projectile.friendly = foundTarget;
    }

    private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
    {
        float speed = 9f;
        float inertia = 13f;

        if (foundTarget)
        {
            Vector2 direction = targetCenter - Projectile.Center;
            float distance = direction.Length();

            if (distance > 0f)
                direction /= distance; // normalize

            // If too close, back away; if too far, move in; otherwise strafe slowly
            if (distance < PreferredRange - RangeTolerance)
            {
                // Too close — move away from the target
                Vector2 away = -direction;
                away *= speed;
                Projectile.velocity = (Projectile.velocity * (inertia - 1) + away) / inertia;
            }
            else if (distance > PreferredRange + RangeTolerance)
            {
                // Too far — move toward the target
                Vector2 toward = direction * speed;
                Projectile.velocity = (Projectile.velocity * (inertia - 1) + toward) / inertia;
            }
            else
            {
                // In the sweet spot — slow drift to avoid being stationary
                Projectile.velocity *= 0.96f;
            }
        }
        else
        {
            if (distanceToIdlePosition > 600f)
            {
                speed = 13f;
                inertia = 60f;
            }
            else
            {
                speed = 6f;
                inertia = 80f;
            }

            if (distanceToIdlePosition > 20f)
            {
                vectorToIdlePosition.Normalize();
                vectorToIdlePosition *= speed;
                Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
            }
            else if (Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity.X = -0.15f;
                Projectile.velocity.Y = -0.5f;
            }
        }
    }

    private void ShootAtTarget(bool foundTarget, Vector2 targetCenter)
    {
        if (!foundTarget)
            return;

        // Fire only on the owning client to prevent duplicate projectiles in multiplayer
        if (Main.myPlayer != Projectile.owner)
            return;

        Projectile.ai[0]++; // shoot cooldown timer

        // Adjust this value to change fire rate (lower = faster)
        const float FireRate = 19f;

        if (Projectile.ai[0] < FireRate)
            return;

        Projectile.ai[0] = 0;

        Vector2 shootDirection = targetCenter - Projectile.Center;
        shootDirection.Normalize();
        shootDirection *= 15.5f; // projectile speed

        Player owner = Main.player[Projectile.owner];
        float damageMultiplier = owner.GetTotalDamage(Projectile.DamageType).ApplyTo(1f);

        int shotDamage = (int)(Projectile.damage * 1.066f / damageMultiplier);

        Projectile.NewProjectile(
            Projectile.GetSource_FromAI(),
            Projectile.Center,
            shootDirection,
            ModContent.ProjectileType<FlyingMachineShot>(),
            shotDamage,
            Projectile.knockBack,
            Projectile.owner
        );
        SoundEngine.PlaySound(new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/FlyingCannon")
        {
            Volume = 0.125f,
            PitchVariance = 0f,
            MaxInstances = 50,
        });

        Projectile.netUpdate = true;
    }

    private void Visuals(bool foundTarget, Vector2 targetCenter)
    {
        if (foundTarget)
        {
            // Gun points left by default. If target is to the right, mirror the sprite.
            float targetDirection = targetCenter.X - Projectile.Center.X;
            Projectile.spriteDirection = targetDirection > 0f ? -1 : 1;
        }
        else
        {
            // Idle: face the direction of travel
            Projectile.spriteDirection = Projectile.velocity.X > 0f ? -1 : 1;
        }

        Projectile.rotation = Projectile.velocity.X * 0.045f;

        int frameSpeed = 6;
        Projectile.frameCounter++;

        if (Projectile.frameCounter >= frameSpeed)
        {
            Projectile.frameCounter = 0;
            Projectile.frame++;

            if (Projectile.frame >= Main.projFrames[Projectile.type])
                Projectile.frame = 0;
        }
    }
}