using HendecamMod.Content.Global;
using HendecamMod.Content.NPCs;

namespace HendecamMod.Content.Buffs;

public class Purpled : ModBuff
{
    public const int DefenseReductionPercent = (int)35f;
    public static float DefenseMultiplier = 0.5f; 

    public override void SetStaticDefaults()
    {
        Main.pvpBuff[Type] = true; // This buff can be applied by other players in Pvp, so we need this to be true.
        BuffID.Sets.IsATagBuff[Type] = true;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        npc.GetGlobalNPC<HendecamGlobalNPC>().Purpled = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.statDefense *= DefenseMultiplier;
    }
}