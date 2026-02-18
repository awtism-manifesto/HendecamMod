using HendecamMod.Content.Items.Weapons;
using Terraria.Audio;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles.Items;

public class TackParagon : ModProjectile
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
      
    }

    public override void SetDefaults()
    {
        Projectile.width = 110;
        Projectile.height = 110;
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
    private int nextSpawnTick;
    private int tickCounter;
    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        CrucibleOfFlameAndSteel instance = new CrucibleOfFlameAndSteel();

        Lighting.AddLight(Projectile.Center, 2.25f, 1.65f, 0.65f);


        const int ShootFrequency = 1; // How long the sentry waits between shots.
        const int TargetingRange = 90 * 16; // The sentry's targeting range, 50 tiles.
      

       
        var modPlayer = player.GetModPlayer<TackCD>();

        float FireVelocity = modPlayer.TackStormActive ? 17.5f : 8.25f;

        player.maxTurrets += -9;
        player.UpdateMaxTurrets();
        // Initialize rotation angle if this is the first time running
        if (JustSpawned)
        {
            JustSpawned = false;
            ShootTimer = ShootFrequency * 30f; // Delay the first shot slightly
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
                    for (int i = 0; i < 15; i++)
                    {
                        Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
                        dust.noGravity = true;
                        dust.velocity *= 11.5f;
                        dust.scale *= 1.45f;
                    }


                    if (nextSpawnTick == 0)
                    {
                        nextSpawnTick = 120;
                    }

                    tickCounter++;

                    if (tickCounter >= nextSpawnTick)
                    {
                        Vector2 velocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(360));
                        Vector2 Peanits = Projectile.Center;
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits, velocity,
                            ModContent.ProjectileType<ParagonMeteor>(), (int)(Projectile.damage * 30f), Projectile.knockBack, Projectile.owner);

                        tickCounter = 0;
                        nextSpawnTick = 240;
                        Projectile.netUpdate = true;
                    }

                    // Calculate rotation in radians - increase by 6 degrees each shot
                    float rotationInRadians = MathHelper.ToRadians(RotationAngle);

                    float backRotationInRadians = MathHelper.ToRadians(0f - RotationAngle);

                    // Create direction vectors at the current rotation and opposite direction
                    Vector2 baseDirection = new Vector2(1, 0).RotatedBy(rotationInRadians);
                    Vector2 oppositeDirection = -baseDirection; // 180 degrees opposite

                    Vector2 base2Direction = new Vector2(1, 0).RotatedBy(backRotationInRadians);
                    Vector2 opposite2Direction = -base2Direction; // 180 degrees opposite

                    // Scale by FireVelocity to get final velocity
                    Vector2 shootVelocity = baseDirection * FireVelocity;
                    Vector2 shoot2Velocity = oppositeDirection * FireVelocity;

                    Vector2 shoot3Velocity = base2Direction * FireVelocity;
                    Vector2 shoot4Velocity = opposite2Direction * FireVelocity;

                    int type = ModContent.ProjectileType<ParagonBlade>();

                    Projectile.NewProjectile(Projectile.GetSource_FromThis(),
                        new Vector2(Projectile.Center.X - 4f, Projectile.Center.Y),
                        shootVelocity, type, Projectile.damage, 3, Projectile.owner);

                    Projectile.NewProjectile(Projectile.GetSource_FromThis(),
                        new Vector2(Projectile.Center.X - 4f, Projectile.Center.Y),
                        shoot2Velocity, type, Projectile.damage, 3, Projectile.owner);

                    Projectile.NewProjectile(Projectile.GetSource_FromThis(),
                       new Vector2(Projectile.Center.X - 4f, Projectile.Center.Y),
                       shoot3Velocity, type, Projectile.damage, 3, Projectile.owner);

                    Projectile.NewProjectile(Projectile.GetSource_FromThis(),
                        new Vector2(Projectile.Center.X - 4f, Projectile.Center.Y),
                        shoot4Velocity, type, Projectile.damage, 3, Projectile.owner);

                    // Increase rotation angle by 6 degrees for next shot
                    RotationAngle += 10f;

                    
                    RotationAngle %= 360f;
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