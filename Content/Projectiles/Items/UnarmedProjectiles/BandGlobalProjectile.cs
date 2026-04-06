namespace HendecamMod.Content.Projectiles.Items.UnarmedProjectiles;

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