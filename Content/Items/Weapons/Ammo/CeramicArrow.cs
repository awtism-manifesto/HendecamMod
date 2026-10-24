using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons.Ammo;

// This example is similar to the Wooden Arrow item
public class CeramicArrow : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.width = 14;
        Item.height = 36;

        Item.damage = 8; // Keep in mind that the arrow's final damage is combined with the bow weapon damage.
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ItemRarityID.Orange;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.knockBack = 1.5f;
        Item.value = Item.sellPrice(copper: 19);
        Item.shoot = ProjectileType<Projectiles.CeramArrow>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 1.8f; // The speed of the projectile.
        Item.ammo = AmmoID.Arrow; // The ammo class this ammo belongs to.
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(63);
        recipe.AddIngredient<CeramicSheet>();
        recipe.AddIngredient(ItemID.WoodenArrow, 63);
        recipe.Register();
    }
}