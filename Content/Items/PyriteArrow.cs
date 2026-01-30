using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items;

// This example is similar to the Wooden Arrow item
public class PyriteArrow : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.width = 14;
        Item.height = 36;

        Item.damage = 6; // Keep in mind that the arrow's final damage is combined with the bow weapon damage.
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ItemRarityID.Green;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.knockBack = 1.5f;
        Item.value = Item.buyPrice(copper: 28);
        Item.shoot = ModContent.ProjectileType<PyriteArrowProj>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 3.95f; // The speed of the projectile.
        Item.ammo = AmmoID.Arrow; // The ammo class this ammo belongs to.
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Leaves a trail of sparks as it flies")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);




    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(150);
        recipe.AddIngredient<PyriteBar>();
        recipe.AddIngredient(ItemID.WoodenArrow, 150);
        recipe.Register();
    }

}