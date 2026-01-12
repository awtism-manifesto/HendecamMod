using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items.Accessories
{
    public class PlutoniumCube : ModItem
    {


        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 26; // Hitbox width of the item.
            Item.height = 26; // Hitbox height of the item.
            Item.rare = ItemRarityID.LightPurple; // The color that the item's name will be in-game.
            Item.value = 55000;
            Item.maxStack = 1;
            Item.accessory = true;
            Item.defense = 10;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
          
            player.accRunSpeed *= Main.rand.NextFloat(0.01f, 5f);
            player.moveSpeed *= Main.rand.NextFloat(0.01f, 5f);
            player.runAcceleration *= Main.rand.NextFloat(0.01f, 5f);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "Randomizes your max run speed");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "It can go from 1% to 500%")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);



           
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<CubicMold>(1);
            recipe.AddIngredient<PlutoniumBar>(12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}