using Terraria.Localization;
using Terraria.ObjectData;
using HendecamMod.Content.Dusts;

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
        TileObjectData.newTile.CopyFrom(TileObjectData.Style5x4);
        DustType = ModContent.DustType<AstatineDust>();
        AdjTiles = new int[] { TileID.LunarCraftingStation };
        TileObjectData.newTile.Width = 4;
        TileObjectData.newTile.Height = 4;
        TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16 };
        TileObjectData.addTile(Type);
        // Etc
        AddMapEntry(new Color(255, 64, 64), Language.GetText("Cultist's Cyclotron"));
        Main.tileLighted[Type] = true;
    }
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 1.75f;
        g = 0.67f;
        b = 0.67f;
    }
    public override void NumDust(int x, int y, bool fail, ref int num)
    {
        num = fail ? 3 : 7;
    }
}