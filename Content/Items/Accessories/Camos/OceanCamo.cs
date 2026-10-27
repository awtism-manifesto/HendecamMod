using HendecamMod.Common.Utilities;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Accessories.Camos;

public class OceanCamo : ModItem
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
      
        recipe.AddIngredient(ItemID.PalmWood, 20);
        recipe.AddIngredient(ItemID.SandBlock, 10);
        recipe.AddIngredient(ItemID.WaterBucket, 5);
        recipe.AddIngredient(ItemID.Silk, 5);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (player.ZoneBeach)

        {
            player.GetHCPlayer().isCamo = true;
        }

    }
}

