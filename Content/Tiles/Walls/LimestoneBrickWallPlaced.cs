using HendecamMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace HendecamMod.Content.Tiles.Walls;

public class LimestoneBrickWallPlaced : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;

        DustType = ModContent.DustType<LimestoneDust>();

        AddMapEntry(new Color(204, 190, 163));
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        num = fail ? 1 : 3;
    }
}