using HendecamMod.Content.NPCs;
namespace HendecamMod.Content.Buffs;

public class Splinters : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }

   

    public override void Update(NPC npc, ref int buffIndex)
    {
        if (Main.rand.NextBool(10)) // 1-in-3 chance every tick
        {
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.WoodFurniture,
                npc.velocity.X * 1.29f, npc.velocity.Y * 1.29f, 70, default, 1.95f);
            Main.dust[dust].noGravity = true;
        }

       

        npc.GetGlobalNPC<HendecamGlobalNPC>().Splintering = true;
    }

   
}

