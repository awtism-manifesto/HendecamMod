using HendecamMod.Common.Utilities;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class GasMask : ModItem
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
        target.gasMask = true;
        player.breathEffectiveness += 2f;

    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DivingHelmet);
        recipe.AddIngredient<Rubber>(25);
        recipe.AddTile(TileID.Solidifier);
        recipe.Register();
    }
}