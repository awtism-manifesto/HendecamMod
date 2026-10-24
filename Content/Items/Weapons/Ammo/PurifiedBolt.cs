using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons.Ammo;

// This example is similar to the Wooden Arrow item
public class PurifiedBolt : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.width = 14;
        Item.height = 36;

        Item.damage = 13; // Keep in mind that the arrow's final damage is combined with the bow weapon damage.
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ItemRarityID.Pink;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.knockBack = 1.5f;
        Item.value = Item.sellPrice(copper: 19);
        Item.shoot = ProjectileType<Projectiles.PurifiedBoltProj>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 4.7f; // The speed of the projectile.
        Item.ammo = AmmoID.Arrow; // The ammo class this ammo belongs to.
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(25);
        recipe.AddIngredient<PurifiedSalt>();
        recipe.AddIngredient<CrossbowBolt>(25);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}