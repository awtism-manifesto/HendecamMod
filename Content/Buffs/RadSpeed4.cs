namespace HendecamMod.Content.Buffs;

public class RadSpeed4 : ModBuff
{
    public static readonly int AttackSpeedBonus = 16;
  

    public override void Update(Player player, ref int buffIndex)
    {
       
      
        player.GetAttackSpeed(DamageClass.Generic) += AttackSpeedBonus / 100f;
     
       
    }
}