using System.Collections.Generic;

namespace HendecamMod.Content.Items.Placeables;

public class SteelAnvil : ModItem
{
    public override void SetDefaults()
    {
        // Common Properties
        Item.width = 20; // Hitbox width of the item.
        Item.height = 20; // Hitbox height of the item.
        Item.rare = ItemRarityID.White; // The color that the item's name will be in-game.
        Item.value = 5000;
        Item.maxStack = 9999;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.useTime = 15;
        Item.DefaultToPlaceableTile(TileType<Tiles.Furniture.SteelAnvilPlaced>());
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<SteelBar>(5);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}