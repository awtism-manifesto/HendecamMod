namespace HendecamMod.Content.Tiles.Blocks;

public class AncientCobaltBrickPlaced : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;

        DustType = DustID.Cobalt;
        HitSound = SoundID.Tink;

        AddMapEntry(new Color(60, 124, 129));
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        num = fail ? 1 : 3;
    }
}