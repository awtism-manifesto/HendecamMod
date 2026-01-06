
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace HendecamMod.Content.Items.Accessories.Rampart
{
    //[AutoloadEquip(EquipType.Beard)]
    public class Placebo : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(silver: 500);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "Face", "Grants immunity to Potion Sickness");
            tooltips.Add(line);
        }
        public override void UpdateEquip(Player player)
        {
            player.buffImmune[BuffID.PotionSickness] = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SuperHealingPotion, 9999);
            recipe.AddIngredient(ItemID.RestorationPotion, 9999);
            recipe.AddIngredient(ItemID.StrangeBrew, 9999);
            recipe.AddIngredient(ItemID.BottomlessShimmerBucket, 1);
            recipe.AddIngredient(ItemID.CharmofMyths, 1);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}