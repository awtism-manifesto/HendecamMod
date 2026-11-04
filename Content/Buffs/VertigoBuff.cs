using HendecamMod.Common.Utilities;

namespace HendecamMod.Content.Buffs;

public class VertigoBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetHCPlayer().hasVertigo = true;
    }
    public override void Update(NPC npc, ref int buffIndex)
    {

    }
}