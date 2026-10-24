using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class ChlorophyteCube : ModItem
{
    public override void SetDefaults()
    {
      
        Item.width = 26; 
        Item.height = 26; 
        Item.rare = ItemRarityID.Lime; 
        Item.value = 285000;
        Item.maxStack = 1;
        Item.accessory = true;
        Item.defense = 5;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.statLifeMax2 = player.statLifeMax2 + 75;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<CubicMold>();
        recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}