using Terraria.Localization;
using Terraria.ObjectData;

namespace HendecamMod.Content.Tiles.Furniture;

public class GoldenSigmaPlaced : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.FramesOnKillWall[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
        TileObjectData.newTile.Width = 8;
        TileObjectData.newTile.Height = 8;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.CoordinateHeights = new[]
        {
            16,
            16,
            16,
            16,
            16,
            16,
            16,
            16
        };
        TileObjectData.addTile(Type);

        AddMapEntry(new Color(225, 173, 66), Language.GetText("Golden Sigma"));
        DustType = DustID.GoldCoin;
    }
}