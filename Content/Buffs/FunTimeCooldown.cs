namespace HendecamMod.Content.Buffs;

public class FunTimeCooldown : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
    }
}