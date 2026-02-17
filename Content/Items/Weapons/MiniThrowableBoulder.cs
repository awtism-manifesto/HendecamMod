using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items.Weapons;

public class MiniThrowableBoulder : ModItem
{
    public override void SetDefaults()
    {
        // Alter any of these values as you see fit, but you should probably keep useStyle on 1, as well as the noUseGraphic and noMelee bools

        // Common Properties
        Item.rare = ItemRarityID.White;
        Item.value = Item.sellPrice(copper: 5);
        Item.maxStack = 9999;

        // Use Properties
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 25;
        Item.useTime = 25;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.consumable = true;

        // Weapon Properties			
        Item.damage = 19;
        Item.knockBack = 5f;
        Item.noUseGraphic = true; // The item should not be visible when used
        Item.noMelee = true; // The projectile will do the damage and not the item
        Item.DamageType = DamageClass.Ranged;

        // Projectile Properties
        Item.shootSpeed = 8.9f;
        Item.shoot = ModContent.ProjectileType<MiniBoulderThrowableProjectile>(); // The projectile that will be thrown
    }

    // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(25);
        recipe.AddIngredient(ItemID.StoneBlock, 100);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient<StoneBar>();
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}