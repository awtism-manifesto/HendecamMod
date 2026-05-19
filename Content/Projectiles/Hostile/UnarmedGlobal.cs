namespace HendecamMod.Content.Projectiles.Hostile;

public class UnarmedGlobal : GlobalProjectile
    {
    public bool UnarmedWeapon;

    public override bool InstancePerEntity => true;

    public override void AI(Projectile projectile)
        {
        if (UnarmedWeapon)
            {
            // do stuff
            }
        }
    }