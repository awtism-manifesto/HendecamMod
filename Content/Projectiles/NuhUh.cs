namespace HendecamMod.Content.Projectiles;

public class NuhUh : ModProjectile
{
    

    public override void SetDefaults()
    {
      
        Projectile.friendly = false; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
        Projectile.timeLeft = 1;
    }
}