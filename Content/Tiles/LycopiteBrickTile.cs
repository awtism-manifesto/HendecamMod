using HendecamMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Tiles
{
    public class LycopiteBrickTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileMerge[ModContent.TileType<AstatineBrickTile>()][Type] = true;
            Main.tileMerge[ModContent.TileType<PlutoniumBrickTile>()][Type] = true;
            Main.tileMerge[ModContent.TileType<UraniumBrickTile>()][Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileShine[Type] = 500;
            DustType = (ModContent.DustType<LycopiteDust>());
            HitSound = SoundID.Tink;
            AddMapEntry(new Color(255, 77, 5));
        }


        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 2 : 5;
        }
    }
}