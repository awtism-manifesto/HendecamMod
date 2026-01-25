using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace HendecamMod.Content.Items.Materials;

public class BlankCanvas : ModItem
{

    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.rare = ItemRarityID.White; 
        Item.value = 10;
        Item.maxStack = 9999;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.autoReuse = true;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Used to craft custom paintings");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        foreach (var l in tooltips)
        {
            if (l.Name.EndsWith(":RemoveMe"))
            {
                l.Hide();
            }
        }
        foreach (var l in tooltips)
        {
            if (l.Name.EndsWith(":RemoveMe"))
            {
                l.Hide();
            }
        }
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(2);
        recipe.AddIngredient(ItemID.Wood, 4);
        recipe.AddIngredient<Paper>(6);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();

        if (ModLoader.TryGetMod("ThoriumMod", out Mod Thor2Merica) && Thor2Merica.TryFind("BlankPainting", out ModItem BlankPainting))

           
        {
            recipe = CreateRecipe();
            recipe.AddIngredient(BlankPainting.Type);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();


        }


    }



}