
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using HendecamMod.Content.Items.Placeables;

namespace HendecamMod.Content.Items.Accessories.Rampart
{
    //[AutoloadEquip(EquipType.Beard)]
    public class SteelSunscreen : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(silver: 1000);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "Face", "Grants immunity to Moon Bite and The Tongue");
            tooltips.Add(line);
        }
        public override void UpdateEquip(Player player)
        {
            player.buffImmune[BuffID.MoonLeech] = true;
            player.buffImmune[BuffID.TheTongue] = true;
        }
            public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe = CreateRecipe();
            recipe.AddIngredient<LeechRepellant>(1);
            recipe.AddIngredient<LargeCactus>(1);
            recipe.AddIngredient<SteelBar>(15);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}