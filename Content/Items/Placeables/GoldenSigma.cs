using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Placeables
{
    public class GoldenSigma : ModItem
    {
        public override void SetDefaults()
        {
            // Vanilla has many useful methods like these, use them! This substitutes setting Item.createTile and Item.placeStyle as well as setting a few values that are common across all placeable items
            Item.DefaultToPlaceableTile(ModContent.TileType<GoldenSigmaPlaced>());

            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Orange;
            Item.value = 225000000;

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<BlankCanvas>(1);
            recipe.AddIngredient(ItemID.GoldBar, 4194304);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient<BlankCanvas>(1);
            recipe.AddIngredient(ItemID.PlatinumBar, 4194304);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }
    }
}