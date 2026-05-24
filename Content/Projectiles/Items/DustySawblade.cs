using Terraria.Audio;

namespace HendecamMod.Content.Projectiles.Items;

// This example is similar to the Wooden Arrow projectile
public class DustySawblade : ModProjectile
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
        Projectile.width = 26; // The width of projectile hitbox
        Projectile.height = 26; // The height of projectile hitbox
        Projectile.aiStyle = -1;
       
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.timeLeft = 275;
        AIType = ProjectileID.Bullet;
        Projectile.extraUpdates = 1;
        Projectile.penetrate = 7;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        SoundEngine.PlaySound(new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/SawbladeHit")
        {
            Volume = 0.44f,
            PitchVariance = 0.2f,
            MaxInstances = 33,
        });
        Projectile.damage = (int)(Projectile.damage * 0.75f);
        for (int i = 0; i < 7; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Blood);
            dust.noGravity = true;
            dust.velocity *= 7.75f;
            dust.scale *= 1.33f;
        }
    }

    public override void AI()
    {
        Projectile.rotation += 0.25f;

        if (nextSpawnTick == 0)
        {
            nextSpawnTick = 1;
        }

        tickCounter++;

        if (tickCounter >= nextSpawnTick)
        {
            
            Vector2 velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(2f));
            Vector2 Peanits = Projectile.Center - new Vector2(0, 0);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits, velocity,
                ProjectileType<SawdustProj>(), (int)(Projectile.damage * 0.375f), Projectile.knockBack, Projectile.owner);
           

            tickCounter = 0;
            nextSpawnTick = 29; 
            Projectile.netUpdate = true;
        }
    }
}