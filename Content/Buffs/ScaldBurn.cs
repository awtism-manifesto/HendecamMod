using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.NPCs;
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
        player.GetModPlayer<HendecamPlayer>().Scald = true;
    }
   
   
    
    public override void Update(NPC npc, ref int buffIndex)
    {
        if (Main.rand.NextBool(3))
        {
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Water,
                npc.velocity.X * 0.69f, npc.velocity.Y * 0.69f, 70, default, 1.75f);
            Main.dust[dust].noGravity = true;
        }
       
       
        npc.GetGlobalNPC<HendecamGlobalNPC>().Scaldded = true;
       
    }

    
}

