namespace HendecamMod.Content.Buffs;

public class HeartBuff : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.lifeRegen = (int)(player.lifeRegen + 7f);
        player.manaCost *= 0.8f;
    }
}