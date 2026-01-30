using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Tiles;

// These three classes showcase how to create tiles that act as corruption/crimson/hallow versions of vanilla tiles.
// For this example, we will be making vanilla's clay tiles convertible into the three spreading biomes
public class PearlclayPlaced : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
        TileID.Sets.Hallow[Type] = true;
        TileID.Sets.HallowBiome[Type] = 1;
        TileID.Sets.HallowBiomeSight[Type] = true;
        TileID.Sets.HallowCountCollection.Add(Type);
        DustType = DustID.Pearlsand;
        AddMapEntry(new Color(157, 76, 152));
        TileID.Sets.ChecksForMerge[Type] = true;
        Main.tileMerge[TileID.ClayBlock][Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileMerge[TileID.Ebonstone][Type] = true;
        Main.tileMerge[TileID.Crimstone][Type] = true;
        Main.tileMerge[TileID.Pearlstone][Type] = true;

        // We need to register a conversion from the vanilla clay into our modded variants, so our custom code can be called when the game attempts to convert the vanilla tile
        TileLoader.RegisterSimpleConversion(TileID.ClayBlock, BiomeConversionID.Hallow, Type);
        TileID.Sets.Infectable[TileID.ClayBlock] = true; // Adding clay to infectable tiles, without it no infection will spread over to it.

        TileLoader.RegisterConversion(Type, BiomeConversionID.Purity, TileID.ClayBlock); // Yellow (desert) solution also converts evil/hallowed tiles back into purity, so don't forget that check!
    }

    public override void RandomUpdate(int i, int j)
    {
        // We use this helper method to mimic vanilla behavior for spreading tiles, letting our hallowed fossil infect convert nearby tiles into hallowed versions of themselves
        WorldGen.SpreadInfectionToNearbyTile(i, j, BiomeConversionID.Hallow);
    }

    public override void ModifyFrameMerge(int i, int j, ref int up, ref int down, ref int left, ref int right, ref int upLeft, ref int upRight, ref int downLeft, ref int downRight)
    {
        // We use this method to set the merge values of the adjacent tiles to -2 if the tile nearby is a pearlsandstone block
        // -2 is what terraria uses to designate the tiles that will merge with ours using the custom frames
        WorldGen.TileMergeAttempt(-2, TileID.ClayBlock, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
    }
}

public class EbonclayPlaced : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
        TileID.Sets.Corrupt[Type] = true;
        TileID.Sets.CorruptBiome[Type] = 1;
        TileID.Sets.CorruptBiomeSight[Type] = true;
        TileID.Sets.CorruptCountCollection.Add(Type);
        DustType = DustID.Corruption;
        AddMapEntry(new Color(65, 48, 99));
        TileID.Sets.ChecksForMerge[Type] = true;
        Main.tileMerge[TileID.ClayBlock][Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileMerge[TileID.Ebonstone][Type] = true;
        Main.tileMerge[TileID.Crimstone][Type] = true;
        Main.tileMerge[TileID.Pearlstone][Type] = true;

        TileLoader.RegisterSimpleConversion(TileID.ClayBlock, BiomeConversionID.Corruption, Type);
        //TileID.Sets.Infectable[TileID.DesertFossil] = true; Since desert fossil was already added to infectable tiles in HallowedFossilTile, we don't need to add it again.
        //Still, having a commented out version of the code here is a reminder that this is needed for the tile to be infectable.

        TileLoader.RegisterConversion(Type, BiomeConversionID.Purity, TileID.ClayBlock);
    }

    public override void RandomUpdate(int i, int j)
    {
        WorldGen.SpreadInfectionToNearbyTile(i, j, BiomeConversionID.Corruption);
    }

    public override void ModifyFrameMerge(int i, int j, ref int up, ref int down, ref int left, ref int right, ref int upLeft, ref int upRight, ref int downLeft, ref int downRight)
    {
        WorldGen.TileMergeAttempt(-2, TileID.ClayBlock, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
    }
}

public class CrimclayPlaced : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
        TileID.Sets.Crimson[Type] = true;
        TileID.Sets.CrimsonBiome[Type] = 1;
        TileID.Sets.CrimsonBiomeSight[Type] = true;
        TileID.Sets.CrimsonCountCollection.Add(Type);
        DustType = DustID.Crimstone;
        AddMapEntry(new Color(112, 33, 32));
        TileID.Sets.ChecksForMerge[Type] = true;
        Main.tileMerge[TileID.ClayBlock][Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileMerge[TileID.Ebonstone][Type] = true;
        Main.tileMerge[TileID.Crimstone][Type] = true;
        Main.tileMerge[TileID.Pearlstone][Type] = true;

        TileLoader.RegisterSimpleConversion(TileID.ClayBlock, BiomeConversionID.Crimson, Type);
        //TileID.Sets.Infectable[TileID.DesertFossil] = true; Since desert fossil was already added to infectable tiles in HallowedFossilTile, we don't need to add it again.
        //Still, having a commented out version of the code here is a reminder that this is needed for the tile to be infectable.

        TileLoader.RegisterConversion(Type, BiomeConversionID.Purity, TileID.ClayBlock);
    }

    public override void Convert(int i, int j, int conversionType)
    {
        switch (conversionType)
        {
            case BiomeConversionID.Sand:
                WorldGen.ConvertTile(i, j, TileID.ClayBlock);
                return;
        }
    }

    public override void RandomUpdate(int i, int j)
    {
        WorldGen.SpreadInfectionToNearbyTile(i, j, BiomeConversionID.Crimson);
    }

    public override void ModifyFrameMerge(int i, int j, ref int up, ref int down, ref int left, ref int right, ref int upLeft, ref int upRight, ref int downLeft, ref int downRight)
    {
        WorldGen.TileMergeAttempt(-2, TileID.ClayBlock, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
    }
}