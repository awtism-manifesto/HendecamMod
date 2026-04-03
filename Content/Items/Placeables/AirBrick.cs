using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Blocks;

namespace HendecamMod.Content.Items.Placeables;

public class AirBrick : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<AirBrickPlaced>());
        Item.width = 12;
        Item.height = 12;
        Item.rare = ItemRarityID.Orange;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(4);
        recipe.AddIngredient<AirBar>();
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
        recipe = CreateRecipe(1);
        recipe.AddIngredient<AirBrickWall>(4);
        recipe.Register();
    }
}