using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Rarities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Tools
{
    public class KingslayerMultiaxe : ModItem
    {
        public override void SetStaticDefaults()
        {

            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 23;
            Item.DamageType =  DamageClass.Melee;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 9;
            Item.useAnimation = 22;
           
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6.5f;
            Item.scale = 1.2f;
            Item.value = 205000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.tileBoost = 1;
            Item.pick = 63;
           
            Item.axe = 20;
            Item.attackSpeedOnlyAffectsWeaponAnimation = true; // Melee speed affects how fast the tool swings for damage purposes, but not how fast it can dig
        }
        public override bool AltFunctionUse(Player player)
        {


            return true;


        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.useTime = 7;
                Item.useAnimation = 7;
                Item.pick = 0;

                Item.axe = 0;
                Item.hammer = 79;

            }
            else
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.useTime = 9;
                Item.useAnimation = 22;
                Item.pick = 63;
                Item.hammer = 0;
                Item.axe = 20;
            }

            return base.CanUseItem(player);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "Left click for pickaxe and axe functionality");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "Right click for hammer functionality")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "Can mine Lycopite")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<GoldMultiaxe>();
            recipe.AddIngredient<KingslayerBar>(6);
            recipe.AddTile(TileID.Solidifier);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient<PlatinumMultiaxe>();
            recipe.AddIngredient<KingslayerBar>(6);
            recipe.AddTile(TileID.Solidifier);
            recipe.Register();





        }

    }
}