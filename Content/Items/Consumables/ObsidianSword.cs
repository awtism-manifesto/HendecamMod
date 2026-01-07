
using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Consumables
    {
    //[AutoloadEquip(EquipType.Beard)]
    public class ObsidianSword : ModItem
        {
        public override void SetDefaults()
            {
            Item.width = 36;
            Item.height = 36;
            Item.value = Item.sellPrice(silver: 1000);
            Item.rare = ItemRarityID.Orange;
          

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Shatter;
            Item.DamageType = ModContent.GetInstance<MeleeStupidDamage>();
            Item.maxStack = Item.CommonMaxStack;
            Item.damage = 40;
            Item.knockBack = 40.0f;
            Item.consumable = true;
            Item.ChangePlayerDirectionOnShoot = true;
            Item.buffType = BuffID.Bleeding; // Specify an existing buff to be applied when used.
            Item.buffTime = 300;
            }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
            {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Makes you bleed when swung. It's shattering in your hand, what did you expect?"));
            }
       
        public override void AddRecipes()
            {
            Recipe recipe = CreateRecipe();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Obsidian, 10);
            recipe.AddTile(TileID.GlassKiln);
            recipe.Register();
            }
        }
    }