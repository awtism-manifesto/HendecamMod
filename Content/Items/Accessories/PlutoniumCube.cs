using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items.Accessories;

public class PlutoniumCube : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 26;
        Item.height = 26;
        Item.rare = ItemRarityID.LightPurple;
        Item.value = 645000;
        Item.maxStack = 1;
        Item.accessory = true;
        Item.defense = 10;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.accRunSpeed *= Main.rand.NextFloat(0.01f, 5f);
        player.moveSpeed *= Main.rand.NextFloat(0.01f, 5f);
        player.runAcceleration *= Main.rand.NextFloat(0.01f, 5f);
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Randomizes your max run speed");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "It can go from 1% to 500%")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<CubicMold>();
        recipe.AddIngredient<PlutoniumBar>(12);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}