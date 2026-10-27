using HendecamMod.Common.Systems.Players;
using HendecamMod.Common.Utilities;
using HendecamMod.Content.Buffs;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class BrokenMouse : ModItem
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
        target.brokenMouse = true;
        player.GetAttackSpeed(DamageClass.Generic) += 0.15f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.IronBar, 5);
        recipe.AddIngredient(ItemID.Wire, 10);
        recipe.AddIngredient<ShatteredKeyboard>();
        recipe.AddIngredient(ItemID.TungstenHammer);
        recipe.AddTile(TileID.HeavyWorkBench);
        recipe.Register();
    }
}