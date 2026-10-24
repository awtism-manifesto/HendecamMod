using System.Collections.Generic;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items.Weapons.Ammo;

public class DirtBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.damage = 2; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
        Item.DamageType = DamageClass.Ranged;

        Item.width = 16;
        Item.height = 16;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
        Item.knockBack = 0.5f;
        Item.value = 0;

        Item.shoot = ProjectileType<DirtBulletProj>();
        Item.shootSpeed = 3.5f; // The speed of the projectile.
        Item.ammo = AmmoID.Bullet; // The ammo class this ammo belongs to.
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(50);
        recipe.AddIngredient(ItemID.DirtBlock, 5);

        recipe.Register();
        recipe = CreateRecipe(50);
        recipe.AddIngredient<DirtBar>();

        recipe.Register();
    }
}