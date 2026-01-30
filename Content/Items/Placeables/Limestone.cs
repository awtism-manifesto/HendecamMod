using HendecamMod.Content.Tiles.Blocks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Placeables;

public class Limestone : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<LimestonePlaced>());
        Item.width = 12;
        Item.height = 12;

    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.StoneBlock, 1);
        recipe.AddIngredient(ItemID.Seashell, 1);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient<LimestoneWall>(4);
        recipe.Register();
    }
}
