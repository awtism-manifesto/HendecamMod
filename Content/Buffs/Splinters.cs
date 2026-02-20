using HendecamMod.Content.Dusts;
using HendecamMod.Content.Global;

namespace HendecamMod.Content.Buffs;

public class Splinters : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }

   

    public override void Update(NPC npc, ref int buffIndex)
    {
        if (Main.rand.NextBool(10)) // 1-in-3 chance every tick
        {
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.WoodFurniture,
                npc.velocity.X * 1.29f, npc.velocity.Y * 1.29f, 70, default, 1.95f);
            Main.dust[dust].noGravity = true;
        }

       

        npc.GetGlobalNPC<SplinterTick>().Splintering = true;
    }

   
}
public class SplinterTick : GlobalNPC
{
    public bool Splintering;
    public override bool InstancePerEntity => true;

    public override void ResetEffects(NPC npc)
    {
        Splintering = false;
    }

    // Helper method for DOT debuffs
    void DOTDebuff(NPC npc, float damagePerSecond, ref int damage)
    {
        if (npc.lifeRegen > 0) npc.lifeRegen = 0;
        npc.lifeRegen -= (int)(damagePerSecond * 5);
        if (damage < damagePerSecond)
        {
            damage = (int)damagePerSecond;
        }
    }

    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        if (Splintering)
        {
            DOTDebuff(npc, 2, ref damage);
        }
    }
}
