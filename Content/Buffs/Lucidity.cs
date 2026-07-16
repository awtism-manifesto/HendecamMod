using HendecamMod.Content.Dusts;
using HendecamMod.Content.Global;

namespace HendecamMod.Content.Buffs;

public class Lucidity : ModBuff
{
   

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<LucidPlayer>().Lucid = true;
    }

   

   
}
public class LucidPlayer : ModPlayer
{
    public bool Lucid;

    public override void ResetEffects()
    {
        Lucid = false;
    }

   
}