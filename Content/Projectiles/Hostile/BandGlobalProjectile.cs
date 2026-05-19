namespace HendecamMod.Content.Projectiles.Hostile;

public class BandGlobalProjectile : GlobalProjectile
    {
    public bool BandWeapon;

    public override bool InstancePerEntity => true;

    public override void AI(Projectile projectile)
        {
        if (BandWeapon)
            {
            // do stuff
            }
        }
    }