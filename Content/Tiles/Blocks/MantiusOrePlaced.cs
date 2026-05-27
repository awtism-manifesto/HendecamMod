using HendecamMod.Content.Global;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;

namespace HendecamMod.Content.Tiles.Blocks;

public class MantiusOrePlaced : ModTile
{
    public override void SetStaticDefaults()
    {
        TileID.Sets.Ore[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileOreFinderPriority[Type] = 595;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 975;
        Main.tileMergeDirt[Type] = true;
        Main.tileMerge[TileID.Stone][Type] = true;
        Main.tileMerge[TileID.Ebonstone][Type] = true;
        Main.tileMerge[TileID.Crimstone][Type] = true;
        Main.tileMerge[TileID.Pearlstone][Type] = true;
        Main.tileMerge[TileType<MantiusOrePlaced>()][Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileLighted[Type] = true;

        LocalizedText name = CreateMapEntryName();
        AddMapEntry(new Color(185, 15, 90), Language.GetText("Mantius Ore"));

        DustType = DustID.RedTorch;
        HitSound = SoundID.Tink;
        MineResist = 2.33f;
        MinPick = 95;
    }
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 0.33f;
        g = 0.05f;
        b = 0.12f;
    }
    public override void RandomUpdate(int i, int j)
    {

        int spreadChance;

        if (!NPC.downedMechBossAny)
        {

            spreadChance = 1;
        }
        else if (!NPC.downedMechBoss1 || !NPC.downedMechBoss2 || !NPC.downedMechBoss3)
        {

            spreadChance = 3;
        }
        else
        {

            spreadChance = 8;
        }

        if (WorldGen.genRand.NextBool(spreadChance))
        {
            TrySpread(i, j);
            if (!NPC.downedMechBossAny)
            {
                TrySpread(i, j);
            }
        }
    }

    public void TrySpread(int i, int j)
    {
        // Check density cap (50 per 300 radius)
        int mantiusCount = 0;
        int radius = 310;

        int minX = Math.Max(0, i - radius);
        int maxX = Math.Min(Main.maxTilesX - 1, i + radius);
        int minY = Math.Max(0, j - radius);
        int maxY = Math.Min(Main.maxTilesY - 1, j + radius);

        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                Tile tile = Main.tile[x, y];
                if (tile.HasTile && tile.TileType == Type)
                {
                    mantiusCount++;
                    if (mantiusCount >= 50)
                        break;
                }
            }
            if (mantiusCount >= 50)
                break;
        }

        if (mantiusCount >= 50)
            return;

        // Scan for stone blocks with random priority
        int maxDistance = 1;

        // Create a list of all possible offsets
        List<Point> offsets = new List<Point>();

        for (int x = -maxDistance; x <= maxDistance; x++)
        {
            for (int y = -maxDistance; y <= maxDistance; y++)
            {
                // Skip the center (0,0)
                if (x == 0 && y == 0)
                    continue;

                // Add each offset multiple times based on priority
                int weight = 1;

                // Horizontal directions get 3x weight
                if (y == 0 && Math.Abs(x) > 0)
                    weight = 2;
                // Diagonal directions get 2x weight
                else if (Math.Abs(x) == Math.Abs(y) && Math.Abs(x) > 0)
                    weight = 2;
                // Vertical directions get 1x weight
                else if (x == 0 && Math.Abs(y) > 0)
                    weight = 1;
                // Other directions (non-cardinal, non-diagonal) get 1x weight
                else
                    weight = 1;

                // Add the offset multiple times based on weight
                for (int w = 0; w < weight; w++)
                {
                    offsets.Add(new Point(x, y));
                }
            }
        }

        // Randomize the order
        for (int o = 0; o < offsets.Count; o++)
        {
            int randomIndex = WorldGen.genRand.Next(offsets.Count);
            Point temp = offsets[o];
            offsets[o] = offsets[randomIndex];
            offsets[randomIndex] = temp;
        }

        // Try each offset in random order
        foreach (Point offset in offsets)
        {
            int checkX = i + offset.X;
            int checkY = j + offset.Y;

            if (checkX < 10 || checkX >= Main.maxTilesX - 10 || checkY < 10 || checkY >= Main.maxTilesY - 10)
                continue;

            Tile tile = Main.tile[checkX, checkY];

            bool isStone = tile.TileType == TileID.Stone ||
                          tile.TileType == TileID.Ebonstone ||
                          tile.TileType == TileID.Crimstone ||
                          tile.TileType == TileID.Pearlstone;

            if (isStone && tile.HasTile && tile.TileType != Type)
            {
                WorldGen.PlaceTile(checkX, checkY, Type, true, true);
                if (WorldGen.PlaceTile(checkX, checkY, Type, true, true))
                {
                    NetMessage.SendTileSquare(-1, checkX, checkY, 1);
                    return;
                }
            }
        }
    }
}