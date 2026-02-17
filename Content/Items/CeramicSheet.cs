using System.Collections.Generic;

namespace HendecamMod.Content.Items;

public class CeramicSheet : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32; 
        Item.height = 32;
        Item.scale = 1.22f;
        Item.rare = ItemRarityID.Orange; 
        Item.value = 500;
        Item.maxStack = 9999;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "");
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
        Recipe recipe = CreateRecipe(5);

        recipe.AddIngredient(ItemID.Bone, 2);
        recipe.AddIngredient(ItemID.ClayBlock, 5);
        recipe.AddTile(TileID.Hellforge);
        recipe.Register();
    }
}