namespace HendecamMod.Content.Buffs;

public class DroneSwarmCooldown : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
    }
}