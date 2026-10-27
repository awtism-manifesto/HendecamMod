using HendecamMod.Common.Systems.Players;
using HendecamMod.Common.Utilities;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class AlphaMaleChecklist : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 1000);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
    }

    public override void UpdateEquip(Player player)
    {
        var target = player.GetHCPlayer();
        target.alphaChecklist = true;
        player.GetDamage(DamageClass.Generic) += 0.33f;

    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.RagePotion, 10);
        recipe.AddIngredient(ItemID.WrathPotion, 10);
        recipe.AddIngredient<Paper>();
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}