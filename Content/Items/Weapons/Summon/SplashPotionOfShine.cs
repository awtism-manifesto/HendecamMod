using System.Collections.Generic;
using HendecamMod.Content.Projectiles.Items;

namespace HendecamMod.Content.Items.Weapons.Summon;

public class SplashPotionOfShine : ModItem // MAG THE COMMENTS ON THIS FILE ARE NOT EXAMPLEMOD COMMENTS, DO NOT DELETE THEM
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.damage = 9;
        Item.DamageType = DamageClass.Summon;
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.width = 13;
        Item.height = 13;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.knockBack = 3.5f;
        Item.value = 125;
        Item.rare = ItemRarityID.White;
        Item.shoot = ProjectileType<SplashPotionShine>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 8.25f; // The speed of the projectile.

        // if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica))
        // {
        //   Item.DamageType = DamageClass.Throwing; // make throwing summon hybrid when thorium thrower multiclasses are implemented 
        // }
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(25);
        recipe.AddIngredient(ItemID.ShinePotion);

        recipe.Register();
    }
}