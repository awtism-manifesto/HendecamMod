using HendecamMod.Common.Systems;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items;

public class RectumsRequiem : ModItem
{
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DildoWhipBuff.TagDamage);

    public override void SetDefaults()
    {
        // This method quickly sets the whip's properties.
        // Mouse over to see its parameters.
        Item.DefaultToWhip(ModContent.ProjectileType<BeadsNDick>(), 20, 2, 7);
        Item.rare = ItemRarityID.Pink;
        Item.damage = 45;
        Item.useTime = 23;
        Item.useAnimation = 23;
        Item.knockBack = 7;
        Item.width = 14;
        Item.height = 14;
        Item.value = Item.buyPrice(silver: 2500);
        Item.DamageType = ModContent.GetInstance<SummonStupidDamage>();
    }
    public float LobotometerCost = 7f;
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(player);
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "10 summon tag damage");
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Uses 7 Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Extra good at penetration ;)")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    // Makes the whip receive melee prefixes
    public override bool MeleePrefix()
    {
        return true;
    }
}