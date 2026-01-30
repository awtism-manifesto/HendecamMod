using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
public class PrisonFood : ModItem
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
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Grants 25 fall distance and 10% Damage Reduction"));
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "No longer gain effects from Food or Exploration Buffs"));
    }

    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<NastyEndurance>().NastyEffect = true;
        player.GetModPlayer<NastyFall>().NastyEffect = true;
        player.buffImmune[BuffID.Featherfall] = true;
        player.buffImmune[BuffID.Flipper] = true;
        player.buffImmune[BuffID.Gills] = true;
        player.buffImmune[BuffID.Mining] = true;
        player.buffImmune[BuffID.NightOwl] = true;
        player.buffImmune[BuffID.ObsidianSkin] = true;
        player.buffImmune[BuffID.Shine] = true;
        player.buffImmune[BuffID.Spelunker] = true;
        player.buffImmune[BuffID.Swiftness] = true;
        player.buffImmune[BuffID.Dangersense] = true;
        player.buffImmune[BuffID.WaterWalking] = true;
        player.buffImmune[BuffID.Gravitation] = true;
        player.buffImmune[BuffID.WellFed] = true;
        player.buffImmune[BuffID.WellFed2] = true;
        player.buffImmune[BuffID.WellFed3] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<DietSchedule>();
        recipe.AddIngredient<AnkleMonitor>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}