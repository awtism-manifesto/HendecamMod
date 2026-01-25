using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Placeables;

public class DirtBar : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
        ItemID.Sets.SortingPriorityMaterials[Type] = 2;
    }
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.DirtBar>());
        Item.width = 20;
        Item.height = 20;
        Item.value = 1;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DirtBlock, 2);
        recipe.AddTile(TileID.Furnaces);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient<AirBar>(10);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}