using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons.Magic;

public class AirburstTome : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1f;
        Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
        Item.value = 175000;
        // Use Properties
        Item.useTime = 24; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 24; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        Item.mana = 10;
        // The sound that this item plays when used.
        Item.UseSound = SoundID.Item32;
        // Weapon Properties
        Item.DamageType = DamageClass.Magic; // Sets the damage type to ranged.
        Item.damage = 44; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 6f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.
        Item.UseSound = SoundID.Item20;

        // Gun Properties
        Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
        Item.shootSpeed = 30f; // The speed of the projectile (measured in pixels per frame.)
    }

   

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Fires a blast of compressed air that bursts into three explosive energy beams");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Deals more damage to high defense targets")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<MantiusBar>(12);
        recipe.AddIngredient(ItemID.Book);
        recipe.AddTile(TileID.Bookcases);
        recipe.Register();
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ProjectileType<MantleAirBlast>();
    }
}