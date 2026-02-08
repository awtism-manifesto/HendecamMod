using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class BowlingBaller : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1.25f;
        Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
        Item.value = 44000;
        Item.useTime = 22; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = Item.useTime; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item61;
        Item.DamageType = ModContent.GetInstance<StupidDamage>(); // Sets the damage type to ranged.
        Item.damage = 25; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true;
        Item.shootSpeed = 19.5f;
        Item.shoot = ProjectileID.PurificationPowder;
    }
    public float LobotometerCost = 8f;
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(player);
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ModContent.ProjectileType<Projectiles.BowlingBall>();
        Item.damage = Main.rand.Next(12, 45);
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int NumProjectiles = Main.rand.Next(0, 10);

        for (int i = 0; i < NumProjectiles; i++)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(16));

            newVelocity *= 1f - Main.rand.NextFloat(0.42f);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false; 
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Randomly shoots between 0-10 bowling balls");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Base damage is randomized every time the weapon is fired")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Uses 8 Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<Rubber>(20);
        recipe.AddIngredient<Kevlar>(10);
        recipe.AddIngredient(ItemID.MythrilBar, 10);
        recipe.AddIngredient(ItemID.IllegalGunParts);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient<Rubber>(20);
        recipe.AddIngredient<Kevlar>(10);
        recipe.AddIngredient(ItemID.OrichalcumBar, 10);
        recipe.AddIngredient(ItemID.IllegalGunParts);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-4f, -5f);
    }
}