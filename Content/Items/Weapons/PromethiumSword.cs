using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons;

public class PromethiumSword : ModItem
{
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 12;
        Item.useTime = 12;
        Item.damage = 421;
        Item.ArmorPenetration = 10;
        Item.knockBack = 5f;
        Item.width = 40;
        Item.height = 40;
        Item.scale = 1.15f;

        Item.UseSound = SoundID.Item15;
        Item.rare = ItemRarityID.Purple;
        Item.value = 132000;
        Item.DamageType = DamageClass.Melee;
        Item.shoot = ModContent.ProjectileType<PromethiumSwing>();
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

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Creates a radioactive aura around the player");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Ignores 10 enemy defense")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<PromethiumBar>(15);
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
        recipe = CreateRecipe();
    }
}