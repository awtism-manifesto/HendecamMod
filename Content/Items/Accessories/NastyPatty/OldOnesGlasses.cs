using HendecamMod.Common.Utilities;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class OldOnesGlasses : ModItem
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
        target.oldOnesGlasses = true;
        player.statManaMax2 += 100;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DD2ElderCrystal, 2);
        recipe.AddIngredient(ItemID.DD2ElderCrystalStand);
        recipe.AddIngredient(ItemID.DefenderMedal, 15);
        recipe.AddTile(TileID.Tables);
        recipe.AddTile(TileID.Chairs);
        recipe.Register();
    }
}