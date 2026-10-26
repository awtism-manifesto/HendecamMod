namespace HendecamMod.Content.Tiles;

public class UraniumGlassTile : ModTile
{
    public override void SetStaticDefaults()
    {
     

        DustType = DustID.CursedTorch;
        HitSound = SoundID.Shatter;
        AddMapEntry(new Color(111, 255, 149));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = false;
        Main.tileShine2[Type] = false;
        TileID.Sets.DrawsWalls[Type] = true;
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        num = fail ? 2 : 5;
    }
}