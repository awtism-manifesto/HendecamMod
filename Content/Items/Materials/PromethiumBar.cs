using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Materials;

public class PromethiumBar : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.ItemIconPulse[Item.type] = true; 
        Item.ResearchUnlockCount = 25; 
    }

    public override void SetDefaults()
    {
        Item.width = 32; 
        Item.height = 32;
        Item.scale = 1f;
        Item.rare = ItemRarityID.Red; 
        Item.value = 98500;
        Item.maxStack = 9999;
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.AstatineBarPlaced>());
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "It feels weirdly stable to the touch");
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
        Recipe recipe = CreateRecipe(1);
        recipe.AddIngredient<PromethiumOre>(5);
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }
}