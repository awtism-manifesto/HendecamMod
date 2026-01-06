using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using HendecamMod.Content.Global;
using System.Drawing;



namespace HendecamMod.Content.Buffs
{
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
}