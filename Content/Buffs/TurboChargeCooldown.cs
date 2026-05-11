namespace HendecamMod.Content.Buffs;

public class TurboChargeCooldown : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
    }
}