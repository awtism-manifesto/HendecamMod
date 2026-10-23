namespace HendecamMod.Content.Tiles;

public class UraniumGlassTile : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;

        DustType = DustID.CursedTorch;
        HitSound = SoundID.Shatter;
        AddMapEntry(new Color(111, 255, 149));
        Main.tileBlockLight[Type] = false;
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        num = fail ? 2 : 5;
    }
}