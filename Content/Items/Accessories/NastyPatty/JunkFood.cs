using HendecamMod.Common.Utilities;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class JunkFood : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 8000);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
    }

    public override void UpdateEquip(Player player)
    {
        var target = player.GetHCPlayer();
        target.junkFood = true;
        player.breathEffectiveness += 2f;
        player.lifeRegen += 6;
        player.jumpSpeedBoost += 0.7f;
        player.statLifeMax2 += 150;
        player.equipmentBasedLuckBonus += 4;
        player.statManaMax2 += 100;
        player.endurance += 0.2f;
        Lighting.AddLight(player.Center, 6.7f, 6.7f, 6.7f);
        player.GetCritChance(DamageClass.Generic) += 25f;
        player.GetAttackSpeed(DamageClass.Generic) += 0.15f;
        player.GetArmorPenetration(DamageClass.Generic) += 40f;
        player.moveSpeed += 1f;
        player.GetDamage(DamageClass.Generic) += 0.33f;
        player.extraFall += 25;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<SlashedJacket>();
        recipe.AddIngredient<SchoolFood>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.Hellforge);
        recipe.Register();
    }
}