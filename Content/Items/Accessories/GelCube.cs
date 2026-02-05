using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items.Accessories;

public class GelCube : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 26; 
        Item.height = 26;
        Item.rare = ItemRarityID.Blue;
        Item.value = 5000;
        Item.maxStack = 1;
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.AddBuff(BuffID.Slimed, 2);
        player.slippy2 = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Makes you sticky and slippery");
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
        recipe.AddIngredient<CubicMold>();
        recipe.AddIngredient(ItemID.Gel, 120);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}