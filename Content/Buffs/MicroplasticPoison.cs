using HendecamMod.Content.Dusts;
using HendecamMod.Content.Global;
using Terraria;
using Terraria.ModLoader;

namespace HendecamMod.Content.Buffs;

public class MicroplasticPoison : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        Main.buffNoSave[Type] = true;
       
        Main.buffNoTimeDisplay[Type] = false;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        // Get the current stack count from the buff's time
        int stackCount = 1;

        // Check if this is the first application or if we're stacking
        if (npc.buffTime[buffIndex] > 0)
        {
            // The buff time is used to store stack information
            // For first application, we set it to 1 stack + base duration
            // For subsequent applications, we increase the stack count
            stackCount = (npc.buffTime[buffIndex] / 60) + 1; // Assuming 60 ticks per stack
        }

        // Spawn dust effects (increased with stacks)
        for (int i = 0; i < stackCount; i++)
        {
            if (Main.rand.NextBool(20))
            {
                int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.GolfPaticle,
                    npc.velocity.X * 1.29f, npc.velocity.Y * 1.29f, 70, default, 1.5f);
                Main.dust[dust].noGravity = true;
            }

           
        }

        npc.GetGlobalNPC<MicroplasticTick>().Microplastic = true;
        // Store the stack count in the GlobalNPC for damage calculation
        npc.GetGlobalNPC<MicroplasticTick>().MicroplasticStacks = stackCount;
    }
}

public class MicroplasticTick : GlobalNPC
{
    public bool Microplastic;
    public int MicroplasticStacks = 0;

    public override bool InstancePerEntity => true;

    public override void ResetEffects(NPC npc)
    {
        Microplastic = false;
        MicroplasticStacks = 0;
    }

    // Helper method for DOT debuffs with stacking damage
    void DOTDebuff(NPC npc, float baseDamagePerSecond, int stacks, ref int damage)
    {
        if (npc.lifeRegen > 0) npc.lifeRegen = 0;

        // Calculate damage with stacking - you can adjust this formula
        // Option 1: Linear scaling (1 + 0.5 per stack)
       // float totalDamage = baseDamagePerSecond * (1 + (stacks - 1) * 0.5f);

        // Option 2: Exponential scaling (more dramatic)
        // float totalDamage = baseDamagePerSecond * MathF.Pow(1.2f, stacks - 1);

        // Option 3: Simple additive (2 damage per stack)
         float totalDamage = baseDamagePerSecond * stacks;

        npc.lifeRegen -= (int)(totalDamage * 2);

        if (damage < totalDamage)
        {
            damage = (int)totalDamage;
        }
    }

    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        if (Microplastic)
        {
            // Use base damage of 1 with stacking
            DOTDebuff(npc, 1f, MicroplasticStacks, ref damage);
        }
    }
}

// Optional: Add a method to handle stacking when applying the debuff
public static class MicroplasticPoisonExtensions
{
    public static void ApplyMicroplasticPoison(this NPC npc, int durationInSeconds, int stacks = 1)
    {
        int buffType = ModContent.BuffType<MicroplasticPoison>();

        if (npc.HasBuff(buffType))
        {
           
            int buffIndex = npc.FindBuffIndex(buffType);
            if (buffIndex != -1)
            {
               
                int currentStacks = npc.buffTime[buffIndex] / 60;
                int newStacks = Math.Min(currentStacks + stacks, 37); 

                // Update buff time to reflect new stack count and reset duration
                npc.buffTime[buffIndex] = newStacks * 60 + durationInSeconds * 60;
            }
        }
        else
        {
            // First application - set buff time to stacks * 60 + duration
            npc.AddBuff(buffType, stacks * 60 + durationInSeconds * 60);
        }
    }
}