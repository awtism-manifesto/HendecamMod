using Terraria.Localization;
using Terraria.ObjectData;

namespace HendecamMod.Content.Tiles.Furniture;

public class CultistCyclotronPlaced : ModTile
{
    public override void SetStaticDefaults()
    {
        // Properties
        Main.tileSolidTop[Type] = false;
        Main.tileNoAttach[Type] = true;
        Main.tileFrameImportant[Type] = true;
        TileID.Sets.DisableSmartCursor[Type] = true;
        TileID.Sets.IgnoredByNpcStepUp[Type] = true; // This line makes NPCs not try to step up this tile during their movement. Only use this for furniture with solid tops.

        DustType = DustID.Stone;
        AdjTiles = new int[] { TileID.LunarCraftingStation };
        TileObjectData.newTile.Width = 4;
        TileObjectData.newTile.Height = 4;
        TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16 };
        TileObjectData.addTile(Type);
        // Etc
        AddMapEntry(new Color(64, 64, 64), Language.GetText(""));
    }

    public override void NumDust(int x, int y, bool fail, ref int num)
    {
        num = fail ? 1 : 3;
    }
}