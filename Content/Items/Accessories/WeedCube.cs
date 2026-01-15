using HendecamMod.Content.Items.Materials;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories
{
    public class WeedCube : ModItem
    {


        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 26; // Hitbox width of the item.
            Item.height = 26; // Hitbox height of the item.
            Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.
            Item.value = 1000;
            Item.maxStack = 1;
            Item.accessory = true;
            Item.defense = 2;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<Weedified>().Weeded = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "'Permanently' makes the player weed colored");
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
            recipe.AddIngredient<CubicMold>(1);
            recipe.AddIngredient<WeedLeaves>(12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
    public class Weedified : ModPlayer
    {
       
        public bool Weeded = false;

        public override void ResetEffects()
        {
            Weeded = false;
        }
       
        
        public override void PostUpdateEquips()
        {

            if (Player.GetModPlayer<Weedified>().Weeded == false) // Strongest boost takes priority, weaker boosts shouldn't prevent this
            {
                return;
            }
            else
            {
                Player.eyeColor = Color.DarkOliveGreen;
                Player.hairColor = Color.DarkOliveGreen;
                Player.pantsColor = Color.DarkOliveGreen;
                Player.shirtColor = Color.DarkOliveGreen;
                Player.shoeColor = Color.DarkOliveGreen;
                Player.underShirtColor = Color.DarkOliveGreen;
                Player.skinColor = Color.DarkOliveGreen;
                Player.hairDyeColor = Color.DarkOliveGreen;
               
            }


        }
    }
}