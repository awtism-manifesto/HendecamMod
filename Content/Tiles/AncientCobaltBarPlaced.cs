using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace HendecamMod.Content.Tiles;

public class AncientCobaltBarPlaced : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileShine[Type] = 690;
        Main.tileSolid[Type] = true;
        Main.tileSolidTop[Type] = true;
        Main.tileFrameImportant[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.addTile(Type);

        AddMapEntry(new Color(160, 225, 255), Language.GetText("Ancient Cobalt Bar")); // localized text for "Metal Bar"
    }
}