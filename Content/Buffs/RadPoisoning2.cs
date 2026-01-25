using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Dusts;
using HendecamMod.Content.Global;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Content.Buffs;

public class RadPoisoning2 : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<Rad2Player>().Rad2 = true;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        

        if (Main.rand.NextBool(3)) 
        {
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<PlutoniumDust>(),
                npc.velocity.X * 0.89f, npc.velocity.Y * 0.89f, 70, default, 1.95f);
            Main.dust[dust].noGravity = true;
        }

        npc.GetGlobalNPC<Rad2Tick>().Radded2 = true;


    }

    public class Rad2Player : ModPlayer
    {
        public bool Rad2;

        public override void ResetEffects()
        {
            Rad2 = false;
        }

        public override void UpdateBadLifeRegen()
        {
            if (Rad2)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 30;
            }
        }
    }
}