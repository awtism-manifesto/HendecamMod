using HendecamMod.Common.Systems.Players;
using HendecamMod.Common.Utilities;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class AnkleMonitor : ModItem
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
        target.ankleMonitor = true;
        player.extraFall += 25;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.IronCrate);
        recipe.AddIngredient(ItemID.Ruby);
        recipe.AddIngredient<Polymer>(10);
        recipe.AddTile(TileID.HeavyWorkBench);
        recipe.Register();
    }
}