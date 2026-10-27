using HendecamMod.Common.Utilities;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class MoldyCheese : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 4000);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
        Item.defense = 50;
    }

    public override void UpdateEquip(Player player)
    {
        var target = player.GetHCPlayer();
        target.moldyCheese = true;
        player.jumpSpeedBoost += 0.7f;
        player.GetCritChance(DamageClass.Generic) += 25f;
        player.GetArmorPenetration(DamageClass.Generic) += 40f;
        player.GetDamage(DamageClass.Generic) += 0.33f;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<Disenchanter>();
        recipe.AddIngredient<Lunchly>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}