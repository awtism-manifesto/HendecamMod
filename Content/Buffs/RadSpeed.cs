namespace HendecamMod.Content.Buffs;

public class RadSpeed : ModBuff
{
    public static readonly int AttackSpeedBonus = 4;
  

    public override void Update(Player player, ref int buffIndex)
    {
       
      
        player.GetAttackSpeed(DamageClass.Generic) += AttackSpeedBonus / 100f;
     
       
    }
}