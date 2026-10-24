using System.Collections.Generic;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items.Weapons.Melee;

public class MorbiumSword : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 60;
        Item.height = 60;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 23;
        Item.useAnimation = 23;
        Item.autoReuse = true;
        Item.scale = 1.2f;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 124;
        Item.knockBack = 9.25f;
        Item.useTurn = true;

        Item.value = 310000;
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item1;

        Item.shoot = ProjectileType<MorbiumBolt>();
        Item.shootSpeed = 9.5f; // Speed of the projectiles the sword will shoot

        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<MorbiumBar>(15);

        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}