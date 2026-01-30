using HendecamMod.Content.Dusts;
using Terraria.Localization;

namespace HendecamMod.Content.Tiles.Blocks;

public class MorbiumOrePlaced : ModTile
{
    public override void SetStaticDefaults()
    {
        TileID.Sets.Ore[Type] = true;
        Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
        Main.tileOreFinderPriority[Type] = 515; // Metal Detector value, see https://terraria.wiki.gg/wiki/Metal_Detector
        Main.tileShine2[Type] = true; // Modifies the draw color slightly.
        Main.tileShine[Type] = 250; // How often tiny dust appear off this tile. Larger is less frequently
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
        AddMapEntry(new Color(89, 255, 167), Language.GetText("Morbium Ore"));

        DustType = (ModContent.DustType<MorbiumDust>());
        HitSound = SoundID.Tink;
        MineResist = 2.75f;
        MinPick = 63;
    }

    // ExampleOreSystem contains code related to spawning ExampleOre. It contains both spawning ore during world generation, seen in ModifyWorldGenTasks, and spawning ore after defeating a boss, seen in BlessWorldWithExampleOre and MinionBossBody.OnKill.
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 0.05f;
        g = 0.75f;
        b = 0.5f;
    }
}