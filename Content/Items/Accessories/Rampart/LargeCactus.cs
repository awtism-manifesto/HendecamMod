
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace HendecamMod.Content.Items.Accessories.Rampart
{
    //[AutoloadEquip(EquipType.Beard)]
    public class LargeCactus : ModItem
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
            var line = new TooltipLine(Mod, "Face", "Grants immunity to The Tongue");
            tooltips.Add(line);
        }
        public override void UpdateEquip(Player player)
        {
            player.buffImmune[BuffID.TheTongue] = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Cactus, 50);
            recipe.AddIngredient(ItemID.Spike, 10);
            recipe.AddIngredient(ItemID.WoodenSpike, 10);
            recipe.AddIngredient(ItemID.ClayPot, 1);
            recipe.AddTile(TileID.LivingLoom);
            recipe.Register();
        }
    }
}