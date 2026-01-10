using System;
using HendecamMod.Content.Tiles;
using Terraria.ModLoader;

namespace HendecamMod.Content.Biomes.Oasis
    {
    public class OasisTileCount : ModSystem
        {
        public int oasisBlockCount;
        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
            {
            oasisBlockCount = tileCounts[ModContent.TileType<AstatineBrickTile>()];
            }
        }
    }
