using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items;

public class Six7Gun : ModItem
{
    private int shotCounter;

    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 67; // Hitbox width of the item.
        Item.height = 67; // Hitbox height of the item.
        Item.scale = 0.85f;
        Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
        Item.value = 67000;
        Item.crit = -67;

        // Use Properties
        // Use Properties
        Item.useTime = 3; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 6; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        Item.reuseDelay = 10;
        Item.ArmorPenetration = 676767;

        Item.ArmorPenetration = 5;
        // Weapon Properties
        Item.DamageType = ModContent.GetInstance<StupidDamage>();
        Item.damage = 67; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 6.7f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.

        // Gun Properties
        // For some reason, all the guns in the vanilla source have this.
        Item.shoot = ModContent.ProjectileType<Six>();

        Item.shootSpeed = 11.67f; // The speed of the projectile (measured in pixels per frame.)
    }

    public override void UpdateInventory(Player player)
    {
        Item.damage = 67;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        damage = 67;
        if (shotCounter <= 0)
        {
            Vector2 newVelocity = velocity.RotatedBy(MathHelper.ToRadians(0.25f));

            type = ModContent.ProjectileType<Six>();
            SoundEngine.PlaySound(SoundID.Item42, player.position);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, (int)(damage * 1f), knockback, player.whoAmI);
            shotCounter = 2;
        }
        else if (shotCounter == 2)
        {
            Vector2 new2Velocity = velocity.RotatedBy(MathHelper.ToRadians(-0.25f));

            type = ModContent.ProjectileType<Seven>();
            SoundEngine.PlaySound(SoundID.Item42, player.position);

            Projectile.NewProjectileDirect(source, position, new2Velocity, type, (int)(damage * 1f), knockback, player.whoAmI);
            shotCounter = 0;
        }

        return false;
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        damage = 67;
    }

    public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
    {
        damage = damage * 1;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Always deals exactly 67 damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'Six Seven'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-16f, -1f);
    }
}