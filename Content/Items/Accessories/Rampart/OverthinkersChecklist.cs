
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace HendecamMod.Content.Items.Accessories.Rampart
{
    //[AutoloadEquip(EquipType.Beard)]
    public class OverthinkersChecklist : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(silver: 2000);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "Face", "Grants immunity to Penetrated, Blood Butchered, Midas, Dryad's Bane, Betsy's Curse, Oiled, Daybroken, and Celled");
            tooltips.Add(line);
        }
        public override void UpdateEquip(Player player)
        {
            player.buffImmune[BuffID.BoneJavelin] = true;
            player.buffImmune[BuffID.BloodButcherer] = true;
            player.buffImmune[BuffID.Midas] = true;
            player.buffImmune[BuffID.DryadsWardDebuff] = true;
            player.buffImmune[BuffID.BetsysCurse] = true;
            player.buffImmune[BuffID.Oiled] = true;
            player.buffImmune[BuffID.Daybreak] = true;
            player.buffImmune[BuffID.StardustMinionBleed] = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe = CreateRecipe();
            recipe.AddIngredient<FirstHalf>(1);
            recipe.AddIngredient<SecondHalf>(1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.Register();
        }
    }
}