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

public class DartshipPlasmaOrb : ModProjectile
{
    public override string Texture => "HendecamMod/Assets/Textures/Misc/GlowBall";

    public ref float Timer => ref Projectile.ai[0];
    public ref float TargetX => ref Projectile.ai[1];
    public ref float TargetY => ref Projectile.ai[2];

    private Vector2 startPos;

    public float TravelTicks = 50f;
    public float ExplosionTicks = 30f;

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.DrawScreenCheckFluff[Type] = 1600;
    }

    public override void SetDefaults()
    {
        Projectile.width = 42;
        Projectile.height = 42;
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
        }

        Timer++;

        if (Timer < TravelTicks)
        {
            float linearT = Timer / TravelTicks;
            Projectile.hide = linearT < 0.72f;

            float zStart = 1f;
            float zEnd = 0.18f;
            float currentZ = MathHelper.Lerp(zStart, zEnd, linearT);
            float depthScale = zEnd / currentZ;
            float perspectiveT = ((1f / currentZ) - (1f / zStart)) / ((1f / zEnd) - (1f / zStart));

            Vector2 prevPos = Projectile.Center;
            Vector2 targetPos = new Vector2(TargetX, TargetY);
            Projectile.Center = Vector2.Lerp(startPos, targetPos, perspectiveT);
            Vector2 stepVel = Projectile.Center - prevPos;
            Projectile.rotation += 0.15f;

            if (!Main.dedServ)
            {
                Color bgStreak = new Color(35, 12, 55, 160);
                Color fgStreak = Main.rand.NextFromList(
                    new Color(195, 65, 255, 220),
                    new Color(240, 140, 255, 220),
                    new Color(145, 45, 225, 200)
                );

                Color streakColor = Color.Lerp(bgStreak, fgStreak, MathF.Pow(linearT, 0.55f));
                float streakScale = MathHelper.Clamp(depthScale * Main.rand.NextFloat(0.45f, 0.75f), 0.12f, 0.75f);
                float streakOpacity = MathHelper.Lerp(0.45f, 0.95f, linearT);

                Vector2 spawnPos = Projectile.Center + Main.rand.NextVector2Circular(6f * depthScale, 6f * depthScale);
                Vector2 streakVel = (-stepVel * 0.35f) + Main.rand.NextVector2Circular(0.8f, 0.8f) * depthScale;
                if (streakVel.LengthSquared() < 0.25f)
                {
                    streakVel = (startPos - targetPos).SafeNormalize(Vector2.UnitY) * (2.5f * depthScale);
                }

                ParticleSystem.SpawnParticle(
                    ParticleID.Streak,
                    spawnPos,
                    streakVel,
                    streakColor,
                    streakOpacity,
                    streakScale
                );
            }
        }
        else if (Timer == TravelTicks)
        {
            Projectile.hide = false;
            Projectile.Center = new Vector2(TargetX, TargetY);
            Projectile.velocity = new Vector2(Main.rand.NextFloat(-0.6f, 0.6f), Main.rand.NextFloat(-1.2f, -2f));

            SoundEngine.PlaySound(SoundID.Item14 with { Volume = 0.65f, Pitch = 0.25f }, Projectile.Center);

            if (!Main.dedServ)
            {
                
            }
        }
        else
        {
            Projectile.hide = false;
            Projectile.velocity *= 0.9f;
            Projectile.rotation += 0.04f;
        }
    }

    public override bool? CanDamage()
    {
        return Timer >= TravelTicks && Timer <= TravelTicks + 10f;
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

            float orbScale = 0.85f * Projectile.scale * depthScale;

            Color bgTint = new Color(35, 12, 55, 0);
            Color fgOuter = new Color(165, 45, 255, 0);
            Color fgInner = new Color(245, 210, 255, 0);

            Color outerColor = Color.Lerp(bgTint, fgOuter, MathF.Pow(linearT, 0.6f));
            Color innerColor = Color.Lerp(bgTint, fgInner, MathF.Pow(linearT, 0.85f));

            Main.EntitySpriteDraw(orbTex, drawPosition, null, outerColor, Projectile.rotation, origin, orbScale, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(orbTex, drawPosition, null, outerColor, Projectile.rotation, origin, orbScale * 0.85f, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(orbTex, drawPosition, null, innerColor, Projectile.rotation + MathHelper.PiOver2, origin, orbScale * 0.55f, SpriteEffects.None, 0);
        }
        else
        {
            float progress = MathHelper.Clamp((Timer - TravelTicks) / ExplosionTicks, 0f, 1f);
            float scale = 2f * Projectile.scale * MathHelper.Lerp(1.1f, 0.2f, progress);

            Color outerColor = Color.Lerp(new Color(180, 50, 255, 220), new Color(18, 12, 24, 255), MathHelper.Clamp(progress * 1.45f, 0f, 1f));
            Color innerColor = Color.Lerp(new Color(255, 230, 255, 240), new Color(45, 12, 55, 255), MathHelper.Clamp(progress * 1.2f, 0f, 1f));

            float opacity = 1f;
            if (progress > 0.6f)
            {
                opacity = 1f - ((progress - 0.6f) / 0.4f);
            }

            Main.EntitySpriteDraw(orbTex, drawPosition, null, outerColor * opacity, Projectile.rotation, origin, scale, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(orbTex, drawPosition, null, innerColor * opacity, Projectile.rotation + MathHelper.PiOver2, origin, scale * 0.68f, SpriteEffects.None, 0);
        }

        return false;
    }
}