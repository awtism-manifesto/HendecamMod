using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using HendecamMod.Content.Tiles;

namespace HendecamMod.Content.Items.Materials;

public class AncientCobaltBar : ModItem
{
    public override void SetStaticDefaults()
    {
       
       

        Item.ResearchUnlockCount = 25;
    }
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.scale = 1f;
        Item.rare = ItemRarityID.Orange;
        Item.value = 9800;
        Item.maxStack = 9999;
        Item.DefaultToPlaceableTile(ModContent.TileType<AncientCobaltBarPlaced>());
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "I'm old!");
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

      
        recipe.AddIngredient<AncientCobaltOre>(3);
        recipe.AddTile(TileID.Furnaces);
        recipe.Register();





    }
}