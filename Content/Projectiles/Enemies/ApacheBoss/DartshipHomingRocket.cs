using HendecamMod.Common.Systems.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles.Enemies.ApacheBoss;

public class DartshipHomingRocket : ModProjectile
{
    public override string Texture => "HendecamMod/Assets/Textures/Misc/GlowBall";

    public ref float TargetPlayerIndex => ref Projectile.ai[0];
    public ref float TargetX => ref Projectile.ai[1];
    public ref float TargetY => ref Projectile.ai[2];
    public ref float Timer => ref Projectile.localAI[0];
    public ref float ArcSide => ref Projectile.localAI[1];

    private Vector2 startPos;

    public float TravelTicks = 90f;
    public float ExplosionTicks = 50f;

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.DrawScreenCheckFluff[Type] = 1600;
    }

    public override void SetDefaults()
    {
        Projectile.width = 76;
        Projectile.height = 76;
        Projectile.aiStyle = -1;
        Projectile.friendly = false;
        Projectile.hostile = true;
        Projectile.penetrate = -1;
        Projectile.timeLeft = (int)(TravelTicks + ExplosionTicks);
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.hide = true;
    }

    public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
    {
        if (Projectile.hide)
        {
            behindNPCsAndTiles.Add(index);
        }
    }

    public override void AI()
    {
        if (Timer == 0f)
        {
            startPos = Projectile.Center;
            Projectile.rotation = Main.rand.NextFloat(MathHelper.TwoPi);

            if (ArcSide == 0f)
            {
                ArcSide = (startPos.X >= TargetX) ? 1f : -1f;
            }
        }

        Timer++;

        if (Timer < TravelTicks)
        {
            float linearT = Timer / TravelTicks;
            Projectile.hide = linearT < 0.72f;

            int pIdx = (int)TargetPlayerIndex;
            if (pIdx >= 0 && pIdx < Main.maxPlayers && Main.player[pIdx].active && !Main.player[pIdx].dead)
            {
                Player target = Main.player[pIdx];
                float homingStrength = MathHelper.Lerp(0.085f, 0.015f, linearT);
                TargetX = MathHelper.Lerp(TargetX, target.Center.X, homingStrength);
                TargetY = MathHelper.Lerp(TargetY, target.Center.Y, homingStrength);
            }

            float zStart = 1f;
            float zEnd = 0.18f;
            float currentZ = MathHelper.Lerp(zStart, zEnd, linearT);
            float depthScale = zEnd / currentZ;
            float perspectiveT = ((1f / currentZ) - (1f / zStart)) / ((1f / zEnd) - (1f / zStart));

            Vector2 prevPos = Projectile.Center;
            Vector2 targetPos = new Vector2(TargetX, TargetY);

            // 3D parabolic arc in world-depth space: bows wide outward laterally and high upward in the mid-Z plane,
            // then curves sharply inward onto targetPos as perspectiveT -> 1f.
            float arcWeight = 4f * perspectiveT * (1f - perspectiveT);
            float depthBowBoost = MathHelper.Lerp(0.65f, 1.35f, linearT);
            Vector2 arcApexOffset = new Vector2(ArcSide * 240f, -195f) * depthBowBoost;

            Projectile.Center = Vector2.Lerp(startPos, targetPos, perspectiveT) + arcApexOffset * arcWeight;
            Vector2 stepVel = Projectile.Center - prevPos;
            Projectile.rotation += 0.14f;

            if (!Main.dedServ)
            {
                Color bgSmoke = new Color(38, 22, 26, 150);
                Color fgFlame = Main.rand.NextFromList(
                    new Color(255, 140, 25, 215),
                    new Color(255, 85, 15, 215),
                    new Color(130, 115, 110, 190)
                );

                float colorDepthT = MathF.Pow(linearT, 0.6f);
                Color gasColor = Color.Lerp(bgSmoke, fgFlame, colorDepthT);
                float gasScale = MathHelper.Clamp(depthScale * Main.rand.NextFloat(0.75f, 1.1f), 0.16f, 1.15f);
                float gasOpacity = MathHelper.Lerp(0.45f, 0.92f, linearT);

                Vector2 gasSpawnPos = Projectile.Center + Main.rand.NextVector2Circular(9f * depthScale, 9f * depthScale);
                Vector2 gasVel = (-stepVel * 0.25f) + Main.rand.NextVector2Circular(1.3f, 1.3f) * depthScale;

                ParticleSystem.SpawnParticle(
                    ParticleID.Gas,
                    gasSpawnPos,
                    gasVel,
                    gasColor,
                    gasOpacity,
                    gasScale,
                    data0: 0.045f
                );
            }
        }
        else if (Timer == TravelTicks)
        {
            Projectile.hide = false;
            Projectile.Center = new Vector2(TargetX, TargetY);
            Projectile.velocity = new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-2f, -3f));

            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            if (!Main.dedServ)
            {
                for (int i = 0; i < 16; i++)
                {
                    Color randColor = Main.rand.NextFromList(Color.Orange, Color.OrangeRed, Color.Gold, Color.SlateGray);
                    ParticleSystem.SpawnParticle(
                        ParticleID.Streak,
                        Projectile.Center + Main.rand.NextVector2Circular(12f, 12f),
                        Vector2.UnitY.RotatedByRandom(MathHelper.TwoPi) * Main.rand.NextFloat(2.5f, 6f),
                        randColor,
                        1f,
                        Main.rand.NextFloat(0.8f, 1.25f)
                    );

                    ParticleSystem.SpawnParticle(
                        ParticleID.Gas,
                        Projectile.Center + Main.rand.NextVector2Circular(16f, 16f),
                        Vector2.UnitY.RotatedByRandom(MathHelper.TwoPi) * Main.rand.NextFloat(1f, 3f),
                        randColor,
                        0.92f,
                        Main.rand.NextFloat(0.65f, 1.05f),
                        data0: 0.038f
                    );
                }
            }
        }
        else
        {
            Projectile.hide = false;
            Projectile.velocity *= 0.92f;
            Projectile.rotation += 0.04f;
        }
    }

    public override bool? CanDamage()
    {
        return Timer >= TravelTicks && Timer <= TravelTicks + 14f;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D orbTex = ModContent.Request<Texture2D>(Texture).Value;
        Vector2 origin = orbTex.Size() / 2f;
        Vector2 drawPosition = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);

        if (Timer < TravelTicks)
        {
            float linearT = MathHelper.Clamp(Timer / TravelTicks, 0f, 1f);
            float zStart = 1f;
            float zEnd = 0.18f;
            float currentZ = MathHelper.Lerp(zStart, zEnd, linearT);
            float depthScale = zEnd / currentZ;

            float orbScale = 1.05f * Projectile.scale * depthScale;

            Color bgTint = new Color(45, 18, 12, 0);
            Color fgOuter = new Color(255, 105, 15, 0);
            Color fgInner = new Color(255, 235, 165, 0);

            Color outerColor = Color.Lerp(bgTint, fgOuter, MathF.Pow(linearT, 0.5f));
            Color innerColor = Color.Lerp(bgTint, fgInner, MathF.Pow(linearT, 0.65f));

            Main.EntitySpriteDraw(orbTex, drawPosition, null, outerColor, Projectile.rotation, origin, orbScale, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(orbTex, drawPosition, null, outerColor, Projectile.rotation, origin, orbScale * 0.85f, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(orbTex, drawPosition, null, innerColor, Projectile.rotation + MathHelper.PiOver2, origin, orbScale * 0.55f, SpriteEffects.None, 0);
        }
        else
        {
            float progress = MathHelper.Clamp((Timer - TravelTicks) / ExplosionTicks, 0f, 1f);
            float scale = 2.8f * Projectile.scale * MathHelper.Lerp(1.15f, 0.25f, progress);

            Color outerColor = Color.Lerp(new Color(255, 110, 20, 220), new Color(28, 18, 16, 255), MathHelper.Clamp(progress * 1.45f, 0f, 1f));
            Color innerColor = Color.Lerp(new Color(255, 240, 180, 240), new Color(65, 25, 12, 255), MathHelper.Clamp(progress * 1.2f, 0f, 1f));

            float opacity = 1f;
            if (progress > 0.6f)
            {
                opacity = 1f - ((progress - 0.6f) / 0.4f);
            }

            Main.EntitySpriteDraw(orbTex, drawPosition, null, outerColor * opacity, Projectile.rotation, origin, scale, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(orbTex, drawPosition, null, innerColor * opacity, Projectile.rotation + MathHelper.PiOver2, origin, scale * 0.7f, SpriteEffects.None, 0);
        }

        return false;
    }
}