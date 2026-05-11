using HendecamMod.Common.Systems.Assets;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.CameraModifiers;

namespace HendecamMod.Common.Utils;

/// <summary>
/// Contains a lot of utillities and global usings
/// </summary>
public static partial class LemonUtils
{
    /// <summary>
    /// <para>Creates a circle of dust around a given position.</para>
    /// <para><paramref name="noGrav"/> - if false, dust will be affected by gravity.</para>
    /// </summary>
    /// <param name="position"></param>
    /// <param name="amount"></param>
    /// <param name="speed"></param>
    /// <param name="dustID"></param>
    /// <param name="scale"></param>
    /// <param name="noGrav"></param>
    /// <param name="alpha"></param>
    /// <param name="newColor"></param>
    public static void DustCircle(Vector2 position, int amount, float speed, int dustID, float scale = 1, bool noGrav = true, int alpha = 0, Color color = default)
    {
        for (int i = 0; i < amount; i++)
        {
            var dust = Dust.NewDustPerfect(position, dustID, newColor: color, Scale: scale);
            dust.velocity = new Vector2(0, -speed).RotatedBy(MathHelper.ToRadians(i * (360 / amount)));
            if (noGrav)
            {
                dust.noGravity = true;
            }

        }
    }

    /// <summary>
    /// Creates a line of dust between 2 points
    /// </summary>
    /// <param name="pos1"></param>
    /// <param name="pos2"></param>
    /// <param name="type"></param>
    /// <param name="distanceBetween"></param>
    /// <param name="scale"></param>
    /// <param name="color"></param>
    public static void DustLine(Vector2 pos1, Vector2 pos2, int type, int distanceBetween = 16, float scale = 1, Color color = default)
    {
        if (color == default) color = Color.White;
        Vector2 dir = pos1.DirectionTo(pos2);
        float distance = pos1.Distance(pos2);
        int dustCount = (int)(distance / 16);
        Vector2 currentPos = pos1;
        while (dustCount > 0)
        {
            Dust.NewDustPerfect(currentPos, type, Scale: scale, newColor: color).noGravity = true;
            currentPos += dir * distanceBetween;
            dustCount--;
        }
    }

    public static void DustBurst(int count, Vector2 pos, int dustID, float randXSpeed, float randYSpeed, float minScale, float maxScale, Color color = default)
    {
        for (int i = 0; i < count; i++)
        {
            Dust.NewDustPerfect(pos, dustID, new Vector2(Main.rand.NextFloat(-randXSpeed, randXSpeed), Main.rand.NextFloat(-randYSpeed, randYSpeed)), Scale: Main.rand.NextFloat(minScale, maxScale), newColor: color).noGravity = true;
        }
    }

    /// <summary>
    /// Spawns smoke gore
    /// </summary>
    /// <param name="source"></param>
    /// <param name="pos"></param>
    /// <param name="minSpeed"></param>
    /// <param name="maxSpeed"></param>
    public static void SmokeGore(IEntitySource source, Vector2 pos, float minSpeed, float maxSpeed)
    {
        if (!Main.dedServ)
        {
            Gore.NewGoreDirect(source, pos, Vector2.UnitY.RotatedByRandom(6.28f) * Main.rand.NextFloat(minSpeed, maxSpeed), Main.rand.NextFromList(GoreID.Smoke1, GoreID.Smoke2, GoreID.Smoke3));
        }
    }

    public static Vector2 BezierCurve(Vector2 pointA, Vector2 pointB, Vector2 controlPoint, float fracComplete)
    {
        Vector2 AToControl = Vector2.Lerp(pointA, controlPoint, fracComplete);
        Vector2 ControlToB = Vector2.Lerp(controlPoint, pointB, fracComplete);
        Vector2 finalPoint = Vector2.Lerp(AToControl, ControlToB, fracComplete);

        return finalPoint;
    }

    /// <summary>
    /// Returns SpriteEffects.FlipHorizontally if spriteDirection == -1, else SpriteEffects.None.
    /// </summary>
    /// <param name="spriteDirection"></param>
    /// <returns></returns>
    public static SpriteEffects SpriteDirectionToSpriteEffects(int spriteDirection)
    {
        if (spriteDirection == -1) return SpriteEffects.FlipHorizontally;
        return SpriteEffects.None;
    }

    /// <summary>
    /// Draws a glow ball with the given params.
    /// Drawing multiple glow balls on top of each other could provide a decent looking effect.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="color"></param>
    /// <param name="opacity"></param>
    /// <param name="scale"></param>
    public static void DrawGlow(Vector2 position, Color color, float opacity, float scale)
    {
        Main.EntitySpriteDraw(
            HendecamTextures.GlowBallTexture.Value,
            position - Main.screenPosition,
            null,
            color * opacity,
            0f,
            HendecamTextures.GlowBallTexture.Size() * 0.5f,
            scale,
            SpriteEffects.None);
    }

    public static void QuickScreenShake(Vector2 pos, float strength, float vibrationCyclesPerSecond, int frames, float distanceFalloff)
    {
        PunchCameraModifier mod = new PunchCameraModifier(
            pos,
            Vector2.UnitY.RotatedByRandom(MathHelper.Pi * 2),
            strength,
            vibrationCyclesPerSecond,
            frames,
            distanceFalloff);
        Main.instance.CameraModifiers.Add(mod);
    }

    public static void BeginSpriteBatchProjectile(SpriteSortMode spriteSortMode = SpriteSortMode.Deferred, BlendState blendstate = default, Effect effect = null)
    {
        if (blendstate == default) blendstate = BlendState.AlphaBlend;
        Main.spriteBatch.Begin(spriteSortMode, blendstate, Main.DefaultSamplerState, default, Main.Rasterizer, effect, Main.GameViewMatrix.TransformationMatrix);
    }

    public static void DrawLaser(Vector2 position1, Vector2 position2, float laserWidth, Color color)
    {
        Main.EntitySpriteDraw(
            HendecamTextures.TrueMagicPixel.Value,
            position1 - Main.screenPosition,
            null,
            color,
            position1.DirectionTo(position2).ToRotation(),
            new Vector2(0, HendecamTextures.TrueMagicPixel.Height() * 0.5f),
            new Vector2((position1 - Main.screenPosition).Distance(position2 - Main.screenPosition) / 4, 1 * laserWidth),
            SpriteEffects.None);
    }
}
