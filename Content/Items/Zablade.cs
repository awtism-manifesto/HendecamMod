using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;

namespace HendecamMod.Content.Items;


public class Zablade : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.autoReuse = true;
        Item.DamageType = ModContent.GetInstance<MeleeStupidDamage>();
        Item.damage = 16;
        Item.knockBack = 5.5f;
        Item.scale = 1.15f;

        Item.value = 62000;
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item1;

        Item.shoot = ModContent.ProjectileType<RazorLeaf>(); // ID of the projectiles the sword will shoot
        Item.shootSpeed = 2.5f; // Speed of the projectiles the sword will shoot

        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }
    public float LobotometerCost = 5f;
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(player);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.RichMahoganySword);
        recipe.AddIngredient<WeedLeaves>(28);

        recipe.AddTile(TileID.Anvils);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient<PoorMahoganySword>();
        recipe.AddIngredient<WeedLeaves>(28);

        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (type == ModContent.ProjectileType<RazorLeaf>())
        {
            damage = (int)(damage * 0.67f);
            knockback = knockback * 0.5f;
        }
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Shoots a bouncing razor leaf with every swing");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Uses 5 Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }
}