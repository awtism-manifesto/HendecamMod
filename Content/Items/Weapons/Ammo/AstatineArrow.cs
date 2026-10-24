using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons.Ammo;

// This example is similar to the Wooden Arrow item
public class AstatineArrow : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.width = 14;
        Item.height = 36;

        Item.damage = 25; // Keep in mind that the arrow's final damage is combined with the bow weapon damage.
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ItemRarityID.Red;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.knockBack = 7.5f;
        Item.value = 595;
        Item.shoot = ProjectileType<Projectiles.AstaArrowProj>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 9.4f; // The speed of the projectile.
        Item.ammo = AmmoID.Arrow; // The ammo class this ammo belongs to.
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(150);
        recipe.AddIngredient<AstatineBar>();
        recipe.AddIngredient(ItemID.WoodenArrow, 150);
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }
}