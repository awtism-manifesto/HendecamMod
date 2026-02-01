using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items.Weapons;

public class DemoniteShuriken : ModItem
{
    public override void SetDefaults()
    {
        // Alter any of these values as you see fit, but you should probably keep useStyle on 1, as well as the noUseGraphic and noMelee bools

        // Common Properties
        Item.rare = ItemRarityID.Blue;
        Item.value = 325;
        Item.maxStack = 9999;

        // Use Properties
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 10;
        Item.useTime = 10;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.consumable = true;

        // Weapon Properties			
        Item.damage = 13;
        Item.knockBack = 5f;
        Item.noUseGraphic = true; // The item should not be visible when used
        Item.noMelee = true; // The projectile will do the damage and not the item
        Item.DamageType = DamageClass.Throwing;

        // Projectile Properties
        Item.shootSpeed = 17.85f;
        Item.shoot = ModContent.ProjectileType<DemoniteShurikenProjectile>(); // The projectile that will be thrown
    }

    // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(60);
        recipe.AddIngredient(ItemID.DemoniteBar, 2);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}