using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Armor;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;

public class SharpeningStone : ModItem
{
   

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.LightRed;
        Item.value = 450000;
       
    }
   

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
       
        var line = new TooltipLine(Mod, "Face", "+8% increased damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "All applicable projectiles have +2 pierce")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "May interact weirdly with certain weapons")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

    }
    public static readonly int AdditiveDamageBonus = 8;
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<Sharpened>().Sharp = true;
        player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 100f;

    }
  
}
public class Sharpened : ModPlayer
{
    public bool Sharp;


    public override void ResetEffects()
    {
        Sharp = false;
    }




}