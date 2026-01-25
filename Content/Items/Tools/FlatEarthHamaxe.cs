using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Rarities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Tools;

public class FlatEarthHamaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 60;
        Item.DamageType =  DamageClass.Melee;
        Item.width = 50;
        Item.height = 50;
        Item.useTime = 7;
        Item.useAnimation = 25;
       
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 7;
        
        Item.value = Item.buyPrice(gold: 69);
        Item.rare = ModContent.RarityType<HotPink>();
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.tileBoost = 4;
        
        Item.hammer = 100;
        Item.axe = 35;
        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();



        recipe.AddIngredient<FragmentFlatEarth>(14);
        recipe.AddIngredient(ItemID.LunarBar,12);
        
        recipe.AddTile(TileID.LunarCraftingStation);

        recipe.Register();
        
    }
}