namespace HendecamMod.Content.Buffs;

public class PyrrhicDefense : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.lifeRegen = (int)(player.lifeRegen + 6.67f);
        player.ClearBuff(BuffID.Ichor);
        player.buffImmune[BuffID.Ichor] = true;
        player.statDefense += 33;
        
    }
}