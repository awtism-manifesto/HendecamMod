using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Dusts;
using HendecamMod.Content.Items.Weapons.Multiclass;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles.Items;

public class FocusBeam : ModProjectile
{
    // Simple beam constants
    private const float MaxBeamLength = 600f;
    private const float BeamTileCollisionWidth = 1f;
    private const float BeamHitboxCollisionWidth = 16f;
    private const int NumSamplePoints = 3;
    private const float BeamLengthChangeFactor = 0.75f;

    // Color constants
    public Color BeamColor = new Color(86, 209, 255) * 0.8f; // Light blue (normal)
    public Color EmpoweredColor = new Color(220, 99, 255) * 0.95f; // Light purple (empowered)

    private const float BeamLightBrightness = 0.75f;

    // Track if beam is empowered
    private bool isEmpowered = false;

    private int pulseTimer = 0;
    private float BeamLength
    {
        get => Projectile.localAI[1];
        set => Projectile.localAI[1] = value;
    }

    public override void SetDefaults()
    {
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.DamageType = GetInstance<OmniDamage>();
        Projectile.penetrate = -1;
        Projectile.alpha = 255;
        Projectile.tileCollide = false;
        Projectile.friendly = true;
        Projectile.scale = 1.5f;

        // Using local NPC immunity
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 10;
    }

    // Send beam data over the network
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(BeamLength);
        writer.Write(isEmpowered);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        BeamLength = reader.ReadSingle();
        isEmpowered = reader.ReadBoolean();
    }

    private int beamFlasher = 0;
    public override void AI()
    {
        beamFlasher++;
        if (beamFlasher >= 12)
        {
            isEmpowered = false;
            beamFlasher = 0;
        }

        Player player = Main.player[Projectile.owner];

        // Check if the player is still holding the Echo Kit and both buttons
        bool shouldContinue = player.active && !player.dead &&
                             player.HeldItem.type == ItemType<EchoKit>() &&
                             Main.mouseLeft && Main.mouseRight &&
                             (player.itemAnimation > 0 || player.itemTime > 0);

        if (!shouldContinue)
        {
            Projectile.Kill();
            return;
        }

        if (player.HeldItem.type == ItemType<EchoKit>())
        {
            // Get the base damage from the held item
            
            float baseDamage = player.HeldItem.damage;

            // Manually calculate total multiplier
            float totalMultiplier = 1f; // Base 100%

            // Generic damage (100% effectiveness)
            totalMultiplier += player.GetDamage(DamageClass.Generic).Additive - 1f;

            // Get all damage classes and check if they should contribute to OmniDamage
            // OmniDamage gets 67% of specialized class bonuses
            DamageClass[] specializedClasses = new DamageClass[] {
        DamageClass.Magic,
        DamageClass.Melee,
        DamageClass.Ranged,
        DamageClass.Summon,
        DamageClass.Throwing,
        GetInstance<StupidDamage>()
        // Add any other classes here
    };

            foreach (var damageClass in specializedClasses)
            {
                // Add 67% of the class's bonus
                float classBonus = player.GetDamage(damageClass).Additive - 1f;
                if (classBonus > 0)
                {
                    totalMultiplier += classBonus * 0.67f;
                }
            }

            // Calculate final damage
            int calculatedDamage = (int)(baseDamage * totalMultiplier);

            // Ensure minimum damage of 1
            if (calculatedDamage < 1) calculatedDamage = 1;

            Projectile.damage = calculatedDamage;
        }

        pulseTimer++;
        float pulseScale = 0.75f + (float)Math.Sin(pulseTimer * 0.05f);

        if (pulseTimer >= 63)
        {
            pulseTimer = 0;
        }

        // Set opacity and scale
        Projectile.Opacity = 1f;
        Projectile.scale = pulseScale;

        if (isEmpowered)
        {
            Projectile.scale = pulseScale * 1.2f;
        }

        // Set beam direction based on player's aim
        Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, true);
        Vector2 mousePosition = Main.MouseWorld;

        Projectile.velocity = Vector2.Normalize(mousePosition - playerCenter);

        if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
        {
            Projectile.velocity = Vector2.UnitX * player.direction;
        }

        Projectile.rotation = Projectile.velocity.ToRotation();

        // Set beam position to player center with offset
        Projectile.Center = playerCenter + Projectile.velocity * 8f;

        // Update beam length through hitscan
        float hitscanBeamLength = PerformBeamHitscan();
        BeamLength = MathHelper.Lerp(BeamLength, hitscanBeamLength, BeamLengthChangeFactor);

        // Cast light along the beam
        Vector2 beamDims = new Vector2(Projectile.velocity.Length() * BeamLength, Projectile.width * Projectile.scale);
        DelegateMethods.v3_1 = (isEmpowered ? EmpoweredColor : BeamColor).ToVector3() * BeamLightBrightness;
        Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * BeamLength, beamDims.Y, new Utils.TileActionAttempt(DelegateMethods.CastLight));

        // Visual effects along the beam
        if (Main.rand.NextBool(3))
        {
            float progress = Main.rand.NextFloat();
            Vector2 dustPos = Projectile.Center + Projectile.velocity * BeamLength * progress;
            Dust dust = Dust.NewDustPerfect(dustPos, isEmpowered ? DustType<PlutoniumDust>() : DustID.Electric,
               (Projectile.velocity.RotatedByRandom(0.3f)) * Main.rand.NextFloat(2.5f, 5f), 100,
                isEmpowered ? EmpoweredColor : BeamColor, isEmpowered ? 2.25f : 1.2f);
            dust.noGravity = true;
        }
    }

    // Override damage calculation for additional effects
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        Player player = Main.player[Projectile.owner];

        // Check if enemy is below half health
        if (target.life <= target.lifeMax / 2)
        {
            Projectile.localNPCHitCooldown = 7;
            // Apply empowered state if not already
            if (!isEmpowered)
            {
                isEmpowered = true;

                // Visual feedback for empowerment
                for (int i = 0; i < 10; i++)
                {
                    Vector2 dustPos = target.Center + Main.rand.NextVector2Circular(70, 70);
                    Dust dust = Dust.NewDustPerfect(dustPos, DustType<PlutoniumDust>(),
                       Projectile.velocity.RotatedByRandom(0.3f), 100, EmpoweredColor, 1.5f);
                    dust.noGravity = true;
                }
            }

            // Apply the empowered damage multiplier
            // Note: This stacks with the base damage multiplier we set in AI
            modifiers.SourceDamage *= 2.25f;
        }
        else
        {
            Projectile.localNPCHitCooldown = 10;
        }
    }

    // Alternative: Use OnHitNPC if you need to apply effects after damage calculation
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (isEmpowered)
        {
            
        }
       
    }

    private float PerformBeamHitscan()
    {
        float[] laserScanResults = new float[NumSamplePoints];
        Collision.LaserScan(Projectile.Center, Projectile.velocity, 0 * Projectile.scale, MaxBeamLength, laserScanResults);

        float averageLengthSample = 0f;
        for (int i = 0; i < laserScanResults.Length; ++i)
        {
            averageLengthSample += laserScanResults[i];
        }
        averageLengthSample /= NumSamplePoints;

        return averageLengthSample;
    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        if (projHitbox.Intersects(targetHitbox))
        {
            return true;
        }

        float _ = float.NaN;
        Vector2 beamEndPos = Projectile.Center + Projectile.velocity * BeamLength;
        return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(),
            Projectile.Center, beamEndPos, BeamHitboxCollisionWidth * Projectile.scale, ref _);
    }

    public override bool PreDraw(ref Color lightColor)
    {
        if (Projectile.velocity == Vector2.Zero)
        {
            return false;
        }

        Texture2D texture = TextureAssets.Projectile[Type].Value;
        Vector2 centerFloored = Projectile.Center.Floor() + Projectile.velocity * Projectile.scale * 10.5f;
        Vector2 drawScale = new Vector2(Projectile.scale);

        float visualBeamLength = BeamLength - 14.5f * Projectile.scale * Projectile.scale;

        Vector2 startPosition = centerFloored - Main.screenPosition;
        Vector2 endPosition = startPosition + Projectile.velocity * visualBeamLength;

        // Draw the main beam (color changes based on empowered state)
        Color currentBeamColor = isEmpowered ? EmpoweredColor : BeamColor;
        DrawBeam(Main.spriteBatch, texture, startPosition, endPosition, drawScale, currentBeamColor);

        // Draw a brighter inner beam for effect
        drawScale *= 0.6f;
        DrawBeam(Main.spriteBatch, texture, startPosition, endPosition, drawScale, Color.White * 0.3f);

        return false;
    }

    private void DrawBeam(SpriteBatch spriteBatch, Texture2D texture, Vector2 startPosition, Vector2 endPosition, Vector2 drawScale, Color beamColor)
    {
        Utils.LaserLineFraming lineFraming = new Utils.LaserLineFraming(DelegateMethods.RainbowLaserDraw);
        DelegateMethods.c_1 = beamColor;
        Utils.DrawLaser(spriteBatch, texture, startPosition, endPosition, drawScale, lineFraming);
    }

    public override void CutTiles()
    {
        DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
        Utils.TileActionAttempt cut = new Utils.TileActionAttempt(DelegateMethods.CutTiles);
        Vector2 beamStartPos = Projectile.Center;
        Vector2 beamEndPos = beamStartPos + Projectile.velocity * BeamLength;

        Utils.PlotTileLine(beamStartPos, beamEndPos, Projectile.width * Projectile.scale, cut);
    }
}