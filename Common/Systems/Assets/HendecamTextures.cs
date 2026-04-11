using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace HendecamMod.Common.Systems.Assets;

public class HendecamTextures : ModSystem
{
    public static string GlowBallTexturePath { get; private set; } = "Hendecam/Assets/Textures/Misc/GlowBall";

    public static Asset<Texture2D> GlowBallTexture { get; private set; }
    public override void Load()
    {
        GlowBallTexture = Request<Texture2D>(GlowBallTexturePath);
    }
}
