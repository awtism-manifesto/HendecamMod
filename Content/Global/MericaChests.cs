using HendecamMod.Content.Items;
using HendecamMod.Content.Items.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Global
{
    // This class showcases adding additional items to vanilla chests.
    // This example simply adds additional items. More complex logic would likely be required for other scenarios.
    // If this code is confusing, please learn about "for loops" and the "continue" and "break" keywords: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/jump-statements
    public class MeAndMagAreFuckingLazy : ModSystem
    {
        // We use PostWorldGen for this because we want to ensure that all chests have been placed before adding items.
        public override void PostWorldGen()
        {
            // Place some additional items in Surface Chests:
            // These are the 3 new items we will place.
            int[] itemsToPlaceInSurfaceChests = [ModContent.ItemType<ImprovisedPistol>(), ModContent.ItemType<FlippingBottle>(), ItemID.Bass];
            // This variable will help cycle through the items so that different Surface Chests get different items
            int itemsToPlaceInSurfaceChestsChoice = 0;
            // Rather than place items in each chest, we'll place up to 6 items (2 of each). 
            int itemsPlaced = 0;
            int maxItems = 12;
            // Loop over all the chests
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null)
                {
                    continue;
                }
                Tile chestTile = Main.tile[chest.x, chest.y];
                // We need to check if the current chest is the Surface Chest. We need to check that it exists and has the TileType and TileFrameX values corresponding to the Surface Chest.
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Surface Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. An alternate approach is to check the wiki and looking for the "Internal Tile ID" section in the infobox: https://terraria.wiki.gg/wiki/Surface_Chest
                if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == 0 * 36)
                {
                    // We have found a Surface Chest
                    // If we don't want to add one of the items to every Surface Chest, we can randomly skip this chest with a 33% chance.
                    if (WorldGen.genRand.NextBool(5))
                        continue;
                    // Next we need to find the first empty slot for our item
                    for (int inventoryIndex = 0; inventoryIndex < Chest.maxItems; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            // Place the item
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInSurfaceChests[itemsToPlaceInSurfaceChestsChoice]);
                            // Decide on the next item that will be placed.
                            itemsToPlaceInSurfaceChestsChoice = (itemsToPlaceInSurfaceChestsChoice + 1) % itemsToPlaceInSurfaceChests.Length;
                            // Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(WorldGen.genRand.Next(itemsToPlaceInSurfaceChests));
                            itemsPlaced++;
                            break;
                        }
                    }
                }
                // Once we've placed as many items as we wanted, break out of the loop
                if (itemsPlaced >= maxItems)
                {
                    break;
                }
            }
        }

    }
    public class SigmaChests : ModSystem
    {
        // We use PostWorldGen for this because we want to ensure that all chests have been placed before adding items.
        public override void PostWorldGen()
        {
            // Place some additional items in Surface Chests:
            // These are the 3 new items we will place.
            int[] itemsToPlaceInSurfaceChests = [ModContent.ItemType<TorchSong>(), ModContent.ItemType<BallisticKnife>(), ModContent.ItemType<HarryPotter>()];
            // This variable will help cycle through the items so that different Surface Chests get different items
            int itemsToPlaceInSurfaceChestsChoice = 0;
            // Rather than place items in each chest, we'll place up to 6 items (2 of each). 
            int itemsPlaced = 0;
            int maxItems = 10;
            // Loop over all the chests
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null)
                {
                    continue;
                }
                Tile chestTile = Main.tile[chest.x, chest.y];
                // We need to check if the current chest is the Surface Chest. We need to check that it exists and has the TileType and TileFrameX values corresponding to the Surface Chest.
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Surface Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. An alternate approach is to check the wiki and looking for the "Internal Tile ID" section in the infobox: https://terraria.wiki.gg/wiki/Surface_Chest
                if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == 2 * 36)
                {
                    // We have found a Surface Chest
                    // If we don't want to add one of the items to every Surface Chest, we can randomly skip this chest with a 33% chance.
                    if (WorldGen.genRand.NextBool(4))
                        continue;
                    // Next we need to find the first empty slot for our item
                    for (int inventoryIndex = 0; inventoryIndex < Chest.maxItems; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            // Place the item
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInSurfaceChests[itemsToPlaceInSurfaceChestsChoice]);
                            // Decide on the next item that will be placed.
                            itemsToPlaceInSurfaceChestsChoice = (itemsToPlaceInSurfaceChestsChoice + 1) % itemsToPlaceInSurfaceChests.Length;
                            // Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(WorldGen.genRand.Next(itemsToPlaceInSurfaceChests));
                            itemsPlaced++;
                            break;
                        }
                    }
                }
                // Once we've placed as many items as we wanted, break out of the loop
                if (itemsPlaced >= maxItems)
                {
                    break;
                }
            }
        }

    }
    public class WattaChests : ModSystem
    {
        // We use PostWorldGen for this because we want to ensure that all chests have been placed before adding items.
        public override void PostWorldGen()
        {
            // Place some additional items in Surface Chests:
            // These are the 3 new items we will place.
            int[] itemsToPlaceInSurfaceChests = [ModContent.ItemType<TheFishStick>(), ItemID.Bass, ModContent.ItemType<StarfishOnAString>()];
            // This variable will help cycle through the items so that different Surface Chests get different items
            int itemsToPlaceInSurfaceChestsChoice = 0;
            // Rather than place items in each chest, we'll place up to 6 items (2 of each). 
            int itemsPlaced = 0;
            int maxItems = 8;
            // Loop over all the chests
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null)
                {
                    continue;
                }
                Tile chestTile = Main.tile[chest.x, chest.y];
                // We need to check if the current chest is the Surface Chest. We need to check that it exists and has the TileType and TileFrameX values corresponding to the Surface Chest.
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Surface Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. An alternate approach is to check the wiki and looking for the "Internal Tile ID" section in the infobox: https://terraria.wiki.gg/wiki/Surface_Chest
                if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == 17 * 36)
                {
                    // We have found a Surface Chest
                    // If we don't want to add one of the items to every Surface Chest, we can randomly skip this chest with a 33% chance.
                    if (WorldGen.genRand.NextBool(4))
                        continue;
                    // Next we need to find the first empty slot for our item
                    for (int inventoryIndex = 0; inventoryIndex < Chest.maxItems; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            // Place the item
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInSurfaceChests[itemsToPlaceInSurfaceChestsChoice]);
                            // Decide on the next item that will be placed.
                            itemsToPlaceInSurfaceChestsChoice = (itemsToPlaceInSurfaceChestsChoice + 1) % itemsToPlaceInSurfaceChests.Length;
                            // Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(WorldGen.genRand.Next(itemsToPlaceInSurfaceChests));
                            itemsPlaced++;
                            break;
                        }
                    }
                }
                // Once we've placed as many items as we wanted, break out of the loop
                if (itemsPlaced >= maxItems)
                {
                    break;
                }
            }
        }

    }
    public class TreeChests : ModSystem
    {
        // We use PostWorldGen for this because we want to ensure that all chests have been placed before adding items.
        public override void PostWorldGen()
        {
            // Place some additional items in Surface Chests:
            // These are the 3 new items we will place.
            int[] itemsToPlaceInSurfaceChests = [ModContent.ItemType<ThePeoplesPitchfork>()];
            // This variable will help cycle through the items so that different Surface Chests get different items
            int itemsToPlaceInSurfaceChestsChoice = 0;
            // Rather than place items in each chest, we'll place up to 6 items (2 of each). 
            int itemsPlaced = 0;
            int maxItems = 2;
            // Loop over all the chests
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null)
                {
                    continue;
                }
                Tile chestTile = Main.tile[chest.x, chest.y];
                // We need to check if the current chest is the Surface Chest. We need to check that it exists and has the TileType and TileFrameX values corresponding to the Surface Chest.
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Surface Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. An alternate approach is to check the wiki and looking for the "Internal Tile ID" section in the infobox: https://terraria.wiki.gg/wiki/Surface_Chest
                if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == 12 * 36)
                {
                    // We have found a Surface Chest
                    // If we don't want to add one of the items to every Surface Chest, we can randomly skip this chest with a 33% chance.
                    if (WorldGen.genRand.NextBool(4))
                        continue;
                    // Next we need to find the first empty slot for our item
                    for (int inventoryIndex = 0; inventoryIndex < Chest.maxItems; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            // Place the item
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInSurfaceChests[itemsToPlaceInSurfaceChestsChoice]);
                            // Decide on the next item that will be placed.
                            itemsToPlaceInSurfaceChestsChoice = (itemsToPlaceInSurfaceChestsChoice + 1) % itemsToPlaceInSurfaceChests.Length;
                            // Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(WorldGen.genRand.Next(itemsToPlaceInSurfaceChests));
                            itemsPlaced++;
                            break;
                        }
                    }
                }
                // Once we've placed as many items as we wanted, break out of the loop
                if (itemsPlaced >= maxItems)
                {
                    break;
                }
            }
        }

    }
    public class FuckDesertChests : ModSystem
    {
        // We use PostWorldGen for this because we want to ensure that all chests have been placed before adding items.
        public override void PostWorldGen()
        {
            // Place some additional items in Surface Chests:
            // These are the 3 new items we will place.
            int[] itemsToPlaceInSurfaceChests = [ModContent.ItemType<SaltPendant>(), ModContent.ItemType<MandibleStaff>(), ModContent.ItemType<Macuahuitl>()];
            // This variable will help cycle through the items so that different Surface Chests get different items
            int itemsToPlaceInSurfaceChestsChoice = 0;
            // Rather than place items in each chest, we'll place up to 6 items (2 of each). 
            int itemsPlaced = 0;
            int maxItems = 9;
            // Loop over all the chests
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null)
                {
                    continue;
                }
                Tile chestTile = Main.tile[chest.x, chest.y];
                // We need to check if the current chest is the Surface Chest. We need to check that it exists and has the TileType and TileFrameX values corresponding to the Surface Chest.
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Surface Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. An alternate approach is to check the wiki and looking for the "Internal Tile ID" section in the infobox: https://terraria.wiki.gg/wiki/Surface_Chest
                if (chestTile.TileType == TileID.Containers2 && chestTile.TileFrameX == 10 * 36)
                {
                    // We have found a Surface Chest
                    // If we don't want to add one of the items to every Surface Chest, we can randomly skip this chest with a 33% chance.
                    if (WorldGen.genRand.NextBool(4))
                        continue;
                    // Next we need to find the first empty slot for our item
                    for (int inventoryIndex = 0; inventoryIndex < Chest.maxItems; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            // Place the item
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInSurfaceChests[itemsToPlaceInSurfaceChestsChoice]);
                            // Decide on the next item that will be placed.
                            itemsToPlaceInSurfaceChestsChoice = (itemsToPlaceInSurfaceChestsChoice + 1) % itemsToPlaceInSurfaceChests.Length;
                            // Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(WorldGen.genRand.Next(itemsToPlaceInSurfaceChests));
                            itemsPlaced++;
                            break;
                        }
                    }
                }
                // Once we've placed as many items as we wanted, break out of the loop
                if (itemsPlaced >= maxItems)
                {
                    break;
                }
            }
        }
    }
    public class UrDeadLolXD : ModSystem
    {
        // We use PostWorldGen for this because we want to ensure that all chests have been placed before adding items.
        public override void PostWorldGen()
        {
            // Place some additional items in Surface Chests:
            // These are the 3 new items we will place.
            int[] itemsToPlaceInSurfaceChests = [ModContent.ItemType<PocketMortar>(), ModContent.ItemType<TheMonkeysPaw>(), ModContent.ItemType<LuckyCigarette>()];
            // This variable will help cycle through the items so that different Surface Chests get different items
            int itemsToPlaceInSurfaceChestsChoice = 0;
            // Rather than place items in each chest, we'll place up to 6 items (2 of each). 
            int itemsPlaced = 0;
            int maxItems = 7;
            // Loop over all the chests
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null)
                {
                    continue;
                }
                Tile chestTile = Main.tile[chest.x, chest.y];
                // We need to check if the current chest is the Surface Chest. We need to check that it exists and has the TileType and TileFrameX values corresponding to the Surface Chest.
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Surface Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. An alternate approach is to check the wiki and looking for the "Internal Tile ID" section in the infobox: https://terraria.wiki.gg/wiki/Surface_Chest
                if (chestTile.TileType == TileID.Containers2 && chestTile.TileFrameX == 4 * 36)
                {
                    // We have found a Surface Chest
                    // If we don't want to add one of the items to every Surface Chest, we can randomly skip this chest with a 33% chance.
                    if (WorldGen.genRand.NextBool(4))
                        continue;
                    // Next we need to find the first empty slot for our item
                    for (int inventoryIndex = 0; inventoryIndex < Chest.maxItems; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            // Place the item
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInSurfaceChests[itemsToPlaceInSurfaceChestsChoice]);
                            // Decide on the next item that will be placed.
                            itemsToPlaceInSurfaceChestsChoice = (itemsToPlaceInSurfaceChestsChoice + 1) % itemsToPlaceInSurfaceChests.Length;
                            // Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(WorldGen.genRand.Next(itemsToPlaceInSurfaceChests));
                            itemsPlaced++;
                            break;
                        }
                    }
                }
                // Once we've placed as many items as we wanted, break out of the loop
                if (itemsPlaced >= maxItems)
                {
                    break;
                }
            }
        }
    }
}
