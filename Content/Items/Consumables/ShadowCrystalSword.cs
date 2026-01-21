
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Rarities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Consumables
    {
    public class ShadowCrystalSword : ModItem
        {
        public override void SetDefaults()
            {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(silver: 999);
            Item.rare = ModContent.RarityType<Seizure>();


            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Shatter;
            Item.DamageType = ModContent.GetInstance<MeleeStupidDamage>();
            Item.maxStack = Item.CommonMaxStack;
            Item.damage = 1120;
            Item.knockBack = 17.5f;
            Item.consumable = true;
            Item.ChangePlayerDirectionOnShoot = true;
            Item.buffType = BuffID.Bleeding; 
            Item.buffTime = 300;
            Item.useTurn = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
            {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Makes you bleed when swung. It's shattering in your hand, what did you expect?"));
            }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
            {
            player.lifeRegen += (int)8f;
            return true;
            }
        public override void AddRecipes()
            {
            Recipe recipe = CreateRecipe(10);
            recipe = CreateRecipe();
            recipe.AddIngredient<LoreAccurateBlackshard>();
            recipe.AddTile(TileID.GlassKiln);
            recipe.Register();
            }
        }
    }