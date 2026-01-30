using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items;

public class BunnyBomber : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1f;
        Item.rare = ItemRarityID.Yellow; // The color that the item's name will be in-game.
        Item.value = 225000;
        // Use Properties
        // Use Properties
        Item.useTime = 23; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 23; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        // The sound that this item plays when used.
        Item.UseSound = SoundID.Item88;
        // Weapon Properties
        Item.DamageType = ModContent.GetInstance<RangedStupidDamage>();
        Item.damage = 192; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 3.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.

        // Gun Properties
        // For some reason, all the guns in the vanilla source have this.
        Item.shoot = ProjectileID.ExplosiveBunny;

        Item.shootSpeed = 21f; // The speed of the projectile (measured in pixels per frame.)
        Item.useAmmo = AmmoID.Rocket;
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ProjectileID.ExplosiveBunny;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 1; // The number of projectiles that this gun will shoot.

        for (int i = 0; i < NumProjectiles; i++)
        {
            // Rotate the velocity randomly by 30 degrees at max.
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));

            // Decrease velocity randomly for nicer visuals.
            newVelocity *= 1f - Main.rand.NextFloat(0.2f);

            // Create a projectile.
            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false; // Return false because we don't want tModLoader to shoot projectile
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "converts rockets into explosive bunnies");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "you are a depraved person.")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.RocketLauncher);
        recipe.AddIngredient(ItemID.ExplosiveBunny, 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();

        if (ModLoader.TryGetMod("Consolaria", out Mod ConsMerica) && ConsMerica.TryFind("SuspiciousLookingEgg", out ModItem SuspiciousLookingEgg))
        {
            recipe.AddIngredient(SuspiciousLookingEgg.Type);
        }
    }

    // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-13.5f, -6f);
    }
}