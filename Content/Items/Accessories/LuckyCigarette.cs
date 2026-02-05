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