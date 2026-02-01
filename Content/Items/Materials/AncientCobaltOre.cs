using System.Collections.Generic;
using HendecamMod.Content.Tiles;

namespace HendecamMod.Content.Items.Materials;

public class AncientCobaltOre : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.scale = 1f;
        Item.rare = ItemRarityID.Green;
        Item.value = 2950;
        Item.maxStack = 9999;
        Item.DefaultToPlaceableTile(ModContent.TileType<AncientCobaltOrePlaced>());
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "'It's true power lies behind a fleshy wall'");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }
}