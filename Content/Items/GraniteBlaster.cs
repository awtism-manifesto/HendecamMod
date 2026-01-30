using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items;

public class GraniteBlaster : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 0.9f;
        Item.rare = ItemRarityID.White; // The color that the item's name will be in-game.
        Item.value = 10000;
        // Use Properties
        // Use Properties
        Item.useTime = 6; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 12; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

        // The sound that this item plays when used.

        // Weapon Properties
        Item.DamageType = ModContent.GetInstance<RangedMagicDamage>();
        Item.damage = 9; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 0.1f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.
        Item.mana = 6;
        Item.consumeAmmoOnLastShotOnly = true;

        // Gun Properties
        // For some reason, all the guns in the vanilla source have this.
        Item.shoot = ModContent.ProjectileType<GraniteLaser>();
        Item.useAmmo = AmmoID.Gel;

        Item.shootSpeed = 15f; // The speed of the projectile (measured in pixels per frame.)
    }

    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        return Main.rand.NextFloat() >= 0.5f;
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ModContent.ProjectileType<GraniteLaser>();
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (Main.rand.NextBool(2))
        {
            Item.mana = 0;
        }
        else
        {
            Item.mana = 6;
        }

        SoundEngine.PlaySound(SoundID.Item99, player.position);
        SoundEngine.PlaySound(SoundID.Item114, player.position);
        return true; // Return false because we don't want tModLoader to shoot projectile
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Requires both gel/oil and mana to fuel the laser");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "50% chance to not consume ammo or mana")
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
        recipe.AddIngredient(ItemID.Granite, 35);
        recipe.AddRecipeGroup("IronBar", 15);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-8f, -1f);
    }
}