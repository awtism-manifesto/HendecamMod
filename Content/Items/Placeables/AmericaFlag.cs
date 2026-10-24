using System.Collections.Generic;

namespace HendecamMod.Content.Items.Placeables;

public class AmericaFlag : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32; 
        Item.height = 32; 
        Item.rare = ItemRarityID.White;
        Item.value = 2400;
        Item.maxStack = 9999;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.autoReuse = true;
        Item.DefaultToPlaceableTile(TileType<Tiles.AmericaFlagPlaced>());
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<BlankFlag>();
        recipe.AddIngredient(ItemID.Wood, 2);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}