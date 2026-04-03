namespace HendecamMod.Content.Buffs;

public class PyrrhicTag : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        Main.buffNoSave[Type] = true;
        Main.buffNoTimeDisplay[Type] = false;
        BuffID.Sets.IsATagBuff[Type] = true;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        if (Main.rand.NextBool(2))
        {
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Ichor,
                npc.velocity.X * 1.23f, npc.velocity.Y * 1.23f, 70, default, 2.33f);
            Main.dust[dust].noGravity = true;
        }
    }
}