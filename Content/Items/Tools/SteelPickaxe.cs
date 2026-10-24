using System.Collections.Generic;
using HendecamMod.Content.Items.Placeables;

namespace HendecamMod.Content.Items.Tools;

public class SteelPickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 36;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 14;
        Item.useAnimation = 20;
        Item.useTurn = true;

        Item.autoReuse = true;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 9;
        Item.knockBack = 6;
        Item.ChangePlayerDirectionOnShoot = false;
        Item.pick = 43;
        Item.value = Item.buyPrice(silver: 15);
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
        recipe.AddIngredient<SteelBar>(10);
        recipe.AddRecipeGroup("Wood", 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}