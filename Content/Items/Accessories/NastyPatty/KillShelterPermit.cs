using HendecamMod.Common.Utilities;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class KillShelterPermit : ModItem
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
        target.killShelterPermit = true;
        player.lifeRegen += 6;

    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SoulofNight, 50);
        recipe.AddIngredient<Paper>();
        recipe.AddTile(TileID.Bookcases);
        recipe.Register();
    }
}