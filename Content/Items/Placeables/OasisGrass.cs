using HendecamMod.Content.Tiles.Blocks;

namespace HendecamMod.Content.Items.Placeables;

public class OasisGrass : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<OasisGrassPlaced>());
        Item.width = 12;
        Item.height = 12;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.GrassSeeds);
        recipe.AddTile(TileID.WorkBenches);
        recipe.AddCondition(Condition.NearWater);
        recipe.Register();
    }
}