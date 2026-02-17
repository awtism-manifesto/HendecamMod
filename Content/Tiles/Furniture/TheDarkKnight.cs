using Terraria.Localization;
using Terraria.ObjectData;

namespace HendecamMod.Content.Tiles.Furniture;

public class TheDarkKnight : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.FramesOnKillWall[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
        TileObjectData.newTile.Width = 5;
        TileObjectData.newTile.Height = 5;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.CoordinateHeights = new[]
        {
            16,
            16,
            16,
            16,
            16
        };
        TileObjectData.addTile(Type);

        AddMapEntry(new Color(5, 3, 6), Language.GetText("Rory Nite"));
        DustType = DustID.ArgonMoss;
    }
}