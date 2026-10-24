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

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.statLifeMax2 = (int)(player.statLifeMax2 * 0.875f);
        player.endurance = 1f - 0.75f * (1f - player.endurance); 
        player.lifeRegen += -2;
    }
}