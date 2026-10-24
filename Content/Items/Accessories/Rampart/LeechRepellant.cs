using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class LeechRepellant : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 500);
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
    }

    public override void UpdateEquip(Player player)
    {
        player.buffImmune[BuffID.MoonLeech] = true;
    }
}