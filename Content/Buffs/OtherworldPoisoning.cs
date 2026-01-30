using Terraria;
using Terraria.ModLoader;

namespace HendecamMod.Content.Buffs;

public class OtherworldPoisoning : ModBuff
{

    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;

    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.lifeRegen = (int)(player.lifeRegen - 625f);
    }
}