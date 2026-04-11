namespace HendecamMod.Common.Utils;

/// <summary>
/// Contains a lot of utillities and global usings
/// </summary>
public static partial class LemonUtils
{
    public static int UnderworldDepth => Main.maxTilesY - 200;

    /// <summary>
    /// Returns 1 for Small Worlds, 2 for Medium Worlds, 3 for Large Worlds (and bigger?)
    /// </summary>
    /// <returns></returns>
    public static int GetWorldSize()
    {
        switch (Main.maxTilesX)
        {
            case >= 8400:
                return 3;
            case >= 6400:
                return 2;
            default:
                return 1;
        }
    }

    public static bool IsDungeonBrick(int tileID)
    {
        return tileID == TileID.BlueDungeonBrick || tileID == TileID.GreenDungeonBrick || tileID == TileID.PinkDungeonBrick;
    }

    /// <summary>
    /// Returns 1 for Classic and Journey, 2 for Expert, 3 for Master.
    /// Doubles value if For the Worthy seed is active.
    /// Useful for scaling values based on difficulty.
    /// </summary>
    /// <returns></returns>
    public static int GetDifficulty()
    {
        int difficulty = 1;
        if (Main.expertMode) difficulty++;
        if (Main.masterMode) difficulty++;
        if (Main.getGoodWorld) difficulty *= 2;
        return difficulty;
    }

    /// <summary>
    /// Used for naturally generating an item in a vanilla chest. Use the wiki to find the appropriate chest's chestTileFrameX.
    /// If decreaseChanceDenominatorOnFail is true, the chance of the item generating will increase every unsuccessful attempt
    /// </summary>
    /// <param name="itemType"></param>
    /// <param name="chestTileFrameX"></param>
    /// <param name="chanceDenominator"></param>
    /// <param name="decreaseChanceDenominatorOnFail"></param>
    public static void GenerateItemInChest(int itemType, int chestTileFrameX, int chanceDenominator, bool decreaseChanceDenominatorOnFail = false)
    {
        int origChanceDenominator = chanceDenominator;
        for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
        {
            Chest chest = Main.chest[chestIndex];
            if (chest == null)
            {
                continue;
            }
            Tile chestTile = Main.tile[chest.x, chest.y];
            if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == chestTileFrameX * 36) // ivy chest
            {
                if (WorldGen.genRand.NextBool(chanceDenominator))
                {
                    for (int inventoryIndex = 0; inventoryIndex < Chest.maxItems; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            chest.item[inventoryIndex].SetDefaults(itemType);
                            break;
                        }
                    }
                }
                if (decreaseChanceDenominatorOnFail)
                {
                    if (chanceDenominator > 0)
                    {
                        chanceDenominator--;
                    }

                    if (chanceDenominator <= 0)
                    {
                        chanceDenominator = origChanceDenominator;
                    }
                }
            }
        }
    }
}
