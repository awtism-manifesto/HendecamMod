using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Materials;

public class CoalLump : ModItem
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
        Item.value = 30;
        Item.maxStack = 9999;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "The industrial revolution and its consequences");
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
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Wood, 2);
        recipe.AddTile(TileID.Furnaces);
        recipe.Register();
        if (ModLoader.TryGetMod("ThoriumMod", out Mod MagThorium) && MagThorium.TryFind("SmoothCoal", out ModItem SmoothCoal))
        {
            recipe = CreateRecipe(2);
            recipe.AddIngredient(SmoothCoal.Type);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}