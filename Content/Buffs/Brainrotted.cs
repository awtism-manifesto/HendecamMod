using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using Terraria.Localization;

namespace HendecamMod.Content.Buffs;

public class BrainRotted : ModBuff
{

    public override void SetStaticDefaults()
    {
        Main.pvpBuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        var loboDecay = player.GetModPlayer<LobotometerPlayer>();
        loboDecay.DecayRateMultiplier *= 1.2f;
    }
}