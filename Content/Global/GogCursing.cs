using HendecamMod.Common.Systems;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.NPCs.Bosses;
using HendecamMod.Content.NPCs.Bosses.PromethiumPlasmoid;
using HendecamMod.Content.Tiles.Blocks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData; // For TileObjectData

namespace HendecamMod.Content.Global;

public class GogCursing : GlobalNPC
{
    public override bool AppliesToEntity(NPC npc, bool lateInstantiation)
    {
        if (npc.type == NPCID.KingSlime ||
            npc.type == NPCID.EyeofCthulhu ||
            npc.type == NPCID.BrainofCthulhu ||
            npc.type == NPCID.SkeletronHead ||
            npc.type == NPCID.QueenSlimeBoss ||
            npc.type == NPCID.TheDestroyer ||
            npc.type == NPCID.SkeletronPrime ||
            npc.type == NPCID.Retinazer ||
            npc.type == NPCID.Spazmatism ||
            npc.type == NPCID.Plantera ||
            npc.type == NPCID.Golem ||
            npc.type == NPCID.DukeFishron ||
            npc.type == NPCID.HallowBoss ||
            npc.type == NPCID.CultistBoss ||
            npc.type == NPCID.MoonLordCore ||
            npc.type == NPCID.MartianSaucerCore ||
            npc.type == NPCID.Pumpking ||
            npc.type == NPCID.IceQueen ||
            npc.type == NPCID.DD2Betsy ||
            npc.type == NPCID.Everscream ||
            npc.type == NPCID.MourningWood ||
            npc.type == NPCID.SantaNK1 ||
            npc.type == NPCID.PirateShip ||
            npc.type == ModContent.NPCType<ApacheElfShip>() ||  // Fixed: ModContent.NPCType
            npc.type == ModContent.NPCType<HeadOfCthulhu>() ||
            npc.type == ModContent.NPCType<PromethiumPlasmoid>())
        {
            return true;
        }
        return false;
    }

    public override void OnKill(NPC npc)
    {
        var config = ModContent.GetInstance<ZeGogEnablers>();
        var config2 = ModContent.GetInstance<HendecamExperimentalConfig>();
        var config3 = ModContent.GetInstance<HendecamConfig>();

        if (config.GogEnabler1 && config.GogEnabler2 && config.GogEnabler3 && config.GogEnabler4 &&
           config.GogEnabler5 && config.GogRandomSpawns && config2.EnableGogEnablers && config3.EnableExperimentalFeatures)
        {
            TrySpawnGogBlock();
        }
    }

    private void TrySpawnGogBlock()
    {
        // Get the tile type once
        int tileType = ModContent.TileType<GogBlockPlaced>();
        if (tileType == 0) return;

        // Try multiple times to find a valid spawn location
        int maxAttempts = 50;
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            int x = Main.rand.Next(100, Main.maxTilesX - 100);
            int y = Main.rand.Next((int)Main.worldSurface, Main.maxTilesY - 50);

            // Validate coordinates are within bounds
            if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
                continue;

            Tile targetTile = Main.tile[x, y];
            if (targetTile == null)
                continue;

            // Check if the tile can be replaced (standard 1x1 block)
            // Don't replace important structures or chests
            if (IsReplaceableTile(targetTile))
            {
                // Remove the existing tile
                WorldGen.KillTile(x, y, false, false, true);

                // Place the Gog block
                if (WorldGen.PlaceTile(x, y, tileType, true, true))
                {
                  

                    // Sync to multiplayer
                    NetMessage.SendTileSquare(-1, x, y, 1);

                  

                    return; // Successfully placed, exit the method
                }
            }
        }

        // If we get here, all attempts failed - optionally log a warning
        // Main.NewText("Failed to spawn Gog block after " + maxAttempts + " attempts", Color.Red);
    }

    private bool IsReplaceableTile(Tile tile)
    {
        // Make sure the tile exists and has a tile type
        if (!tile.HasTile) return false;

        // Get the tile type
        ushort tileType = tile.TileType;

        // List of tile types that should NOT be replaced (important structures)
        // You can expand this list as needed
        int[] unreplaceableTiles = new int[]
        {
           
            TileID.Containers,       // Other containers
            TileID.Tombstones,       // Tombstones
            TileID.LihzahrdAltar,    // Lihzahrd altar
            TileID.LihzahrdBrick,    // Lihzahrd bricks (for Golem arena)
            TileID.MetalBars,        // Bars
            TileID.Anvils,           // Anvils
            TileID.Furnaces,         // Furnaces
            TileID.WorkBenches,      // Workbenches
            TileID.Beds,             // Beds
            TileID.Signs,            // Signs
            TileID.Teleporter,       // Teleporters
            TileID.LunarMonolith,    // Monoliths
        };

        // Check if tile is in the unreplaceable list
        for (int i = 0; i < unreplaceableTiles.Length; i++)
        {
            if (tileType == unreplaceableTiles[i])
                return false;
        }

        // Check if the tile is part of a multitile structure (size > 1x1)
        TileObjectData data = TileObjectData.GetTileData(tileType, 0);
        if (data != null && (data.Width > 1 || data.Height > 1))
            return false;

        // Also don't replace walls, active/inactive stones, etc. if you want
        // But for basic 1x1 blocks, this should be fine

        return true;
    }
}