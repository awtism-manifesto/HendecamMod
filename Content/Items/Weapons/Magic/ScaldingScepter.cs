using System.Collections.Generic;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;

namespace HendecamMod.Content.Items.Weapons.Magic;

public class ScaldingScepter : ModItem
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
        Item.useTime = 9;
        Item.useAnimation = 18;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item13;
        Item.mana = 9;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 29;
        Item.knockBack = 4f;
        Item.noMelee = true;
        Item.ArmorPenetration = 10;
        Item.value = 221000;
        Item.rare = ItemRarityID.LightRed;
        Item.shoot = ProjectileType<ScaldStream>(); // ID of the projectiles the sword will shoot
        Item.shootSpeed = 14.5f; // Speed of the projectiles the sword will shoot

        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ProjectileType<ScaldStream>();
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Shoots streams of scalding water that inflict Scald Burn");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Deals more damage to high defense targets")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Scald Burned enemies deal less damage")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);


    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(6f, -15f);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<MantiusBar>(12);
        recipe.AddIngredient(ItemID.AquaScepter);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}