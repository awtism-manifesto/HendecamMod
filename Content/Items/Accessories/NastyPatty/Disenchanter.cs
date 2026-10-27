using HendecamMod.Common.Utilities;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class Disenchanter : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 1000);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
        Item.defense = 50;
    }

    public override void UpdateEquip(Player player)
    {
        var target = player.GetHCPlayer();
        target.disenchanter = true;
        player.GetArmorPenetration(DamageClass.Generic) += 40f;

    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<LeadPlating>();
        recipe.AddIngredient<Grindstone>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}