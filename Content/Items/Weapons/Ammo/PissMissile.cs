using System.Collections.Generic;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items.Weapons.Ammo;

public class PissMissile : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 34; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
        Item.DamageType = DamageClass.Ranged;
        Item.width = 16;
        Item.height = 16;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
        Item.knockBack = 3f;
        Item.value = 106;
        Item.rare = ItemRarityID.LightRed;
        Item.shoot = ProjectileType<PissMissileProj>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 11.5f; // The speed of the projectile.
        Item.ammo = AmmoID.Rocket; // The ammo class this ammo belongs to.
    }

    public override void SetStaticDefaults()
    {
        AmmoID.Sets.IsSpecialist[Type] = true; // This item will benefit from the Shroomite Helmet.

        // This is where we tell the game which projectile to spawn when using this rocket as ammo with certain launchers.
        // This specific rocket ammo is like Rocket I's.
        AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.RocketLauncher].Add(Type, ProjectileType<PissMissileProj>());
        AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.GrenadeLauncher].Add(Type, ProjectileType<PissMissileProj>());
        AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.ProximityMineLauncher].Add(Type, ProjectileType<PissMissileProj>());
        AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.SnowmanCannon].Add(Type, ProjectileType<PissMissileProj>());
        // We also need to say which type of Celebration Mk2 rockets to use.
        // The Celebration Mk 2 only has four types of rockets. Change the projectile to match your ammo type.
        // Rocket I like   == ProjectileID.Celeb2Rocket
        // Rocket II like  == ProjectileID.Celeb2RocketExplosive
        // Rocket III like == ProjectileID.Celeb2RocketLarge
        // Rocket IV like  == ProjectileID.Celeb2RocketExplosiveLarge
        AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.Celeb2].Add(Type, ProjectileID.Celeb2Rocket);
        // The Celebration and Electrosphere Launcher will always use their own projectiles no matter which rocket you use as ammo.
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(75);
        recipe.AddIngredient(ItemID.Ichor);
        recipe.AddIngredient<RocketNeg1>(75);
        recipe.Register();
    }
}