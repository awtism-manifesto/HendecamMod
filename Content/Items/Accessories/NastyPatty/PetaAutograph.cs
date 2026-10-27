using HendecamMod.Common.Utilities;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class PetaAutograph : ModItem
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
        target.petaAutograph = true;
        player.lifeRegen += 6;
        Lighting.AddLight(player.Center, 6.7f, 6.7f, 6.7f);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<KillShelterPermit>();
        recipe.AddIngredient<PoachersContract>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}