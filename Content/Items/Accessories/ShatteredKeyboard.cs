using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;

public class ShatteredKeyboard : ModItem
{
    // By declaring these here, changing the values will alter the effect, and the tooltip

    public static readonly int AdditiveDamageBonus = 13;

    // Insert the modifier values into the tooltip localization. More info on this approach can be found on the wiki: https://github.com/tModLoader/tModLoader/wiki/Localization#binding-values-to-localizations
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AdditiveDamageBonus);

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.LightRed;
        Item.value = 7000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "13% increased damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "AJSFPEKASDVOHSRTPMO0IAWHBHCAUFOWJEPJLKWUIOH")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        
        player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 100f;
    }
}