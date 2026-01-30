using HendecamMod.Content.Dusts;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace HendecamMod.Content.Tiles.Blocks;

public class LimestonePlaced : ModTile
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

        DustType = ModContent.DustType<LimestoneDust>();
        HitSound = SoundID.Tink;

        AddMapEntry(new Color(204, 190, 163));

    }

    public class LimestoneSystem : ModSystem
    {
        public static LocalizedText MagnoliaOrePassMessage { get; private set; }
        public static LocalizedText BlessedWithThroarbiumOreMessage { get; private set; }

        public override void SetStaticDefaults()
        {
            MagnoliaOrePassMessage = Mod.GetLocalization($"WorldGen.{nameof(MagnoliaOrePassMessage)}");
            BlessedWithThroarbiumOreMessage = Mod.GetLocalization($"WorldGen.{nameof(BlessedWithThroarbiumOreMessage)}");
        }

        public void BlessWorldWithExampleOre()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }
            ThreadPool.QueueUserWorkItem(_ =>
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(BlessedWithThroarbiumOreMessage.Value, 50, 255, 130);
                }
                else if (Main.netMode == NetmodeID.Server)
                {
                    ChatHelper.BroadcastChatMessage(BlessedWithThroarbiumOreMessage.ToNetworkText(), new Color(50, 255, 130));
                }

                int splotches = (int)(100 * (Main.maxTilesX / 4200f));
                int highestY = (int)Utils.Lerp(Main.rockLayer, Main.UnderworldLayer, 0.5);
                for (int iteration = 0; iteration < splotches; iteration++)
                {
                    int i = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
                    int j = WorldGen.genRand.Next(highestY, Main.UnderworldLayer);

                    WorldGen.OreRunner(i, j, WorldGen.genRand.Next(5, 9), WorldGen.genRand.Next(5, 9), (ushort)ModContent.TileType<TransOrePlaced>());
                }
            });
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Clay"));

            if (ShiniesIndex != -1)
            {
                tasks.Insert(ShiniesIndex + 1, new MagnoliaOrePass("Magnolia Mod Ores", 237.4298f));
            }
        }
    }

    public class MagnoliaOrePass : GenPass
    {
        public MagnoliaOrePass(string name, float loadWeight) : base(name, loadWeight)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = LimestoneSystem.MagnoliaOrePassMessage.Value;

            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);

                int y = WorldGen.genRand.Next((int)GenVars.rockLayer, Main.maxTilesY);
                Tile tile = Framing.GetTileSafely(x, y);
                if (tile.HasTile && tile.TileType == TileID.Stone)
                {
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(9, 18), WorldGen.genRand.Next(6, 18), ModContent.TileType<LimestonePlaced>());
                }
            }
        }
    }
}