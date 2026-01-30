using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
public class EmptyFishingBucket : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 500);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Grants 4 luck, at the cost of Fishing Buffs");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<NastyLuck>().NastyEffect = true;
        player.buffImmune[BuffID.Crate] = true;
        player.buffImmune[BuffID.Fishing] = true;
        player.buffImmune[BuffID.Sonar] = true;
        player.buffImmune[BuffID.Lucky] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient(ItemID.EmptyBucket);
        recipe.AddIngredient<PlasticScrap>(10);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}