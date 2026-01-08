
using HendecamMod.Content.Items.Materials;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.Rampart
{   
    //[AutoloadEquip(EquipType.Beard)]
    public class LostFragment3 : ModItem
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
            var line = new TooltipLine(Mod, "Face", "Grants immunity to Betsy's Curse and Oiled");
            tooltips.Add(line);
        }
        public override void UpdateEquip(Player player)
        {
            player.buffImmune[BuffID.BetsysCurse] = true;
            player.buffImmune[BuffID.Oiled] = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ApprenticeStaffT3);
            recipe.AddIngredient(ItemID.DD2ExplosiveTrapT3Popper);
            recipe.AddIngredient<Paper>();
            recipe.AddTile(TileID.Tables);
            recipe.Register();
        }
    }
}