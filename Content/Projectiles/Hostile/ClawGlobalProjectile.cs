namespace HendecamMod.Content.Projectiles.Hostile;

public class ClawGlobalProjectile : GlobalProjectile
    {
    public bool ClawWeapon;

    public override bool InstancePerEntity => true;

    public override void AI(Projectile projectile)
        {
        if (ClawWeapon)
            {
            // do stuff
            }
        }
    }