using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class MorbiumCube : ModItem
{
  

    public override void SetDefaults()
    {
        Item.width = 26;
        Item.height = 26;
        Item.rare = ItemRarityID.Yellow; 
        Item.value = 266000;
        Item.maxStack = 1;
        Item.accessory = true;
        Item.defense = 25;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
       
        player.maxFallSpeed = player.maxFallSpeed * -1.1f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<CubicMold>();
        recipe.AddIngredient<Placeables.MorbiumBar>(12);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}