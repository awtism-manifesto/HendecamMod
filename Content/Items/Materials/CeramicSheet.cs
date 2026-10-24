using System.Collections.Generic;

namespace HendecamMod.Content.Items.Materials;

public class CeramicSheet : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32; 
        Item.height = 32;
        Item.scale = 1.22f;
        Item.rare = ItemRarityID.Orange; 
        Item.value = 500;
        Item.maxStack = 9999;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(5);

        recipe.AddIngredient(ItemID.Bone, 2);
        recipe.AddIngredient(ItemID.ClayBlock, 5);
        recipe.AddTile(TileID.Hellforge);
        recipe.Register();
    }
}