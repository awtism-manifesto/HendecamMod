using HendecamMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Tiles.Blocks;

public class TransBrickPlaced : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileMerge[TileID.Stone][Type] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        Main.tileMerge[TileID.GrayBrick][Type] = true;
        Main.tileMerge[TileID.RedBrick][Type] = true;
        Main.tileMerge[TileID.LivingFire][Type] = true;
        Main.tileMerge[TileID.Ebonstone][Type] = true;
        Main.tileMerge[TileID.Crimstone][Type] = true;
        Main.tileMerge[TileID.Pearlstone][Type] = true;
        Main.tileBlockLight[Type] = true;

        DustType = ModContent.DustType<TransDust>();
        HitSound = SoundID.Tink;

        AddMapEntry(new Color(255, 255, 255));
    }


    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        num = fail ? 1 : 3;
    }
}