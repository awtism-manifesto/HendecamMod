namespace HendecamMod.Content.Tiles.Blocks;

public class GalvanizedSquareSteelPlaced : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = false;
        Main.tileBlockLight[Type] = true;

        DustType = DustID.Stone;
        HitSound = SoundID.Item53;

        AddMapEntry(new Color(28, 28, 28));
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        num = fail ? 1 : 3;
    }
}