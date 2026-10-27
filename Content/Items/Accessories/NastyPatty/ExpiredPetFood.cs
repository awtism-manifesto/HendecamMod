using HendecamMod.Common.Utilities;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class ExpiredPetFood : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 8000);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
        Item.defense = 50;
    }

    public override void UpdateEquip(Player player)
    {
        var target = player.GetHCPlayer();
        target.expiredFood = true;
        player.lifeRegen += 6;
        Lighting.AddLight(player.Center, 6.7f, 6.7f, 6.7f);
        player.GetCritChance(DamageClass.Generic) += 25f;
        player.GetArmorPenetration(DamageClass.Generic) += 40f;
        player.moveSpeed += 1f;
        player.GetDamage(DamageClass.Generic) += 0.33f;

    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<ZoologistHateLetter>();
        recipe.AddIngredient<MoldyCheese>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.Hellforge);
        recipe.Register();
    }
}