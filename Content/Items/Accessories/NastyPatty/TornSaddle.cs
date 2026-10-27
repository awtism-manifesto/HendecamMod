using HendecamMod.Common.Utilities;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class TornSaddle : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 500);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
    }

    public override void UpdateEquip(Player player)
    {
        var target = player.GetHCPlayer();
        target.tornSaddle = true;
        player.breathEffectiveness += 2f;
        player.GetAttackSpeed(DamageClass.Generic) += 0.15f;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DarkHorseSaddle);
        recipe.AddIngredient(ItemID.MajesticHorseSaddle);
        recipe.AddIngredient(ItemID.PaintedHorseSaddle);
        recipe.AddIngredient(ItemID.SlimySaddle);
        recipe.AddIngredient(ItemID.HardySaddle);
        recipe.AddTile(TileID.Sawmill);
        recipe.Register();
    }
}