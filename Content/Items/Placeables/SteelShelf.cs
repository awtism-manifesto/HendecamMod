using HendecamMod.Content.Tiles.Furniture;
using Terraria;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Placeables;

public class SteelShelf : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 200;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<SteelShelfPlaced>());
        Item.width = 8;
        Item.height = 10;
    }

    // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(2);
        recipe.AddIngredient<SteelBar>();
        recipe.Register();
    }
}