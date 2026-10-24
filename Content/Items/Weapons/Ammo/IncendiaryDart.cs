using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items.Weapons.Ammo;

public class IncendiaryDart : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.damage = 9; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
        Item.DamageType = DamageClass.Ranged;

        Item.width = 13;
        Item.height = 13;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
        Item.knockBack = 2f;
        Item.value = 60;
        Item.rare = ItemRarityID.Orange;
        Item.shoot = ProjectileType<NapalmDart>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 4.9f; // The speed of the projectile.
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
            recipe = CreateRecipe(275);

            recipe.AddIngredient(WoodenDart.Type, 275);
            recipe.AddIngredient(ItemID.HellstoneBar);
            recipe.AddIngredient<CrudeOil>();
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        else
        {
            recipe = CreateRecipe(175);
            recipe.AddIngredient(ItemID.HellstoneBar);
            recipe.AddIngredient<CrudeOil>();
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}