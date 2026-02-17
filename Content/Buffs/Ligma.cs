using HendecamMod.Content.Global;

namespace HendecamMod.Content.Buffs;

public class Ligma : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
        BuffID.Sets.IsATagBuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<LigmaPlayer>().Ligma = true;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        if (Main.rand.NextBool(5)) // 1-in-3 chance every tick
        {
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Poop,
                npc.velocity.X * 2.9f, npc.velocity.Y * 2.9f, 70, default, 2.25f);
            Main.dust[dust].noGravity = true;
        }

        if (Main.rand.NextBool(5)) // 1-in-3 chance every tick
        {
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood,
                npc.velocity.X * 2.9f, npc.velocity.Y * 2.9f, 70, default, 2.25f);
            Main.dust[dust].noGravity = true;
        }

        npc.GetGlobalNPC<WhoTfIsSteveJobs>().DyingOfLigma = true;
    }

    public class LigmaPlayer : ModPlayer
    {
        public bool Ligma;

        public override void ResetEffects()
        {
            Ligma = false;
        }

        public override void UpdateBadLifeRegen()
        {
            if (Ligma)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= (int)(1500f);
            }
        }
    }
}