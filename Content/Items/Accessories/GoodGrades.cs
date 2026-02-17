using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using System.Collections.Generic;
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
        Item.value = 25000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Increases Stupid attack speed by 10%, bonus decays as Lobotometer rises");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'Now your parents might finally be proud of you'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       

    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();



        float lobotometerPercent = loboPlayer.Current / loboPlayer.Max;
       // float damageBonus = lobotometerPercent * 0.10f;
        float speedBonus = (1f - lobotometerPercent) * 0.10f; 

        player.GetAttackSpeed(ModContent.GetInstance<StupidDamage>()) += speedBonus;
    }
}