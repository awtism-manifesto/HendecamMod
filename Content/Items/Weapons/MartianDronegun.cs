using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles.Items;

namespace HendecamMod.Content.Items.Weapons;

public class MartianDronegun : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1f;
        Item.rare = ItemRarityID.Yellow; // The color that the item's name will be in-game.
        Item.value = 990000;
        // Use Properties
        Item.useTime = 33; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 33; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        // The sound that this item plays when used.
        Item.UseSound = SoundID.Item102;
        // Weapon Properties
        Item.DamageType = ModContent.GetInstance<RangedSummonDamage>(); // Sets the damage type to ranged.
        Item.damage = 90; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 9f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.

        // Gun Properties
        // For some reason, all the guns in the vanilla source have this.
        Item.shootSpeed = 9.66f; // The speed of the projectile (measured in pixels per frame.)
       
        Item.shoot = ModContent.ProjectileType<MartianDrone>();
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ModContent.ProjectileType<MartianDrone>();
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Shoots Martian Drones that auto-target enemies and explode on contact");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Does not require ammo")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.MartianConduitPlating, 50);
        recipe.AddIngredient<CyberneticGunParts>();
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
        
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-4f, -2f);
    }
}