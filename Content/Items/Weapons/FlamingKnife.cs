using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Items.Materials;
using Microsoft.Build.Evaluation;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace HendecamMod.Content.Items.Weapons
{
    public class FlamingKnife : ModItem
    {
        public override void SetDefaults()
        {
            // Alter any of these values as you see fit, but you should probably keep useStyle on 1, as well as the noUseGraphic and noMelee bools

            // Common Properties
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 5);
            Item.maxStack = 9999;

            // Use Properties
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = true;

            // Weapon Properties			
            Item.damage = 16;
            Item.knockBack = 5f;
            Item.noUseGraphic = true; // The item should not be visible when used
            Item.noMelee = true; // The projectile will do the damage and not the item
            Item.DamageType = DamageClass.Throwing;

            // Projectile Properties
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<FlamingKnifeProjectile>(); // The projectile that will be thrown
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "Explodes every time it hits an enemy");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);

        }
        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(100);
            recipe.AddIngredient<FireDiamond>(1);
            recipe.AddIngredient(ItemID.HellstoneBar, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
