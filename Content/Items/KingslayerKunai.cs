using System.Collections.Generic;
using HendecamMod.Content.Projectiles;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

/// <summary>
///     Star Wrath/Starfury style weapon. Spawn projectiles from sky that aim towards mouse.
///     See Source code for Star Wrath projectile to see how it passes through tiles.
///     For a detailed sword guide see <see cref="ExampleSword" />
/// </summary>
public class KingslayerKunai : ModItem
{
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 25;
        Item.useTime = 25;
        Item.damage = 16;
        Item.knockBack = 3.33f;
        Item.width = 40;
        Item.height = 40;
        Item.shootSpeed = 9.5f;
        Item.scale = 1f;
        Item.noUseGraphic = true;
        Item.UseSound = SoundID.Item1;
        Item.rare = ItemRarityID.Green;
        Item.value = Item.buyPrice(gold: 6); // Sell price is 5 times less than the buy price.
        Item.DamageType = DamageClass.Ranged;
        Item.shoot = ModContent.ProjectileType<KingslayerKunaiProj>();
        Item.noMelee = true; // This is set the sword itself doesn't deal damage (only the projectile does).
        Item.shootsEveryUse = true; // This makes sure Player.ItemAnimationJustStarted is set when swinging.
        Item.autoReuse = true;
        Item.ArmorPenetration = 2;
        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica))
        {
            Item.DamageType = DamageClass.Throwing;
        }
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        damage = (int)(damage * 1f);

        Vector2 newVelocity = velocity.RotatedBy(MathHelper.ToRadians(0f));
        Vector2 new2Velocity = velocity.RotatedBy(MathHelper.ToRadians(-4.8f));
        Vector2 new3Velocity = velocity.RotatedBy(MathHelper.ToRadians(4.8f));

        Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        Projectile.NewProjectileDirect(source, position, new2Velocity, type, damage, knockback, player.whoAmI);
        Projectile.NewProjectileDirect(source, position, new3Velocity, type, damage, knockback, player.whoAmI);

        return false; // Return false because we don't want tModLoader to shoot projectile
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Throws a spread of three enchanted kunai");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica))
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod Cross-Mod (Thorium): Now deals Throwing damage") { OverrideColor = Color.LightSeaGreen });
        }
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<KingslayerBar>(11);
        recipe.AddIngredient(ItemID.Ruby, 6);

        recipe.AddTile(TileID.Solidifier);
        recipe.Register();
    }
}