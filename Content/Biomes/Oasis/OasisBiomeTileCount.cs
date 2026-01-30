using HendecamMod.Content.Tiles.Blocks;

namespace HendecamMod.Content.Biomes.Oasis;

public class OasisTileCount : ModSystem
{
    public int oasisBlockCount;

    public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
    {
        oasisBlockCount = tileCounts[ModContent.TileType<OasisSandPlaced>()];
    }
}