using HendecamMod.Content.Tiles.Blocks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Placeables;

public class BlackBrick : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<OrangeBrickPlaced>());
        Item.width = 12;
        Item.height = 12;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(2);
        recipe.AddIngredient<Materials.CoalLump>();
        recipe.AddIngredient(ItemID.StoneBlock);
        recipe.AddTile(TileID.Furnaces);
        recipe.Register();
    }
}