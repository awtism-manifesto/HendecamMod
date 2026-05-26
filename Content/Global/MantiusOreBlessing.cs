using HendecamMod.Content.Tiles.Blocks;
using System.Collections.Generic;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.Localization;

namespace HendecamMod.Content.Global;

public class MantiusOreBlessed : GlobalNPC
{
    public override bool AppliesToEntity(NPC npc, bool lateInstantiation)
    {
        return npc.type == NPCID.WallofFlesh;
    }

    public override void OnSpawn(NPC npc, IEntitySource source)
    {
        if (!Main.hardMode && !MantiusSystem.WallOfFleshIsAlive)
        {
            MantiusSystem.StartMantiusSpawning();
        }
    }

    public override void OnKill(NPC npc)
    {
        // Jump-start spreading before stopping the system
        if (!NPC.downedMechBossAny) // Only if mech bosses aren't defeated yet
        {
            MantiusSystem.JumpStartMantiusSpreading();
        }

        MantiusSystem.StopMantiusSpawning();
    }
}

public class MantiusSystem : ModSystem // manifesto i remember you're vibecoding
{
    public static LocalizedText MantiusMessage { get; private set; }
    public static LocalizedText MantiusBlessMessage { get; private set; }

    public static bool WallOfFleshIsAlive = false;
    private static int spawnCounter = 0;
    private static int ticksBetweenSpawns = 30; 
    private static Random random = new Random();

    // Optimization variables
    private static int failedAttempts = 0;
    private static int dynamicDelay = 45; 

    public override void SetStaticDefaults()
    {
        MantiusMessage = Mod.GetLocalization($"WorldGen.{nameof(MantiusMessage)}");
        MantiusBlessMessage = Mod.GetLocalization($"WorldGen.{nameof(MantiusBlessMessage)}");
    }

    public static void StartMantiusSpawning()
    {
        WallOfFleshIsAlive = true;
        spawnCounter = 0;
        failedAttempts = 0;
        dynamicDelay = 45;

        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            Main.NewText("The Wall of Flesh shakes the earth's mantle...", 185, 15, 90);
            Main.NewText("Mantius ore spawning system activated!", 185, 15, 90);
        }
        else if (Main.netMode == NetmodeID.Server)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromKey("The Wall of Flesh shakes the earth's mantle..."), new Color(185, 15, 90));
        }
    }
    public static void JumpStartMantiusSpreading()
    {
        if (Main.netMode == NetmodeID.MultiplayerClient)
            return;

       

        // Find all Mantius ore tiles
        List<Point> mantiusTiles = new List<Point>();

        for (int x = 0; x < Main.maxTilesX; x++)
        {
            for (int y = 0; y < Main.maxTilesY; y++)
            {
                Tile tile = Main.tile[x, y];
                if (tile.HasTile && tile.TileType == TileType<MantiusOrePlaced>())
                {
                    mantiusTiles.Add(new Point(x, y));
                }
            }
        }

        

        // Create an instance of the tile
        MantiusOrePlaced oreTile = new MantiusOrePlaced();

        // Each Mantius tile will attempt to spread multiple times
        int spreadAttemptsPerTile = 11;

        for (int attempt = 0; attempt < spreadAttemptsPerTile; attempt++)
        {
            foreach (Point tilePos in mantiusTiles)
            {
                // Call TrySpread directly (now public)
                oreTile.TrySpread(tilePos.X, tilePos.Y);
            }
        }

      
    }
    public static void StopMantiusSpawning()
    {
        WallOfFleshIsAlive = false;
        failedAttempts = 0;
        dynamicDelay = 120;

        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            Main.NewText("The earth stops violently shaking...", 185, 15, 90);
        }
        else if (Main.netMode == NetmodeID.Server)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromKey("The earth stops violently shaking..."), new Color(185, 15, 90));
        }
    }

    public override void PostUpdateEverything()
    {
        // Check if Wall of Flesh still exists - if not, stop spawning
        if (WallOfFleshIsAlive)
        {
            bool foundWoF = false;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == NPCID.WallofFlesh)
                {
                    foundWoF = true;
                    break;
                }
            }

            if (!foundWoF)
            {
                // WoF despawned (player died or ran away) - stop spawning
                StopMantiusSpawning();
                return;
            }
        }

        // Only run if WoF is alive and not on client in multiplayer
        if (!WallOfFleshIsAlive || Main.netMode == NetmodeID.MultiplayerClient)
            return;

        spawnCounter++;

        if (spawnCounter >= dynamicDelay) // Use dynamic delay instead of fixed
        {
            spawnCounter = 0;
            TrySpawnSingleMantiusOre();
        }
    }

    private void TrySpawnSingleMantiusOre()
    {
        // Dynamically reduce attempts based on previous failures
        int maxAttempts = Math.Max(50, 500 - (failedAttempts * 2)); // Fewer attempts if we keep failing

        int attempts = 0;
        bool success = false;

        while (attempts < maxAttempts && !success)
        {
            int x = random.Next(100, Main.maxTilesX - 100);
            int y = random.Next((int)Main.rockLayer, Main.maxTilesY - 50);

            Tile targetTile = Main.tile[x, y];

            bool isStone = targetTile.TileType == TileID.Stone ||
                          targetTile.TileType == TileID.Ebonstone ||
                          targetTile.TileType == TileID.Crimstone ||
                          targetTile.TileType == TileID.Pearlstone;

            if (!isStone || !targetTile.HasTile || targetTile.TileType == TileType<MantiusOrePlaced>())
            {
                attempts++;
                continue;
            }

            if (IsAreaTooDense(x, y, 300, 50))
            {
                attempts++;
                continue;
            }

            // Check if within 10 blocks of lava (keep this condition)
            if (IsWithinTenBlocksOfLava(x, y))
            {
                if (WorldGen.PlaceTile(x, y, TileType<MantiusOrePlaced>(), true, true))
                {
                    success = true;
                    NetMessage.SendTileSquare(-1, x, y, 1);
                    failedAttempts = 0; // Reset failure counter on success
                    dynamicDelay = 75; // Reset to normal speed

                    
                    break;
                }
            }

            attempts++;
        }

        // If we failed to spawn, increase delay to reduce lag
        if (!success)
        {
            failedAttempts++;

            // Gradually increase delay between attempts (up to 10 seconds)
            if (failedAttempts > 10)
            {
                dynamicDelay = Math.Min(600, dynamicDelay + 10); // Max 10 seconds

                
            }
        }
    }

    private bool IsAreaTooDense(int x, int y, int radius, int cap)
    {
        int mantiusCount = 0;
        int minX = Math.Max(0, x - radius);
        int maxX = Math.Min(Main.maxTilesX - 1, x + radius);
        int minY = Math.Max(0, y - radius);
        int maxY = Math.Min(Main.maxTilesY - 1, y + radius);

        for (int cx = minX; cx <= maxX && mantiusCount < cap; cx++)
        {
            for (int cy = minY; cy <= maxY && mantiusCount < cap; cy++)
            {
                Tile tile = Main.tile[cx, cy];
                if (tile.HasTile && tile.TileType == TileType<MantiusOrePlaced>())
                {
                    mantiusCount++;
                }
            }
        }

        return mantiusCount >= cap;
    }

    private bool IsWithinTenBlocksOfLava(int x, int y)
    {
        int minX = Math.Max(0, x - 10);
        int maxX = Math.Min(Main.maxTilesX - 1, x + 10);
        int minY = Math.Max(0, y - 10);
        int maxY = Math.Min(Main.maxTilesY - 1, y + 10);

        for (int checkX = minX; checkX <= maxX; checkX++)
        {
            for (int checkY = minY; checkY <= maxY; checkY++)
            {
                Tile tile = Main.tile[checkX, checkY];
                if (tile.LiquidType == LiquidID.Lava && tile.LiquidAmount > 0)
                {
                    return true;
                }
            }
        }
        return false;
    }
}