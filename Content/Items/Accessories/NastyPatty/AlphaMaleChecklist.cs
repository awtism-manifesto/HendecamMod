
using HendecamMod.Content.Items.Accessories.Rampart;
using HendecamMod.Content.Items.Materials;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty
{
    //[AutoloadEquip(EquipType.Beard)]
    public class AlphaMaleChecklist : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(silver: 1000);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "Face", "Grants 50% more damage, at the cost of Accessory Buffs");
            tooltips.Add(line);
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<NastyDamage>().NastyEffect = true;
            player.buffImmune[BuffID.Werewolf] = true;
            player.buffImmune[BuffID.Merfolk] = true;
            player.buffImmune[BuffID.IceBarrier] = true;
            player.buffImmune[BuffID.PaladinsShield] = true;
            player.buffImmune[BuffID.Panic] = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
           
            recipe.AddIngredient(ItemID.RagePotion, 10);
            recipe.AddIngredient(ItemID.WrathPotion, 10);
            recipe.AddIngredient<Paper>();
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}