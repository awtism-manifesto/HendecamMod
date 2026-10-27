using HendecamMod.Common.Utilities;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class TornFilter : ModItem
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
        target.tornFilter = true;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<GasMask>();
        recipe.AddIngredient<BrokenMouse>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}