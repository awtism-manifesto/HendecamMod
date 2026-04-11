using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace HendecamMod.Common.Systems.Assets;

public class HendecamTextures : ModSystem
{
    public static string GlowBallTexturePath { get; private set; } = "HendecamMod/Assets/Textures/Misc/GlowBall";
    public static string TrueMagicPixelPath { get; private set; } = "HendecamMod/Assets/Textures/Misc/TrueMagicPixel";

    public static Asset<Texture2D> GlowBallTexture { get; private set; }
    public static Asset<Texture2D> TrueMagicPixel { get; private set; }
    public override void Load()
    {
        GlowBallTexture = Request<Texture2D>(GlowBallTexturePath);
        TrueMagicPixel = Request<Texture2D>(TrueMagicPixelPath);
    }
}
