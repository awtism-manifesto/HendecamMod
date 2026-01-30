using System.Collections.Generic;
using HendecamMod.Content.Rarities;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Icons;

public class KingSlimeIcon : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 3;
    }

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.rare = ModContent.RarityType<DarkGreen>();
        Item.value = 10;
        Item.maxStack = 9999;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "A token for those who have slain the King Slime");
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
}