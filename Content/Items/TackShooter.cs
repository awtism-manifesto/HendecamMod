using System.Collections.Generic;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

// This is the item that summons ExampleSentry.
public class TackShooter : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.GamepadWholeScreenUseRange[Type] = true;
        ItemID.Sets.LockOnIgnoresCollision[Type] = true;
    }

    public override void SetDefaults()
    {
        Item.damage = 10;
        Item.DamageType = DamageClass.Summon;
        Item.sentry = true;
     
        Item.width = 26;
        Item.height = 28;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.knockBack = 3;
        Item.value = Item.buyPrice(gold: 10);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item99;
        Item.shoot = ModContent.ProjectileType<TackShooterProj>();
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Summons a tack shooter sentry that shoots in eight directions")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<SteelBar>(8);
        recipe.AddIngredient(ItemID.IllegalGunParts);
        recipe.AddIngredient(ItemID.PinkGel, 32);
       
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        position = Main.MouseWorld;
        player.LimitPointToPlayerReachableArea(ref position);
        int halfProjectileHeight = (int)Math.Ceiling(ContentSamples.ProjectilesByType[type].height / 2f);
        position.Y -= halfProjectileHeight; // Adjust in-air option to spawn with bottom at cursor.
        // Spawn the sentry projectile at the calculated location.
        Projectile.NewProjectile(source, position, Vector2.Zero, type, damage, knockback, Main.myPlayer);

        // Kills older sentry projectiles according to player.maxTurrets
        player.UpdateMaxTurrets();

        return false;
    }
}