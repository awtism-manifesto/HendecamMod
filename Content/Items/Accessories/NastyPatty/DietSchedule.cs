using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
public class DietSchedule : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Grants 10% damage reduction, at the cost of Food Buffs");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<NastyEndurance>().NastyEffect = true;
        player.buffImmune[BuffID.WellFed] = true;
        player.buffImmune[BuffID.WellFed2] = true;
        player.buffImmune[BuffID.WellFed3] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DontStarveShaderItem);
        recipe.AddIngredient(ItemID.Book);
        recipe.AddTile(TileID.Bookcases);
        recipe.Register();
    }
}