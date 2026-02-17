using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Projectiles;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons;

public class LunarStaff : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.staff[Type] = true; // This makes the useStyle animate as a staff instead of as a gun.
    }

    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 23;
        Item.useAnimation = 23;
        Item.autoReuse = true;
        Item.mana = 11;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 20;
        Item.knockBack = 3.5f;
        Item.noMelee = true;
        Item.value = 75000;
        Item.rare = ItemRarityID.Orange;
        Item.shoot = ModContent.ProjectileType<LunarBolt>(); // ID of the projectiles the sword will shoot
        Item.shootSpeed = 13.5f; // Speed of the projectiles the sword will shoot

        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item91, player.position);

        return true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Shoots a bolt of lunar energy that projects a homing contrail");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(3f, -7f);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<AncientCobaltBar>(10);
        recipe.AddIngredient<LunarGem>(8);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}