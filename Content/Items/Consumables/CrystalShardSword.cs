
using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Consumables
    {
    public class CrystalShardSword : ModItem
        {
        public override void SetDefaults()
            {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(copper: 1250);
            Item.rare = ItemRarityID.LightRed;
           

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Shatter;
            Item.DamageType = ModContent.GetInstance<MeleeStupidDamage>();
            Item.maxStack = Item.CommonMaxStack;
            Item.damage = 80;
            Item.knockBack = 6.0f;
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
        
        public override void AddRecipes()
            {
            Recipe recipe = CreateRecipe();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrystalShard, 10);
            recipe.AddTile(TileID.GlassKiln);
            recipe.Register();
            }
        }
    }