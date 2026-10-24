using System.Collections.Generic;

namespace HendecamMod.Content.Items.Placeables;

public class WarIsOver : ModItem
{
    public override void SetDefaults()
    {
        // Vanilla has many useful methods like these, use them! This substitutes setting Item.createTile and Item.placeStyle as well as setting a few values that are common across all placeable items
        Item.DefaultToPlaceableTile(TileType<Tiles.Furniture.FabsolDeadPlaced>());

        Item.width = 32;
        Item.height = 32;
        Item.rare = ItemRarityID.Blue;
        Item.value = 350;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<Materials.BlankCanvas>();
        recipe.AddIngredient(ItemID.AshBlock, 10);

        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}