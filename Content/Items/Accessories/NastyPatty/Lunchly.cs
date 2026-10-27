using HendecamMod.Common.Utilities;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class Lunchly : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 1000);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
    }

    public override void UpdateEquip(Player player)
    {
        var target = player.GetHCPlayer();
        target.lunchly = true;
        player.GetDamage(DamageClass.Generic) += 0.33f;
        player.jumpSpeedBoost += 0.7f;

    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<NostalgicDiamond>();
        recipe.AddIngredient<AlphaMaleChecklist>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}