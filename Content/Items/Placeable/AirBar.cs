using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Placeable
{
    public class AirBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
            ItemID.Sets.SortingPriorityMaterials[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.AirBar>());
            Item.width = 20;
            Item.height = 20;
            Item.value = 1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            if (ModLoader.TryGetMod("OxygenOreMod", out Mod OxygenOreMod) && OxygenOreMod.TryFind<ModItem>("OxygenOre", out ModItem OxygenOre))
            {
                recipe = CreateRecipe();
                recipe.AddIngredient(OxygenOre, 2);
                recipe.AddTile(TileID.Furnaces);
                recipe.Register();
            }
            else
            {
                recipe = CreateRecipe();
                recipe.AddTile(TileID.Furnaces);
                recipe.Register();
            }
        }
    }
}