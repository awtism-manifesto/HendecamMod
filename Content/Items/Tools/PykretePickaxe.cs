using HendecamMod.Content.Items.Placeables;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Tools;

public class PykretePickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 36;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 11;
        Item.useAnimation = 22;
        Item.autoReuse = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 7;
        Item.knockBack = 3;
        Item.ChangePlayerDirectionOnShoot = false;
        Item.pick = 40;
        Item.useTurn = true;
        Item.value = 6600;
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item1;

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
        recipe.AddIngredient<Pykrete>(20);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}