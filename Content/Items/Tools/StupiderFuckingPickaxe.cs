using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Items.Weapons;
using HendecamMod.Content.Rarities;

namespace HendecamMod.Content.Items.Tools;

public class StupiderFuckingPickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 999999;
        Item.DamageType = ModContent.GetInstance<StupidDamage>();
        Item.width = 100;
        Item.height = 100;
        Item.useTime = 1;
        Item.useAnimation = 10;
        Item.scale = 2.5f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTurn = true;

        Item.knockBack = 6;
        Item.crit = 69416;
        Item.ArmorPenetration = 999;
        Item.value = Item.buyPrice(gold: 135000);
        Item.rare = ModContent.RarityType<HotPink>();
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.tileBoost = 696969;
        Item.pick = 3000; // How strong the pickaxe is, see https://terraria.wiki.gg/wiki/Pickaxe_power for a list of common values
        Item.attackSpeedOnlyAffectsWeaponAnimation = true; // Melee speed affects how fast the tool swings for damage purposes, but not how fast it can dig
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "WARNING: DISABLE SMART CURSOR BEFORE HOLDING THIS ITEM");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "I'm gonna disassemble your molecules!")
        {
            OverrideColor = new Color(55, 70, 254)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Zenith);
        recipe.AddIngredient(ItemID.DrillContainmentUnit, 2);
        recipe.AddIngredient(ItemID.LunarBar, 420);
        recipe.AddIngredient(ItemID.BrokenHeroSword, 6);
        recipe.AddIngredient(ItemID.NypmhBanner, 5);
        recipe.AddIngredient(ItemID.Muramasa);
        recipe.AddIngredient(ItemID.GoldBirdCage);
        recipe.AddIngredient(ItemID.TerraBlade);
        recipe.AddIngredient(ItemID.HandOfCreation);
        recipe.AddIngredient(ItemID.PortalGun, 3);
        recipe.AddIngredient(ItemID.DemonHeart);
        recipe.AddIngredient(ItemID.GalaxyPearl);
        recipe.AddIngredient(ItemID.DirtiestBlock);
        recipe.AddIngredient(ItemID.BottomlessHoneyBucket);
        recipe.AddIngredient(ItemID.MusicBoxDD2);
        recipe.AddIngredient(ItemID.TinAxe, 10);
        recipe.AddIngredient(ItemID.TerrasparkBoots);
        recipe.AddIngredient(ItemID.AnkhCharm);
        recipe.AddIngredient(ItemID.CrimsonKey, 2);
        recipe.AddIngredient(ItemID.WireKite);
        recipe.AddIngredient(ItemID.LaserDrill);
        recipe.AddIngredient<TheAutismManifesto>();
        recipe.AddIngredient<IForgor>(11);
        recipe.AddIngredient<FireDiamond>(690);
        recipe.AddIngredient<TheAshesOfCalamity>();
        recipe.AddIngredient<K2Avalanche>(2);
        recipe.AddIngredient<TransBar>(67);
        recipe.AddIngredient<LuckyCigarette>(500);
        recipe.AddIngredient<FidgetThrower3>(3);
        recipe.AddIngredient<PlasmaRifle3>(3);
        recipe.AddIngredient<CompoundBow3>(3);
        recipe.AddIngredient<ATFsNightmare>();
        recipe.AddIngredient<PlanetoidPunisher>();
        recipe.AddIngredient<AirBar>(80085);
        recipe.AddIngredient<StupidFuckingPickaxe>();
        recipe.Register();
    }
}