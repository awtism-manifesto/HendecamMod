using System.Collections.Generic;
using HendecamMod.Content.Projectiles;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons.Melee;

/// <summary>
///     Star Wrath/Starfury style weapon. Spawn projectiles from sky that aim towards mouse.
///     See Source code for Star Wrath projectile to see how it passes through tiles.
///     For a detailed sword guide see <see cref="ExampleSword" />
/// </summary>
public class Macuahuitl : ModItem
{
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 22;
        Item.useTime = 22;
        Item.damage = 28;
        Item.knockBack = 5.75f;
        Item.width = 40;
        Item.height = 40;
        Item.ArmorPenetration = 5;
        Item.scale = 1.05f;
        Item.shootSpeed = 10f;
        Item.UseSound = SoundID.Item1;
        Item.rare = ItemRarityID.Green;
        Item.value = Item.buyPrice(gold: 10); // Sell price is 5 times less than the buy price.
        Item.DamageType = DamageClass.Melee;
        Item.shoot = ProjectileType<MacuaSwing>();
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

        recipe.AddRecipeGroup("Wood", 15);
        recipe.AddIngredient(ItemID.Obsidian, 25);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}