using Terraria.Localization;
using Terraria.ObjectData;

namespace HendecamMod.Content.Tiles.Furniture;

// Simple 3x3 tile that can be placed on a wall
public class KawkawPaintingTile : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.FramesOnKillWall[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
        TileObjectData.newTile.Width = 5;
        TileObjectData.newTile.Height = 4;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.CoordinateHeights = new[]
        {
            16,
            16,
            16,
            16
        };
        TileObjectData.addTile(Type);

        AddMapEntry(new Color(175, 103, 54), Language.GetText("Custom Painting"));
        DustType = DustID.Corruption;
    }
}