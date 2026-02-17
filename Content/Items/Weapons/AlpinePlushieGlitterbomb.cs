using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons;

public class AlpinePlushieGlitterbomb : ModItem
{
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 21;
        Item.useTime = 21;
        Item.damage = 149;
        Item.knockBack = 7.5f;
        Item.width = 40;
        Item.height = 40;
        Item.shootSpeed = 15.5f;
        Item.ArmorPenetration = 10;
        Item.scale = 1f;
        Item.noUseGraphic = true;
        Item.UseSound = SoundID.Item1;
        Item.rare = ItemRarityID.Cyan;
        Item.value = 595000;
        Item.DamageType = ModContent.GetInstance<SummonStupidDamage>();
        Item.shoot = ModContent.ProjectileType<AlpineGlitterbomb>();
        Item.noMelee = true; // This is set the sword itself doesn't deal damage (only the projectile does).
        Item.shootsEveryUse = true; // This makes sure Player.ItemAnimationJustStarted is set when swinging.
        Item.autoReuse = true;
    }
    public float LobotometerCost = 5f;
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
        var line = new TooltipLine(Mod, "Face", "Throws a glitter bomb disguised as an Alpine plushie");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "The shiny glitter causes your summons to focus hit enemies, and homes in, dealing up to 150 un-negateable damage")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Uses 5 Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "11 summon tag damage")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
       
        line = new TooltipLine(Mod, "Face", "'Huh, so that's why they won't tell us where glitter comes from'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }
}