using HendecamMod.Content.Dusts;

namespace HendecamMod.Content.Tiles.Walls;

public class AirBrickWallTile : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;

        AddMapEntry(new Color(191, 191, 191));
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        num = fail ? 1 : 3;
    }
}