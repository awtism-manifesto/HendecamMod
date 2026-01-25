using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Content.Buffs;

public class DarkPower : ModBuff
{

    public static readonly int MaxManaIncrease = 120;



    public override void Update(Player player, ref int buffIndex)
    {
        player.statManaMax2 += MaxManaIncrease;
    }
}
