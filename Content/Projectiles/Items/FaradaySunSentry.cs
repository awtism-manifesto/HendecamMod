using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Dusts;
using Terraria.Audio;

namespace HendecamMod.Content.Projectiles.Items;

public class FaradaySunSentry : ModProjectile
{
    // Use ai[0] for current angle (in radians)
    public ref float OrbitAngle => ref Projectile.ai[0];

    // Use ai[1] for orbit speed (radians per frame)
    public ref float OrbitSpeed => ref Projectile.ai[1];

    // Use localAI[0] for initialization flag
    public bool JustSpawned
    {
        get => Projectile.localAI[0] == 0;
        set => Projectile.localAI[0] = value ? 0 : 1;
    }

    // Shooting timer (using localAI[1] for this)
    public ref float ShootTimer => ref Projectile.localAI[1];

    // Constants for orbit
    private const float OrbitDistance = 275f;
    private const float FullRevolutionTime = 1f;

    // Constants for targeting and shooting
    private const int TargetingRange = 64 * 16;
    private const float FireVelocity = 15f;

    // Eclipse cooldown timer (using localAI[2] for this)
    public ref float EclipseCooldown => ref Projectile.localAI[2];

    // Eclipse constants
    private const int EclipseCooldownFrames = 225; // Cooldown in frames
    private const float AngleTolerance = 0.05f; // Tolerance for angle matching in radians

    public override void SetDefaults()
    {
        Projectile.width = 64;
        Projectile.height = 64;
        Projectile.aiStyle = -1; // Custom AI
        Projectile.light = 0.67f;
        Projectile.tileCollide = false;
        Projectile.friendly = false;
        Projectile.DamageType = GetInstance<StupidDamage>();
        Projectile.penetrate = -1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.alpha = 255;
        Projectile.extraUpdates = 0;
        Projectile.minion = true;
        Projectile.ignoreWater = true;
        Projectile.netImportant = true;

        // Critical: Set these to prevent automatic movement
        Projectile.velocity = Vector2.Zero;

        // Set a long time but not int.MaxValue (can cause issues)
        Projectile.timeLeft = 1000000;
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void AI()
    {
        Player player = Main.player[Projectile.owner];

        // Safety checks
        if (!player.active || player.dead)
        {
            Projectile.Kill();
            return;
        }

        // Initialize orbit parameters if just spawned
        if (JustSpawned)
        {
            JustSpawned = false;

            // Set initial random angle
            OrbitAngle = Main.rand.NextFloat(0f, MathHelper.TwoPi);

            // Calculate orbit speed: 2π radians per 60 frames (1 second)
            OrbitSpeed = MathHelper.TwoPi / 150f;

            // Initialize timers
            ShootTimer = 0;
            EclipseCooldown = 0;

            // Play spawn sound
            SoundEngine.PlaySound(SoundID.Item99, Projectile.position);

            // Spawn spawn dust effects
            for (int i = 0; i < 36; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Electric, speed * 2, Scale: 0.5f);
                d.noGravity = true;
            }
        }

        // Update orbit angle
        OrbitAngle += OrbitSpeed;

        // Keep angle within range
        if (OrbitAngle >= MathHelper.TwoPi)
            OrbitAngle -= MathHelper.TwoPi;

        // Calculate new position
        Vector2 offset = new Vector2((float)Math.Cos(OrbitAngle), (float)Math.Sin(OrbitAngle)) * OrbitDistance;
        Vector2 newPosition = player.Center + offset;

        // Update position
        Projectile.Center = newPosition;

        // IMPORTANT: For multiplayer, we need to tell other clients about the position change
        if (Main.myPlayer == Projectile.owner)
        {
            Projectile.netUpdate = true;
        }

        // Optional: Add a rotation effect
        Projectile.rotation = OrbitAngle + MathHelper.PiOver2;

        // Visual effects
        if (Main.rand.NextBool(3))
        {
            Dust dust = Dust.NewDustDirect(
                Projectile.position,
                Projectile.width,
                Projectile.height,
                DustID.Torch,
                0, 0
            );
            dust.noGravity = true;
            dust.velocity = -Projectile.velocity * 0.5f;
            dust.scale = 0.8f;
        }

        // ----- ECLIPSE LASER LOGIC -----
        if (EclipseCooldown > 0)
            EclipseCooldown--;

        // Find the moon sentry
        Projectile moonSentry = FindMoonSentry();

        // Check if sun and moon are aligned
        if (moonSentry != null && EclipseCooldown <= 0)
        {
            float moonAngle = moonSentry.ai[0];
            float angleDifference = Math.Abs(OrbitAngle - moonAngle);
            angleDifference = Math.Min(angleDifference, MathHelper.TwoPi - angleDifference);

            if (angleDifference <= AngleTolerance)
            {
                // Spawn eclipse laser
                if (Main.myPlayer == Projectile.owner)
                {
                    // Targeting logic for eclipse
                    Vector2 laserDirection = Vector2.Zero;
                    NPC eclipseTarget = null;
                    float closestDistance = TargetingRange;

                    if (Projectile.OwnerMinionAttackTargetNPC != null)
                    {
                        TryTargetingForEclipse(Projectile.OwnerMinionAttackTargetNPC, ref closestDistance, ref eclipseTarget);
                    }

                    if (eclipseTarget == null)
                    {
                        foreach (var npc in Main.ActiveNPCs)
                        {
                            TryTargetingForEclipse(npc, ref closestDistance, ref eclipseTarget);
                        }
                    }

                    if (eclipseTarget != null)
                    {
                        laserDirection = (eclipseTarget.Center - Projectile.Center).SafeNormalize(Vector2.UnitY);
                    }
                    else
                    {
                        laserDirection = (Projectile.Center - player.Center).SafeNormalize(Vector2.UnitY);
                    }

                    int type = ProjectileType<EclipseLaser>();
                    Vector2 laserVelocity = laserDirection * 25f;

                    Projectile.NewProjectile(
                        Projectile.GetSource_FromThis(),
                        Projectile.Center,
                        laserVelocity,
                        type,
                        (int)(Projectile.damage * 12.5f),
                        5f,
                        Projectile.owner
                    );

                    SoundEngine.PlaySound(SoundID.Item99 with { Volume = 1.5f }, Projectile.Center);
                    SoundEngine.PlaySound(SoundID.Item92 with { Volume = 1.5f }, Projectile.Center);
                    SoundEngine.PlaySound(SoundID.Item114 with { Volume = 1.5f }, Projectile.Center);

                    for (int i = 0; i < 50; i++)
                    {
                        Vector2 dustSpeed = Main.rand.NextVector2Circular(5f, 5f);
                        Dust eclipseDust = Dust.NewDustPerfect(Projectile.Center, DustType<AstatineDust>(), dustSpeed, Scale: 1.5f);
                        eclipseDust.noGravity = true;
                    }

                    EclipseCooldown = EclipseCooldownFrames;
                    Projectile.netUpdate = true;
                }
            }
        }

        // ----- SENTRY FIRING LOGIC -----
        // Calculate attack speed based on player's equipment
        float attackSpeed = Math.Max(0.01f, player.GetAttackSpeed(GetInstance<StupidDamage>()));

        // Base delay in frames between shots (adjust as needed)
        int baseDelay = 42;
        int shootFrequency = (int)Math.Max(2f, baseDelay / attackSpeed);

        // Find an enemy to target
        float closestTargetDistance = TargetingRange;
        NPC targetNPC = null;

        // Prioritize the owner's minion attack target (right click or whip feature)
        if (Projectile.OwnerMinionAttackTargetNPC != null)
        {
            TryTargeting(Projectile.OwnerMinionAttackTargetNPC, ref closestTargetDistance, ref targetNPC);
        }

        // If no minion attack target or if it was out of range, find the closest enemy to target
        if (targetNPC == null)
        {
            foreach (var npc in Main.ActiveNPCs)
            {
                TryTargeting(npc, ref closestTargetDistance, ref targetNPC);
            }
        }

        // Shoot at target if we have one
        if (targetNPC != null)
        {
            // Rotate to face target (optional visual)
            Vector2 directionToTarget = targetNPC.Center - Projectile.Center;
            float targetRotation = directionToTarget.ToRotation();
            Projectile.rotation = Projectile.rotation.AngleLerp(targetRotation, 0.2f);

            // Shoot if timer is ready
            if (ShootTimer <= 0)
            {
                ShootTimer = shootFrequency;

                // Play shoot sound
                SoundEngine.PlaySound(SoundID.Item91 with { Volume = 1f }, Projectile.Center);

                // Spawn projectile (only if local player is owner for multiplayer)
                if (Main.myPlayer == Projectile.owner)
                {
                    // Direction to target
                    Vector2 shootDirection = (targetNPC.Center - Projectile.Center).SafeNormalize(Vector2.UnitX);

                    // Final velocity vector
                    Vector2 shootVelocity = shootDirection * FireVelocity;

                    // Determine projectile type to shoot
                    int type = ProjectileType<FaradaySunShot>();

                    // Spawn the projectile
                    Projectile.NewProjectile(
                        Projectile.GetSource_FromThis(),
                        Projectile.Center,
                        shootVelocity,
                        type,
                        (int)(Projectile.damage * 3),
                        3,
                        Projectile.owner
                    );
                }
            }
        }

        // Count down the shoot timer
        ShootTimer--;

        // Keep projectile alive
        Projectile.timeLeft = 2;
    }

    private void TryTargeting(NPC npc, ref float closestTargetDistance, ref NPC targetNPC)
    {
        if (npc.CanBeChasedBy(this))
        {
            float distanceToTargetNPC = Vector2.Distance(Projectile.Center, npc.Center);
            // Is this enemy closer than others? Is it in line of sight?
            if (distanceToTargetNPC < closestTargetDistance)
            {
                closestTargetDistance = distanceToTargetNPC;
                targetNPC = npc;
            }
        }
    }

    private void TryTargetingForEclipse(NPC npc, ref float closestDistance, ref NPC eclipseTarget)
    {
        if (npc.CanBeChasedBy(this))
        {
            float distance = Vector2.Distance(Projectile.Center, npc.Center);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                eclipseTarget = npc;
            }
        }
    }

    private Projectile FindMoonSentry()
    {
        for (int i = 0; i < Main.maxProjectiles; i++)
        {
            Projectile proj = Main.projectile[i];
            if (proj.active &&
                proj.owner == Projectile.owner &&
                proj.type == ProjectileType<FaradayMoonSentry>())
            {
                return proj;
            }
        }
        return null;
    }
}