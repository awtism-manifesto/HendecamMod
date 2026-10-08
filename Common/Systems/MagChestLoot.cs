using HendecamMod.Content.Items.Accessories.Cubes;
using HendecamMod.Content.Items.Weapons.Magic;
using HendecamMod.Content.Items.Weapons.Melee;

namespace HendecamMod.Common.Systems;

public class MagChestLoot1 : ModSystem
{
    public override void PostWorldGen()
    {
        int[] itemsToPlaceInFrozenChests = [ItemType<FrozenMace>()];
        int itemsToPlaceInFrozenChestsChoice = 0;
        int itemsPlaced = 0;
        int maxItems = 6;
        for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
        {
            Chest chest = Main.chest[chestIndex];
            if (chest == null)
            {
                continue;
            }

            Tile chestTile = Main.tile[chest.x, chest.y];
            if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == 11 * 36)
            {
                if (WorldGen.genRand.NextBool(3))
                    continue;
                for (int inventoryIndex = 0; inventoryIndex < chest.maxItems; inventoryIndex++)
                {
                    if (chest.item[inventoryIndex].type == ItemID.None)
                    {
                        chest.item[inventoryIndex].SetDefaults(itemsToPlaceInFrozenChests[itemsToPlaceInFrozenChestsChoice]);
                        itemsToPlaceInFrozenChestsChoice = (itemsToPlaceInFrozenChestsChoice + 1) % itemsToPlaceInFrozenChests.Length;
                        itemsPlaced++;
                        break;
                    }
                }
            }

            if (itemsPlaced >= maxItems)
            {
                break;
            }
        }
    }
}

public class MagChestLoot2 : ModSystem
{
    public override void PostWorldGen()
    {
        int[] itemsToPlaceInHellChests = [ItemType<MeteorCube>(), ItemType<FireDiamondStaff>()];
        int itemsToPlaceInHellChestsChoice = 0;
        int itemsPlaced = 0;
        int maxItems = 8;
        for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
        {
            Chest chest = Main.chest[chestIndex];
            if (chest == null)
            {
                continue;
            }

            Tile chestTile = Main.tile[chest.x, chest.y];

            if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == 4 * 36)
            {
                if (WorldGen.genRand.NextBool(3))
                    continue;
                for (int inventoryIndex = 0; inventoryIndex < chest.maxItems; inventoryIndex++)
                {
                    if (chest.item[inventoryIndex].type == ItemID.None)
                    {
                        chest.item[inventoryIndex].SetDefaults(itemsToPlaceInHellChests[itemsToPlaceInHellChestsChoice]);
                        itemsToPlaceInHellChestsChoice = (itemsToPlaceInHellChestsChoice + 1) % itemsToPlaceInHellChests.Length;
                        itemsPlaced++;
                        break;
                    }
                }
            }

            if (itemsPlaced >= maxItems)
            {
                break;
            }
        }
    }
}