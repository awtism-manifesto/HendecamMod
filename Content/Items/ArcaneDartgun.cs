using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items;

public class ArcaneDartgun : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 62; 
        Item.height = 32;
        Item.scale = 0.9f;
        Item.rare = ItemRarityID.Orange;
        Item.value = 115000;
        Item.useTime = 24;
        Item.useAnimation = 24; 
        Item.useStyle = ItemUseStyleID.Shoot; 
        Item.autoReuse = true;
        Item.mana = 7;
        Item.UseSound = SoundID.Item99;
        Item.DamageType = GetInstance<RangedMagicDamage>();
        Item.damage = 24;
        Item.knockBack = 3.5f;
        Item.noMelee = true;
        Item.shootSpeed = 14.25f;
        Item.useAmmo = AmmoID.Dart;
        Item.shoot = ProjectileType<ArcaneDart>();
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ProjectileType<ArcaneDart>();
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Converts darts into magic arcane darts that pierce once and have powerful homing");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-6.5f, -1f);
    }
}