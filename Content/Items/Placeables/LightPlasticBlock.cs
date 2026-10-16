using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Blocks;

namespace HendecamMod.Content.Items.Placeables;

public class LightPlasticBlock : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(TileType<LightPlasticBlockTile>());
        Item.width = 12;
        Item.height = 12;
        Item.rare = ItemRarityID.Blue;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(5);
        recipe.AddIngredient<PlasticScrap>(5);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}