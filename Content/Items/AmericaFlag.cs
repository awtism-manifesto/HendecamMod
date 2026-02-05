using System.Collections.Generic;

namespace HendecamMod.Content.Items;

public class AmericaFlag : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32; 
        Item.height = 32; 
        Item.rare = ItemRarityID.White;
        Item.value = 2400;
        Item.maxStack = 9999;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.autoReuse = true;
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.AmericaFlagPlaced>());
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Independent we stand, dependent we fall...");
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
        recipe.AddIngredient<BlankFlag>();
        recipe.AddIngredient(ItemID.Wood, 2);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}