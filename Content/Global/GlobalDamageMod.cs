using HendecamMod.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace HendecamMod.Content.Global;

public class DamageModificationGlobalNPC : GlobalNPC
{
    public bool stamped;
    public override bool InstancePerEntity => true;

    public override void ResetEffects(NPC npc)
    {
        stamped = false;
    }

    public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
        if (stamped)
        {
            // For best results, defense debuffs should be multiplicative
            modifiers.Defense *= Stamped.DefenseMultiplier;
        }
    }

    public override void DrawEffects(NPC npc, ref Color drawColor)
    {
        // This simple color effect indicates that the buff is active
        if (stamped)
        {
            drawColor.R = 255;
            drawColor.G = 5;
            drawColor.B = 5;
        }
    }
}

public class OrangeDebuff : GlobalNPC
{
    public bool orange;
    public override bool InstancePerEntity => true;

    public override void ResetEffects(NPC npc)
    {
        orange = false;
    }

    public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
        if (orange)
        {
            // For best results, defense debuffs should be multiplicative
            modifiers.Defense *= LycopiteSpores.DefenseMultiplier;
        }
    }

    public override void DrawEffects(NPC npc, ref Color drawColor)
    {
        // This simple color effect indicates that the buff is active
        if (orange)
        {
            drawColor.R = 255;
            drawColor.G = 85;
            drawColor.B = 5;
        }
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