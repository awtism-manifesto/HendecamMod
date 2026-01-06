
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace HendecamMod.Content.Items.Accessories.Rampart
{
    //[AutoloadEquip(EquipType.Beard)]
    public class ArmoredSling : ModItem
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
            tooltips.Add(new TooltipLine(Mod, "Face", "Grants immunity to Weakness, Withered Weapon,"));
            tooltips.Add(new TooltipLine(Mod, "Face", "Broken Armor, Withered Armor, Bleeding,"));
            tooltips.Add(new TooltipLine(Mod, "Face", "Feral Bite, Poison, and Acid Venom"));
        }
        public override void UpdateEquip(Player player)
        {
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.WitheredWeapon] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.WitheredArmor] = true;
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Rabies] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Venom] = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ArmorBracing, 1);
            recipe.AddIngredient(ItemID.MedicatedBandage, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.Register();
        }
    }
}