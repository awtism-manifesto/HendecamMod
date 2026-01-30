using HendecamMod.Content.DamageClasses;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Poop;

// This example is similar to the Wooden Arrow item
public class PoopArrow : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.width = 14;
        Item.height = 36;

        Item.damage = 9; // Keep in mind that the arrow's final damage is combined with the bow weapon damage.
        Item.DamageType = ModContent.GetInstance<RangedStupidDamage>();

        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.knockBack = 1.5f;
        Item.value = Item.sellPrice(copper: 1);
        Item.shoot = ModContent.ProjectileType<PoopArrowProj>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 1f; // The speed of the projectile.
        Item.ammo = AmmoID.Arrow; // The ammo class this ammo belongs to.
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(100);
        recipe.AddIngredient(ItemID.PoopBlock);
        recipe.AddIngredient(ItemID.WoodenArrow, 100);
        recipe.Register();
    }
}