using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace HendecamMod.Content.Items.Placeables
{
    public class WarIsOver : ModItem
    {
        public override void SetDefaults()
        {
            // Vanilla has many useful methods like these, use them! This substitutes setting Item.createTile and Item.placeStyle as well as setting a few values that are common across all placeable items
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.FabsolDeadPlaced>());

            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Blue;
            Item.value = 350;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "Now we MIGHT get Yharim before 2035");
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
            recipe.AddIngredient<Items.Materials.BlankCanvas>(1);
            recipe.AddIngredient(ItemID.AshBlock, 10);
           
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();


        }


    }
}