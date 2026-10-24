using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Accessories;

public class CatScarf : ModItem
{
   
    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Blue;
        Item.value = 110000;
        Item.defense = 4;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.maxTurrets += 1;
        player.maxMinions += 1;
        player.manaCost *= 0.9f;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Silk, 12);
        recipe.AddIngredient(ItemID.NaturesGift);

        recipe.AddTile(TileID.Loom);

        recipe.Register();
        
    }
}

