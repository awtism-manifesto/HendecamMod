namespace HendecamMod.Content.Buffs;

public class RudeBusterCooldown : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
    }
}