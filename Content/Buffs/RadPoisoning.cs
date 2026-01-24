using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Dusts;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Content.Buffs
{
    public class RadPoisoning : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            
        }
        

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<RadPlayer>().Rad = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            

            if (Main.rand.NextBool(3)) 
            {
                int dust = Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<UraniumDust>(),
                    npc.velocity.X * 0.69f, npc.velocity.Y * 0.69f, 70, default, 1.75f);
                Main.dust[dust].noGravity = true;
            }

            if (npc.lifeRegen > 0)
                npc.lifeRegen = 0;

            npc.lifeRegen -= 20;
            

        }

        public class RadPlayer : ModPlayer
        {
            public bool Rad;

            public override void ResetEffects()
            {
                Rad = false;
            }

            public override void UpdateBadLifeRegen()
            {
                if (Rad)
                {
                    if (Player.lifeRegen > 0)
                        Player.lifeRegen = 0;
                    Player.lifeRegenTime = 0;
                    Player.lifeRegen -= 16;
                }
            }
        }
    }
}