using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons.Melee;

public class TungstenLongsword : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 36;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 26;
        Item.useAnimation = 26;
        Item.autoReuse = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 17;
        Item.knockBack = 6;
        Item.ArmorPenetration = 4;
        Item.ChangePlayerDirectionOnShoot = true;
        Item.scale = 1.32f;

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
        recipe.AddIngredient(ItemID.TungstenBar, 12);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.TungstenBroadsword);
        recipe.AddIngredient(ItemID.TungstenBar, 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}