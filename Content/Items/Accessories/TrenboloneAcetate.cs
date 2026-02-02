using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;

public class TrenboloneAcetate : ModItem
{
    // By declaring these here, changing the values will alter the effect, and the tooltip

    public static readonly int AdditiveStupidDamageBonus = 9;
    public static readonly int MeleeAttackSpeedBonus = 9;
    public static readonly int StupidAttackSpeedBonus = 9;
    public static readonly int AdditiveMeleeDamageBonus = 9;

    // Insert the modifier values into the tooltip localization. More info on this approach can be found on the wiki: https://github.com/tModLoader/tModLoader/wiki/Localization#binding-values-to-localizations
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AdditiveStupidDamageBonus);

    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Orange;
        Item.value = 198000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "9% incrased damage and attack speed for both the Stupid and Melee classes");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "For only the truest of meatheads")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
       
        player.GetDamage<StupidDamage>() += AdditiveStupidDamageBonus / 100f;
        player.GetAttackSpeed<StupidDamage>() += StupidAttackSpeedBonus / 100f;
        player.GetDamage(DamageClass.Melee) += AdditiveStupidDamageBonus / 100f;
        player.GetAttackSpeed(DamageClass.Melee) += MeleeAttackSpeedBonus / 100f;
        player.aggro += 400;
    }
}