using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles.Enemies.ApacheBoss
{
    public class DartshipBullet : ModProjectile
    {
        public MiscShaderData Shader => GameShaders.Misc["MagicMissile"];

        public VertexStrip TrailStrip = new();

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 360;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
        }

        public override void OnSpawn(IEntitySource source)
        {
            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.rotation = Projectile.velocity.ToRotation();
            }
        }

        public override void AI()
        {
            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.rotation = Projectile.velocity.ToRotation();
            }

            Lighting.AddLight(Projectile.Center, 0.14f, 0.65f, 0.85f);

            if (!Main.dedServ && Main.rand.NextBool(6))
            {
                Dust spark = Dust.NewDustPerfect(
                    Projectile.Center + Main.rand.NextVector2Circular(2f, 2f),
                    DustID.OrangeTorch,
                    -Projectile.velocity * 0.12f,
                    0,
                    Color.DarkOrange,
                    0.55f * Projectile.scale
                );
                spark.noGravity = true;
            }
        }

        public override void OnKill(int timeLeft)
        {
            if (Main.dedServ)
                return;

            SoundEngine.PlaySound(SoundID.Item10 with { Pitch = 0.45f, Volume = 0.35f }, Projectile.Center);

            for (int i = 0; i < 6; i++)
            {
                Vector2 vel = (-Projectile.velocity.SafeNormalize(Vector2.UnitY)).RotatedByRandom(0.9f) * Main.rand.NextFloat(1.5f, 4.5f);
                Dust spark = Dust.NewDustPerfect(Projectile.Center, DustID.OrangeTorch, vel, 0, Color.OrangeRed, Main.rand.NextFloat(0.6f, 1.0f));
                spark.noGravity = true;
            }

            for (int i = 0; i < 3; i++)
            {
                Dust smoke = Dust.NewDustPerfect(Projectile.Center, DustID.Smoke, Main.rand.NextVector2Circular(1.5f, 1.5f), 120, default, Main.rand.NextFloat(0.6f, 1.0f));
                smoke.noGravity = true;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        private Color StripColors(float progressOnStrip)
        {
            float fade = 1f - Utils.GetLerpValue(0f, 0.96f, progressOnStrip, true);
            Color color = Color.Lerp(new Color(220, 252, 255), Color.OrangeRed, Utils.GetLerpValue(0f, 0.45f, progressOnStrip, true));
            color *= fade * fade;
            color.A = (byte)(color.A * 0.45f);
            return color * Projectile.Opacity;
        }

        private float StripWidth(float progressOnStrip)
        {
            return MathHelper.Lerp(20f, 2f, progressOnStrip) * Projectile.scale;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tearTex = TextureAssets.Extra[ExtrasID.SharpTears].Value;
            Texture2D orbTex = ModContent.Request<Texture2D>("HendecamMod/Assets/Textures/Misc/GlowBall").Value;

            Vector2 drawPos = Projectile.Center - Main.screenPosition;
            float rot = Projectile.velocity == Vector2.Zero ? Projectile.rotation : Projectile.velocity.ToRotation();

            Shader.Apply(null);
            TrailStrip.PrepareStrip(
                Projectile.oldPos,
                Projectile.oldRot,
                StripColors,
                StripWidth,
                Projectile.Size * 0.5f - Main.screenPosition,
                Projectile.oldPos.Length,
                true
            );
            TrailStrip.DrawTrail();
            Main.pixelShader.CurrentTechnique.Passes[0].Apply();

            Vector2 orbOrigin = orbTex.Size() * 0.5f;
            Main.EntitySpriteDraw(
                orbTex,
                drawPos,
                null,
                Color.OrangeRed * (0.65f * Projectile.Opacity),
                rot,
                orbOrigin,
                new Vector2(0.42f, 0.15f) * Projectile.scale,
                SpriteEffects.None,
                0
            );

            Vector2 tearOrigin = tearTex.Size() * 0.5f;
            Main.EntitySpriteDraw(
                tearTex,
                drawPos,
                null,
                Color.OrangeRed * Projectile.Opacity,
                rot + MathHelper.PiOver2,
                tearOrigin,
                new Vector2(0.28f, 0.90f) * Projectile.scale,
                SpriteEffects.None,
                0
            );

            Main.EntitySpriteDraw(
                tearTex,
                drawPos,
                null,
                new Color(240, 252, 255, 0) * Projectile.Opacity,
                rot + MathHelper.PiOver2,
                tearOrigin,
                new Vector2(0.14f, 0.62f) * Projectile.scale,
                SpriteEffects.None,
                0
            );

            return false;
        }
    }
}