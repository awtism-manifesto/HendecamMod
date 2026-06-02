using HendecamMod.Content.Buffs;

namespace HendecamMod.Content.Projectiles.Items;

public class Phlinx : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 12;
        Main.projPet[Projectile.type] = true;

        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        for (int i = 0; i < 7; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.RedTorch);
            dust2.noGravity = true;
            dust2.velocity *= 6.75f;
            dust2.scale *= 1.15f;
        }
    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {

        modifiers.SourceDamage = modifiers.SourceDamage + (target.defense * 0.0375f);
    }
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.FlinxMinion);
        AIType = ProjectileID.FlinxMinion;

        Projectile.netImportant = true;
        Projectile.width = 39;
        Projectile.height = 39;
        Projectile.timeLeft = 13000;
        Projectile.friendly = true;
        Projectile.ignoreWater = true;
        Projectile.minion = true;
        Projectile.minionSlots = 1f;
        Projectile.DamageType = DamageClass.Summon;

        Projectile.penetrate = -1;
    }

    public override bool? CanCutTiles()
    {
        return false;
    }

    public override bool MinionContactDamage()
    {
        return true;
    }

    public override void AI()
    {
        Player owner = Main.player[Projectile.owner];

        if (!CheckActive(owner))
        {
        }
    }

  

    private bool CheckActive(Player owner)
    {
        if (owner.dead || !owner.active)
        {
            owner.ClearBuff(BuffType<PhlinxBuff>());

            return false;
        }

        if (owner.HasBuff(BuffType<PhlinxBuff>()))
        {
            Projectile.timeLeft = 3;
        }

        return true;
    }
}