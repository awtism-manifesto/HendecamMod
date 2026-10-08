using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class SilverCube : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 26;
        Item.height = 26;
        Item.rare = ItemRarityID.Blue;
        Item.value = 37000;
        Item.maxStack = 1;
        Item.accessory = true;
        Item.defense = 3;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.maxFallSpeed = player.maxFallSpeed * 2f;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Doubles your fall speed");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            Color = new Color(255, 255, 255)
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
        recipe.AddIngredient(ItemID.SilverBar, 12);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}