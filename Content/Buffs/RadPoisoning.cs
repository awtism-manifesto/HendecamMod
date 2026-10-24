using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.Dusts;
using HendecamMod.Content.Global;
using HendecamMod.Content.NPCs;

namespace HendecamMod.Content.Buffs;

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
        player.GetModPlayer<HendecamPlayer>().Rad = true;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        if (Main.rand.NextBool(3))
        {
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustType<UraniumDust>(),
                npc.velocity.X * 0.69f, npc.velocity.Y * 0.69f, 70, default, 1.75f);
            Main.dust[dust].noGravity = true;
        }

        npc.GetGlobalNPC<HendecamGlobalNPC>().Radded = true;
    }
}