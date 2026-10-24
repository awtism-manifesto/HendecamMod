using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.VapeDyes.Red40VapeDye;

namespace HendecamMod.Content.Items.Accessories.VapeDyes;

public class Yellow5VapeDye : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Blue;
        Item.value = 60000;
      
    }
       public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.YellowDye);
        recipe.AddIngredient<Polymer>(15);
        recipe.AddIngredient<CrudeOil>(25);
        recipe.AddTile(TileID.DyeVat);
        recipe.Register();
    }
    public override void UpdateInventory(Player player)
    {
        player.GetModPlayer<VapeDyePlayer>().Yellow5Active = true;
    }
    public override void UpdateVanity(Player player)
    {
        player.GetModPlayer<VapeDyePlayer>().Yellow5Active = true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<VapeDyePlayer>().Yellow5Active = true;

    }
   
}