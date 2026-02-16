using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Dusts;
using Terraria;

namespace HendecamMod.Content.Buffs;

public class YelmutTag : ModBuff
{


    public override void SetStaticDefaults()
    {
        
        BuffID.Sets.IsATagBuff[Type] = true;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {

        npc.GetGlobalNPC<YelmutDebuff>().hasDebuff = true;
    }
}

public class YelmutDebuff : GlobalNPC //shoutout to Antibody of Awful Garbage Mod for this 
{
   
    public override bool InstancePerEntity => true;

    public bool hasDebuff;

    public override void ResetEffects(NPC npc)
    {
        hasDebuff = false;
    }
    public override void DrawEffects(NPC npc, ref Color drawColor)
    {
        if (hasDebuff)
        {
            if (Main.rand.NextBool(7))
            {
                int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<LycopiteDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, Color.Orange);
                Main.dust[dust].velocity *= 1.2f;
                Main.dust[dust].velocity.Y -= 0.5f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale *= 1.2f;
                Main.dust[dust].alpha /= 3;
               
            }
            if (Main.rand.NextBool(8))
            {
                int dust2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<PlutoniumDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, Color.LightPink);
                Main.dust[dust2].velocity *= 1.2f;
                Main.dust[dust2].velocity.Y -= 0.5f;
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].scale *= 1.2f;
                Main.dust[dust2].alpha /= 3;
            }
        }
    }
    // TODO: Inconsistent with vanilla, increasing damage AFTER it is randomised, not before. Change to a different hook in the future.
    // it's okay antibody you're still goated
    public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
    {
        // Only player attacks should benefit from this buff, hence the NPC and trap checks.
        if (hasDebuff && projectile.CountsAsClass<StupidDamage>() || (hasDebuff && projectile.DamageType.CountsAsClass<MeleeDamageClass>()))
        {
            modifiers.SourceDamage *= 1.15f;

           
            
        }
    }
    public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
    {
        if (hasDebuff && item.DamageType.CountsAsClass<StupidDamage>() || hasDebuff && item.DamageType.CountsAsClass<MeleeDamageClass>())
        {
            modifiers.SourceDamage *= 1.15f;

            

           
        }
    }
}