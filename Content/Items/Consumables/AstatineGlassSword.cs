
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
    public class AstatineGlassSword : ModItem
        {
        public override void SetDefaults()
            {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(silver: 1000);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Shatter;
            Item.DamageType = ModContent.GetInstance<MeleeStupidDamage>();
            Item.maxStack = Item.CommonMaxStack;
            Item.damage = 480;
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
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
            {
            player.lifeRegen += (int)8f;
            return true;
            }
        public override void AddRecipes()
            {
            Recipe recipe = CreateRecipe();
            recipe = CreateRecipe();
            recipe.AddIngredient<AstatineGlass>(10);
            recipe.AddTile(TileID.GlassKiln);
            recipe.Register();
            }
        }
    }