
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace HendecamMod.Content.Items.Accessories.Rampart
{
    //[AutoloadEquip(EquipType.Beard)]
    public class SpotlightShield : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.defense = 4;
            Item.value = Item.sellPrice(silver: 2000);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "Face", "Grants immunity to Burning, OnFire, Dazed, and Knockback");
            tooltips.Add(line);
        }
        public override void UpdateEquip(Player player)
        {
            player.buffImmune[BuffID.Burning] = true;
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.Dazed] = true;
            player.fireWalk = true;
            player.noKnockback = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ObsidianShield, 1);
            recipe.AddIngredient(ItemID.ReflectiveShades, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.Register();
        }
    }
}