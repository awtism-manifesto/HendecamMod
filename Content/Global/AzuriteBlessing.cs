using HendecamMod.Content.Tiles.Blocks;
using Microsoft.Xna.Framework;
using System.Threading;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Content.Global;

public class AzuriteBlessing : GlobalNPC
{

    public override bool AppliesToEntity(NPC npc, bool lateInstantiation)
    {
        return npc.type == NPCID.SkeletronHead;
    }

    public override void OnKill(NPC npc)
    {
        if (!NPC.downedBoss3)
        {
            ModContent.GetInstance<AzuriteSystem>().BlessWorldWithAzurite();

        }

    }
}
public class AzuriteSystem : ModSystem
{
    public static LocalizedText AzuriteMessage { get; private set; }
    public static LocalizedText AzuriteBlessMessage { get; private set; }

    public override void SetStaticDefaults()
    {
        AzuriteMessage = Mod.GetLocalization($"WorldGen.{nameof(AzuriteMessage)}");
        AzuriteBlessMessage = Mod.GetLocalization($"WorldGen.{nameof(AzuriteBlessMessage)}");
    }

    // This method is called from MinionBossBody.OnKill the first time the boss is killed.
    // The logic is located here for organizational purposes.
    public void BlessWorldWithAzurite()
    {
        if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            return;
        }

        // Since this happens during gameplay, we need to run this code on another thread. If we do not, the game will experience lag for a brief moment. This is especially necessary for world generation tasks that would take even longer to execute.
        // See https://github.com/tModLoader/tModLoader/wiki/World-Generation/#long-running-tasks for more information.
        ThreadPool.QueueUserWorkItem(_ =>
        {
            // Broadcast a message to notify the user.
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(AzuriteBlessMessage.Value, 25, 50, 255);
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                ChatHelper.BroadcastChatMessage(AzuriteBlessMessage.ToNetworkText(), new Color(25, 50, 255));
            }

            // 100 controls how many splotches of ore are spawned into the world, scaled by world size. For comparison, the first 3 times altars are smashed about 275, 190, or 120 splotches of the respective hardmode ores are spawned. 
            int splotches = (int)(276 * (Main.maxTilesX / 4200f));
            int highestY = (int)Utils.Lerp(Main.worldSurface, Main.UnderworldLayer, 0.445);
            for (int iteration = 0; iteration < splotches; iteration++)
            {
                // Find a point in the lower half of the rock layer but above the underworld depth.
                int i = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
                int j = WorldGen.genRand.Next(highestY, Main.UnderworldLayer);

                // OreRunner will spawn ExampleOre in splotches. OnKill only runs on the server or single player, so it is safe to run world generation code.
                WorldGen.OreRunner(i, j, WorldGen.genRand.Next(6, 9), WorldGen.genRand.Next(7, 11), (ushort)ModContent.TileType<AzuriteOrePlaced>());
            }
        });
    }

    // World generation is explained more in https://github.com/tModLoader/tModLoader/wiki/World-Generation

}
