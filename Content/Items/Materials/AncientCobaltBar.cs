using System.Collections.Generic;
using HendecamMod.Content.Tiles;

namespace HendecamMod.Content.Items.Materials;

public class AncientCobaltBar : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.scale = 1f;
        Item.rare = ItemRarityID.Orange;
        Item.value = 9800;
        Item.maxStack = 9999;
        Item.DefaultToPlaceableTile(TileType<AncientCobaltBarPlaced>());
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<AncientCobaltOre>(3);
        recipe.AddTile(TileID.Furnaces);
        recipe.Register();
    }
}