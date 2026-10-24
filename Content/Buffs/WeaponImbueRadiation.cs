using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.Dusts;

namespace HendecamMod.Content.Buffs;

public class WeaponImbueRadiation : ModBuff
{
    public override void SetStaticDefaults()
    {
        BuffID.Sets.IsAFlaskBuff[Type] = true;
        Main.meleeBuff[Type] = true;
        Main.persistentBuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<HendecamPlayer>().radWeaponImbue = true;
        player.MeleeEnchantActive = true; // MeleeEnchantActive indicates to other mods that a weapon imbue is active.
    }
}

