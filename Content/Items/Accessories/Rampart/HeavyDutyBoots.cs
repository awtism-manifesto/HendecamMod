
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace HendecamMod.Content.Items.Accessories.Rampart
{
    //[AutoloadEquip(EquipType.Beard)]
    public class HeavyDutyBoots : ModItem
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
            var line = new TooltipLine(Mod, "Face", "Grants immunity to Mighty Wind");
            tooltips.Add(line);
        }
        public override void UpdateEquip(Player player)
        {
            player.buffImmune[BuffID.WindPushed] = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.RocketBoots);
            recipe.AddIngredient(ItemID.HeavyWorkBench);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}