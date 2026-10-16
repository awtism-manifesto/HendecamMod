using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Blocks;

namespace HendecamMod.Content.Items.Placeables;

public class DarkPlasticBlock : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(TileType<DarkPlasticBlockTile>());
        Item.width = 12;
        Item.height = 12;
        Item.rare = ItemRarityID.Blue;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(5);
        recipe.AddIngredient<PlasticScrap>(4);
        recipe.AddIngredient<Polymer>();
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}