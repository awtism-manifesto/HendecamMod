using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Blocks;

namespace HendecamMod.Content.Items.Placeables;

public class AncientCobaltBrick : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<AncientCobaltBrickPlaced>());
        Item.width = 12;
        Item.height = 12;
        Item.rare = ItemRarityID.Orange;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(5);
        recipe.AddIngredient<AncientCobaltOre>();
        recipe.AddIngredient(ItemID.StoneBlock, 5);
        recipe.AddTile(TileID.Furnaces);
        recipe.Register();
    }
}