using System.Collections.Generic;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items.Weapons.Ammo;

public class PermafrostDart : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.damage = 10; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
        Item.DamageType = DamageClass.Ranged;

        Item.width = 11;
        Item.height = 11;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
        Item.knockBack = 4.5f;
        Item.value = 80;
        Item.rare = ItemRarityID.Pink;
        Item.shoot = ProjectileType<FrostDart>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 5.8f; // The speed of the projectile.
        Item.ammo = AmmoID.Dart; // The ammo class this ammo belongs to.
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica) && CalMerica.TryFind("EssenceofEleum", out ModItem EssenceofEleum))
        {
            recipe = CreateRecipe(100);
            recipe.AddIngredient(EssenceofEleum.Type, 5);

            recipe.Register();
        }

        {
            recipe = CreateRecipe(350);
            recipe.AddIngredient(ItemID.FrostCore);

            recipe.Register();
        }
    }
}