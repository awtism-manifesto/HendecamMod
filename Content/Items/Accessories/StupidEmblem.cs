using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;

public class StupidEmblem : ModItem
{
    // By declaring these here, changing the values will alter the effect, and the tooltip

    public static readonly int AdditiveStupidDamageBonus = 15;

    // Insert the modifier values into the tooltip localization. More info on this approach can be found on the wiki: https://github.com/tModLoader/tModLoader/wiki/Localization#binding-values-to-localizations
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AdditiveStupidDamageBonus);

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.LightRed;
        Item.value = 20000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "15% increased stupid damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Yall rockin with the stupid class so far!?!?!?!?!?")
        {
            OverrideColor = new Color(201, 133, 0)
        };
        tooltips.Add(line);

       
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        
        player.GetDamage<StupidDamage>() += AdditiveStupidDamageBonus / 100f;
    }
}