using HendecamMod.Content.Dusts;
using HendecamMod.Content.Global;

namespace HendecamMod.Content.Buffs;

public class RadPoisoning4 : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<Rad4Player>().Rad4 = true;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        if (Main.rand.NextBool(5)) // 1-in-3 chance every tick
        {
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustType<PromethiumDust>(),
                npc.velocity.X * 1.29f, npc.velocity.Y * 1.29f, 70, default, 1.95f);
            Main.dust[dust].noGravity = true;
        }

        if (Main.rand.NextBool(5)) // 1-in-3 chance every tick
        {
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustType<PromethiumDust>(),
                npc.velocity.X * 1.29f, npc.velocity.Y * 1.29f, 70, default, 2.25f);
            Main.dust[dust].noGravity = true;
        }

        npc.GetGlobalNPC<Rad4Tick>().Radded4 = true;
    }

    public class Rad4Player : ModPlayer
    {
        public bool Rad4;

        public override void ResetEffects()
        {
            Rad4 = false;
        }

        public override void UpdateBadLifeRegen()
        {
            if (Rad4)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= (int)(59.5f);
            }
        }
    }
}