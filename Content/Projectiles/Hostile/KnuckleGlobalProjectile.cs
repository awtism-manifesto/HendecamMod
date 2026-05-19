namespace HendecamMod.Content.Projectiles.Hostile;

public class KnuckleGlobalProjectile : GlobalProjectile
    {
    public bool KnuckleWeapon;

    public override bool InstancePerEntity => true;

    public override void AI(Projectile projectile)
        {
        if (KnuckleWeapon)
            {
            // do stuff
            }
        }
    }