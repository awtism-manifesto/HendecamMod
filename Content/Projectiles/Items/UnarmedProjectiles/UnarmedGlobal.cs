namespace HendecamMod.Content.Projectiles.Items.UnarmedProjectiles;

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