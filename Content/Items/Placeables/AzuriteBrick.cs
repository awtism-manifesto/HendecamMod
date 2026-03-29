using HendecamMod.Content.Tiles.Blocks;

namespace HendecamMod.Content.Items.Placeables;

public class AzuriteBrick : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<AzuriteBrickPlaced>());
        Item.width = 12;
        Item.height = 12;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(5);
        recipe.AddIngredient<AzuriteOre>();
        recipe.AddIngredient(ItemID.StoneBlock, 5);
        recipe.AddTile(TileID.Furnaces);
        recipe.Register();
    }
}