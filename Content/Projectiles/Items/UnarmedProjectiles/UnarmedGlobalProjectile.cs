using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Enemies;
using HendecamMod.Content.Projectiles.Items;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;

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