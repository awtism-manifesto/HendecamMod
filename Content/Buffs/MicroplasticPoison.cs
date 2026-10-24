using HendecamMod.Content.NPCs;
using System.IO;
using Terraria.ModLoader.IO;

namespace HendecamMod.Content.Buffs;

public class MicroplasticPoison : ModBuff // kids, do not vibe code.
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        Main.buffNoSave[Type] = true;
        Main.buffNoTimeDisplay[Type] = false;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        

        int stackCount = 1;

        if (npc.buffTime[buffIndex] > 0)
        {
            // Use integer division to avoid floating point issues
            stackCount = (npc.buffTime[buffIndex] / 30) + 1;

            // Cap stack count to prevent overflow
            if (stackCount > 40) stackCount = 40;
        }

        // Visual effects should be synced or handled on all clients
        if (Main.netMode != NetmodeID.Server)
        {
            for (int i = 0; i < Math.Min(stackCount, 5); i++) // Cap dust effects for performance
            {
                if (Main.rand.NextBool(20))
                {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.GolfPaticle,
                        npc.velocity.X * 1.29f, npc.velocity.Y * 1.29f, 70, default, 1.5f);
                    Main.dust[dust].noGravity = true;
                }
            }
        }

        var globalNPC = npc.GetGlobalNPC<HendecamGlobalNPC>();
        globalNPC.Microplastic = true;
        globalNPC.MicroplasticStacks = stackCount;

        // Sync the global NPC data in multiplayer
        if (Main.netMode == NetmodeID.Server)
        {
            globalNPC.SyncData(npc);
        }
    }
}



public static class MicroplasticPoisonExtensions
{
    public static void ApplyMicroplasticPoison(this NPC npc, int durationInSeconds, int stacks = 1)
    {
       

        int buffType = BuffType<MicroplasticPoison>();

        if (npc.HasBuff(buffType))
        {
            int buffIndex = npc.FindBuffIndex(buffType);
            if (buffIndex != -1)
            {
                // Get current stacks more safely
                int currentStacks = npc.buffTime[buffIndex] / 60;
                if (currentStacks < 1) currentStacks = 1;

                int newStacks = Math.Min(currentStacks + stacks, 40);

                // Update buff time
                npc.buffTime[buffIndex] = newStacks * 60 + durationInSeconds * 30;

                // Sync the buff data in multiplayer
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI);
                }
            }
        }
        else
        {
            npc.AddBuff(buffType, stacks * 60 + durationInSeconds * 30);
        }
    }
}