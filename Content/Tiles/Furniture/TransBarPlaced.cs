using HendecamMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace HendecamMod.Content.Tiles.Furniture;

public class TransBarPlaced : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileShine[Type] = 1000;
        Main.tileSolid[Type] = true;
        Main.tileSolidTop[Type] = true;
        Main.tileFrameImportant[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.addTile(Type);
        DustType = ModContent.DustType<TransDust>();

        AddMapEntry(new Color(120, 178, 224), Language.GetText("Trans Bar")); // localized text for "Metal Bar"
    }
}