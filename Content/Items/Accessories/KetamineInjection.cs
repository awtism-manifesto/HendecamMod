using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

public class KetamineInjection : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.LightPurple;
        Item.value = 7500000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "25% increased damage reduction");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "-12.5% max life and -1 hp/s life regen")
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
        player.statLifeMax2 = (int)(player.statLifeMax2 * 0.875f);
        player.endurance = 1f - 0.75f * (1f - player.endurance); 
        player.lifeRegen += -2;
    }
}