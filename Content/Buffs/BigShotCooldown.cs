using Terraria;
using Terraria.ModLoader;

namespace HendecamMod.Content.Buffs;

public class BigShotCooldown : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
    }
}