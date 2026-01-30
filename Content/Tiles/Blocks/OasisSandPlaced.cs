namespace HendecamMod.Content.Tiles.Blocks;

public class OasisSandPlaced : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileMerge[TileID.Stone][Type] = true;
        Main.tileMerge[TileID.Ebonstone][Type] = true;
        Main.tileMerge[TileID.Crimstone][Type] = true;
        Main.tileMerge[TileID.Pearlstone][Type] = true;
        Main.tileMerge[TileID.Granite][Type] = true;
        Main.tileMerge[TileID.Marble][Type] = true;
        Main.tileMerge[TileID.ClayBlock][Type] = true;
        Main.tileMerge[TileID.Mud][Type] = true;
        Main.tileMerge[TileID.HardenedSand][Type] = true;
        Main.tileMerge[TileID.IceBlock][Type] = true;
        Main.tileMerge[TileID.SnowBlock][Type] = true;
        Main.tileBlockLight[Type] = true;

        HitSound = SoundID.Dig;

        AddMapEntry(new Color(170, 142, 69));
    }
}