using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles.Items;

public class EchoStickyBomb : ModProjectile
{
    private int stuckNPC = -1; // Which NPC this is stuck to (-1 means not stuck)
    private Vector2 stuckOffset; // Offset from NPC's center
    private bool explosionTriggered = false;
    private int pulseTimer = 0;
    private const int EXPLOSION_DELAY = 30; // Time before explosion
    private const int EXPLOSION_SIZE_SCALE = 5; // How much to scale hitbox at explosion

    public override void SetDefaults()
    {
        Projectile.width = 14;
        Projectile.height = 14;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.DamageType = ModContent.GetInstance<OmniDamage>();
        Projectile.light = 0.6f;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.timeLeft = 300;
        Projectile.alpha = 135;
       
      
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (stuckNPC == -1) // Only stick if not already stuck to an NPC
        {
            stuckNPC = -2; // Use -2 to represent "stuck to tile" (since -1 means not stuck)
            stuckOffset = Projectile.Center; // Store the world position where it stuck
            Projectile.timeLeft = EXPLOSION_DELAY;
            Projectile.tileCollide = false; // Prevent further tile collision
            Projectile.velocity = Vector2.Zero; // Stop all movement

            // Set explosion damage for tile stick
            Projectile.damage = (int)(Projectile.damage * 2.25f);
        }
        return false; 
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (stuckNPC == -1) // First hit - stick to enemy
        {
            stuckNPC = target.whoAmI;
            stuckOffset = Projectile.Center - target.Center;
            Projectile.timeLeft = EXPLOSION_DELAY;
            Projectile.damage = (int)(Projectile.damage * 2.75f); // Set explosion damage

            // Reset local immunity to allow the explosion to hit again
            Projectile.localNPCHitCooldown = -1;

            // Optional: Play a sticking sound
            // SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }

    public override void AI()
    {
        if (stuckNPC == -2) // Stuck to tile
        {
            // Stay in place at the stored position
            Projectile.Center = stuckOffset;

            // Same pulsing behavior as NPC stick
            pulseTimer++;
            float pulseScale = 1f + 0.5f * (float)Math.Sin(pulseTimer * 0.67f);

            if (Projectile.timeLeft < 15)
            {
                float explosionScale = 1f + (EXPLOSION_SIZE_SCALE - 1f) * (1f - Projectile.timeLeft / 15f);
                pulseScale = MathHelper.Lerp(pulseScale, explosionScale, 0.5f);
            }

            Projectile.scale = pulseScale;

            // Update hitbox
            int newWidth = (int)(14 * pulseScale);
            int newHeight = (int)(14 * pulseScale);
            Vector2 oldCenter = Projectile.Center;
            Projectile.width = newWidth;
            Projectile.height = newHeight;
            Projectile.Center = oldCenter;

            Projectile.alpha = Math.Max(0, Projectile.alpha - 3);

            // Trigger explosion
            if (Projectile.timeLeft <= 1 && !explosionTriggered)
            {
                explosionTriggered = true;
                Projectile.Resize(100, 100);
                Projectile.Center = oldCenter;
                Projectile.localNPCHitCooldown = -1;

                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    Projectile.Center,
                    Vector2.Zero,
                    ModContent.ProjectileType<EchoStickyBombExplosion>(),
                    Projectile.damage,
                    Projectile.knockBack,
                    Projectile.owner
                );
            }
        }
        else if (stuckNPC >= 0) // Stuck to NPC
        {
            // Follow the stuck NPC
            if (stuckNPC >= 0 && stuckNPC < Main.maxNPCs) // Add bounds checking
            {
                NPC target = Main.npc[stuckNPC];
                if (target.active && !target.dontTakeDamage)
                {
                    // Update position to follow NPC
                    Projectile.Center = target.Center + stuckOffset;
                }
                else
                {
                    // NPC is dead or invalid - just stay in place
                    stuckNPC = -1;
                }
            }
            else
            {
                stuckNPC = -1; // Invalid NPC index
            }

            // Handle pulsing effect
            pulseTimer++;

            // More noticeable pulsing - sine wave between 1.0 and 1.5
            float pulseScale = 1f + 0.5f * (float)Math.Sin(pulseTimer * 0.3f);

            // If close to explosion, scale up more dramatically
            if (Projectile.timeLeft < 15)
            {
                // Rapid pulsing and growth before explosion
                float explosionScale = 1f + (EXPLOSION_SIZE_SCALE - 1f) * (1f - Projectile.timeLeft / 15f);
                pulseScale = MathHelper.Lerp(pulseScale, explosionScale, 0.5f);
            }

            Projectile.scale = pulseScale;

            // Update hitbox based on scale
            int newWidth = (int)(14 * pulseScale);
            int newHeight = (int)(14 * pulseScale);

            // Keep centered while resizing
            Vector2 oldCenter = Projectile.Center;
            Projectile.width = newWidth;
            Projectile.height = newHeight;
            Projectile.Center = oldCenter;

            // Fade in
            Projectile.alpha = Math.Max(0, Projectile.alpha - 3);

            // Trigger explosion when time is up
            if (Projectile.timeLeft <= 1 && !explosionTriggered)
            {
                explosionTriggered = true;

                // Make hitbox huge for final explosion
                Projectile.Resize(100, 100);
                Projectile.Center = oldCenter; // Keep centered

                // Allow hitting the same NPC again
                Projectile.localNPCHitCooldown = -1;

                // Deal explosion damage
                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    Projectile.Center,
                    Vector2.Zero,
                    ModContent.ProjectileType<EchoStickyBombExplosion>(),
                    Projectile.damage,
                    Projectile.knockBack,
                    Projectile.owner
                );
            }
        }
        else // Not stuck
        {
            Projectile.rotation += 0.1f; // Spin while flying

            // Fade in slightly
            Projectile.alpha = Math.Max(75, Projectile.alpha - 2);
        }
    }

    public override bool? CanHitNPC(NPC target)
    {
        // If stuck (either to NPC or tile) and explosion hasn't triggered, don't hit anything
        if ((stuckNPC >= 0 || stuckNPC == -2) && !explosionTriggered)
            return false;

        return base.CanHitNPC(target);
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = TextureAssets.Projectile[Type].Value;
        Vector2 textureCenter = new Vector2(texture.Width / 2f, texture.Height / 2f);
        Vector2 drawPosition = Projectile.Center - Main.screenPosition;

        // Add slight glow effect when stuck
        Color drawColor = Projectile.GetAlpha(lightColor);
        if (stuckNPC != -1)
        {
            // Add pulsing glow
            float glowStrength = 0.3f + 0.2f * (float)Math.Sin(pulseTimer * 0.3f);
            drawColor = Color.Lerp(drawColor, Color.OrangeRed, glowStrength);
        }

        Main.EntitySpriteDraw(
            texture,
            drawPosition,
            null,
            drawColor,
            Projectile.rotation,
            textureCenter,
            Projectile.scale,
            SpriteEffects.None,
            0
        );

        return false;
    }

    public override void OnKill(int timeLeft)
    {
        // Dust effects
        for (int j = 0; j < 10; j++)
        {
            Dust fireDust = Dust.NewDustDirect(
                Projectile.position,
                Projectile.width,
                Projectile.height,
                DustID.Electric,
                0f, 0f, 100, default, 0.5f
            );
            fireDust.noGravity = true;
            fireDust.velocity *= 7f;

            fireDust = Dust.NewDustDirect(
                Projectile.position,
                Projectile.width,
                Projectile.height,
                DustID.IceTorch,
                0f, 0f, 100, default, 2f
            );
            fireDust.velocity *= 6f;
            fireDust.noGravity = true;
        }
    }
}

// Separate explosion projectile for cleaner implementation
public class EchoStickyBombExplosion : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 100;
        Projectile.height = 100;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.DamageType = ModContent.GetInstance<OmniDamage>();
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.alpha = 255;
        Projectile.timeLeft = 2;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 10;
    }

    public override void AI()
    {
        // Ensure the explosion hits everything in one frame
        Projectile.Center = Projectile.Center; // Keep position
    }

    public override void OnKill(int timeLeft)
    {
        // Extra dust effects for explosion
        for (int j = 0; j < 20; j++)
        {
            Dust dust = Dust.NewDustDirect(
                Projectile.position,
                Projectile.width,
                Projectile.height,
                DustID.Electric,
                0f, 0f, 100, default, 1f
            );
            dust.velocity *= 3f;
            dust.noGravity = true;
        }
    }
}