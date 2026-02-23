using HendecamMod.Content.Dusts;
using HendecamMod.Content.Global;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace HendecamMod.Content.Buffs;

public class MicroplasticPoison : ModBuff // manifesto i remember you're vibe coding (comment written by manifesto) (bro how the FUCK was i supposed to do ts on my own)
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        Main.buffNoSave[Type] = true;
        Main.buffNoTimeDisplay[Type] = false;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        // Only the server should handle stack calculations
        if (Main.netMode == NetmodeID.MultiplayerClient && !Main.dedServ)
            return;

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

        var globalNPC = npc.GetGlobalNPC<MicroplasticTick>();
        globalNPC.Microplastic = true;
        globalNPC.MicroplasticStacks = stackCount;

        // Sync the global NPC data in multiplayer
        if (Main.netMode == NetmodeID.Server)
        {
            globalNPC.SyncData(npc);
        }
    }
}

public class MicroplasticTick : GlobalNPC
{
    public bool Microplastic;
    public int MicroplasticStacks = 0;

    public override bool InstancePerEntity => true;

    public override void ResetEffects(NPC npc)
    {
        // Don't reset if the buff is still active
        if (!npc.HasBuff(ModContent.BuffType<MicroplasticPoison>()))
        {
            Microplastic = false;
            MicroplasticStacks = 0;
        }
    }

    void DOTDebuff(NPC npc, float baseDamagePerSecond, int stacks, ref int damage)
    {
        if (npc.lifeRegen > 0) npc.lifeRegen = 0;

        float totalDamage = baseDamagePerSecond * stacks;
        npc.lifeRegen -= (int)(totalDamage * 2);

        if (damage < totalDamage)
        {
            damage = (int)totalDamage;
        }
    }

    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        if (Microplastic)
        {
            DOTDebuff(npc, 1f, MicroplasticStacks, ref damage);
        }
    }

    // Add networking support
    public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
        binaryWriter.Write(Microplastic);
        binaryWriter.Write(MicroplasticStacks);
    }

    public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
        Microplastic = binaryReader.ReadBoolean();
        MicroplasticStacks = binaryReader.ReadInt32();
    }

    // Helper method to sync data
    public void SyncData(NPC npc)
    {
        if (Main.netMode == NetmodeID.Server)
        {
            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI);
        }
    }
}

public static class MicroplasticPoisonExtensions
{
    public static void ApplyMicroplasticPoison(this NPC npc, int durationInSeconds, int stacks = 1)
    {
        // Only the server should handle applying buffs
        if (Main.netMode == NetmodeID.MultiplayerClient)
            return;

        int buffType = ModContent.BuffType<MicroplasticPoison>();

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