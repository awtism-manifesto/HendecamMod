using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.Beard)]
public class LuckyCigarette : ModItem
{
    public static readonly int CritBonus = 10;

    public override void SetDefaults()
    {
        Item.width = 22; 
        Item.height = 18; 
        Item.value = Item.sellPrice(silver: 66); 
        Item.rare = ItemRarityID.Orange; 
        Item.accessory = true;
    }

    public override void UpdateEquip(Player player)
    {
        player.luck += 0.067f;
        player.breathMax = 150;
        player.GetCritChance(DamageClass.Generic) += CritBonus;
    }
}