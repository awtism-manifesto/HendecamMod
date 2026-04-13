using HendecamMod.Content.Global;

namespace HendecamMod.Content.Buffs;

public class Purpled : ModBuff
{
    public const int DefenseReductionPercent = (int)35f;
    public static float DefenseMultiplier = 0.5f;

    public override void SetStaticDefaults()
    {
        Main.pvpBuff[Type] = true; // This buff can be applied by other players in Pvp, so we need this to be true.
        BuffID.Sets.IsATagBuff[Type] = true;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        npc.GetGlobalNPC<PurpDebuff>().purp = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.statDefense *= DefenseMultiplier;
    }
}
public class PurpDebuff : GlobalNPC
{
    public bool purp;
    public override bool InstancePerEntity => true;

    public override void ResetEffects(NPC npc)
    {
        purp = false;
    }
    void DOTDebuff(NPC npc, float damagePerSecond, ref int damage)
    {
        if (npc.lifeRegen > 0) npc.lifeRegen = 0;
        npc.lifeRegen -= (int)(damagePerSecond * 7);
        if (damage < damagePerSecond)
        {
            damage = (int)damagePerSecond;
        }
    }

    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        if (purp)
        {
            DOTDebuff(npc, 72, ref damage);
        }
    }
    public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
        if (purp)
        {
            // For best results, defense debuffs should be multiplicative
            modifiers.Defense *= Purpled.DefenseMultiplier;
        }
    }

    public override void DrawEffects(NPC npc, ref Color drawColor)
    {
        // This simple color effect indicates that the buff is active
        if (purp)
        {
            drawColor.R = 203;
            drawColor.G = 69;
            drawColor.B = 255;
        }
    }
}