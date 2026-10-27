using HendecamMod.Common.Utilities;
using HendecamMod.Content.Buffs;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class PacifistsMark : ModItem
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
        target.pacifistsMark = true;
        player.statLifeMax2 += 150;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.FallenStar, 4);
        recipe.AddIngredient(ItemID.DemonHeart);
        recipe.AddTile(TileID.AdamantiteForge);
        recipe.Register();
    }
}