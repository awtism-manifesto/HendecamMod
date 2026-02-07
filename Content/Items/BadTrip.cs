using System.Collections.Generic;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items;

public class BadTrip : ModItem
{
    private static readonly int[] unwantedPrefixes = new[] { PrefixID.Terrible, PrefixID.Dull, PrefixID.Shameful, PrefixID.Annoying, PrefixID.Broken, PrefixID.Damaged, PrefixID.Shoddy };

    public override void SetStaticDefaults()
    {
        ItemID.Sets.Yoyo[Item.type] = true; // Used to increase the gamepad range when using Strings.
        ItemID.Sets.GamepadExtraRange[Item.type] = 11; // Increases the gamepad range. Some vanilla values: 4 (Wood), 10 (Valor), 13 (Yelets), 18 (The Eye of Cthulhu), 21 (Terrarian).
        ItemID.Sets.GamepadSmartQuickReach[Item.type] = true; // Unused, but weapons that require aiming on the screen are in this set.
    }

    public override void SetDefaults()
    {
        Item.width = 24; 
        Item.height = 24; 
        Item.useStyle = ItemUseStyleID.Shoot; // The way the item is used (e.g. swinging, throwing, etc.)
        Item.useTime = 25; // All vanilla yoyos have a useTime of 25.
        Item.useAnimation = 25; // All vanilla yoyos have a useAnimation of 25.
        Item.noMelee = true; // This makes it so the item doesn't do damage to enemies (the projectile does that).
        Item.noUseGraphic = true; // Makes the item invisible while using it (the projectile is the visible part).
        Item.UseSound = SoundID.Item1; // The sound that will play when the item is used.
        Item.damage = 31; // The amount of damage the item does to an enemy or player.
        Item.DamageType = DamageClass.MeleeNoSpeed; // The type of damage the weapon does. MeleeNoSpeed means the item will not scale with attack speed.
        Item.knockBack = 1.5f; // The amount of knockback the item inflicts.
        Item.channel = true; // Set to true for items that require the attack button to be held out (e.g. yoyos and magic missile weapons)
        Item.rare = ItemRarityID.Orange; // The item's rarity. This changes the color of the item's name.
        Item.value = 122000; // The amount of money that the item is can be bought for.
        Item.shoot = ModContent.ProjectileType<TrippyYoyo>(); // Which projectile this item will shoot. We set this to our corresponding projectile.
        Item.shootSpeed = 16f; // The velocity of the shot projectile.			
    }

    public override bool AllowPrefix(int pre)
    {
        if (Array.IndexOf(unwantedPrefixes, pre) > -1)
        {
            return false;
        }
        return true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Occasionally drops stationary mushroom mines while in flight");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "The shroom mines explode on contact with an enemy or after a few seconds")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<LycopiteBar>(13);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}