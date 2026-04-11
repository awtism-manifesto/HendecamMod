using HendecamMod.Content.Tiles.Walls;

namespace HendecamMod.Content.Items.Placeables;

public class AirBrickWall : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 400;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableWall(WallType<LimestoneBrickWallPlaced>());
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(4);
        recipe.AddIngredient<AirBrick>();
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}