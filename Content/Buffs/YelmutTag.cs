using HendecamMod.Content.NPCs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Dusts;

namespace HendecamMod.Content.Buffs;

public class YelmutTag : ModBuff
{


    public override void SetStaticDefaults()
    {
        
        BuffID.Sets.IsATagBuff[Type] = true;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {

        npc.GetGlobalNPC<HendecamGlobalNPC>().hasDebuff = true;
    }
}

