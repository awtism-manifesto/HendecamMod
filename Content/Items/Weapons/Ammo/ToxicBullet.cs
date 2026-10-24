using System.Collections.Generic;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items.Weapons.Ammo;

public class ToxicBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.damage = 8; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
        Item.DamageType = DamageClass.Ranged;
        Item.ArmorPenetration = 2;
        Item.width = 16;
        Item.height = 16;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
        Item.knockBack = 0.5f;
        Item.value = 36;
        Item.rare = ItemRarityID.Green;
        Item.shoot = ProjectileType<PoisonBullet>();
        Item.shootSpeed = 5f; // The speed of the projectile.
        Item.ammo = AmmoID.Bullet; // The ammo class this ammo belongs to.
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(100);
        recipe.AddIngredient(ItemID.MusketBall, 100);
        recipe.AddIngredient(ItemID.Stinger);

        recipe.Register();

        if (ModLoader.TryGetMod("Spooky", out Mod SpookMerica) && SpookMerica.TryFind("RottenChunk", out ModItem RottenChunk))
        {
            recipe = CreateRecipe(100);
            recipe.AddIngredient(ItemID.MusketBall, 100);
           
            recipe.AddIngredient(RottenChunk.Type);
            recipe.Register();
           
        }
    }
}