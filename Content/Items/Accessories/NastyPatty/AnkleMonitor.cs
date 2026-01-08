
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty
    {
    //[AutoloadEquip(EquipType.Beard)]
    public class AnkleMonitor : ModItem
        {
        public override void SetDefaults()
            {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(silver: 500);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
            {
            var line = new TooltipLine(Mod, "Face", "Doubles your safe fall distance, at the cost of Exploration Buffs");
            tooltips.Add(line);
            }
        public override void UpdateEquip(Player player)
            {
            player.GetModPlayer<NastyFall>().NastyEffect = true;
            player.buffImmune[BuffID.Featherfall] = true;
            player.buffImmune[BuffID.Flipper] = true;
            player.buffImmune[BuffID.Gills] = true;
            player.buffImmune[BuffID.Mining] = true;
            player.buffImmune[BuffID.NightOwl] = true;
            player.buffImmune[BuffID.ObsidianSkin] = true;
            player.buffImmune[BuffID.Shine] = true;
            player.buffImmune[BuffID.Spelunker] = true;
            player.buffImmune[BuffID.Swiftness] = true;
            player.buffImmune[BuffID.Dangersense] = true;
            player.buffImmune[BuffID.WaterWalking] = true;
            player.buffImmune[BuffID.Gravitation] = true;
            }
        public override void AddRecipes()
            {
            Recipe recipe = CreateRecipe();
           
            recipe.AddIngredient(ItemID.IronCrate, 1);
            recipe.AddIngredient(ItemID.Ruby, 1);
            recipe.AddIngredient<Polymer>(10);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.Register();
            }
        }
    }