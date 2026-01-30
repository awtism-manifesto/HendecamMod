using HendecamMod.Content.Dusts;
using Terraria.Localization;

namespace HendecamMod.Content.Tiles.Blocks;

public class MintalOrePlaced : ModTile
{
    public override void SetStaticDefaults()
    {
        TileID.Sets.Ore[Type] = true;
        Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
        Main.tileOreFinderPriority[Type] = 425; // Metal Detector value, see https://terraria.wiki.gg/wiki/Metal_Detector
        Main.tileShine2[Type] = true; // Modifies the draw color slightly.
        Main.tileShine[Type] = 975; // How often tiny dust appear off this tile. Larger is less frequently
        Main.tileMergeDirt[Type] = true;
        Main.tileMerge[TileID.Stone][Type] = true;
        Main.tileMerge[TileID.Ebonstone][Type] = true;
        Main.tileMerge[TileID.Crimstone][Type] = true;
        Main.tileMerge[TileID.Pearlstone][Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;

        LocalizedText name = CreateMapEntryName();
        AddMapEntry(new Color(86, 226, 192), Language.GetText("Mintal Ore"));

        DustType = ModContent.DustType<MintalDust>();
        HitSound = SoundID.Tink;
        MineResist = 2f;
        MinPick = 115;
    }
}