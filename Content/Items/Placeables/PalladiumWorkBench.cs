using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Placeables
{
    public class PalladiumWorkBench : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.PalladiumWorkBenchPlaced>());
            Item.width = 28;
            Item.height = 14;
            Item.value = 150;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.CraftingObjects;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PalladiumBar, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}