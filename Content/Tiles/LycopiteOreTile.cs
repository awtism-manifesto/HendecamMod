using HendecamMod.Content.Dusts;
using Terraria.Localization;

namespace HendecamMod.Content.Tiles;

public class LycopiteOreTile : ModTile
{
    public override void SetStaticDefaults()
    {
        TileID.Sets.Ore[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileOreFinderPriority[Type] = 515; 
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 250;
        Main.tileMergeDirt[Type] = true;
        Main.tileMerge[TileID.Stone][Type] = true;
        Main.tileMerge[TileID.Ebonstone][Type] = true;
        Main.tileMerge[TileID.Crimstone][Type] = true;
        Main.tileMerge[TileID.Pearlstone][Type] = true;
        Main.tileMerge[TileID.Mud][Type] = true;
        Main.tileMerge[TileID.SnowBlock][Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileLighted[Type] = true;
        LocalizedText name = CreateMapEntryName();
        AddMapEntry(new Color(255, 95, 66), Language.GetText("Lycopite Ore"));

        DustType = (ModContent.DustType<LycopiteDust>());
        HitSound = SoundID.Item50;
        MineResist = 2.75f;
        MinPick = 63;
    }

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 0.95f;
        g = 0.395f;
        b = 0.05f;
    }
}