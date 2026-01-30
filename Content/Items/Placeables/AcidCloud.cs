namespace HendecamMod.Content.Items.Placeables;

public class AcidCloud : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Blocks.AcidCloudPlaced>());
        Item.width = 12;
        Item.height = 12;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Cloud);
        recipe.AddTile(TileID.SkyMill);
        recipe.Register();
    }
}