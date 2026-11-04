using HendecamMod.Common.Systems.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles.Enemies.ApacheBoss;

public class DartshipRocket : ModProjectile
{
    public MiscShaderData Shader = new MiscShaderData(Main.VertexPixelShaderRef, "MagicMissile")
        .UseProjectionMatrix(true)
        .UseImage0("Images/Extra_191")
        .UseImage1("Images/Extra_194")
        .UseImage2("Images/Extra_190")
        .UseSaturation(-2.8f)
        .UseOpacity(2f);

    public static VertexStrip TrailStrip = new();

    private ref float Timer => ref Projectile.ai[0];
    private ref float Target => ref Projectile.ai[1];

    public bool ExpertMode => Main.expertMode;
    public bool MasterMode => Main.masterMode;
    public bool LegendaryMode => (Main.getGoodWorld || Main.zenithWorld) && Main.masterMode;

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
    }

    public override void SetDefaults()
    {
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.aiStyle = -1;
        Projectile.friendly = false;
        Projectile.hostile = true;
        Projectile.penetrate = 1;
        Projectile.timeLeft = 300;
        Projectile.alpha = 0;
        Projectile.Opacity = 1f;
        Projectile.tileCollide = true;
        Projectile.scale = 0.75f;
    }

    public override void AI()
    {
        int targetIndex = (int)Target;
        if (targetIndex < 0 || targetIndex >= Main.maxPlayers || !Main.player[targetIndex].active || Main.player[targetIndex].dead)
        {
            Target = Player.FindClosest(Projectile.Center, 3000, 3000);
            targetIndex = (int)Target;
        }

        Player player = Main.player[targetIndex];
        Timer++;

        if (Projectile.velocity != Vector2.Zero)
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        if (Timer >= 15f && Timer <= 20f && player.active && !player.dead)
        {
            float desiredSpeed = 12f;
            Vector2 targetPos = player.Center;
            if (MasterMode || LegendaryMode)
            {
                targetPos = player.Center + player.velocity * 5f;
            }

            float targetAngle = Projectile.AngleTo(targetPos);
            Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(20f)).ToRotationVector2() * desiredSpeed;
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        if (Timer < 30f)
        {
            Projectile.tileCollide = false;
        }
        else
        {
            Projectile.tileCollide = true;
            if (!Main.dedServ && Main.rand.NextBool(3))
            {
                Vector2 tailPos = Projectile.Center - Projectile.rotation.ToRotationVector2() * 14f;
                Dust d = Dust.NewDustPerfect(tailPos, DustID.SolarFlare, -Projectile.velocity * 0.15f, 100, default, 1.3f);
                d.noGravity = true;
            }
        }
    }

    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
        int targetIndex = (int)MathHelper.Clamp(Target, 0, Main.maxPlayers - 1);
        Player player = Main.player[targetIndex];
        width = 10;
        height = 10;
        fallThrough = player.active && (player.Bottom.Y - 20f >= Projectile.Center.Y);
        return true;
    }

    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

        if (Main.dedServ) return;
        Color randColor = Main.rand.NextFromList(Color.Orange, Color.OrangeRed, Color.SlateGray, Color.Gray);

        for (int i = 0; i < 12; i++)
        {
            randColor = Main.rand.NextFromList(Color.Orange, Color.OrangeRed, Color.SlateGray, Color.Gray);
            ParticleSystem.SpawnParticle(
                ParticleID.Streak,
                Projectile.Center,
                Vector2.UnitY.RotatedByRandom(MathHelper.Pi) * Main.rand.NextFloat(2f, 4.5f),
                randColor,
                1f,
                Main.rand.NextFloat(0.85f, 1.3f)
            );
        }
        for (int i = 0; i < 18; i++)
        {
            randColor = Main.rand.NextFromList(Color.Orange, Color.OrangeRed, Color.SlateGray, Color.Gray);
            ParticleSystem.SpawnParticle(
                ParticleID.Gas,
                Projectile.Center,
                Vector2.UnitY.RotatedByRandom(MathHelper.Pi) * Main.rand.NextFloat(1f, 2.8f),
                randColor,
                0.95f,
                Main.rand.NextFloat(0.6f, 0.95f),
                data0: 0.03f
            );
        }
    }

    private Color StripColors(float progressOnStrip)
    {
        Color color = Color.Lerp(new Color(255, 120, 0), new Color(15, 0, 0, 255), MathHelper.Clamp(progressOnStrip * 1.5f, 0f, 1f));
        color.A = (byte)(color.A * 0.6f);
        return color * (1f - progressOnStrip) * Projectile.Opacity;
    }

    private float StripWidth(float progressOnStrip)
    {
        return 40f * (1f - progressOnStrip * 0.5f) * Projectile.scale;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Main.spriteBatch.End();

        Shader.Apply(null);
        Vector2 trailOffset = Projectile.Size * 0.5f - Projectile.rotation.ToRotationVector2() * 10f - Main.screenPosition;
        TrailStrip.PrepareStrip(Projectile.oldPos, Projectile.oldRot, StripColors, StripWidth, trailOffset, Projectile.oldPos.Length, true);
        TrailStrip.DrawTrail();
        Main.pixelShader.CurrentTechnique.Passes[0].Apply();

        Main.spriteBatch.Begin(
            SpriteSortMode.Deferred,
            BlendState.AlphaBlend,
            Main.DefaultSamplerState,
            DepthStencilState.None,
            Main.Rasterizer,
            null,
            Main.GameViewMatrix.TransformationMatrix
        );

        Texture2D rocketTex = TextureAssets.Projectile[Type].Value;
        Vector2 drawPos = Projectile.Center - Main.screenPosition;
        Vector2 origin = rocketTex.Size() / 2f;

        Main.EntitySpriteDraw(
            rocketTex,
            drawPos,
            null,
            lightColor * Projectile.Opacity,
            Projectile.rotation + MathHelper.PiOver2,
            origin,
            Projectile.scale,
            SpriteEffects.None,
            0
        );

        return false;
    }
}