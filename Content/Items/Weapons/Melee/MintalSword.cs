using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons.Melee;

public class MintalSword : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 36;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 45;
        Item.useAnimation = 45;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 146;
        Item.knockBack = 12;
        Item.ChangePlayerDirectionOnShoot = true;
        Item.value = 155000;
        Item.rare = ItemRarityID.LightRed;
        Item.UseSound = SoundID.Item1;
        Item.scale = 2f;
        Item.useTurn = true;
        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<Placeables.MintalBar>(14);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}