using HendecamMod.Content.NPCs.Town.Alpine;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace HendecamMod.Common.Systems;

public class AlpineNPCRespawnSystem : ModSystem
{
    public static bool unlockedAlpineSpawn = false;
    public override void ClearWorld()
    {
        unlockedAlpineSpawn = false;
    }
    public override void SaveWorldData(TagCompound tag)
    {
        tag[nameof(unlockedAlpineSpawn)] = unlockedAlpineSpawn;
    }
    public override void LoadWorldData(TagCompound tag)
    {
        unlockedAlpineSpawn = tag.GetBool(nameof(unlockedAlpineSpawn));
        unlockedAlpineSpawn |= NPC.AnyNPCs(ModContent.NPCType<Alpine>());
    }
    public override void NetSend(BinaryWriter writer)
    {
        writer.WriteFlags(unlockedAlpineSpawn);
    }
    public override void NetReceive(BinaryReader reader)
    {
        reader.ReadFlags(out unlockedAlpineSpawn);
    }
}
