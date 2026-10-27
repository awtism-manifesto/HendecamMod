using HendecamMod.Common.Systems.Players;
using HendecamMod.Common.Utilities;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Accessories.Camos;

public class CorruptionCamo : ModItem
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
      
        recipe.AddIngredient(ItemID.Ebonwood, 20);
        recipe.AddIngredient(ItemID.EbonstoneBlock, 10);
        recipe.AddIngredient(ItemID.ShadowScale, 5);
        recipe.AddIngredient(ItemID.Silk, 5);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (player.ZoneCorrupt)
        {
            player.GetHCPlayer().isCamo = true;
        }

    }
}