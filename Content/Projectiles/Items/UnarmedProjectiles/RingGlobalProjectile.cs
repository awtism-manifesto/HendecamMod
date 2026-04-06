namespace HendecamMod.Content.Projectiles.Items.UnarmedProjectiles;

public class RingGlobalProjectile : GlobalProjectile
    {
    public bool RingWeapon;

    public override bool InstancePerEntity => true;

    public override void AI(Projectile projectile)
        {
        if (RingWeapon)
            {
            // do stuff
            }
        }
    }