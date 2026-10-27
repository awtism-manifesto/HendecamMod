using HendecamMod.Common.Utilities;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Accessories.NastyPatty;
using HendecamMod.Content.Items.Accessories.PeaceAmongNations;
using HendecamMod.Content.Items.Accessories.Rampart;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NormalOnes;

//[AutoloadEquip(EquipType.Beard)]
public class SoulOfImmunityAccessory : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.value = Item.sellPrice(silver: 64000);
        Item.rare = ItemRarityID.Expert;
        Item.accessory = true;
        Item.defense = 32;
    }

    public override void UpdateEquip(Player player)
    {
        var target = player.GetHCPlayer();
        target.soulOfImmunity = true;
        player.CancelAllBootRunVisualEffects();
        player.hellfireTreads = true;
        if (!player.mount.Active || player.mount.Type != MountID.WallOfFleshGoat)
        {
            player.DoBootsEffect(player.DoBootsEffect_PlaceFlamesOnTile);
            player.DoBootsEffect(player.DoBootsEffect_PlaceFlamesOnTile);
        }

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
        player.buffImmune[BuffID.Blackout] = true;
        player.buffImmune[BuffID.Darkness] = true;
        player.buffImmune[BuffID.Stoned] = true;
        player.buffImmune[BuffID.Horrified] = true;
        player.breathEffectiveness += 200000f;
        player.fishingSkill += 30;
        player.rocketBoots = 2;
        player.vanityRocketBoots = 5;
        player.moveSpeed += 1f;
        player.accRunSpeed = 12f;
        player.autoJump = true;
        player.jumpSpeedBoost += 0.6f;
        player.extraFall += 10;
        player.ignoreWater = true;
        player.lavaImmune = true;
        player.iceSkate = true;
        player.waterWalk2 = true;
        player.lavaRose = true;
        player.desertBoots = true;
        player.noFallDmg = true;
        player.flowerBoots = true;
        
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<RampartAccessory>();
        recipe.AddIngredient<NastyPattyAccessory>();
      
        recipe.AddIngredient<PeaceAmongNationsAccessory>();
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica) && CalMerica.TryFind("SeraphTracers", out ModItem SeraphTracers))
        {
            recipe.AddIngredient(SeraphTracers.Type);
        }
        if (!ModLoader.TryGetMod("CalamityMod", out Mod Ca))
        {
            recipe.AddIngredient(ItemID.TerrasparkBoots);
        }
    }
}