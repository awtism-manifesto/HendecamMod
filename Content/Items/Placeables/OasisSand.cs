using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using HendecamMod.Content.Tiles.Blocks;

namespace HendecamMod.Content.Items.Placeables
{
    public class OasisSand : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<OasisSandPlaced>());
            Item.width = 12;
            Item.height = 12;

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SandBlock, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddCondition(Condition.NearWater);
            recipe.Register();
        }
    }
}
