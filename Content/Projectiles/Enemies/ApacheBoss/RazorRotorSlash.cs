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

    public VertexStrip TrailStrip = new();
    public ref float Timer => ref Projectile.ai[0];

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
    }

    public override void SetDefaults()
    {
        Projectile.width = 36;
        Projectile.height = 36;
        Projectile.hostile = true;
        Projectile.friendly = false;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 120;
        Projectile.Opacity = 1f;
    }

    public override bool? CanDamage() => Projectile.Opacity > 0.25f;

    public override void AI()
    {
        if (Timer == 0f)
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Vector2 dir = Projectile.rotation.ToRotationVector2();

            Projectile.velocity = dir * (16);
        }

        Timer++;

        if (Timer >= 5)
        {
            Projectile.velocity *= 0.95f;
        }
        if (Timer >= 30)
        {
            Projectile.Kill();
        }
    }


    private Color StripColors(float progressOnStrip)
    {
        Color result = Color.Lerp(Color.White, Color.White, Utils.GetLerpValue(0f, 0.7f, progressOnStrip, true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip, false));
        result.A /= 2;
        return result * 0.5f;
    }
    private float StripWidth(float progressOnStrip)
    {
        return MathHelper.Lerp(1f, 40f, Utils.GetLerpValue(0f, 0.6f, progressOnStrip, true)) * Utils.GetLerpValue(0f, 0.1f, progressOnStrip, true);
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override bool PreDraw(ref Color lightColor)
    {

        GameShaders.Misc["LightDisc"].Apply(null);
        TrailStrip.PrepareStrip(Projectile.oldPos, Projectile.oldRot, StripColors, StripWidth, Projectile.Size * 0.5f - Main.screenPosition, Projectile.oldPos.Length, true);
        TrailStrip.DrawTrail();

        return false;
    }
}