using Terraria.Audio;

namespace HendecamMod.Content.Projectiles.Items;

public class SuperMaelstromProj : ModProjectile
{
    public ref float ShootTimer => ref Projectile.ai[0];
    public ref float RotationAngle => ref Projectile.ai[1]; // Store the current rotation angle

    public bool Floating => Projectile.ai[2] == 0;

    public bool JustSpawned
    {
        get => Projectile.localAI[0] == 0;
        set => Projectile.localAI[0] = value ? 0 : 1;
    }

    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 1;
        ProjectileID.Sets.MinionTargettingFeature[Type] = true;
    }

    public override void SetDefaults()
    {
        Projectile.width = 64;
        Projectile.height = 64;
        Projectile.DamageType = DamageClass.Summon;
        Projectile.sentry = true;
        Projectile.timeLeft = Projectile.SentryLifeTime;
        Projectile.ignoreWater = true;
        Projectile.netImportant = true;

        DrawOffsetX = 0;
    }

    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
        fallThrough = false;
        return true;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        return false;
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void AI()
    {
        const int ShootFrequency = 2; // How long the sentry waits between shots.
        const int TargetingRange = 81 * 16; // The sentry's targeting range, 50 tiles.
        const float FireVelocity = 21.85f; // The velocity the sentry's shot projectile will travel.

        Player player = Main.player[Projectile.owner];
        player.maxTurrets += -4;
        player.UpdateMaxTurrets();

        // Initialize rotation angle if this is the first time running
        if (JustSpawned)
        {
            JustSpawned = false;
            ShootTimer = ShootFrequency * 1.75f; // Delay the first shot slightly
            RotationAngle = 0f; // Start at 0 degrees

            SoundEngine.PlaySound(SoundID.Item99, Projectile.position);

            for (int i = 0; i < 30; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Smoke, speed * 4, Scale: 1.5f);
                d.noGravity = true;
            }
        }

        if (Main.rand.NextBool(10))
        {
            Dust dust = Dust.NewDustDirect(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Smoke, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            dust.noGravity = true;
            dust.velocity *= 0.8f;
        }

        // Find an enemy to target.
        float closestTargetDistance = TargetingRange;
        NPC targetNPC = null;

        if (Projectile.OwnerMinionAttackTargetNPC != null)
        {
            TryTargeting(Projectile.OwnerMinionAttackTargetNPC, ref closestTargetDistance, ref targetNPC);
        }

        if (targetNPC == null)
        {
            foreach (var npc in Main.ActiveNPCs)
            {
                TryTargeting(npc, ref closestTargetDistance, ref targetNPC);
            }
        }

        if (targetNPC != null)
        {
            if (ShootTimer <= 0)
            {
                ShootTimer = ShootFrequency;

                SoundEngine.PlaySound(SoundID.Item99 with { Volume = 0.4f }, Projectile.Center);

                if (Main.myPlayer == Projectile.owner)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke);
                        dust.noGravity = true;
                        dust.velocity *= 11.5f;
                        dust.scale *= 1.45f;
                    }

                   
                    float rotationInRadians = MathHelper.ToRadians(RotationAngle);

                   
                    float[] Angles = { 0f, 90f, 180f, 270f };
                    int type = ModContent.ProjectileType<Blade>();

                    foreach (float Angle in Angles)
                    {
                        // Convert the relative angle to radians and add to base rotation
                        float totalRotation = rotationInRadians + MathHelper.ToRadians(Angle);
                        Vector2 direction = new Vector2(1, 0).RotatedBy(totalRotation);
                        Vector2 shootVelocity = direction * FireVelocity;

                        Projectile.NewProjectile(Projectile.GetSource_FromThis(),
                            new Vector2(Projectile.Center.X - 4f, Projectile.Center.Y),
                            shootVelocity, type, Projectile.damage, 3, Projectile.owner);
                    }

                    // Increase rotation angle by 6 degrees for next shot
                    RotationAngle += 15f;
                }
            }
        }

        ShootTimer--;
    }

    private void TryTargeting(NPC npc, ref float closestTargetDistance, ref NPC targetNPC)
    {
        if (npc.CanBeChasedBy(this))
        {
            float distanceToTargetNPC = Vector2.Distance(Projectile.Center, npc.Center);
            if (distanceToTargetNPC < closestTargetDistance && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
            {
                closestTargetDistance = distanceToTargetNPC;
                targetNPC = npc;
            }
        }
    }

    public override void OnKill(int timeLeft)
    {
        for (int i = 0; i < 20; i++)
        {
            Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
            Dust d = Dust.NewDustPerfect(Projectile.Center + speed * 50, DustID.Torch, speed * -5, Scale: 1.5f);
            d.noGravity = true;
        }
    }
}