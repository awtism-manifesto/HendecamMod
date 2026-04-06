namespace HendecamMod.Content.Buffs;

public class SkeletalDefense : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.endurance = 1f - 0.95f * (1f - player.endurance);
        player.statDefense += 10;
        
    }
}