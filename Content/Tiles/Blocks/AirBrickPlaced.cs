namespace HendecamMod.Content.Tiles.Blocks;

public class AirBrickPlaced : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = false;

        HitSound = SoundID.Dig;

        AddMapEntry(new Color(60, 124, 129));
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        num = fail ? 1 : 3;
    }
}