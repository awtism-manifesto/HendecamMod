using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;

public class SaltPendant : ModItem
{
    // By declaring these here, changing the values will alter the effect, and the tooltip
    // Insert the modifier values into the tooltip localization. More info on this approach can be found on the wiki: https://github.com/tModLoader/tModLoader/wiki/Localization#binding-values-to-localizations
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs();

    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Green;
        Item.value = 75000;
        Item.defense = 1;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "5.55% increased damage reduction");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'For your spiritual protection'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.endurance = 1f - 0.9555f * (1f - player.endurance);
    }
}