using System.Collections.Generic;
using HendecamMod.Content.Items.Weapons;
using HendecamMod.Content.Projectiles;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;


public class NapalmBlade : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 55;
        Item.height = 55;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 11;
        Item.useAnimation = 22;
        Item.autoReuse = true;
        Item.scale = 1.75f;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 166;
        Item.knockBack = 3;
        Item.value = 3200000;
        Item.rare = ItemRarityID.Red;
        Item.UseSound = SoundID.Item1;

        Item.shoot = ModContent.ProjectileType<Napalm>(); // ID of the projectiles the sword will shoot
        Item.shootSpeed = 21.25f; // Speed of the projectiles the sword will shoot

        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
       

        if (type == ModContent.ProjectileType<Napalm>())
        {
            damage = (int)(damage * 0.8);
        }
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 15; // The number of projectiles that this gun will shoot.
        damage = (int)(damage * 0.75f);
        for (int i = 0; i < NumProjectiles; i++)
        {
            // Rotate the velocity randomly by 30 degrees at max.
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(360));

            // Decrease velocity randomly for nicer visuals.
            newVelocity *= 1f - Main.rand.NextFloat(0.25f);

            // Create a projectile.
            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return true; // Return false because we don't want tModLoader to shoot projectile
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Flings smoldering napalm everywhere");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Geneva Convention? More like Geneva Suggestions!")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.FragmentSolar, 5);
        recipe.AddIngredient(ItemID.Ichor, 10);
        recipe.AddIngredient<RefinedOil>(50);
        recipe.AddIngredient<FissionDrive>();
        recipe.AddIngredient<SpookyWoodSword>();
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}