using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.Wings)]
public class KulakWings : ModItem
{
    public override void SetStaticDefaults()
    {
        ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(21, 6.75f, 1.25f);
    }

    public override void SetDefaults()
    {
        Item.width = 22;
        Item.height = 20;
        Item.value = 950000;
        Item.rare = ItemRarityID.Green;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Allows flight and slow fall");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'You'll be flying high after eating the rich!'")
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
        ascentWhenFalling = 0.85f; 
        ascentWhenRising = 0.15f; 
        maxCanAscendMultiplier = 1f;
        maxAscentMultiplier = 3f;
        constantAscend = 0.135f;
    }
}