using HendecamMod.Content.DamageClasses;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;

namespace HendecamMod.Content.Projectiles;

public class MintalArrowProjectile : ModProjectile
{
    
    

    private bool hasManaForHoming = true;

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12; // The length of old position to be recorded
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
    }

    public override void SetDefaults()
    {
        Projectile.width = 6;
        Projectile.height = 6;
        Projectile.friendly = true;
        Projectile.penetrate = 2; 
        Projectile.DamageType = GetInstance<RangedMagicDamage>();
        Projectile.light = 0.25f;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.extraUpdates = 1;
        Projectile.timeLeft = 240;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 20;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
    }



    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = TextureAssets.Projectile[Type].Value;

      
        Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
        for (int k = 0; k < Projectile.oldPos.Length; k++)
        {
            Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
            Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None);
        }

        return true;
    }

    public bool isHoming;

    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        isHoming = false;

        hasManaForHoming = player.statMana > 0;
        bool thisArrowControlled = false;

        for (int i = 0; i < 2; i++)
        {
            if (!player.controlUseItem && hasManaForHoming)
            {
                Vector2 targetPos = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
                float length = Projectile.velocity.Length();
                float targetAngle = Projectile.AngleTo(targetPos);
                Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(5f)).ToRotationVector2() * length;
                Projectile.velocity *= 1.01f;
                isHoming = true;
                thisArrowControlled = true;
            }
            else if (Projectile.timeLeft < 175)
            {
                Projectile.velocity *= 0.993f;
            }
        }

       
        

        if (thisArrowControlled)
        {
            ManaDrainSystem.AnyControlledArrow = true;
        }

    }

    
}
public class ManaDrainSystem : ModSystem
{
    public static bool AnyControlledArrow { get; set; }
    private int manaDrainTimer;
    private const int MANA_DRAIN_INTERVAL = 10; // 5 frames = about 0.083 seconds at 60 fps
    private const int MANA_DRAIN_AMOUNT = 3;

    // Track if we've reset the flag for this frame yet
    private bool flagResetThisFrame = false;

    public override void PreUpdateProjectiles()
    {
        // Reset the flag BEFORE projectiles update
        AnyControlledArrow = false;
        flagResetThisFrame = true;
    }

    public override void PostUpdateProjectiles()
    {
        // After all projectiles have updated, we can use the flag
        // But we need to process mana drain BEFORE PostUpdatePlayers
    }

    public override void PostUpdatePlayers()
    {
        // Only run on the client player
        if (Main.netMode == NetmodeID.Server)
            return;

        Player player = Main.LocalPlayer;

        // Check if any controlled arrow exists
        if (AnyControlledArrow)
        {
            manaDrainTimer++;

            // Debug: Check if timer is working
            // Main.NewText($"Timer: {manaDrainTimer}, AnyControlledArrow: {AnyControlledArrow}");

            if (manaDrainTimer >= MANA_DRAIN_INTERVAL)
            {
                // Try to consume mana
                if (player.CheckMana(MANA_DRAIN_AMOUNT, true))
                {
                    player.manaRegenDelay = (int)player.maxRegenDelay;
                }

                // Reset timer after attempting consumption
                manaDrainTimer = 0;
            }
        }
        else
        {
            // Reset timer when no arrows are controlled
            manaDrainTimer = 0;
        }

        // Reset the frame flag for next cycle
        flagResetThisFrame = false;
    }
}

