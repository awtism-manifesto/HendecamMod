using HendecamMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace HendecamMod.Content.Biomes.Oasis;

public class OasisWaterStyle : ModWaterStyle
{
    private Asset<Texture2D> rainTexture;

    public override void Load()
    {
        rainTexture = Mod.Assets.Request<Texture2D>("Content/Biomes/Oasis/OasisRain");
    }

    public override int ChooseWaterfallStyle()
    {
        return ModContent.GetInstance<OasisWaterfallStyle>().Slot;
    }

    public override int GetSplashDust()
    {
        return ModContent.DustType<LycopiteDust>();
    }

    public override int GetDropletGore()
    {
        return ModContent.GoreType<OasisDroplet>();
    }

    public override void LightColorMultiplier(ref float r, ref float g, ref float b)
    {
        r = 1f;
        g = 1f;
        b = 1f;
    }

    public override Color BiomeHairColor()
    {
        return Color.White;
    }

    public override byte GetRainVariant()
    {
        return (byte)Main.rand.Next(3);
    }

    public override Asset<Texture2D> GetRainTexture() => rainTexture;
}