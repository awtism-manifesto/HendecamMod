using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons.Melee;

/// <summary>
///     Star Wrath/Starfury style weapon. Spawn projectiles from sky that aim towards mouse.
///     See Source code for Star Wrath projectile to see how it passes through tiles.
///     For a detailed sword guide see <see cref="ExampleSword" />
/// </summary>
public class BigBertha : ModItem
{
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 36;
        Item.useTime = 36;
        Item.damage = 500;
        Item.knockBack = 15f;
        Item.width = 64;
        Item.height = 64;
        Item.scale = 3.725f;
        Item.ArmorPenetration = 45;
        Item.UseSound = SoundID.Item1;
        Item.rare = ItemRarityID.Red;
        Item.value = 5000000; // Sell price is 5 times less than the buy price.
        Item.DamageType = DamageClass.Melee;
        Item.shoot = ProjectileType<BigBerthaProj>();
        Item.noMelee = true; // This is set the sword itself doesn't deal damage (only the projectile does).
        Item.shootsEveryUse = true; // This makes sure Player.ItemAnimationJustStarted is set when swinging.
        Item.autoReuse = true;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        float adjustedItemScale = player.GetAdjustedItemScale(Item); // Get the melee scale of the player and item.
        Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
        NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.

        return base.Shoot(player, source, position, velocity, type, damage, knockback);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.BreakerBlade);
        recipe.AddIngredient(ItemID.Excalibur);
        recipe.AddIngredient<AstatineBar>(25);
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
        recipe = CreateRecipe();
    }
}