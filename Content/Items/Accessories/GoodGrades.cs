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

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();



        float lobotometerPercent = loboPlayer.Current / loboPlayer.Max;
       // float damageBonus = lobotometerPercent * 0.10f;
        float speedBonus = (1f - lobotometerPercent) * 0.10f; 

        player.GetAttackSpeed(GetInstance<StupidDamage>()) += speedBonus;
    }
}