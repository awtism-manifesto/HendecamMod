using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class Placebo : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 500);
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Grants immunity to Potion Sickness");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.ClearBuff(BuffID.PotionSickness);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<PlasticScrap>(2);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}