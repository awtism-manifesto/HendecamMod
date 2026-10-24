using HendecamMod.Content.Items.Placeables;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons.Ranger;

public class PykreteBow : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1f;
        Item.rare = ItemRarityID.White; // The color that the item's name will be in-game.
        Item.value = 5800;
        // Use Properties
        Item.useTime = 24; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 24; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        // The sound that this item plays when used.
        Item.UseSound = new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/BowShot")
        {
            Volume = 3f,
            PitchVariance = 0.2f,
            MaxInstances = 10,
        };
        // Weapon Properties
        Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
        Item.damage = 13; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 1.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.

        // Gun Properties
        // For some reason, all the guns in the vanilla source have this.
        Item.shootSpeed = 7.5f; // The speed of the projectile (measured in pixels per frame.)
        Item.useAmmo = ItemID.WoodenArrow;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (Main.rand.NextBool(4))
        {
            int proj = Projectile.NewProjectile(source, position, velocity, ProjectileID.FrostburnArrow, damage, knockback, player.whoAmI);
            Main.projectile[proj].GetGlobalProjectile<PykreteBowBuff>().fromPykreteBow = true;
            return false; // Return false because we don't want tModLoader to shoot projectile
        }

        return true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<Pykrete>(15);

        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-5f, -1f);
    }
}

public class PykreteBowBuff : GlobalProjectile
{
    public bool fromPykreteBow;

    public override bool InstancePerEntity => true;

    public override void AI(Projectile projectile)
    {
        if (fromPykreteBow)
        {
            projectile.scale = 1.15f;
        }
    }

    public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
    {
        if (fromPykreteBow)
        {
            modifiers.SourceDamage *= 1.33f;
        }
    }
}