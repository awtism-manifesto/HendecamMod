using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class SmellingSalts : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Grants immunity to Cerebral Mindtrick, Tipsy, and the Water Candle");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.buffImmune[BuffID.BrainOfConfusionBuff] = true;
        player.buffImmune[BuffID.Tipsy] = true;
        player.buffImmune[BuffID.WaterCandle] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.BottledWater, 1000);
        recipe.AddIngredient<RockSalt>(45);
        recipe.AddIngredient<PurifiedSalt>(9);
        recipe.AddTile(TileID.Furnaces);
        recipe.Register();
    }
}