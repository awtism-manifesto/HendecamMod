using System.Collections.Generic;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items.Weapons.Ammo;

public class CIAJournalismAward : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.damage = 22; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
        Item.DamageType = DamageClass.Ranged;
        Item.ArmorPenetration = 10;
        Item.width = 16;
        Item.height = 16;
        Item.maxStack = 1;
        Item.consumable = false; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
        Item.knockBack = 2.75f;
        Item.value = 3250000;
        Item.rare = ItemRarityID.Red;
        Item.shoot = ProjectileType<CiaSpawn>();
        Item.shootSpeed = 5.25f; // The speed of the projectile.
        Item.ammo = AmmoID.Bullet; // The ammo class this ammo belongs to.
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }
}