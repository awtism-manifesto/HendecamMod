using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Furniture;

namespace HendecamMod.Content.Items.Placeables;

public class GoldenSigma : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<GoldenSigmaPlaced>());

        Item.width = 32;
        Item.height = 32;
        Item.rare = ItemRarityID.Orange;
        Item.value = 225000000;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<BlankCanvas>();
        recipe.AddIngredient(ItemID.GoldBar, 4194304);
        recipe.AddTile(TileID.Loom);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient<BlankCanvas>();
        recipe.AddIngredient(ItemID.PlatinumBar, 4194304);
        recipe.AddTile(TileID.Loom);
        recipe.Register();
    }
}