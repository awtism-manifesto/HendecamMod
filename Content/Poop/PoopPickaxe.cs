
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Poop
{
    public class PoopPickaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 6;
            Item.DamageType = DamageClass.Melee;
            Item.width = 30;
            Item.height = 30;
            Item.useTime = 11;
            Item.useAnimation = 19;
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2;

            Item.value = Item.buyPrice(copper: 70); // Buy this item for one gold - change gold to any coin and change the value to any number <= 100
         
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.tileBoost = -1;
            Item.pick = 45;
           
            Item.attackSpeedOnlyAffectsWeaponAnimation = true; // Melee speed affects how fast the tool swings for damage purposes, but not how fast it can dig
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
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
            recipe.AddRecipeGroup("Wood", 4);
            recipe.AddIngredient(ItemID.PoopBlock, 8);

            recipe.Register();
        }

    }
}