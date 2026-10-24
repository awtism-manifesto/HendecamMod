namespace HendecamMod.Content.Buffs;

public class ParagonCooldown : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;

    }
}