using HendecamMod.Common.Utilities;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class NintendoPatent : ModItem
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
        target.nintendoPatent = true;
        player.GetCritChance(DamageClass.Generic) += 25f;

    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.GemBunnyDiamond, 5);
        recipe.AddIngredient(ItemID.PlumbersHat);
        recipe.AddIngredient(ItemID.PlumbersShirt);
        recipe.AddIngredient(ItemID.PlumbersPants);
        recipe.AddIngredient<Paper>();
        recipe.AddTile(TileID.Hellforge);
        recipe.Register();
    }
}