namespace HendecamMod.Content.Tiles.Blocks;

public class SteelBrickPlaced : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = false;
        Main.tileBlockLight[Type] = true;

        DustType = DustID.Stone;
        HitSound = SoundID.Tink;

        AddMapEntry(new Color(28, 28, 28));
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        num = fail ? 1 : 3;
    }
}