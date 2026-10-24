using HendecamMod.Content.Projectiles;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons.Magic;

public class RunicCodex : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1f;
        Item.rare = ItemRarityID.Pink; // The color that the item's name will be in-game.
        Item.value = 250000;
        // Use Properties
        Item.useTime = 17; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 17; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        Item.mana = 11;
        // The sound that this item plays when used.
        Item.UseSound = SoundID.Item32;
        // Weapon Properties
        Item.DamageType = DamageClass.Magic; // Sets the damage type to ranged.
        Item.damage = 91; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 5.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.
        Item.UseSound = SoundID.Item20;

        // Gun Properties
        Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
        Item.shootSpeed = 7f; // The speed of the projectile (measured in pixels per frame.)
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void AddRecipes()
    {
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ProjectileType<RunicCodexProjectile>();
    }
}