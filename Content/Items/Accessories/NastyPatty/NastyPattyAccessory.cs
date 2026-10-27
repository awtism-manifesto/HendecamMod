using HendecamMod.Common.Systems.Players;
using HendecamMod.Common.Utilities;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class NastyPattyAccessory : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 16000);
        Item.rare = ItemRarityID.Quest;
        Item.accessory = true;
        Item.defense = 50;
    }

    public override void UpdateEquip(Player player)
    {
        var target = player.GetHCPlayer();
        target.nastyPatty = true;
        player.lifeRegen += 6;
        Lighting.AddLight(player.Center, 6.7f, 6.7f, 6.7f);
        player.GetCritChance(DamageClass.Generic) += 25f;
        player.moveSpeed += 1f;
        player.GetArmorPenetration(DamageClass.Generic) += 40f;
        player.beetleBuff = false;
        player.hasTitaniumStormBuff = false;
        player.setNebula = false;
        player.setSolar = false;
        player.crystalLeaf = false;
        player.GetDamage(DamageClass.Generic) += 0.33f;
        player.jumpSpeedBoost += 0.7f;
        player.breathEffectiveness += 2f;
        player.GetAttackSpeed(DamageClass.Generic) += 0.15f;
        player.statLifeMax2 += 150;
        player.equipmentBasedLuckBonus += 4;
        player.statManaMax2 += 100;
        player.endurance += 0.2f;
        player.extraFall += 25;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient<ExpiredPetFood>()
            .AddIngredient<JunkFood>()
            .AddTile<CultistCyclotronPlaced>()
            .Register();
    }
}