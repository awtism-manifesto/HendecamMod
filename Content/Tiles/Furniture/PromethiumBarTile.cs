using HendecamMod.Content.Dusts;
using Terraria.Localization;
using Terraria.ObjectData;

namespace HendecamMod.Content.Tiles.Furniture;

public class PromethiumBarTile : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileShine[Type] = 1;
        Main.tileSolid[Type] = true;
        Main.tileSolidTop[Type] = true;
        Main.tileFrameImportant[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.addTile(Type);
        HitSound = SoundID.Dig;
        DustType = ModContent.DustType<MorbiumDust>();

        AddMapEntry(new Color(85, 145, 166), Language.GetText("Promethium Bar"));
    }
}