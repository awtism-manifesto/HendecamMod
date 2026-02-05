using System.Collections.Generic;
using HendecamMod.Content.Projectiles;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class ThePrimeTime : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 62; 
        Item.height = 32;
        Item.scale = 1.3f;
        Item.rare = ItemRarityID.LightPurple; // The color that the item's name will be in-game.
        Item.value = 115000;
        Item.useTime = 40; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 40; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; 
        Item.UseSound = SoundID.Item94;
        Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
        Item.damage = 56; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 6.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; 
        Item.ArmorPenetration = 8;
        Item.shootSpeed = 90f; 
        Item.useAmmo = ItemID.MusketBall;
        Item.shoot = ModContent.ProjectileType<ChargeLaser>();
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (type == ProjectileID.Bullet)
        {
            type = ModContent.ProjectileType<ChargeLaser>();
        }

        if (type == ModContent.ProjectileType<ChargeLaser>())
        {
            damage = (int)(damage * 1.25f);
        }
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 4; 

        for (int i = 0; i < NumProjectiles; i++)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(1.15f));

            newVelocity *= 1f - Main.rand.NextFloat(0.15f);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false; 
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Electrifies musket balls, turning them into four tightly packed hypersonic lasers")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        foreach (var l in tooltips)
        {
            if (l.Name.EndsWith(":RemoveMe"))
            {
                l.Hide();
            }
        }
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.HallowedBar, 12);
        recipe.AddIngredient(ItemID.SoulofFright, 15);
        recipe.AddIngredient<LaserCharge>(250);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-6f, -1f);
    }
}