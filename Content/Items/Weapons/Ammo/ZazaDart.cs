using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items.Weapons.Ammo;

public class ZazaDart : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.damage = 8; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
        Item.DamageType = GetInstance<RangedStupidDamage>();

        Item.width = 13;
        Item.height = 13;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
        Item.knockBack = 1.5f;
        Item.value = 19;
        Item.rare = ItemRarityID.Green;
        Item.shoot = ProjectileType<WeedDart>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 4.5f; // The speed of the projectile.
        Item.ammo = AmmoID.Dart; // The ammo class this ammo belongs to.
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        if (ModLoader.TryGetMod("CalamityFables", out Mod CalamityFablesMerica) && CalamityFablesMerica.TryFind("WoodenDart", out ModItem WoodenDart))
        {
            recipe = CreateRecipe(150);

            recipe.AddIngredient(WoodenDart.Type, 150);
            recipe.AddIngredient<WeedLeaves>(3);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }
        else
        {
            recipe = CreateRecipe(50);
            recipe.AddIngredient<WeedLeaves>();
            recipe.AddRecipeGroup("Wood", 2);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }
    }
}