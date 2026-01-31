using HendecamMod.Content.Dusts;

namespace HendecamMod.Content.Buffs;

public class SheildOffense : ModBuff
{
    public static readonly int AttackSpeedBonus = 11;
    public static readonly int CritBonus = 22;
    public static readonly int AdditiveDamageBonus = 33;

    public override void Update(Player player, ref int buffIndex)
    {
        if (Main.rand.NextBool(7))
        {
            int dust = Dust.NewDust(player.position, player.width, player.height, DustID.CrimtaneWeapons,
                player.velocity.X * Main.rand.NextFloat(-1.99f, 1.99f), player.velocity.Y * Main.rand.NextFloat(-1.99f, 1.99f), 70, default, 1f);
            Main.dust[dust].noGravity = true;
        }
        if (Main.rand.NextBool(7))
        {
            int dust = Dust.NewDust(player.position, player.width, player.height, DustID.GemRuby,
                player.velocity.X * Main.rand.NextFloat(-1.99f, 1.99f), player.velocity.Y * Main.rand.NextFloat(-1.99f, 1.99f), 70, default, 1f);
            Main.dust[dust].noGravity = true;
        }
        player.GetCritChance(DamageClass.Generic) += CritBonus;
        player.GetAttackSpeed(DamageClass.Generic) += AttackSpeedBonus / 100f;
        player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 100f;
       
    }
}