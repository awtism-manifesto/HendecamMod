using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles;

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
        Item.damage = 7; // Keep in mind that the arrow's final damage is combined with the bow weapon damage.
        Item.DamageType = ModContent.GetInstance<RangedMagicDamage>();
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.knockBack = 0.5f;
        Item.value = 91;
        Item.shoot = ModContent.ProjectileType<MintalArrowProjectile>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 2.66f; // The speed of the projectile.
        Item.ammo = AmmoID.Arrow; // The ammo class this ammo belongs to.
        Item.rare = ItemRarityID.Orange;
        Item.ArmorPenetration = 5;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Homes in on the mouse while not firing");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Controlling the arrows slowly drains your mana")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(125);
        recipe.AddIngredient<MintalBar>();
        recipe.AddIngredient(ItemID.WoodenArrow, 125);
        recipe.Register();
    }
}