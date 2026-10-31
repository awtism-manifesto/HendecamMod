using HendecamMod.Content.NPCs.Bosses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles.Enemies.ApacheBoss;

public class RazorRotorSlash : ModProjectile
{
    public override string Texture => "HendecamMod/Content/NPCs/Bosses/Dartship_Rotor";

    public static VertexStrip strip = new();
    public MiscShaderData SlashShader = new MiscShaderData(Main.VertexPixelShaderRef, "MagicMissile")
        .UseProjectionMatrix(true)
        .UseImage0("Images/Extra_" + 201)
        .UseImage1("Images/Extra_" + 193)
        .UseImage2("Images/Extra_" + 252)
        .UseSaturation(-2.8f)
        .UseOpacity(2f);

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 14;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
    }

    public override void SetDefaults()
    {
        Projectile.width = 48;
        Projectile.height = 20;
        Projectile.hostile = true;
        Projectile.friendly = false;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 180;
    }

    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation();
        Projectile.localAI[0] = MathHelper.WrapAngle(Projectile.localAI[0] + 0.45f);
    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        float point = 0f;
        Vector2 dir = Projectile.velocity.SafeNormalize(Vector2.UnitX);
        return Collision.CheckAABBvLineCollision(
            targetHitbox.TopLeft(),
            targetHitbox.Size(),
            Projectile.Center - dir * 44f,
            Projectile.Center + dir * 44f,
            20f * Projectile.scale,
            ref point
        );
    }

    private Color StripColors(float progressOnStrip)
    {
        Color c = new Color(160, 185, 205, 220);
        c.A /= 2;
        return c * (1f - progressOnStrip);
    }

    private float StripWidth(float progressOnStrip)
    {
        return 36f * (1f - progressOnStrip * 0.5f) * Projectile.scale;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        SlashShader.Apply(null);
        strip.PrepareStrip(Projectile.oldPos, Projectile.oldRot, StripColors, StripWidth, Projectile.Size * 0.5f - Main.screenPosition, Projectile.oldPos.Length, true);
        strip.DrawTrail();

        Main.spriteBatch.End();
        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

        Texture2D rotorTex = ApacheElfShip.rotorTexture.Value;
        Vector2 origin = new Vector2(3f, rotorTex.Height - 3f);

        for (int i = 0; i < 4; i++)
        {
            float angle = Projectile.localAI[0] + i * MathHelper.PiOver2;
            Vector2 localVec = new Vector2(MathF.Cos(angle) * 44f, MathF.Sin(angle) * 10f);
            Vector2 worldVec = localVec.RotatedBy(Projectile.rotation);
            float bladeRot = worldVec.ToRotation() + MathHelper.PiOver4;
            float lenRatio = localVec.Length() / 44f;
            Vector2 scale = new Vector2(lenRatio, MathHelper.Lerp(0.35f, 1f, lenRatio)) * Projectile.scale;

            Main.EntitySpriteDraw(
                rotorTex,
                Projectile.Center - Main.screenPosition,
                null,
                Color.White,
                bladeRot,
                origin,
                scale,
                SpriteEffects.None,
                0
            );
        }

        return false;
    }
}