using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Items.Weapons;
using HendecamMod.Content.Rarities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Tools;

public class StupidFuckingPickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 1;
        Item.DamageType = ModContent.GetInstance<StupidDamage>();
        Item.width = 500;
        Item.height = 500;
        Item.useTime = 1;
        Item.useAnimation = 10;
        Item.useTurn = true;

        Item.scale = 2.25f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 0;
        Item.crit = -999;
        Item.value = Item.buyPrice(gold: 1); // Buy this item for one gold - change gold to any coin and change the value to any number <= 100
        Item.rare = ModContent.RarityType<HotPink>();
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.tileBoost = 696969;
        Item.pick = 2; // How strong the pickaxe is, see https://terraria.wiki.gg/wiki/Pickaxe_power for a list of common values
        Item.attackSpeedOnlyAffectsWeaponAnimation = true; // Melee speed affects how fast the tool swings for damage purposes, but not how fast it can dig
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "WARNING: DISABLE SMART CURSOR BEFORE HOLDING THIS ITEM");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "There won't be bloodshed")
        {
            OverrideColor = new Color(255, 70, 70)
        };
        tooltips.Add(line);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.NightsEdge);
       
       
       
       
        recipe.AddIngredient(ItemID.GoldBrickWall, 333);
        recipe.AddIngredient(ItemID.BladeofGrass, 1);
        recipe.AddIngredient(ItemID.PortableStool, 67);
        recipe.AddIngredient(ItemID.CrabBanner);
      
        recipe.AddIngredient(ItemID.WhitePearl, 69);
       
        recipe.AddIngredient(ItemID.TinShortsword, 167);
        recipe.AddIngredient(ItemID.SpectreBoots, 1);
     
        
        recipe.AddIngredient<WeedLeaves>(420);
        recipe.AddIngredient<LunarGem>(911);
        recipe.AddIngredient<BadGrades>();
        recipe.AddIngredient<SuperMonkeysPaw>();
        recipe.AddIngredient<PhatBlunt>();
        recipe.AddIngredient<AzuriteSaber>();
        recipe.AddIngredient<GunOfRoses>();
        recipe.AddIngredient<RednecksRustBucket>(1);
        
        recipe.AddIngredient<DirtBar>(6767);
        recipe.AddIngredient(ItemID.RedDye, 500);

        recipe.Register();
    }
   

}