using HendecamMod.Content.Dusts;

namespace HendecamMod.Content.Tiles.Blocks;

public class SmoothLimestonePlaced : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = false;
        Main.tileBlockLight[Type] = true;

        DustType = ModContent.DustType<LimestoneDust>();
        HitSound = SoundID.Tink;

        AddMapEntry(new Color(204, 190, 163));
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        num = fail ? 1 : 3;
    }
}