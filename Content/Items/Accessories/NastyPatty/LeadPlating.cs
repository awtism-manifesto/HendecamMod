using HendecamMod.Common.Utilities;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class LeadPlating : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 500);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
        Item.defense = 50;
    }

    public override void UpdateEquip(Player player)
    {
        var target = player.GetHCPlayer();
        target.leadPlating = true;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.LeadBar, 150);
        recipe.AddTile(TileID.Hellforge);
        recipe.Register();
    }
}