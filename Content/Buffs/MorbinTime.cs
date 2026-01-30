using HendecamMod.Content.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace HendecamMod.Content.Buffs;

public class MorbinTime : ModBuff
{
    public static readonly int AttackSpeedBonus = 11;
    public static readonly int CritBonus = 17;
    public static readonly int AdditiveDamageBonus = 23;
    public override void Update(Player player, ref int buffIndex)
    {

        if (Main.rand.NextBool(2))
        {
            int dust = Dust.NewDust(player.position, player.width, player.height, ModContent.DustType<MorbiumDust>(),
                player.velocity.X * Main.rand.NextFloat(-1.99f, 1.99f), player.velocity.Y * Main.rand.NextFloat(-1.99f, 1.99f), 70, default, 3f);
            Main.dust[dust].noGravity = true;
        }
        player.GetCritChance(DamageClass.Generic) += CritBonus;
        player.GetAttackSpeed(DamageClass.Generic) += AttackSpeedBonus / 100f;
        player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 100f;
        player.statDefense += 11;
    }
}