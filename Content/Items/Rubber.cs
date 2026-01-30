using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items;

public class Rubber : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.buyPrice(silver: 3);
        Item.consumable = false;
        Item.maxStack = 9999;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(2);
        recipe.AddIngredient<PoorMahogany>(9);
        recipe.AddTile(TileID.CookingPots);
        recipe.Register();
        recipe = CreateRecipe(2);
        recipe.AddIngredient<PoorMahogany>();
        recipe.AddIngredient<Polymer>();
        recipe.AddTile(TileID.CookingPots);
        recipe.Register();
    }
}