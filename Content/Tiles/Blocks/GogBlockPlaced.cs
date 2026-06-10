using HendecamMod.Common.Systems;
using HendecamMod.Content.Global;
using HendecamMod.Content.Items.Accessories.PeaceAmongNations;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;


namespace HendecamMod.Content.Tiles.Blocks;

public class GogBlockPlaced : ModTile
{
  
    public override void SetStaticDefaults()
    {
        
        Main.tileMergeDirt[Type] = true;
        Main.tileMerge[TileID.Stone][Type] = true;
        Main.tileMerge[TileID.Ebonstone][Type] = true;
        Main.tileMerge[TileID.Crimstone][Type] = true;
        Main.tileMerge[TileID.Pearlstone][Type] = true;
       
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileLighted[Type] = true;

        LocalizedText name = CreateMapEntryName();
        AddMapEntry(new Color(20, 20, 180), Language.GetText("The Gog"));

        DustType = DustID.BlueMoss;
        HitSound = SoundID.Tink;
        MineResist = 15f;
        MinPick = 2;
    }

    public override void RandomUpdate(int i, int j)
    {
        var config = ModContent.GetInstance<ZeGogEnablers>();
        var config2 = ModContent.GetInstance<HendecamExperimentalConfig>();
        var config3 = ModContent.GetInstance<HendecamConfig>();

        int spreadStrength = 1;

        if (NPC.downedBoss1) spreadStrength += 1;
        if (NPC.downedBoss2) spreadStrength += 1;
        if (NPC.downedBoss3) spreadStrength += 1;
        if (NPC.downedAncientCultist) spreadStrength += 1;
        if (NPC.downedDeerclops) spreadStrength += 1;
        if (NPC.downedEmpressOfLight) spreadStrength += 1;
        if (Main.hardMode) spreadStrength += 2;
        if (NPC.downedMechBossAny) spreadStrength += 1;
        if (NPC.downedPlantBoss) spreadStrength += 1;
        if (NPC.downedGolemBoss) spreadStrength += 1;
        if (NPC.downedQueenSlime) spreadStrength += 1;
        if (NPC.downedMoonlord) spreadStrength += 2;
        if (BossDownedSystem.downedApacheElfShip) spreadStrength += 1;
        if (Main.zenithWorld) spreadStrength += 5;

        if (config.GogEnabler1 && config.GogEnabler2 && config.GogEnabler3 && config.GogEnabler4 &&
            config.GogEnabler5 && config2.EnableGogEnablers && config3.EnableExperimentalFeatures)
        {
           
            for (int i2 = 0; i2 < spreadStrength; i2++)
            {
                TrySpread(i, j);

               
                if (i2 % 3 == 0) 
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            if (x == 0 && y == 0) continue;
                            int adjX = i + x;
                            int adjY = j + y;
                            if (adjX > 10 && adjX < Main.maxTilesX - 10 &&
                                adjY > 10 && adjY < Main.maxTilesY - 10)
                            {
                                Tile adjTile = Main.tile[adjX, adjY];
                                if (adjTile.HasTile && adjTile.TileType == Type)
                                {
                                    TrySpread(adjX, adjY);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void TrySpread(int i, int j)
    {
        int maxDistance = 2; 

        // Pre-allocate list with capacity for better performance
        List<Point> offsets = new List<Point>(maxDistance * maxDistance * 4);

        for (int x = -maxDistance; x <= maxDistance; x++)
        {
            for (int y = -maxDistance; y <= maxDistance; y++)
            {
                if (x == 0 && y == 0) continue;

                int weight = 1;
                if (y == 0 && Math.Abs(x) > 0) weight = 3; // Horizontal priority
                else if (Math.Abs(x) == Math.Abs(y) && Math.Abs(x) > 0) weight = 2; // Diagonal
                else if (x == 0 && Math.Abs(y) > 0) weight = 1; // Vertical
                else weight = 1;

                for (int w = 0; w < weight; w++)
                {
                    offsets.Add(new Point(x, y));
                }
            }
        }

        // Fisher-Yates shuffle is more efficient
        for (int o = offsets.Count - 1; o > 0; o--)
        {
            int randomIndex = WorldGen.genRand.Next(o + 1);
            Point temp = offsets[o];
            offsets[o] = offsets[randomIndex];
            offsets[randomIndex] = temp;
        }

        foreach (Point offset in offsets)
        {
            int checkX = i + offset.X;
            int checkY = j + offset.Y;

            if (checkX < 10 || checkX >= Main.maxTilesX - 10 ||
                checkY < 10 || checkY >= Main.maxTilesY - 10)
                continue;

            Tile tile = Main.tile[checkX, checkY];

            // Check if tile can be replaced
            if (tile.HasTile && tile.TileType != Type && IsReplaceableTile(tile))
            {
                // Only call PlaceTile once
                if (WorldGen.PlaceTile(checkX, checkY, Type, true, true))
                {
                    NetMessage.SendTileSquare(-1, checkX, checkY, 1);
                    return; // Successfully spread, exit method
                }
            }
        }
    }

    // Helper method to define which tiles can be converted
    private bool IsReplaceableTile(Tile tile)
    {
        // Can't replace air or missing tiles
        if (!tile.HasTile)
            return false;

        // Don't replace chests, doors, or other important tiles
        if (TileID.Sets.BasicChest[tile.TileType] ||
            TileID.Sets.BasicDresser[tile.TileType] ||
            TileID.Sets.BasicDresser[tile.TileType] ||
         
            TileID.Sets.Torch[tile.TileType])
            return false;

        // Don't replace anything that isn't a full block
        if (!Main.tileSolid[tile.TileType] || Main.tileSolidTop[tile.TileType])
            return false;

        // Don't replace tiles that block tile replacement
        if (TileID.Sets.DoesntGetReplacedWithTileReplacement[tile.TileType])
            return false;

        // Allow everything else that's a basic solid block
        return true;
    }
}