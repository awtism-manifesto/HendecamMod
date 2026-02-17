using HendecamMod.Content.DamageClasses;
using Terraria.Audio;

namespace HendecamMod.Content.Projectiles.Items;


public class MartianDrone : ModProjectile
{

    public ref float ShootTimer => ref Projectile.ai[0];

   

    public bool JustSpawned
    {
        get => Projectile.localAI[0] == 0;
        set => Projectile.localAI[0] = value ? 0 : 1;
    }
    public override void SetStaticDefaults()
    {
       
        ProjectileID.Sets.Explosive[Type] = true;
        ProjectileID.Sets.RocketsSkipDamageForPlayers[Type] = true;

        ProjectileID.Sets.IsARocketThatDealsDoubleDamageToPrimaryEnemy[Type] = true;
    }
    public override void SetDefaults()
    {
        Projectile.width = 48; // The width of projectile hitbox
        Projectile.height = 48; // The height of projectile hitbox
        Projectile.timeLeft = 610;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
        Projectile.light = 0.67f;
        Projectile.tileCollide = false;
        Projectile.friendly = true;
        Projectile.DamageType = ModContent.GetInstance<RangedSummonDamage>();
        Projectile.penetrate = -1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.alpha = 255;
        Projectile.extraUpdates = 0;
        Projectile.sentry = true; // Sets the weapon as a sentry for sentry accessories to properly work.
        
        Projectile.ignoreWater = true;
        Projectile.netImportant = true; // Sentries need this so they are synced to newly joining players
    }
    public override Color? GetAlpha(Color lightColor)
    {
        // Always draw fully bright. This is important because sentries can usually be placed inside tiles where it would be dark.
        return Color.White;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.OnFire, 250);
        target.AddBuff(BuffID.OnFire3, 250);
       
    }
    

    public override void AI()
    {

        Player player = Main.player[Projectile.owner];

        
        float attackSpeed = Math.Max(0.01f, player.GetAttackSpeed(ModContent.GetInstance<RangedSummonDamage>()));

        
        int baseDelay = 12; // Base frames between shots
        int ShootFrequency = (int)Math.Max(2f, baseDelay / attackSpeed); // Cap at minimum 2 frames

      

        const int TargetingRange = 75 * 16; // The sentry's targeting range, 50 tiles.
        const float FireVelocity = 18f; // The velocity the sentry's shot projectile will travel.
        if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
        {
            Projectile.PrepareBombToBlow();
        }
        // Code to run when spawned
        if (JustSpawned)
        {
            JustSpawned = false;
            ShootTimer = ShootFrequency * 1f; // Delay the first shot slightly

            // The sound that Frost Hydra, Spider Turret, and Houndius Shootius play when spawned. Optional.
            SoundEngine.PlaySound(SoundID.Item99, Projectile.position);

            // Dust indicating the sentry spawned. Optional.
            for (int i = 0; i < 36; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Electric, speed * 2, Scale: 0.5f);
                d.noGravity = true;
            }
        }
        if (Projectile.timeLeft <= 585)
        {
            Projectile.tileCollide = true;
        }
        else Projectile.tileCollide = false;

        // Spawn dust randomly
        if (Main.rand.NextBool(10))
        {
            Dust dust = Dust.NewDustDirect(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Smoke, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            dust.noGravity = true;
            dust.velocity *= 0.8f;
        }

        // Find an enemy to target.
        float closestTargetDistance = TargetingRange;
        NPC targetNPC = null;
        // Prioritize the owner's minion attack target. (Right click or whip feature)
        if (Projectile.OwnerMinionAttackTargetNPC != null)
        {
            TryTargeting(Projectile.OwnerMinionAttackTargetNPC, ref closestTargetDistance, ref targetNPC);
        }

        // If no minion attack target or if it was out of range, find the closest enemy to target.
        if (targetNPC == null)
        {
            foreach (var npc in Main.ActiveNPCs)
            {
                TryTargeting(npc, ref closestTargetDistance, ref targetNPC);
            }
        }

        if (targetNPC != null)
        {
            Vector2 directionToTarget = targetNPC.Center - Projectile.Right;
            float targetRotation = directionToTarget.ToRotation();

          
            if (Projectile.rotation != targetRotation)
            {
                Projectile.rotation = Projectile.rotation.AngleLerp(targetRotation, 1.25f);
            }

            if (ShootTimer <= 0)
            {
                ShootTimer = ShootFrequency;

                // Play a shoot sound
                SoundEngine.PlaySound(SoundID.Item99 with { Volume = 0.4f }, Projectile.Center);

                // Actually spawning the projectile only runs if the local player is the owner
                if (Main.myPlayer == Projectile.owner)
                {
                    // The direction the projectile will fire.
                    Vector2 shootDirection = (targetNPC.Center - Projectile.Center).SafeNormalize(Vector2.UnitX);
                   
                    // The final velocity vector
                    Vector2 shootVelocity = shootDirection * FireVelocity;

                    // The type of projectile the sentry will shoot. It is important that sentry shots are included in ProjectileID.Sets.SentryShot, so reusing unrelated vanilla projectiles as-is won't work 100%.
                    int type = ModContent.ProjectileType<MartianDroneBolt>();

                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X - 4f, Projectile.Center.Y), shootVelocity, type, (int)(Projectile.damage * 0.67f), 3, Projectile.owner);
                    // Note that Projectile.damage will take into account current equipment damage bonuses automatically for sentries and minions, so there is no need to calculate that here to take advantage of current equipment bonuses.
                    // See Projectile.ContinuouslyUpdateDamageStats docs for more information.
                }
            }
        }

        // Count down the shoot timer
        ShootTimer--;
    }
    private void TryTargeting(NPC npc, ref float closestTargetDistance, ref NPC targetNPC)
    {
        if (npc.CanBeChasedBy(this))
        {
            float distanceToTargetNPC = Vector2.Distance(Projectile.Center, npc.Center);
            // Is this enemy closer than others? Is it in line of sight?
            if (distanceToTargetNPC < closestTargetDistance && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
            {
                closestTargetDistance = distanceToTargetNPC; // Set a new closest distance value
                targetNPC = npc;
            }
        }
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.velocity *= 0f; // Stop moving so the explosion is where the rocket was.
        Projectile.timeLeft = 3; // Set the timeLeft to 3 so it can get ready to explode.
        return false; // Returning false is important here. Otherwise the projectile will die without being resized (no blast radius).
    }

    public override void PrepareBombToBlow()
    {
        Projectile.tileCollide = false; // This is important or the explosion will be in the wrong place if the rocket explodes on slopes.
        Projectile.alpha = 255; // Make the rocket invisible.

        // Resize the hitbox of the projectile for the blast "radius".
        // Rocket I: 128, Rocket III: 200, Mini Nuke Rocket: 250
        // Measurements are in pixels, so 128 / 16 = 8 tiles.
        Projectile.Resize(224, 224);
        // Set the knockback of the blast.
        // Rocket I: 8f, Rocket III: 10f, Mini Nuke Rocket: 12f
        Projectile.knockBack = 6.5f;
    }

    public override void OnKill(int timeLeft)
    {
        // Vanilla code takes care ensuring that in For the Worthy or Get Fixed Boi worlds the blast can damage other players because
        // this projectile is ProjectileID.Sets.Explosive[Type] = true;. It also takes care of hurting the owner. The Projectile.PrepareBombToBlow
        // and Projectile.HurtPlayer methods can be used directly if needed for a projectile not using ProjectileID.Sets.Explosive

        // Play an exploding sound.
        SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

        // Resize the projectile again so the explosion dust and gore spawn from the middle.
        // Rocket I: 22, Rocket III: 80, Mini Nuke Rocket: 50
        Projectile.Resize(210, 210);

        // Spawn a bunch of smoke dusts.
        for (int i = 0; i < 8; i++)
        {
            Dust smokeDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Iron, 0f, 0f, 100, default, 1.25f);
            smokeDust.velocity *= 5.5f;
            smokeDust.noGravity = true;
            Dust smoke3Dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Lead, 0f, 0f, 100, default, 1.25f);
            smoke3Dust.velocity *= 4.25f;
        }
        for (int i = 0; i < 8; i++)
        {
            Dust smokeDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, 0f, 0f, 100, default, 0.5f);
            smokeDust.velocity *= 5.5f;
            smokeDust.noGravity = true;
            Dust smoke3Dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, 0f, 0f, 100, default, 0.5f);
            smoke3Dust.velocity *= 4.25f;
        }

        // Spawn a bunch of fire dusts.
        for (int j = 0; j < 12; j++)
        {
            Dust fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2.5f);
            fireDust.noGravity = true;
            fireDust.velocity *= 6f;
            fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.25f);
            fireDust.velocity *= 2.5f;
        }
    }

}