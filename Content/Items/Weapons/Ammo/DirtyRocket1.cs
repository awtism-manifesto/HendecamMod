using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items.Weapons.Ammo;

public class DirtyRocket1 : ModItem
{
    public override void SetStaticDefaults()
    {
        AmmoID.Sets.IsSpecialist[Type] = true; // This item will benefit from the Shroomite Helmet.

        // This is where we tell the game which projectile to spawn when using this rocket as ammo with certain launchers.
        // This specific rocket ammo is like Rocket I's.
        AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.RocketLauncher].Add(Type, ProjectileType<UraniumRocket>());
        AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.GrenadeLauncher].Add(Type, ProjectileType<UraniumRocket>());
        AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.ProximityMineLauncher].Add(Type, ProjectileType<UraniumRocket>());
        AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.SnowmanCannon].Add(Type, ProjectileType<UraniumRocket>());
        // We also need to say which type of Celebration Mk2 rockets to use.
        // The Celebration Mk 2 only has four types of rockets. Change the projectile to match your ammo type.
        // Rocket I like   == ProjectileID.Celeb2Rocket
        // Rocket II like  == ProjectileID.Celeb2RocketExplosive
        // Rocket III like == ProjectileID.Celeb2RocketLarge
        // Rocket IV like  == ProjectileID.Celeb2RocketExplosiveLarge
        AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.Celeb2].Add(Type, ProjectileID.Celeb2Rocket);
        // The Celebration and Electrosphere Launcher will always use their own projectiles no matter which rocket you use as ammo.
    }

    public override void SetDefaults()
    {
        Item.damage = 17; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
        Item.DamageType = DamageClass.Ranged;
        Item.width = 12;
        Item.height = 12;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
        Item.knockBack = 1.5f;
        Item.value = 300;
        Item.rare = ItemRarityID.Green;
        Item.shoot = ProjectileType<UraniumRocket>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 10f; // The speed of the projectile.
        Item.ammo = AmmoID.Rocket; // The ammo class this ammo belongs to.
        AmmoID.Sets.IsSpecialist[Type] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(75);
        recipe.AddIngredient<UraniumBar>();
        recipe.AddIngredient<RocketNeg1>(75);
        recipe.Register();
    }
}