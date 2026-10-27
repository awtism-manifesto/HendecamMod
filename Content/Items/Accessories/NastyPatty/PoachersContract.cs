using HendecamMod.Common.Utilities;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class PoachersContract : ModItem
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
        target.poachersContract = true;
        Lighting.AddLight(player.Center, 6.7f, 6.7f, 6.7f);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SoulofLight, 50);
        recipe.AddIngredient<Paper>();
        recipe.AddTile(TileID.Bookcases);
        recipe.Register();
    }
}