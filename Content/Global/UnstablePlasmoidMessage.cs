using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Content.Global;

public class UnstablePlasmoidMessage : GlobalNPC
{
    public override bool AppliesToEntity(NPC npc, bool lateInstantiation)
    {
        return npc.type == NPCID.HallowBoss;
    }

    public override void OnKill(NPC npc)
    {
        if (!NPC.downedEmpressOfLight)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Ominous red orbs can be seen looming high up in the night sky..."), new Color(185, 15, 15));
        }
    }
}