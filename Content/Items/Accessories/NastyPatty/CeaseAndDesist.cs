using HendecamMod.Common.Utilities;
using HendecamMod.Content.Buffs;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class CeaseAndDesist : ModItem
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
        target.ceaseDesist = true;
        player.GetCritChance(DamageClass.Generic) += 25f;
        player.moveSpeed += 1f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<NintendoPatent>();
        recipe.AddIngredient<TornSaddle>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}