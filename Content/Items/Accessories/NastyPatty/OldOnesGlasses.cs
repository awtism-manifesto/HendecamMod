
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty
    {
    //[AutoloadEquip(EquipType.Beard)]
    public class OldOnesGlasses : ModItem
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
            var line = new TooltipLine(Mod, "Face", "Grants 50 more Mana, at the cost of Building Buffs");
            tooltips.Add(line);
            }
        public override void UpdateEquip(Player player)
            {
            player.GetModPlayer<NastyMana>().NastyEffect = true;
            player.buffImmune[BuffID.Builder] = true;
            player.buffImmune[BuffID.BiomeSight] = true;
            player.buffImmune[BuffID.Invisibility] = true;
            }
        public override void AddRecipes()
            {
            Recipe recipe = CreateRecipe();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DD2ElderCrystal, 2);
            recipe.AddIngredient(ItemID.DD2ElderCrystalStand, 1);
            recipe.AddIngredient(ItemID.DefenderMedal, 15);
            recipe.AddTile(TileID.Tables);
            recipe.AddTile(TileID.Chairs);
            recipe.Register();
            }
        }
    }