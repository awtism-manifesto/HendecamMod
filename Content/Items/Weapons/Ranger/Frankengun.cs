using HendecamMod.Content.Projectiles;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons.Ranger;

public class Frankengun : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1.1f;
        Item.rare = ItemRarityID.Cyan; // The color that the item's name will be in-game.
        Item.value = 225000;
        // Use Properties
        Item.useTime = 5; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 40; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.reuseDelay = 30;
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        Item.UseSound = new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/BigZap")
        {
            Volume = 0.75f,
            PitchVariance = 0.2f,
            MaxInstances = 5,
        };
        // Weapon Properties
        Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
        Item.damage = 56; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 2f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.
        Item.ArmorPenetration = 15;
        // Gun Properties
        // For some reason, all the guns in the vanilla source have this.
        Item.shoot = ProjectileID.PurificationPowder;

        Item.shootSpeed = 22f; // The speed of the projectile (measured in pixels per frame.)
        Item.useAmmo = ItemID.MusketBall;
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ProjectileType<LightningShot>();
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 5; // The number of projectiles that this gun will shoot.

        for (int i = 0; i < NumProjectiles; i++)
        {
            // Rotate the velocity randomly by 30 degrees at max.
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(14.25f));


            // Create a projectile.
            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
           
        }

        return false; // Return false because we don't want tModLoader to shoot projectile
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        
        recipe.AddIngredient<GenderDefender>();
        recipe.AddIngredient<MintalMachinePistol>();
        recipe.AddIngredient<CopperShortmachinegun>();
        recipe.AddIngredient<VP70>();
        recipe.AddIngredient<ThePrimeTime>();
       
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-24f, -1f);
    }
}