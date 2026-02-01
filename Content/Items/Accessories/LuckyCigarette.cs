using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Beard)]
public class LuckyCigarette : ModItem
{
    public static readonly int CritBonus = 10;

    public override void SetDefaults()
    {
        Item.width = 22; // Width of the item
        Item.height = 18; // Height of the item
        Item.value = Item.sellPrice(silver: 66); // How many coins the item is worth
        Item.rare = ItemRarityID.Orange; // The rarity of the item
        Item.accessory = true;
    }

    public override void UpdateEquip(Player player)
    {
        player.breathMax = 167;
        player.GetCritChance(DamageClass.Generic) += CritBonus;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "10% increased crit chance");
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Slightly reduces underwater breath time")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Also looks really badass")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
       
    }
}