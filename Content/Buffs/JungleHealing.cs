using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Buffs;

public class JungleHealing : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;

    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.lifeRegen = (int)(player.lifeRegen + 11f);
        player.ClearBuff(BuffID.Poisoned);
        player.buffImmune[BuffID.Poisoned] = true;
    }
}