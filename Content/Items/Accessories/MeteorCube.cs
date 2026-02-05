using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items.Accessories;

public class MeteorCube : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 26; 
        Item.height = 26; 
        Item.rare = ItemRarityID.Green; 
        Item.value = 87000;
        Item.maxStack = 1;
        Item.accessory = true;
        Item.defense = 5;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.maxFallSpeed = player.maxFallSpeed * 4f;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Quadrouples your max fall speed");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Good thing this can't become a black hole")
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
        recipe.AddIngredient(ItemID.MeteoriteBar, 12);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}