using HendecamMod.Content.Dusts;

namespace HendecamMod.Content.Buffs;

public class TurboCharged : ModBuff
{
    public static readonly int AttackSpeedBonus = 67;
   

    public override void Update(Player player, ref int buffIndex)
    {
        if (Main.rand.NextBool(7))
        {
            int dust = Dust.NewDust(player.position, player.width, player.height, DustID.Electric,
                player.velocity.X * Main.rand.NextFloat(-1.99f, 1.99f), player.velocity.Y * Main.rand.NextFloat(-1.99f, 1.99f), 70, Color.Red, 1f);
            Main.dust[dust].noGravity = true;
        }
        if (Main.rand.NextBool(7))
        {
            int dust = Dust.NewDust(player.position, player.width, player.height, DustID.CursedTorch,
                player.velocity.X * Main.rand.NextFloat(-1.99f, 1.99f), player.velocity.Y * Main.rand.NextFloat(-1.99f, 1.99f), 70, default, 1f);
            Main.dust[dust].noGravity = true;
        }
        if (Main.rand.NextBool(7))
        {
            int dust = Dust.NewDust(player.position, player.width, player.height, ModContent.DustType<AstatineDust>(),
                player.velocity.X * Main.rand.NextFloat(-1.99f, 1.99f), player.velocity.Y * Main.rand.NextFloat(-1.99f, 1.99f), 70, default, 1f);
            Main.dust[dust].noGravity = true;
        }

        player.GetAttackSpeed(DamageClass.Generic) += AttackSpeedBonus / 100f;
      
       
    }
}