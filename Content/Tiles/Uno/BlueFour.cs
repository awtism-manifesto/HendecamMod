using Terraria.Localization;
using Terraria.ObjectData;

namespace HendecamMod.Content.Tiles.Uno;

public class BlueFour : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
        Main.tileSolidTop[Type] = true;
        Main.tileFrameImportant[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.LavaDeath = true;
        TileObjectData.addTile(Type);
        VanillaFallbackOnModDeletion = TileID.MetalBars;

        AddMapEntry(new Color(0, 0, 200), Language.GetText("Blue Uno Card"));
    }

    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        if (!WorldGen.SolidTileAllowBottomSlope(i, j + 1))
        {
            WorldGen.KillTile(i, j);
        }

        return true;
    }
}