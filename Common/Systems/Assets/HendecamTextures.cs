using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace HendecamMod.Common.Systems.Assets;

public class HendecamTextures : ModSystem
{
    public static string NoiseTexturePath { get; private set; } = "HendecamMod/Assets/Textures/Noise/NoiseTexture";
    public static string TransparentNoiseTexturePath { get; private set; } = "HendecamMod/Assets/Textures/Noise/TransparentNoise";
    public static string GlowBallTexturePath { get; private set; } = "HendecamMod/Assets/Textures/Misc/GlowBall";
    public static string TrueMagicPixelPath { get; private set; } = "HendecamMod/Assets/Textures/Misc/TrueMagicPixel";
    public static string Empty100TexPath { get; private set; } = "HendecamMod/Assets/Textures/Misc/Empty100Tex";
    public static string FadingIndicatorPath { get; private set; } = "HendecamMod/Assets/Textures/Misc/FadingIndicator";

    public static Asset<Texture2D> NoiseTexture { get; private set; }
    public static Asset<Texture2D> TransparentNoiseTexture { get; private set; }
    public static Asset<Texture2D> GlowBallTexture { get; private set; }
    public static Asset<Texture2D> TrueMagicPixel { get; private set; }
    public static Asset<Texture2D> Empty100Tex { get; private set; }
    public static Asset<Texture2D> FadingIndicator { get; private set; }
    public override void Load()
    {
        NoiseTexture = Request<Texture2D>(NoiseTexturePath);
        TransparentNoiseTexture = Request<Texture2D>(TransparentNoiseTexturePath);
        GlowBallTexture = Request<Texture2D>(GlowBallTexturePath);
        TrueMagicPixel = Request<Texture2D>(TrueMagicPixelPath);
        Empty100Tex = Request<Texture2D>(Empty100TexPath);
        FadingIndicator = Request<Texture2D>(FadingIndicatorPath);
    }
}
