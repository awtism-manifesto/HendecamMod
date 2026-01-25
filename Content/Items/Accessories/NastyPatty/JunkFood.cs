
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
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
    public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Grants 200 Health, 30% increased attack speed, double the breath timer, Hellfire for all attacks,"));
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "50 mana, 4 Luck, 10% Damage Reduction, 25 Safe Fall Distance, and much higher jump speed"));
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "No longer gain effects from Flasks, Combat, Activated, Enviornmental,"));
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Building, Fishing, Food, or Exploration Buffs"));
        }
    public override void UpdateEquip(Player player)
        {
        player.GetModPlayer<NastyBreath>().NastyEffect = true;
        player.GetModPlayer<NastySpeed>().NastyEffect = true;
        player.GetModPlayer<NastyHealth>().NastyEffect = true;
        player.GetModPlayer<NastyFire>().NastyEffect = true;
        player.GetModPlayer<NastyLuck>().NastyEffect = true;
        player.GetModPlayer<NastyMana>().NastyEffect = true;
        player.GetModPlayer<NastyEndurance>().NastyEffect = true;
        player.GetModPlayer<NastyFall>().NastyEffect = true;
        player.buffImmune[BuffID.WeaponImbueConfetti] = true;
        player.buffImmune[BuffID.WeaponImbueCursedFlames] = true;
        player.buffImmune[BuffID.WeaponImbueFire] = true;
        player.buffImmune[BuffID.WeaponImbueGold] = true;
        player.buffImmune[BuffID.WeaponImbueIchor] = true;
        player.buffImmune[BuffID.WeaponImbueNanites] = true;
        player.buffImmune[BuffID.WeaponImbuePoison] = true;
        player.buffImmune[BuffID.WeaponImbueVenom] = true;
        player.buffImmune[BuffID.AmmoReservation] = true;
        player.buffImmune[BuffID.Archery] = true;
        player.buffImmune[BuffID.Endurance] = true;
        player.buffImmune[BuffID.Heartreach] = true;
        player.buffImmune[BuffID.Inferno] = true;
        player.buffImmune[BuffID.Calm] = true;
        player.buffImmune[BuffID.Battle] = true;
        player.buffImmune[BuffID.Ironskin] = true;
        player.buffImmune[BuffID.Lifeforce] = true;
        player.buffImmune[BuffID.MagicPower] = true;
        player.buffImmune[BuffID.ManaRegeneration] = true;
        player.buffImmune[BuffID.Rage] = true;
        player.buffImmune[BuffID.Summoning] = true;
        player.buffImmune[BuffID.Thorns] = true;
        player.buffImmune[BuffID.Titan] = true;
        player.buffImmune[BuffID.Warmth] = true;
        player.buffImmune[BuffID.Wrath] = true;
        player.buffImmune[BuffID.Regeneration] = true;
        player.buffImmune[BuffID.Hunter] = true;
        player.buffImmune[BuffID.AmmoBox] = true;
        player.buffImmune[BuffID.Bewitched] = true;
        player.buffImmune[BuffID.Clairvoyance] = true;
        player.buffImmune[BuffID.Sharpened] = true;
        player.buffImmune[BuffID.WarTable] = true;
        player.buffImmune[BuffID.SugarRush] = true;
        player.buffImmune[BuffID.Campfire] = true;
        player.buffImmune[BuffID.DryadsWard] = true;
        player.buffImmune[BuffID.Sunflower] = true;
        player.buffImmune[BuffID.HeartLamp] = true;
        player.buffImmune[BuffID.Honey] = true;
        player.buffImmune[BuffID.PeaceCandle] = true;
        player.buffImmune[BuffID.StarInBottle] = true;
        player.buffImmune[BuffID.CatBast] = true;
        player.buffImmune[BuffID.MonsterBanner] = true;
        player.buffImmune[BuffID.Crate] = true;
        player.buffImmune[BuffID.Fishing] = true;
        player.buffImmune[BuffID.Sonar] = true;
        player.buffImmune[BuffID.Lucky] = true;
        player.buffImmune[BuffID.Builder] = true;
        player.buffImmune[BuffID.BiomeSight] = true;
        player.buffImmune[BuffID.Invisibility] = true;
        player.buffImmune[BuffID.Featherfall] = true;
        player.buffImmune[BuffID.Flipper] = true;
        player.buffImmune[BuffID.Gills] = true;
        player.buffImmune[BuffID.Mining] = true;
        player.buffImmune[BuffID.NightOwl] = true;
        player.buffImmune[BuffID.ObsidianSkin] = true;
        player.buffImmune[BuffID.Shine] = true;
        player.buffImmune[BuffID.Spelunker] = true;
        player.buffImmune[BuffID.Swiftness] = true;
        player.buffImmune[BuffID.Dangersense] = true;
        player.buffImmune[BuffID.WaterWalking] = true;
        player.buffImmune[BuffID.Gravitation] = true;
        player.buffImmune[BuffID.WellFed] = true;
        player.buffImmune[BuffID.WellFed2] = true;
        player.buffImmune[BuffID.WellFed3] = true;
        }
    public override void AddRecipes()
        {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<SlashedJacket>(1);
        recipe.AddIngredient<SchoolFood>(1);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.Hellforge);
        recipe.Register();
        }
    }
