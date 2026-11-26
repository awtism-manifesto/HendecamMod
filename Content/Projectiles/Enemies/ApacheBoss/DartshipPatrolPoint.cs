using HendecamMod.Content.NPCs.Bosses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles.Enemies.ApacheBoss;

public class DartshipPatrolPoint : ModProjectile
{
    public static readonly Asset<Texture2D> glowBallTexture = ModContent.Request<Texture2D>("HendecamMod/Assets/Textures/Misc/GlowBall");

    public const float DragDelay = 14f;
    public const float DragDuration = 32f;

    public ref float BossWhoAmI => ref Projectile.ai[0];
    public ref float TargetX => ref Projectile.ai[1];
    public ref float TargetY => ref Projectile.ai[2];
    public ref float Timer => ref Projectile.localAI[0];
    public ref float FadeTimer => ref Projectile.localAI[1];

    private bool initialized;
    private Vector2 startPos;

    private bool IsDraggedPoint => TargetX != 0f || TargetY != 0f;

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.DrawScreenCheckFluff[Type] = 2000;
    }

    public override void SetDefaults()
    {
        Projectile.width = 32;
        Projectile.height = 32;
        Projectile.hostile = false;
        Projectile.friendly = false;
        Projectile.damage = 0;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.timeLeft = 600;
    }

    public override bool? CanDamage() => false;

    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(initialized);
        writer.WriteVector2(startPos);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        initialized = reader.ReadBoolean();
        startPos = reader.ReadVector2();
    }

    public override void AI()
    {
        if (!initialized)
        {
            startPos = Projectile.Center;
            initialized = true;
        }

        Timer++;
        Projectile.velocity = Vector2.Zero;

        if (IsDraggedPoint)
        {
            Vector2 endPos = new Vector2(TargetX, TargetY);
            float dragT = Utils.GetLerpValue(DragDelay, DragDelay + DragDuration, Timer, true);
            float eased = MathHelper.SmoothStep(0f, 1f, dragT);
            Projectile.Center = Vector2.Lerp(startPos, endPos, eased);
        }

        int bossIdx = (int)BossWhoAmI;
        bool validBoss = bossIdx >= 0
            && bossIdx < Main.maxNPCs
            && Main.npc[bossIdx].active
            && Main.npc[bossIdx].type == ModContent.NPCType<ApacheElfShip>()
            && Main.npc[bossIdx].ModNPC is ApacheElfShip boss
            && boss.AIState == ApacheElfShip.ActionState.ChinookSupply;

        if (!validBoss || BossWhoAmI <= -2f)
        {
            FadeTimer++;
            if (FadeTimer >= 18f)
            {
                Projectile.Kill();
                return;
            }
        }
        else if (Projectile.timeLeft < 120)
        {
            Projectile.timeLeft = 120;
        }

        Lighting.AddLight(Projectile.Center, 0.75f, 0.62f, 0.12f);
    }

    public override bool PreDraw(ref Color lightColor)
    {
        if (!initialized)
        {
            startPos = Projectile.Center;
            initialized = true;
        }

        Texture2D pointTex = TextureAssets.Projectile[Type].Value;
        Texture2D glowTex = glowBallTexture.Value;

        float fadeIn = Utils.GetLerpValue(0f, 12f, Timer, true);
        float fadeOut = Utils.GetLerpValue(18f, 0f, FadeTimer, true);
        float opacity = fadeIn * fadeOut;

        if (opacity <= 0.01f)
            return false;

        Vector2 glowOrigin = glowTex.Size() * 0.5f;
        float glowNorm = 1f / MathF.Max(1f, glowTex.Width);

        if (IsDraggedPoint)
        {
            Vector2 endPos = new Vector2(TargetX, TargetY);
            Vector2 fullDiff = endPos - startPos;
            float fullDist = fullDiff.Length();
            float currentDist = Vector2.Distance(startPos, Projectile.Center);

            if (fullDist > 96f && currentDist > 54f)
            {
                Vector2 dir = fullDiff / fullDist;
                float pad = 46f;
                float usableSpan = fullDist - pad * 2f;
                int dotCount = Math.Max(3, (int)MathF.Round(usableSpan / 62f));

                for (int i = 0; i < dotCount; i++)
                {
                    float frac = dotCount == 1 ? 0.5f : i / (float)(dotCount - 1);
                    float dotDist = MathHelper.Lerp(pad, fullDist - pad, frac);
                    float dotReveal = Utils.GetLerpValue(dotDist + 8f, dotDist + 40f, currentDist, true);

                    if (dotReveal <= 0.01f)
                        continue;

                    Vector2 dotDrawPos = startPos + dir * dotDist - Main.screenPosition;
                    float wave = 0.88f + 0.16f * MathF.Sin(Timer * 0.14f - frac * 6.5f);
                    float dotScale = MathHelper.Lerp(0.4f, 1f, dotReveal) * wave;

                    Main.EntitySpriteDraw(
                        glowTex,
                        dotDrawPos,
                        null,
                        new Color(255, 190, 20, 0) * (0.85f * opacity * dotReveal),
                        0f,
                        glowOrigin,
                        42f * glowNorm * dotScale,
                        SpriteEffects.None,
                        0
                    );

                    Main.EntitySpriteDraw(
                        glowTex,
                        dotDrawPos,
                        null,
                        new Color(255, 255, 215, 0) * (0.95f * opacity * dotReveal),
                        0f,
                        glowOrigin,
                        18f * glowNorm * dotScale,
                        SpriteEffects.None,
                        0
                    );
                }
            }
        }

        Vector2 drawPos = Projectile.Center - Main.screenPosition;
        float pulse = 1f + 0.05f * MathF.Sin(Timer * 0.1f);
        float settleBoost = 0f;
        if (IsDraggedPoint && Timer >= DragDelay + DragDuration)
        {
            float settleT = Utils.GetLerpValue(DragDelay + DragDuration, DragDelay + DragDuration + 10f, Timer, true);
            settleBoost = MathF.Sin(settleT * MathHelper.Pi) * 0.16f;
        }

        float popScale = (MathHelper.Lerp(0.55f, 1f, MathF.Sin(fadeIn * MathHelper.PiOver2)) * pulse + settleBoost) * Projectile.scale;
        float pointSize = MathF.Max(pointTex.Width, pointTex.Height) * popScale;

        Main.EntitySpriteDraw(
            glowTex,
            drawPos,
            null,
            new Color(255, 195, 25, 0) * (0.65f * opacity),
            0f,
            glowOrigin,
            pointSize * 2.5f * glowNorm,
            SpriteEffects.None,
            0
        );

        Main.EntitySpriteDraw(
            glowTex,
            drawPos,
            null,
            new Color(255, 235, 115, 0) * (0.8f * opacity),
            0f,
            glowOrigin,
            pointSize * 1.35f * glowNorm,
            SpriteEffects.None,
            0
        );

        Main.EntitySpriteDraw(
            pointTex,
            drawPos,
            null,
            Color.White * opacity,
            0f,
            pointTex.Size() * 0.5f,
            popScale,
            SpriteEffects.None,
            0
        );

        return false;
    }
}