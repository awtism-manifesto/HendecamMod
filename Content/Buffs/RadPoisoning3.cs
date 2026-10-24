using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.Dusts;
using HendecamMod.Content.Global;
using HendecamMod.Content.NPCs;

namespace HendecamMod.Content.Buffs;

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
        player.GetModPlayer<HendecamPlayer>().Rad3 = true;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        if (Main.rand.NextBool(5)) // 1-in-3 chance every tick
        {
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustType<AstatineDust>(),
                npc.velocity.X * 1.29f, npc.velocity.Y * 1.29f, 70, default, 1.95f);
            Main.dust[dust].noGravity = true;
        }

        if (Main.rand.NextBool(5)) // 1-in-3 chance every tick
        {
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustType<AstatineDust>(),
                npc.velocity.X * 1.29f, npc.velocity.Y * 1.29f, 70, default, 2.15f);
            Main.dust[dust].noGravity = true;
        }

        npc.GetGlobalNPC<HendecamGlobalNPC>().Radded3 = true;
    }
}