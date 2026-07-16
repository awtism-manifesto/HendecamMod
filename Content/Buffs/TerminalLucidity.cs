using HendecamMod.Content.Dusts;
using HendecamMod.Content.Global;

namespace HendecamMod.Content.Buffs;

public class TerminalLucidity : ModBuff
{
   

    public override void Update(Player player, ref int buffIndex)
    {
        if (player.statLife <= player.statLifeMax2 / 4)
        {
            player.GetModPlayer<TermLucidPlayer>().TermLucid = true;
        }
    }

   

   
}
public class TermLucidPlayer : ModPlayer
{
    public bool TermLucid;

    public override void ResetEffects()
    {
        TermLucid = false;
    }

   
}