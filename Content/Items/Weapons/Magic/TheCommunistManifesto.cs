using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons.Magic;

public class TheCommunistManifesto : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1.15f;
        Item.rare = ItemRarityID.Orange; // The color that the item's name will be in-game.
        Item.value = 16969;
        // Use Properties
        // Use Properties
        Item.useTime = 16; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 16; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

        // The sound that this item plays when used.
        Item.UseSound = SoundID.Item71;
        // Weapon Properties
        Item.DamageType = DamageClass.Magic; // Sets the damage type to ranged.
        Item.damage = 36; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 2.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.

        Item.mana = 6;
        // Gun Properties
        // For some reason, all the guns in the vanilla source have this.
        Item.shoot = ProjectileID.PurificationPowder;

        Item.shootSpeed = 21.75f; // The speed of the projectile (measured in pixels per frame.)
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ProjectileType<Projectiles.HammerNSickle>();
    }

    // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-1f, 0f);
    }
}