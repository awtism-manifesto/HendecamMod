using HendecamMod.Common.Utilities;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class Grindstone : ModItem
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
        target.grindstone = true;
        player.GetArmorPenetration(DamageClass.Generic) += 40f;

    }

    public override void AddRecipes()
        {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SharpeningStation);
        recipe.AddTile(TileID.SharpeningStation);
        recipe.Register();
        }
    }