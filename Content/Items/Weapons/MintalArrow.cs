using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Weapons;

// This example is similar to the Wooden Arrow item
public class MintalArrow : ModItem
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
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.knockBack = 0.5f;
        Item.value = Item.sellPrice(silver: 2);
        Item.shoot = ModContent.ProjectileType<MintalArrowProjectile>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 2.66f; // The speed of the projectile.
        Item.ammo = AmmoID.Arrow; // The ammo class this ammo belongs to.
        Item.rare = ItemRarityID.Blue;
        Item.ArmorPenetration = 5;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Randomly bounces in a new direction when it hits an enemy");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);




    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(75);
        recipe.AddIngredient<MintalBar>(1);
        recipe.AddIngredient(ItemID.WoodenArrow, 75);
        recipe.Register();
    }

}