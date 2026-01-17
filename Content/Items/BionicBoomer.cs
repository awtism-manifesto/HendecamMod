using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items
{
    public class BionicBoomer : ModItem
    {


        public override void SetDefaults()
        {
            Item.width = 44; // The width of the item's hitbox.
            Item.height = 44; // The height of the item's hitbox.

            Item.useStyle = ItemUseStyleID.Shoot; // The way the item is used (e.g. swinging, throwing, etc.)
            Item.useTime = 8; // All vanilla yoyos have a useTime of 25.
            Item.useAnimation = 8; // All vanilla yoyos have a useAnimation of 25.
            Item.noMelee = true; // This makes it so the item doesn't do damage to enemies (the projectile does that).
            Item.scale = 0.75f;
            Item.UseSound = SoundID.Item1; // The sound that will play when the item is used.

            Item.damage = 180; // The amount of damage the item does to an enemy or player.
            Item.DamageType = ModContent.GetInstance<MeleeRangedDamage>(); // The type of damage the weapon does. MeleeNoSpeed means the item will not scale with attack speed.
            Item.knockBack = 5.5f; // The amount of knockback the item inflicts.
            Item.ArmorPenetration = 45;

            Item.rare = ItemRarityID.Red; // The item's rarity. This changes the color of the item's name.
            Item.value = Item.buyPrice(gold: 1150); // The amount of money that the item is can be bought for.

            Item.shoot = ModContent.ProjectileType<AstaGlaive>(); // Which projectile this item will shoot. We set this to our corresponding projectile.
            Item.shootSpeed = 21.25f; // The velocity of the shot projectile.			
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "Throws armor piercing astatine glaives at superhuman speeds");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "IS able to benefit from attack speed bonuses")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);



            
        }

       

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20f, 4f);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<TheImperialBoomerangs>();
            recipe.AddIngredient<RadRang>();
            recipe.AddIngredient<FissionDrive>();
            recipe.AddIngredient<AstatineBar>(5);
            recipe.AddIngredient(ItemID.LunarBar, 5);

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.Register();

        }
    }
}