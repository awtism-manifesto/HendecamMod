using System.Collections.Generic;

namespace HendecamMod.Content.Items.Placeables;

public class KawkawPainting : ModItem
{
    public override void SetDefaults()
    {
        // Vanilla has many useful methods like these, use them! This substitutes setting Item.createTile and Item.placeStyle as well as setting a few values that are common across all placeable items
        Item.DefaultToPlaceableTile(TileType<Tiles.Furniture.KawkawPaintingTile>());

        Item.width = 32;
        Item.height = 32;
        Item.rare = ItemRarityID.Blue;
        Item.value = 555;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Nyon!");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            Color = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<Materials.BlankCanvas>();
        recipe.AddRecipeGroup("Birds");
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}