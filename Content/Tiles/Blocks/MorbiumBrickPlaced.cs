using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using HendecamMod.Content.Dusts;

namespace HendecamMod.Content.Tiles.Blocks
{
    public class MorbiumBrickPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;

            DustType = ModContent.DustType<MorbiumDust>();
            HitSound = SoundID.Tink;

            AddMapEntry(new Color(41, 72, 92));
        }


        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}