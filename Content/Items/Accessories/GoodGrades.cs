using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;

public class GoodGrades : ModItem
{

    public static readonly int AdditiveStupidDamageBonus = -5;
    public static readonly int StupidCritBonus = 12;

     public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs();

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Green;
        Item.value = 20000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "5% decreased stupid damage, 12% increased stupid critical strike chance");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Now your parents might finally be proud of you")
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

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage<StupidDamage>() += AdditiveStupidDamageBonus / 100f;
        player.GetCritChance<StupidDamage>() += StupidCritBonus;
    }
}