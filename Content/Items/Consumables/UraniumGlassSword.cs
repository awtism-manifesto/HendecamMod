
using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Consumables
    {
    public class UraniumGlassSword : ModItem
        {
        public override void SetDefaults()
            {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(silver: 10);
            Item.rare = ItemRarityID.Green;
           

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 19;
            Item.useAnimation = 19;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Shatter;
            Item.DamageType = ModContent.GetInstance<MeleeStupidDamage>();
            Item.maxStack = Item.CommonMaxStack;
            Item.damage = 60;
            Item.knockBack = 6.7f;
            Item.consumable = true;
            Item.ChangePlayerDirectionOnShoot = true;
            Item.buffType = BuffID.Bleeding; 
            Item.buffType = ModContent.BuffType<RadPoisoning>();
            Item.buffTime = 300;
            Item.useTurn = true;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
           
            target.AddBuff(ModContent.BuffType<RadPoisoning>(), 300);

        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
            {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Makes you bleed when swung. It's shattering in your hand, what did you expect?"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Also irradiates both you and enemies"));
        }

       
        public override void AddRecipes()
            {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<UraniumGlass>(10);
            recipe.AddTile(TileID.GlassKiln);
            recipe.Register();
            }
        }
    }