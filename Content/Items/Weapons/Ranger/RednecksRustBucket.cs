using System.Collections.Generic;
using HendecamMod.Content.Global;
using HendecamMod.Content.Projectiles;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons.Ranger;

public class RednecksRustBucket : ModItem
{

    public override void SetStaticDefaults()
    {
        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
    }
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 0.8f;
        Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
        Item.value = 330000;
        // Use Properties
        Item.useTime = 21; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 21; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        // The sound that this item plays when used.

        // Weapon Properties
        Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
        Item.damage = 36; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.

        Item.ArmorPenetration = 10;

        // Gun Properties
        Item.shoot = ProjectileID.Bullet; // For some reason, all the guns in the vanilla source have this.
        Item.shootSpeed = 10.15f; // The speed of the projectile (measured in pixels per frame.)
        Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
    }

    public override bool AltFunctionUse(Player player)
    {
        return true;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (player.altFunctionUse == 2)
        {
            Item.noUseGraphic = true;

            int proj = Projectile.NewProjectile(source, position, velocity * 1.18f, ProjectileType<RedneckShovel>(), (int)(damage * 0.95f), knockback, player.whoAmI);
            Main.projectile[proj].GetGlobalProjectile<HendecamGlobalProjectile>().ProjectileSource = HendecamGlobalProjectile.ProjectileItemSource.RedneckGun;
            SoundEngine.PlaySound(SoundID.Item1, player.position);

            return false;
        }

       
        Item.noUseGraphic = false;
        SoundEngine.PlaySound(new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/HeavyShotgun")
        {
            Volume = 1.1f,
            PitchVariance = 0.2f,
            MaxInstances = 10,

        });
        int NumProjectiles = Main.rand.Next(3, 6); // The number of projectiles that this gun will shoot.
        damage = (int)(damage * Main.rand.NextFloat(0.43f, 0.445f));
        for (int i = 0; i < NumProjectiles; i++)
        {
            // Rotate the velocity randomly by 30 degrees at max.
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(6f));

            // Decrease velocity randomly for nicer visuals.
            newVelocity *= 1f - Main.rand.NextFloat(0.335f);

            // Create a projectile.
            int proj = Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            Main.projectile[proj].GetGlobalProjectile<HendecamGlobalProjectile>().ProjectileSource = HendecamGlobalProjectile.ProjectileItemSource.RedneckGun;
        }

        return false; // Return false because we don't want tModLoader to shoot projectile
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-29.25f, -2.66f);
    }

    public override void AddRecipes()
    {
        Recipe
        recipe = CreateRecipe();
        recipe.AddIngredient<Boomshark>();
        recipe.AddIngredient<ImprovisedMachineGun>();
        recipe.AddIngredient<ToothlessWyrm>();
        recipe.AddIngredient<AK47>();
        recipe.AddIngredient<UraniumShotgun>();
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
        Recipe
        recipe2 = CreateRecipe();
        recipe2.AddIngredient<Boomshark>();
        recipe2.AddIngredient<ImprovisedMachineGun>();
        recipe2.AddIngredient<Brainderbuss>();
        recipe2.AddIngredient<AK47>();
        recipe2.AddIngredient<UraniumShotgun>();
        recipe2.AddTile(TileID.DemonAltar);
        recipe2.Register();
        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica) && CalMerica.TryFind("PurifiedGel", out ModItem PurifiedGel))
        {
            recipe.AddIngredient(PurifiedGel.Type, 10);
            recipe2.AddIngredient(PurifiedGel.Type, 10);
        }
    }
}