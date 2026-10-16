using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Dusts;
using HendecamMod.Content.Global;
using Terraria.DataStructures;
using static HendecamMod.Content.Items.Accessories.IronLung;

namespace HendecamMod.Content.Projectiles.Items.VapeProjectiles;

// This example is similar to the Wooden Arrow projectile
public class GalaxyVapeSmoke : ModProjectile
{
    public override void SetStaticDefaults()
    {
       
    }

    public override void SetDefaults()
    {
        Projectile.width = 42; // The width of projectile hitbox
        Projectile.height = 42; // The height of projectile hitbox
        Projectile.usesLocalNPCImmunity = true;
        Projectile.penetrate = 12;
        Projectile.localNPCHitCooldown = 11;
        Projectile.tileCollide = false;
        Projectile.friendly = true;
        Projectile.DamageType = GetInstance<StupidDamage>();
        Projectile.timeLeft = 236;
        Projectile.GetGlobalProjectile<VapeMark>().VapeProj = true;
        Projectile.extraUpdates = 1;
    }
    public override void OnSpawn(IEntitySource source)
    {
        var vapeMark = Projectile.GetGlobalProjectile<VapeMark>();
        vapeMark.VapeProj = true;
        vapeMark.DustScale = 3.05f;


    }
    public override void AI()
    {
        Player player = Main.player[Projectile.owner];

        if (player.GetModPlayer<IronLungPlayer>().IronLungs == true)
        {
            Projectile.extraUpdates = 3;
        }

        // --- Spiral parameters (tweak these to taste) ---
         float angularSpeed = MathHelper.ToRadians(6);   // how fast it rotates around player
        const float radiusGrowth = 1.75f;                       // how fast it expands outward
        const float maxRadius = 5550f;                        // cap so it doesn't fly off forever
        const float followSpeed = 25f;                         // how strongly it snaps to target position

        // ai[0] = current angle, ai[1] = current radius
        // Initialize on first tick based on spawn position
        if (Projectile.localAI[0] == 0f)
        {
            Vector2 spawnOffset = Projectile.Center - player.Center;
            Projectile.ai[0] = spawnOffset.ToRotation();       // start angle
            Projectile.ai[1] = Math.Max(spawnOffset.Length(), 20f); // start radius
            Projectile.localAI[0] = 1f;
        }

        // Advance the spiral
        Projectile.ai[0] += angularSpeed;
        Projectile.ai[1] += radiusGrowth;
        if (Projectile.ai[1] > maxRadius)
            Projectile.ai[1] = maxRadius;

        // Compute the target position on the spiral
        Vector2 offset = Projectile.ai[0].ToRotationVector2() * Projectile.ai[1];
        Vector2 targetPos = player.Center + offset;

        // Steer the projectile toward that target position
        Vector2 toTarget = targetPos - Projectile.Center;
        Projectile.velocity = Vector2.Lerp(Projectile.velocity, toTarget * 0.2f, 0.375f);
        if (Projectile.velocity.Length() > followSpeed)
            Projectile.velocity = Vector2.Normalize(Projectile.velocity) * followSpeed;

        // Rotate sprite to face travel direction
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

        // --- Dust (unchanged, just kept your visuals) ---
        for (int i = 0; i < 2; i++)
        {
            float posOffsetX = 0f;
            float posOffsetY = 0f;
            if (i == 1)
            {
                posOffsetX = Projectile.velocity.X * 2.5f;
                posOffsetY = Projectile.velocity.Y * 2.5f;
            }

            Dust fireDust = Dust.NewDustDirect(
                new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f,
                Projectile.width - 10, Projectile.height - 10,
                DustID.Vortex, 0f, 0f, 100, Color.AliceBlue, 1.25f);
            fireDust.fadeIn = 0.1f + Main.rand.Next(2) * 0.1f;
            fireDust.noGravity = true;
            fireDust.velocity *= 1.33f;

            Dust fire2Dust = Dust.NewDustDirect(
                new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f,
                Projectile.width - 10, Projectile.height - 10,
                DustID.Torch, 0f, 0f, 100, default, 0.75f);
            fire2Dust.fadeIn = 0.05f + Main.rand.Next(1) * 0.05f;
            fire2Dust.noGravity = true;
            fire2Dust.velocity *= 1.05f;

            Dust fire22Dust = Dust.NewDustDirect(
                new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f,
                Projectile.width - 10, Projectile.height - 10,
                DustType<BlueLycopiteDust>(), 0f, 0f, 100, default, 0.1f);
            fire22Dust.fadeIn = 0.05f + Main.rand.Next(1) * 0.05f;
            fire22Dust.noGravity = true;
            fire22Dust.velocity *= 1.5f;
        }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
       

        Projectile.damage = (int)(Projectile.damage * 0.95f);
    }

   
}