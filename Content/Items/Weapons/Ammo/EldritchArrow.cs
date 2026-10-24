using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;

namespace HendecamMod.Content.Items.Weapons.Ammo;

// This example is similar to the Wooden Arrow item
public class EldritchArrow : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.width = 14;
        Item.height = 36;

        Item.damage = 19; // Keep in mind that the arrow's final damage is combined with the bow weapon damage.
        Item.DamageType = GetInstance<RangedSummonDamage>();
        Item.rare = ItemRarityID.Cyan;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.knockBack = 5f;
        Item.ArmorPenetration = 5;
        Item.value = Item.sellPrice(silver: 1);
        Item.shoot = ProjectileType<Projectiles.EldritchProj>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 2.5f; // The speed of the projectile.
        Item.ammo = AmmoID.Arrow; // The ammo class this ammo belongs to.
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(666);

        recipe.AddIngredient(ItemID.CursedArrow, 222);
        recipe.AddIngredient<ShadowflameArrow>(222);
        recipe.AddIngredient(ItemID.IchorArrow, 222);
        recipe.AddIngredient(ItemID.Ectoplasm, 3);
        recipe.Register();
        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica) && ThorMerica.TryFind("CursedCloth", out ModItem CursedCloth))
        {
            recipe.AddIngredient(CursedCloth.Type, 2);
        }
    }
}