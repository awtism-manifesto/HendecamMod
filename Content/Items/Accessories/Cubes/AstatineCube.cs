using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class AstatineCube : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 26; 
        Item.height = 26; 
        Item.rare = ItemRarityID.Red; 
        Item.value = 1230000;
        Item.maxStack = 1;
        Item.accessory = true;
        Item.defense = 15;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.maxFallSpeed = player.maxFallSpeed * Main.rand.NextFloat(-0.5f, 5f);
        player.accRunSpeed *= Main.rand.NextFloat(0.01f, 5f);
        player.moveSpeed *= Main.rand.NextFloat(0.01f, 5f);
        player.runAcceleration *= Main.rand.NextFloat(0.01f, 5f);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<CubicMold>();
        recipe.AddIngredient<AstatineBar>(12);
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }
}