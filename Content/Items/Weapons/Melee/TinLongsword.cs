using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons.Melee;

public class TinLongsword : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 36;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.autoReuse = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 12;
        Item.knockBack = 6;
        Item.ArmorPenetration = 2;
        Item.ChangePlayerDirectionOnShoot = true;
        Item.scale = 1.2f;
        Item.value = Item.buyPrice(gold: 1);
        Item.rare = ItemRarityID.White;
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
        recipe.AddIngredient(ItemID.TinBar, 10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.TinBroadsword);
        recipe.AddIngredient(ItemID.TinBar, 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}