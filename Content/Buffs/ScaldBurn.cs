using HendecamMod.Content.Dusts;
using HendecamMod.Content.Global;

namespace HendecamMod.Content.Buffs;

public class ScaldBurn : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    public static readonly float AdditiveDamageBonus = -22.5f;
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 100f;
        player.GetModPlayer<ScaldPlayer>().Scald = true;
    }
   
   
    
    public override void Update(NPC npc, ref int buffIndex)
    {
        if (Main.rand.NextBool(3))
        {
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Water,
                npc.velocity.X * 0.69f, npc.velocity.Y * 0.69f, 70, default, 1.75f);
            Main.dust[dust].noGravity = true;
        }
       
       
        npc.GetGlobalNPC<ScaldTick>().Scaldded = true;
       
    }

    public class ScaldPlayer : ModPlayer
    {
        public bool Scald;

        public override void ResetEffects()
        {
            Scald = false;
        }

        public override void UpdateBadLifeRegen()
        {
            if (Scald)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 15;
            }
        }
    }
}
public class ScaldTick : GlobalNPC
{
    public bool Scaldded;
    public int ScaldTimer = 298;
    public override bool InstancePerEntity => true;

    public override void ResetEffects(NPC npc)
    {
        Scaldded = false;
    }


    void DOTDebuff(NPC npc, float damagePerSecond, ref int damage)
    {
        if (npc.lifeRegen > 0) npc.lifeRegen = 0;
        npc.lifeRegen -= (int)(damagePerSecond * 6);
        if (damage < damagePerSecond)
        {
            damage = (int)damagePerSecond;
        }
    }
    public override void AI(NPC npc)
    {
        if (Scaldded)
        {
            ScaldTimer--;
            npc.damage = (int)(npc.defDamage * 0.775f);

            if (ScaldTimer <= 0)
            {
                npc.damage = npc.defDamage;
            }
        }
    }
    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        
        if (Scaldded)
        {
            DOTDebuff(npc, 8, ref damage);
        }
        // float damagePerSecond = npc.lifeMax * 0.005f + 10; // debuff that scales based on max life
    }
}
