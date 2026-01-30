using HendecamMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace HendecamMod.Content.Tiles.Walls;

public class MorbiumBrickWallPlaced : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;

        DustType = ModContent.DustType<MorbiumDust>();

        AddMapEntry(new Color(19, 15, 68));
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        num = fail ? 1 : 3;
    }
}