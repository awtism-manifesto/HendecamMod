using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class MintalCube : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 26;
        Item.height = 26;
        Item.rare = ItemRarityID.LightRed;
        Item.value = 181000;
        Item.maxStack = 1;
        Item.accessory = true;
        Item.defense = 3;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.accRunSpeed *= 20f;
        player.moveSpeed *= 20f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<CubicMold>();
        recipe.AddIngredient<MintalBar>(12);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}