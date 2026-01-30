using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Terraria.GameContent.Generation;

namespace HendecamMod.Content.Tiles.Blocks;

public class AzuriteOrePlaced : ModTile
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
        Main.tileMerge[TileID.Mud][Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;

        LocalizedText name = CreateMapEntryName();
        AddMapEntry(new Color(25, 50, 255), Language.GetText("Azurite"));

        DustType = DustID.BlueTorch;
        HitSound = SoundID.Tink;
        MineResist = 2f;
        MinPick = 70;

    }

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {

        r = 0.1f;
        g = 0.1f;
        b = 0.75f;

    }

}