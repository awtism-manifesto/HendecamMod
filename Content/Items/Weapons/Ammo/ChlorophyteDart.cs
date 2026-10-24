using System.Collections.Generic;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items.Weapons.Ammo;

public class ChlorophyteDart : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.damage = 9; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
        Item.DamageType = DamageClass.Ranged;

        Item.width = 13;
        Item.height = 13;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
        Item.knockBack = 3.5f;
        Item.value = 235;
        Item.rare = ItemRarityID.Lime;
        Item.shoot = ProjectileType<ChloroDart>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 8.25f; // The speed of the projectile.
        Item.ammo = AmmoID.Dart; // The ammo class this ammo belongs to.
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(125);
        recipe.AddIngredient(ItemID.ChlorophyteBar);

        recipe.Register();
    }
}