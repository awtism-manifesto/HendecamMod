using HendecamMod.Common.Utilities;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Placeables;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class HotWax : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 500);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
    }

    public override void UpdateEquip(Player player)
    {
        var target = player.GetHCPlayer();
        target.hotWax = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<SteelBar>(5);
        recipe.AddIngredient(ItemID.BeeWax, 30);
        recipe.AddIngredient(ItemID.MagmaStone);
        recipe.AddTile(TileID.HoneyDispenser);
        recipe.Register();
    }
}