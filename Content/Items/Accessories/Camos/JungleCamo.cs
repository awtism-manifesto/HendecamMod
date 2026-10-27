using HendecamMod.Common.Utilities;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Accessories.Camos;

public class JungleCamo : ModItem
{
    
    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Blue;
        Item.value = 22000;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
      
        recipe.AddIngredient(ItemID.RichMahogany, 20);
        recipe.AddIngredient(ItemID.BeeWax, 10);
        recipe.AddIngredient(ItemID.JungleSpores, 5);
        recipe.AddIngredient(ItemID.Silk, 5);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (player.ZoneJungle)
        {
            player.GetHCPlayer().isCamo = true;
        }

    }
}

