using HendecamMod.Content.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Placeables;

public class VoidPearl : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<TheDarkKnight>());

        Item.width = 32;
        Item.height = 32;
        Item.rare = ItemRarityID.White;
        Item.value = 999;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<AirBar>(100);
        recipe.Register();
    }
}