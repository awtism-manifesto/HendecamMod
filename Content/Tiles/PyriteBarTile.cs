using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace HendecamMod.Content.Tiles;

public class PyriteBarTile : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(191, 128, 51), Language.GetText("PyriteBar"));
        Main.tileSolid[Type] = false;
        Main.tileSolidTop[Type] = true;
        Main.tileFrameImportant[Type] = true;
        TileObjectData.newTile.StyleHorizontal = true;
        Main.tileMergeDirt[Type] = false;
        Main.tileBlockLight[Type] = true;
        Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
        HitSound = SoundID.Tink;
    }
}