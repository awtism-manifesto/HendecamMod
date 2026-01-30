using Terraria;
using Terraria.ModLoader;

namespace HendecamMod.Content.Tiles;

public class PoorMahoganyTile : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
    }
}