using HendecamMod.Content.Items.Weapons.Melee;



namespace HendecamMod.Content.Projectiles;

public class KusariProj : ModProjectile
{
    public override void SetStaticDefaults()
    {
        // The following sets are only applicable to yoyo that use aiStyle 99.

        // YoyosLifeTimeMultiplier is how long in seconds the yoyo will stay out before automatically returning to the player. 
        // Vanilla values range from 3f (Wood) to 16f (Chik), and defaults to -1f. Leaving as -1 will make the time infinite.
        ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f;

        // YoyosMaximumRange is the maximum distance the yoyo sleep away from the player. 
        // Vanilla values range from 130f (Wood) to 400f (Terrarian), and defaults to 200f.
        ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 325f;

        // YoyosTopSpeed is top speed of the yoyo Projectile.
        // Vanilla values range from 9f (Wood) to 17.5f (Terrarian), and defaults to 10f.
        ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 21.5f;
        Main.projFrames[Projectile.type] = 6;
    }

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        Player player = Main.player[Projectile.owner];
        var modPlayer = player.GetModPlayer<FunCD>();
        if (modPlayer.FunTimeActive == true)
        {
            modifiers.SourceDamage *= 2.5f;
        }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        Player player = Main.player[Projectile.owner];
        var modPlayer = player.GetModPlayer<FunCD>();
        target.AddBuff(BuffID.OnFire, 300);
        target.AddBuff(BuffID.OnFire3, 300);
        if (modPlayer.FunTimeActive == true)
        {
            target.AddBuff(BuffID.Oiled, 300);
            Vector2 Spawn = (target.Center);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Spawn,
                new Vector2(0, 0).RotatedBy((Spawn).DirectionTo(Projectile.Center).ToRotation()),
                ProjectileType<BoomSmallish>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
        }
    }

    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        var modPlayer = player.GetModPlayer<FunCD>();

        if (modPlayer.FunTimeActive == true)
        {
            // Handle yoyo frame animation
            int frameSpeed = 8;

            Projectile.frameCounter++;

            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 1;
                }
            }

            // NEW: Pull player toward mouse cursor with enhanced vertical strength
            Vector2 mousePosition = Main.MouseWorld;
            Vector2 directionToMouse = mousePosition - player.Center;

            // Only pull if the distance is significant (avoid jittering)
            if (directionToMouse.Length() > 5f)
            {
                Vector2 normalizedDirection = directionToMouse;
                normalizedDirection.Normalize();

                // Max speed of 21.5 (matches yoyo's top speed)
                float maxSpeed = 21.5f;

                // Different pull strengths for horizontal and vertical movement
                float horizontalPullStrength = 0.5f;  // Base horizontal pull
                float verticalPullStrength = 1.2f;    // Stronger vertical pull to overcome gravity
                float upwardPullStrength = 1.5f;   // Pulling up
                float downwardPullStrength = 6.7f; // Pulling down
                // Apply different strengths based on direction
                Vector2 pullVelocity;
                pullVelocity.X = normalizedDirection.X * horizontalPullStrength;
                pullVelocity.Y = normalizedDirection.Y * verticalPullStrength;

                // Bonus: Extra upward pull when jumping
                if (normalizedDirection.Y < 0 && player.velocity.Y < 0)
                {
                    // If already moving upward and mouse is above, give extra boost
                    pullVelocity.Y *= 1.5f;
                }
                if (normalizedDirection.Y < 0)
                    pullVelocity.Y = normalizedDirection.Y * upwardPullStrength;
                else
                    pullVelocity.Y = normalizedDirection.Y * downwardPullStrength;
                // Apply the pull to the player's velocity
                player.velocity += pullVelocity;

                // Clamp the player's velocity to the maximum speed
                if (player.velocity.Length() > maxSpeed)
                {
                    player.velocity = Vector2.Normalize(player.velocity) * maxSpeed;
                }

                // Optional: Reduce gravity effect while being pulled vertically
                if (Math.Abs(normalizedDirection.Y) > 0.5f)
                {
                    player.gravity = 0.3f; // Reduced from default 0.4f
                }
                else
                {
                    player.gravity = 0.4f; // Restore normal gravity
                }
            }
            else
            {
                // Restore normal gravity when not actively pulling
                player.gravity = 0.4f;
            }

            // NEW: Make the yoyo move at a similar rate to the player
            // Calculate the yoyo's target position (relative to player movement)
            Vector2 yoyoToPlayer = player.Center - Projectile.Center;

            // If yoyo is too far, pull it toward the player's movement direction
            if (yoyoToPlayer.Length() > ProjectileID.Sets.YoyosMaximumRange[Projectile.type] * 0.8f)
            {
                // Calculate the player's movement direction and speed
                Vector2 playerMovementDir = player.velocity;
                if (playerMovementDir.Length() > 0.1f)
                {
                    playerMovementDir.Normalize();

                    // Make the yoyo follow the player's movement direction
                    Vector2 followVelocity = playerMovementDir * ProjectileID.Sets.YoyosTopSpeed[Projectile.type] * 0.8f;
                    Projectile.velocity = (Projectile.velocity * 0.95f + followVelocity * 0.05f);
                }
            }

            // Smoothly move the yoyo to maintain the visual of being pulled
            if (player.velocity.Length() > 0.5f)
            {
                // Add a small force to the yoyo in the player's movement direction
                Vector2 playerMovement = player.velocity;
                playerMovement.Normalize();
                Projectile.velocity += playerMovement * 0.3f;

                // Cap the yoyo's velocity to its max speed
                if (Projectile.velocity.Length() > ProjectileID.Sets.YoyosTopSpeed[Projectile.type])
                {
                    Projectile.velocity = Vector2.Normalize(Projectile.velocity) * ProjectileID.Sets.YoyosTopSpeed[Projectile.type];
                }
            }
        }
        else
        {
            Projectile.frame = 0;
           
            
            player.gravity = 0.4f;
        }

        if (Math.Abs(Projectile.velocity.X) >= 0f || Math.Abs(Projectile.velocity.Y) >= 0f)
        {
            for (int i = 0; i < 2; i++)
            {
                float posOffsetX = 0f;
                float posOffsetY = 0f;
                if (i == 1)
                {
                    posOffsetX = Projectile.velocity.X * 2.5f;
                    posOffsetY = Projectile.velocity.Y * 2.5f;
                }

                Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + -5f + posOffsetX, Projectile.position.Y + 5f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 8, Projectile.height - 8, DustID.Torch, 0f, 0.33f, 100);
                fireDust.fadeIn = 0.2f + Main.rand.Next(4) * 0.1f;
                fireDust.noGravity = true;
                fireDust.velocity *= 2.33f;
                Dust fire2Dust = Dust.NewDustDirect(new Vector2(Projectile.position.X + -5f + posOffsetX, Projectile.position.Y + 5f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 8, Projectile.height - 8, DustID.FlameBurst, 0f, 0.33f, 100);
                fire2Dust.fadeIn = 0.2f + Main.rand.Next(4) * 0.1f;
                fire2Dust.noGravity = true;
                fire2Dust.velocity *= 2.33f;
            }
        }
    }

    public override void SetDefaults()
    {
        Projectile.width = 24; // The width of the projectile's hitbox.
        Projectile.height = 24; // The height of the projectile's hitbox.

        Projectile.aiStyle = ProjAIStyleID.Yoyo; // The projectile's ai style. Yoyos use aiStyle 99 (ProjAIStyleID.Yoyo). A lot of yoyo code checks for this aiStyle to work properly.
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 10;
        Projectile.friendly = true; // Player shot projectile. Does damage to enemies but not to friendly Town NPCs.
        Projectile.DamageType = DamageClass.MeleeNoSpeed; // Benefits from melee bonuses. MeleeNoSpeed means the item will not scale with attack speed.
        Projectile.penetrate = -1; // All vanilla yoyos have infinite penetration. The number of enemies the yoyo can hit before being pulled back in is based on YoyosLifeTimeMultiplier.
        // Projectile.scale = 1f; // The scale of the projectile. Most yoyos are 1f, but a few are larger. The Kraken is the largest at 1.2f
    }
}