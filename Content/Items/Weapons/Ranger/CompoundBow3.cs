using HendecamMod.Content.Global;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Rarities;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons.Ranger;

public class CompoundBow3 : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 0.96f;
        Item.rare = RarityType<HotPink>(); // The color that the item's name will be in-game.
        Item.value = 1980000;
        // Use Properties
        Item.useTime = 3; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 30; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        Item.reuseDelay = 17;

        // The sound that this item plays when used.

        // Weapon Properties
        Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
        Item.damage = 111; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.

        // Gun Properties
        // For some reason, all the guns in the vanilla source have this.
        Item.shootSpeed = 15.33f; // The speed of the projectile (measured in pixels per frame.)
        Item.useAmmo = ItemID.WoodenArrow;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
    }

    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        return Main.rand.NextFloat() >= 0.5f;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
        Main.projectile[proj].GetGlobalProjectile<HendecamGlobalProjectile>().ProjectileSource = HendecamGlobalProjectile.ProjectileItemSource.CompoundBow;
        SoundEngine.PlaySound(new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/BowShot")
        {
            Volume = 2.67f,
            PitchVariance = 0.2f,
            MaxInstances = 100,
        });
        return false; // Prevent vanilla projectile spawn
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<CompoundBow2>();

        recipe.AddIngredient(ItemID.LunarBar, 10);

        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
        if (ModLoader.TryGetMod("Macrocosm", out Mod MacroMerica) && MacroMerica.TryFind("ArtemiteBar", out ModItem ArtemiteBar))
        {
            recipe.AddIngredient(ArtemiteBar.Type, 5);
        }
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-6f, -1f);
    }
}