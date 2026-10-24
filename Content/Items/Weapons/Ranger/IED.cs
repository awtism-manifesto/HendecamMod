using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;

namespace HendecamMod.Content.Items.Weapons.Ranger;

public class IED : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.damage = 66; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
        Item.DamageType = DamageClass.Ranged;
        Item.useTime = 32;
        Item.useAnimation = 32;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.width = 13;
        Item.height = 13;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
        Item.knockBack = 7f;
        Item.value = 22000;
        Item.rare = ItemRarityID.LightRed;
        Item.shoot = ProjectileType<IedThrown>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 13.25f; // The speed of the projectile.

        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica))
        {
            Item.DamageType = DamageClass.Throwing;
        }
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(50);
        recipe.AddIngredient(ItemID.CobaltBar, 2);
        recipe.AddIngredient(ItemID.Wire, 3);
        recipe.AddIngredient(ItemID.Dynamite, 3);
        recipe.AddIngredient<RefinedOil>(5);
        recipe.Register();
        recipe = CreateRecipe(50);
        recipe.AddIngredient(ItemID.PalladiumBar, 2);
        recipe.AddIngredient(ItemID.Wire, 3);
        recipe.AddIngredient(ItemID.Dynamite, 3);
        recipe.AddIngredient<RefinedOil>(5);
        recipe.Register();
    }
}