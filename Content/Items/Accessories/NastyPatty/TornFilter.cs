using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class TornFilter : ModItem
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
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Grants 30% more Attack Speed and double your breath timer"));
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "No longer gain effects from Activated or Enviornmental Buffs"));
    }

    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<NastyBreath>().NastyEffect = true;
        player.GetModPlayer<NastySpeed>().NastyEffect = true;
        player.buffImmune[BuffID.AmmoBox] = true;
        player.buffImmune[BuffID.Bewitched] = true;
        player.buffImmune[BuffID.Clairvoyance] = true;
        player.buffImmune[BuffID.Sharpened] = true;
        player.buffImmune[BuffID.WarTable] = true;
        player.buffImmune[BuffID.SugarRush] = true;
        player.buffImmune[BuffID.Campfire] = true;
        player.buffImmune[BuffID.DryadsWard] = true;
        player.buffImmune[BuffID.Sunflower] = true;
        player.buffImmune[BuffID.HeartLamp] = true;
        player.buffImmune[BuffID.Honey] = true;
        player.buffImmune[BuffID.PeaceCandle] = true;
        player.buffImmune[BuffID.StarInBottle] = true;
        player.buffImmune[BuffID.CatBast] = true;
        player.buffImmune[BuffID.MonsterBanner] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<GasMask>();
        recipe.AddIngredient<BrokenMouse>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}