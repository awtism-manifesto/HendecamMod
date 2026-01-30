using Terraria;
using Terraria.ModLoader;

namespace HendecamMod.Content.Global;

public class RadTick : GlobalNPC
{
    public bool Radded;
    public override bool InstancePerEntity => true;

    public override void ResetEffects(NPC npc)
    {
        Radded = false;
    }

    // Helper method for DOT debuffs
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
        if (Radded)
        {
            DOTDebuff(npc, 7, ref damage);
        }
        // float damagePerSecond = npc.lifeMax * 0.005f + 10; // debuff that scales based on max life
    }
}

public class Rad2Tick : GlobalNPC
{
    public bool Radded2;
    public override bool InstancePerEntity => true;

    public override void ResetEffects(NPC npc)
    {
        Radded2 = false;
    }

    // Helper method for DOT debuffs
    void DOTDebuff(NPC npc, float damagePerSecond, ref int damage)
    {
        if (npc.lifeRegen > 0) npc.lifeRegen = 0;
        npc.lifeRegen -= (int)(damagePerSecond * 8);
        if (damage < damagePerSecond)
        {
            damage = (int)damagePerSecond;
        }
    }

    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        if (Radded2)
        {
            DOTDebuff(npc, 13, ref damage);
        }
    }
}

public class Rad3Tick : GlobalNPC
{
    public bool Radded3;
    public override bool InstancePerEntity => true;

    public override void ResetEffects(NPC npc)
    {
        Radded3 = false;
    }

    // Helper method for DOT debuffs
    void DOTDebuff(NPC npc, float damagePerSecond, ref int damage)
    {
        if (npc.lifeRegen > 0) npc.lifeRegen = 0;
        npc.lifeRegen -= (int)(damagePerSecond * 9);
        if (damage < damagePerSecond)
        {
            damage = (int)damagePerSecond;
        }
    }

    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        if (Radded3)
        {
            DOTDebuff(npc, 21, ref damage);
        }
    }
}

public class MoonBurning : GlobalNPC
{
    public bool MoonCooked;
    public override bool InstancePerEntity => true;

    public override void ResetEffects(NPC npc)
    {
        MoonCooked = false;
    }

    // Helper method for DOT debuffs
    void DOTDebuff(NPC npc, float damagePerSecond, ref int damage)
    {
        if (npc.lifeRegen > 0) npc.lifeRegen = 0;
        npc.lifeRegen -= (int)(damagePerSecond * 11);
        if (damage < damagePerSecond)
        {
            damage = (int)damagePerSecond;
        }
    }

    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        if (MoonCooked)
        {
            DOTDebuff(npc, 66, ref damage);
        }
    }
}

public class WhoTfIsSteveJobs : GlobalNPC
{
    public bool DyingOfLigma;
    public override bool InstancePerEntity => true;

    public override void ResetEffects(NPC npc)
    {
        DyingOfLigma = false;
    }

    // Helper method for DOT debuffs
    void DOTDebuff(NPC npc, float damagePerSecond, ref int damage)
    {
        if (npc.lifeRegen > 0) npc.lifeRegen = 0;
        npc.lifeRegen -= (int)(damagePerSecond * 13);
        if (damage < damagePerSecond)
        {
            damage = (int)damagePerSecond;
        }
    }

    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        if (DyingOfLigma)
        {
            DOTDebuff(npc, 911, ref damage);
        }
    }
}