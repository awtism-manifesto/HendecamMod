namespace HendecamMod.Content.Buffs;

public class ShinyTag2 : ModBuff
{
    public static readonly int TagDamage = 10;

    public override void SetStaticDefaults()
    {
        // This allows the debuff to be inflicted on NPCs that would otherwise be immune to all debuffs.
        // Other mods may check it for different purposes.
        BuffID.Sets.IsATagBuff[Type] = true;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        Lighting.AddLight(npc.Center, 1.25f, 1.25f, 1.25f);
    }

    
}