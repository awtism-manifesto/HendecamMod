
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
public class DustyMedal : ModItem
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
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Grants 50 mana and 4 Luck"));
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "No longer gain effects from Building or Fishing Buffs"));
    }
    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<NastyLuck>().NastyEffect = true;
        player.GetModPlayer<NastyMana>().NastyEffect = true;
        player.buffImmune[BuffID.Crate] = true;
        player.buffImmune[BuffID.Fishing] = true;
        player.buffImmune[BuffID.Sonar] = true;
        player.buffImmune[BuffID.Lucky] = true;
        player.buffImmune[BuffID.Builder] = true;
        player.buffImmune[BuffID.BiomeSight] = true;
        player.buffImmune[BuffID.Invisibility] = true;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<EmptyFishingBucket>(1);
        recipe.AddIngredient<OldOnesGlasses>(1);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}