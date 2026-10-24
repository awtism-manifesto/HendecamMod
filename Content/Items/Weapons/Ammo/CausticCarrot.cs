using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons.Ammo;

// This example is similar to the Wooden Arrow item
public class CausticCarrot : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.width = 14;
        Item.height = 36;

        Item.damage = 11; // Keep in mind that the arrow's final damage is combined with the bow weapon damage.
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ItemRarityID.Orange;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.knockBack = 1.5f;
        Item.value = 59;
        Item.shoot = ProjectileType<Projectiles.CarrotProj>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 9.99f; // The speed of the projectile.
        Item.ammo = AmmoID.Arrow; // The ammo class this ammo belongs to.
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(125);
       
        recipe.AddIngredient(ItemID.WoodenArrow, 125);
        recipe.AddIngredient<LycopiteBar>();
        recipe.Register();
        if (ModLoader.TryGetMod("Spooky", out Mod SpookMerica) && SpookMerica.TryFind("RottenChunk", out ModItem RottenChunk))
        {
            recipe.AddIngredient(RottenChunk.Type);
        }
    }
}