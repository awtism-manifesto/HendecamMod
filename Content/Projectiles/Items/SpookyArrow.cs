namespace HendecamMod.Content.Projectiles.Items;

// This example is similar to the Wooden Arrow projectile
public class SpookyArrow : ModProjectile
{
    private int nextSpawnTick;
    private int tickCounter;

    public override void SetStaticDefaults()
    {
        // If this arrow would have strong effects (like Holy Arrow pierce), we can make it fire fewer projectiles from Daedalus Stormbow for game balance considerations like this:
        //ProjectileID.Sets.FiresFewerFromDaedalusStormbow[Type] = true;
    }

    public override void SetDefaults()
    {
        Projectile.width = 12; // The width of projectile hitbox
        Projectile.height = 12; // The height of projectile hitbox
        Projectile.aiStyle = 1;
        Projectile.arrow = true;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.timeLeft = 275;
        AIType = ProjectileID.Bullet;
        Projectile.extraUpdates = 1;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.OnFire3, 360);
    }

    public override void AI()
    {
       

        if (nextSpawnTick == 0)
        {
            nextSpawnTick = 3;
        }

        tickCounter++;

        if (tickCounter >= nextSpawnTick)
        {
            Vector2 velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(4.75f));
            Vector2 Peanits = Projectile.Center - new Vector2(0, 0);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits, velocity,
                ModContent.ProjectileType<SpookySpark>(), (int)(Projectile.damage * 0.38f), Projectile.knockBack, Projectile.owner);
            Vector2 velocity2 = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(1));
            Vector2 Peanits2 = Projectile.Center - new Vector2(0, 0);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits2, velocity2,
                ModContent.ProjectileType<SpookySpark>(), (int)(Projectile.damage * 0.38f), Projectile.knockBack, Projectile.owner);
            Vector2 velocity3 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(-4.75f));
            Vector2 Peanits3 = Projectile.Center - new Vector2(0, 0);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits3, velocity3,
                ModContent.ProjectileType<SpookySpark>(), (int)(Projectile.damage * 0.38f), Projectile.knockBack, Projectile.owner);

            tickCounter = 0;
            nextSpawnTick = 24; 
            Projectile.netUpdate = true;
        }
    }
}