using System.Threading;
using HendecamMod.Common.Systems;
using HendecamMod.Content.NPCs.Bosses;
using HendecamMod.Content.Tiles;
using Terraria.Chat;
using Terraria.Localization;

namespace HendecamMod.Content.Global;



public class PlutoniumSystem : ModSystem
{
    public static LocalizedText PlutoniumMessage { get; private set; }
    public static LocalizedText PlutoniumBlessMessage { get; private set; }

    public override void SetStaticDefaults()
    {
        PlutoniumMessage = Mod.GetLocalization($"WorldGen.{nameof(PlutoniumMessage)}");
        PlutoniumBlessMessage = Mod.GetLocalization($"WorldGen.{nameof(PlutoniumBlessMessage)}");
    }

    // This method is called from MinionBossBody.OnKill the first time the boss is killed.
    // The logic is located here for organizational purposes.
    public void BlessWorldWithPlutonium()
    {
        if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            return; // This should not happen, but just in case.
        }

        Main.QueueMainThreadAction(() =>
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(PlutoniumBlessMessage.Value, 115, 25);
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                ChatHelper.BroadcastChatMessage(
                    PlutoniumBlessMessage.ToNetworkText(),
                    new Color(115, 25, 255)
                );
            }

            int splotches = (int)(298 * (Main.maxTilesX / 4200f));
            int highestY = (int)Utils.Lerp(Main.worldSurface, Main.UnderworldLayer, 0.525);

            for (int k = 0; k < splotches; k++)
            {
                int i = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
                int j = WorldGen.genRand.Next(highestY, Main.UnderworldLayer);

                WorldGen.OreRunner(
                    i,
                    j,
                    WorldGen.genRand.Next(6, 7),
                    WorldGen.genRand.Next(7, 8),
                    (ushort)ModContent.TileType<PlutoniumOrePlaced>()
                );
            }
        });

        // World generation is explained more in https://github.com/tModLoader/tModLoader/wiki/World-Generation
    }
}