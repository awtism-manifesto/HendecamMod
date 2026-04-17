using HendecamMod.Content.Items.Accessories.PeaceAmongNations;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Accessories.Camos;

public class UrbanCamo : ModItem
{
    
    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Blue;
        Item.value = 22000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Increases damage and attack speed by 10% and 7% and decreased aggro while in the Dungeon biome");
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

        recipe.AddIngredient<Polymer>(20);
        recipe.AddIngredient(ItemID.Bone, 10);
        recipe.AddIngredient(ItemID.Spike, 5);
        recipe.AddIngredient(ItemID.Silk, 5);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (player.ZoneDungeon)

        {
            player.GetModPlayer<CamoBoost>().isCamo = true;
        }
       
    }
}

