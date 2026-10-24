using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items.Weapons.Ammo;

public class RadBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.damage = 5; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
        Item.DamageType = DamageClass.Ranged;
        Item.ArmorPenetration = 5;
        Item.width = 16;
        Item.height = 16;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
        Item.knockBack = 0.5f;
        Item.value = 89;

        Item.rare = ItemRarityID.LightRed;
        Item.shoot = ProjectileType<RadShot>();
        Item.shootSpeed = 9.95f; // The speed of the projectile.
        Item.ammo = AmmoID.Bullet; // The ammo class this ammo belongs to.
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(50);
        recipe.AddIngredient<UraniumBar>();
        recipe.AddIngredient(ItemID.EmptyBullet, 50);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}