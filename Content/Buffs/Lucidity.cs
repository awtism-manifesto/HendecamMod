using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.Dusts;
using HendecamMod.Content.Global;

namespace HendecamMod.Content.Buffs;

public class Lucidity : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<HendecamPlayer>().Lucid = true;
    }
}
