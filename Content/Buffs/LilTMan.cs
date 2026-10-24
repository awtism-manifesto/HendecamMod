namespace HendecamMod.Content.Buffs;

public class LilTMan : ModBuff
{
    public static readonly int TagDamage = 4;

    public override void SetStaticDefaults()
    {
        // This allows the debuff to be inflicted on NPCs that would otherwise be immune to all debuffs.
        // Other mods may check it for different purposes.
        BuffID.Sets.IsATagBuff[Type] = true;
    }
}

