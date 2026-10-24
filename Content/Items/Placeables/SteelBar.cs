using System.Collections.Generic;

namespace HendecamMod.Content.Items.Placeables;

public class SteelBar : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 20; // Hitbox width of the item.
        Item.height = 20; // Hitbox height of the item.
        Item.rare = ItemRarityID.White; // The color that the item's name will be in-game.
        Item.value = 1215;
        Item.maxStack = 9999;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.useTime = 15;
        Item.DefaultToPlaceableTile(TileType<Tiles.Furniture.SteelBarPlaced>());
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(2);
        recipe.AddIngredient<Materials.CoalLump>();
        recipe.AddRecipeGroup("IronBar");
        recipe.AddTile(TileID.Furnaces);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient<SteelShelf>(2);
        recipe.Register();
    }
}