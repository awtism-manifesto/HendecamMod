using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;

namespace HendecamMod.Content.Items.Accessories;

public class MintalCube : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 26;
        Item.height = 26;
        Item.rare = ItemRarityID.Blue;
        Item.value = 5;
        Item.maxStack = 1;
        Item.accessory = true;
        Item.defense = 3;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.accRunSpeed *= 20f;
        player.moveSpeed *= 20f;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Makes you run uncomfortably fast");
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
        recipe.AddIngredient<MintalBar>(12);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}