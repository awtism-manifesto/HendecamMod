using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Tiles.Blocks;

public class BlackBrickPlaced : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileMerge[TileID.Stone][Type] = true;
        Main.tileMerge[TileID.ClayBlock][Type] = true;
        Main.tileMerge[TileID.GrayBrick][Type] = true;
        Main.tileMerge[TileID.RedBrick][Type] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;

        DustType = DustID.Torch;
        HitSound = SoundID.Tink;

        AddMapEntry(new Color(13, 13, 13));
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        num = fail ? 1 : 3;
    }
}