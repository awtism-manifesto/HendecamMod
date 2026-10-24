using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.Dusts;
using HendecamMod.Content.Global;

namespace HendecamMod.Content.Buffs;

public class TerminalLucidity : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        if (player.statLife <= player.statLifeMax2 / 4)
        {
            player.GetModPlayer<HendecamPlayer>().TermLucid = true;
        }
    }
}