using Terraria.Localization;
using Terraria.ObjectData;

namespace HendecamMod.Content.Tiles;

public class BergentruckingTile : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.FramesOnKillWall[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
        TileObjectData.addTile(Type);

        AddMapEntry(new Color(220, 255, 250), Language.GetText("Bergentrucking"));
        DustType = 7;
    }
}