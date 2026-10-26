namespace HendecamMod.Content.Tiles;

public class PlutoniumGlassTile : ModTile
{
    public override void SetStaticDefaults()
    {
      
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = false;
        Main.tileShine2[Type] = false;
        TileID.Sets.DrawsWalls[Type] = true;
        DustType = DustID.PurpleTorch;
        HitSound = SoundID.Shatter;
        AddMapEntry(new Color(215, 171, 255));
      
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        num = fail ? 2 : 5;
    }
}