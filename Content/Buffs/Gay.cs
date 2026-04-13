using HendecamMod.Content.Global;

namespace HendecamMod.Content.Buffs;

public class Gay : ModBuff
{
    public const int DefenseReductionPercent = (int)35f;
    public static float DefenseMultiplier = 0.65f;

    public override void SetStaticDefaults()
    {
        Main.pvpBuff[Type] = true; // This buff can be applied by other players in Pvp, so we need this to be true.
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        npc.GetGlobalNPC<GayDebuff>().gay = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.statDefense *= DefenseMultiplier;
    }
}
public class GayDebuff : GlobalNPC
{
    public bool gay;
    public override bool InstancePerEntity => true;

    public override void ResetEffects(NPC npc)
    {
        gay = false;
    }

    public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
        if (gay)
        {
            // For best results, defense debuffs should be multiplicative
            modifiers.Defense *= Gay.DefenseMultiplier;
        }
    }

    public override void DrawEffects(NPC npc, ref Color drawColor)
    {
        // This simple color effect indicates that the buff is active
        if (gay)
        {
            drawColor.R = (byte)Main.rand.Next(89, 233);
            drawColor.G = (byte)Main.rand.Next(89, 233);
            drawColor.B = (byte)Main.rand.Next(89, 233);
        }
    }
}