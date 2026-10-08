using System.Collections.Generic;

namespace HendecamMod.Content.Items.Placeables;

public class AlpineTrophy : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(TileType<Tiles.Furniture.AlpineTrophyPlaced>());

        Item.width = 32;
        Item.height = 32;
        Item.rare = ItemRarityID.Blue;
        Item.value = 555;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            Color = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }
}