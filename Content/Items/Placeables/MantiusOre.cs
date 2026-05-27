using HendecamMod.Content.Tiles.Blocks;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Placeables;

public class MantiusOre : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;

       
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {

        var line = new TooltipLine(Mod, "Face", "A remarkable ore that grows from stone.");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "The immense forces the Wall of Flesh exert on the world appear to aid it's spread")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(TileType<MantiusOrePlaced>());
        Item.width = 12;
        Item.height = 12;
        Item.rare = ItemRarityID.Orange;
        Item.value = 5195;
    }
}