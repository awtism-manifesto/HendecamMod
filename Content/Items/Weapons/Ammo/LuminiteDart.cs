using System.Collections.Generic;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items.Weapons.Ammo;

public class LuminiteDart : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.damage = 1; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
        Item.DamageType = DamageClass.Ranged;

        Item.width = 13;
        Item.height = 13;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
        Item.knockBack = 3.5f;
        Item.value = 1100;
        Item.rare = ItemRarityID.Cyan;
        Item.shoot = ProjectileType<LunarDart>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 8.25f; // The speed of the projectile.
        Item.ammo = AmmoID.Dart; // The ammo class this ammo belongs to.
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(444);
        recipe.AddIngredient(ItemID.LunarBar);

        recipe.Register();
    }
}