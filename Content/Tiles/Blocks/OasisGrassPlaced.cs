using HendecamMod.Content.Dusts;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace HendecamMod.Content.Tiles.Blocks
{
    public class OasisGrassPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileMerge[TileID.ClayBlock][Type] = true;
            Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileMerge[TileID.HardenedSand][Type] = true;
            Main.tileMerge[TileID.SnowBlock][Type] = true;
            Main.tileMerge[TileID.Sand][Type] = true;
            Main.tileMerge[TileID.Sandstone][Type] = true;
            Main.tileMerge[TileID.Ebonsand][Type] = true;
            Main.tileMerge[TileID.Crimsand][Type] = true;
            Main.tileMerge[TileID.Pearlsand][Type] = true;
            Main.tileMerge[TileID.Grass][Type] = true;
            Main.tileBlockLight[Type] = true;

            HitSound = SoundID.Dig;

            AddMapEntry(new Color(170, 142, 69));

        }
    }
}