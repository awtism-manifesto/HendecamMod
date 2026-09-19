using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons.Developer;


public class TheJfkExperience : ModItem
{
    public override void SetDefaults()
    {
        Item.useTime = 5; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 5; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        Item.width = 50;
        Item.height = 20;
        Item.noMelee = true;
        Item.damage = 99999;
        Item.noUseGraphic = true;

        Item.knockBack = 4f;
        Item.UseSound = SoundID.Item161;
        Item.value = Item.buyPrice(copper: 1);
        Item.rare = ItemRarityID.Cyan;
        Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
        Item.shootSpeed = -1f; // The speed of the projectile (measured in pixels per frame.)

        if (ModLoader.TryGetMod("Avalon", out Mod Avalon))
        {
            Item.damage = 67;
        }
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ProjectileID.SniperBullet;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 1; // The number of projectiles that this gun will shoot.

        for (int i = 0; i < NumProjectiles; i++)
        {
            // Rotate the velocity randomly by 30 degrees at max.
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(1));

            // Decrease velocity randomly for nicer visuals.
            newVelocity *= -0.01f;

            // Create a projectile.
            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }
        if (ModLoader.TryGetMod("Avalon", out Mod Avalon))
        {
            damage *= 666;
        }
            return false; // Return false because we don't want tModLoader to shoot projectile
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Well, what did you expect?")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

      
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DirtBlock, 69);

        recipe.Register();
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-8f, 2f); // Moves the position of the weapon in the player's hand.
    }
}