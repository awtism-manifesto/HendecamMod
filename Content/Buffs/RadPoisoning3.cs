using HendecamMod.Content.DamageClasses;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Content.Buffs
{
    public class RadPoisoning3 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<Rad3Player>().Rad3 = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (Main.rand.NextBool(5)) // 1-in-3 chance every tick
            {
                int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.CrimsonTorch,
                    npc.velocity.X * 1.29f, npc.velocity.Y * 1.29f, 70, default, 1.95f);
                Main.dust[dust].noGravity = true;
            }

            if (Main.rand.NextBool(5)) // 1-in-3 chance every tick
            {
                int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Wraith,
                    npc.velocity.X * 1.29f, npc.velocity.Y * 1.29f, 70, default, 2.15f);
                Main.dust[dust].noGravity = true;
            }

            if (npc.lifeRegen > 0)
                npc.lifeRegen = 0;

            npc.lifeRegen -= 135;

           
        }

        public class Rad3Player : ModPlayer
        {
            public bool Rad3;

            public override void ResetEffects()
            {
                Rad3 = false;
            }

            public override void UpdateBadLifeRegen()
            {
                if (Rad3)
                {
                    if (Player.lifeRegen > 0)
                        Player.lifeRegen = 0;
                    Player.lifeRegenTime = 0;
                    Player.lifeRegen -= (int)(48.5f);
                }
            }
        }
    }
}