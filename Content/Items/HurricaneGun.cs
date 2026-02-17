using System.Collections.Generic;
using HendecamMod.Content.Projectiles;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class HurricaneGun : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 0.75f;
        Item.rare = ItemRarityID.Cyan; // The color that the item's name will be in-game.
        Item.value = 299000;

        Item.useTime = 8; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 24; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        Item.reuseDelay = 18;

        Item.UseSound = SoundID.Item122;
        Item.DamageType = DamageClass.Magic; // Sets the damage type to ranged.
        Item.damage = 51; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 5.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.

        Item.ArmorPenetration = 40;
        Item.mana = 18;

        Item.shoot = ProjectileID.PurificationPowder;

        Item.shootSpeed = 14.25f;
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ModContent.ProjectileType<Hurricane>();
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 1;

        for (int i = 0; i < NumProjectiles; i++)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(25));

            newVelocity *= 1f - Main.rand.NextFloat(0.47f);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Unleashes multiple hurricanes that continuously damage enemies");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SoulofFlight, 15);
        recipe.AddIngredient(ItemID.WeatherPain);
        recipe.AddIngredient(ItemID.NimbusRod);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-62f, -3.5f);
    }
}