using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons.Melee;

public class PoliceBaton : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 25;
        Item.height = 25;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 12;
        Item.useAnimation = 12;
        Item.autoReuse = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 13;
        Item.knockBack = 1.5f;
        Item.ChangePlayerDirectionOnShoot = false;

        Item.value = Item.buyPrice(gold: 2);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item1;
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
        recipe.AddIngredient<Polymer>(6);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}