using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.Wings)]
public class PondsWings : ModItem
{
    public override void SetStaticDefaults()
    {
        ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(29, 6.15f, 1.05f);
    }

    public override void SetDefaults()
    {
        Item.width = 22;
        Item.height = 20;
        Item.value = 1750000;
        Item.rare = ItemRarityID.Blue;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Allows flight and slow fall");
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

    public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
        ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
    {
        ascentWhenFalling = 0.75f; 
        ascentWhenRising = 0.1f;
        maxCanAscendMultiplier = 1f;
        maxAscentMultiplier = 2.5f;
        constantAscend = 0.125f;
    }
}