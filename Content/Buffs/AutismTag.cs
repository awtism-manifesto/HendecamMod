namespace HendecamMod.Content.Buffs;

public class AutismTag : ModBuff
{
    public static readonly int TagDamage = 3;

    public override void SetStaticDefaults()
    {
        // This allows the debuff to be inflicted on NPCs that would otherwise be immune to all debuffs.
        // Other mods may check it for different purposes.
        BuffID.Sets.IsATagBuff[Type] = true;
    }
}

public class SorryMirrorman : GlobalNPC
{
    public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
    {
        // Only player attacks should benefit from this buff, hence the NPC and trap checks.
        if (projectile.npcProj || projectile.trap )
            return;


        // SummonTagDamageMultiplier scales down tag damage for some specific minion and sentry projectiles for balance purposes.
        var projTagMultiplier = ProjectileID.Sets.SummonTagDamageMultiplier[projectile.type];
        if (npc.HasBuff<AutismTag>())
        {
            // Apply a flat bonus to every hit
            modifiers.FlatBonusDamage += AutismTag.TagDamage * projTagMultiplier;
        }

       
    }
}