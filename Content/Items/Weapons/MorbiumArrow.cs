using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Weapons;

// This example is similar to the Wooden Arrow item
public class MorbiumArrow : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.width = 14;
        Item.height = 36;
        Item.damage = 5; // Keep in mind that the arrow's final damage is combined with the bow weapon damage.
        Item.DamageType = DamageClass.Ranged;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.knockBack = 0.5f;
        Item.value = Item.sellPrice(silver: 2);
        Item.shoot = ModContent.ProjectileType<MorbiumArrowProj>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 0.67f; // The speed of the projectile.
        Item.ammo = AmmoID.Arrow; // The ammo class this ammo belongs to.
        Item.rare = ItemRarityID.Yellow;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Morb");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);




    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(200);
        recipe.AddIngredient<MorbiumBar>(2);
        recipe.AddIngredient(ItemID.WoodenArrow, 200);
        recipe.Register();
    }

}