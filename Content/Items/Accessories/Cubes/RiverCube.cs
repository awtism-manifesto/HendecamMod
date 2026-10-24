using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class RiverCube : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 26;
        Item.height = 26;
        Item.rare = ItemRarityID.Blue;
        Item.value = 1;
        Item.maxStack = 1;
        Item.accessory = true;
        Item.defense = -1;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.maxFallSpeed = player.maxFallSpeed * 128f;
        player.fallStart = (int)(player.fallStart * 0.01f);
        player.stairFall = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<CubicMold>();
        recipe.AddIngredient(ItemID.CrabBanner, 6);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}