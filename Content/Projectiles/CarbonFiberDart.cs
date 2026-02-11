using Terraria.Audio;

namespace HendecamMod.Content.Projectiles;

public class CarbonFiberDart : ModProjectile
{
   

   

    public override void SetDefaults()
    {
        Projectile.width = 14;
        Projectile.height = 14;
        Projectile.friendly = true;
        Projectile.penetrate = 20; // Infinite penetration so that the blast can hit all enemies within its radius.

        Projectile.light = 0.1f; // How much light emit around the projectile
        Projectile.usesLocalNPCImmunity = true;
        Projectile.timeLeft = 360;
        Projectile.extraUpdates = 1;
       
        Projectile.aiStyle = 1;
        Projectile.alpha = 2;
        Projectile.localNPCHitCooldown = -1;
        Projectile.hostile = true;
        AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        // Rockets use explosive AI, ProjAIStyleID.Explosive (16). You could use that instead here with the correct AIType.
        // But, using our own AI allows us to customize things like the dusts that the rocket creates.
        // Projectile.aiStyle = ProjAIStyleID.Explosive;
        // AIType = ProjectileID.RocketI;
    }

    
    
}
