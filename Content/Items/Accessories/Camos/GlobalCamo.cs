using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Accessories.Camos;

public class GlobalCamo : ModItem
{
    
    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.LightRed;
        Item.value = 155000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Increases crit chance and attack speed by 10% and 5% and decreased aggro regardless of biome");
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
        recipe.AddIngredient<CorruptionCamo>();
        recipe.AddIngredient<DesertCamo>();
        recipe.AddIngredient<JungleCamo>();
        recipe.AddIngredient<SnowCamo>();
        recipe.AddIngredient<OceanCamo>();
        recipe.AddIngredient<UrbanCamo>();

        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient<CrimsonCamo>();
        recipe.AddIngredient<DesertCamo>();
        recipe.AddIngredient<JungleCamo>();
        recipe.AddIngredient<SnowCamo>();
        recipe.AddIngredient<OceanCamo>();
        recipe.AddIngredient<UrbanCamo>();

        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
       
            player.GetModPlayer<CamoBoost>().isCamo = true;
        
       
    }
}

