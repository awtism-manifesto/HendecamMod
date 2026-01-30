using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items;

public class SkibidiToilet : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1.25f;
        Item.rare = ItemRarityID.Orange; // The color that the item's name will be in-game.
        Item.value = 250000;
        // Use Properties

        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        // The sound that this item plays when used.
        Item.UseSound = SoundID.Item45;
        // Weapon Properties
        Item.DamageType = ModContent.GetInstance<StupidDamage>(); // Sets the damage type to ranged.

        Item.knockBack = 0.5f;
        Item.noMelee = true;
        Item.damage = 29;
        Item.useTime = 27;
        Item.useAnimation = 27;
        // Gun Properties
        // For some reason, all the guns in the vanilla source have this.
        Item.shoot = ModContent.ProjectileType<SkibidiHead>();

        Item.shootSpeed = 9.1f; // The speed of the projectile (measured in pixels per frame.)
    }

    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        return Main.rand.NextFloat() >= 0.85f;
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ModContent.ProjectileType<SkibidiHead>();
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int NumProjectiles = 2;

        for (int i = 0; i < NumProjectiles; i++)
        {
            // Rotate the velocity randomly by 30 degrees at max.
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));

            // Decrease velocity randomly for nicer visuals.
            newVelocity *= 1f - Main.rand.NextFloat(0.4f);

            // Create a projectile.
            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false; // Return false because we don't want tModLoader to shoot projectile
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        // Here we will hide all tooltips whose title end with ':RemoveMe'
        // One like that is added at the start of this method
        foreach (var l in tooltips)
        {
            if (l.Name.EndsWith(":RemoveMe"))
            {
                l.Hide();
            }
        }

        // Another method of hiding can be done if you want to hide just one line.
        // tooltips.FirstOrDefault(x => x.Mod == "ExampleMod" && x.Name == "Verbose:RemoveMe")?.Hide();
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica) && CalMerica.TryFind("RottenBrain", out ModItem RottenBrain))
        {
            recipe = CreateRecipe();

            recipe.AddIngredient(RottenBrain.Type);
            recipe.AddIngredient(ItemID.Toilet);

            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }

    // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-6f, -3f);
    }
}