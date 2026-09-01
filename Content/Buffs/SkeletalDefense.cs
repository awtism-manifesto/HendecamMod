namespace HendecamMod.Content.Buffs;

public class SkeletalDefense : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.endurance += 0.05f;
        player.statDefense += 10;
        
    }
}