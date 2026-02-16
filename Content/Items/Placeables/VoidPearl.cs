using HendecamMod.Content.Tiles.Furniture;

namespace HendecamMod.Content.Items.Placeables;

public class VoidPearl : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<TheDarkKnight>());

        Item.width = 32;
        Item.height = 32;
        Item.rare = ItemRarityID.White;
        Item.value = 25000;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(100);
        recipe.AddIngredient<AirBar>(100);
        recipe.AddIngredient<LoreAccurateBlackshard>();
        recipe.Register();
    }
}