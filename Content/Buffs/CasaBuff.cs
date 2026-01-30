namespace HendecamMod.Content.Buffs;

public class CasaBuff : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.lifeRegen = (int)(player.lifeRegen + 3.5f);
        player.aggro = -525;
    }
}