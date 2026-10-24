using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Placeables;

public class CultistCyclotron : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 20;
        Item.rare = ItemRarityID.Red;
        Item.value = 7500000;
        Item.maxStack = 9999;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.useTime = 15;
        Item.DefaultToPlaceableTile(TileType<Tiles.Furniture.CultistCyclotronPlaced>());
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.LunarCraftingStation);
        recipe.AddIngredient<AstatineOre>(135);
        recipe.AddIngredient<PlutoniumBar>(15);
        recipe.AddIngredient<UraniumBar>(30);

        recipe.AddTile(TileID.AdamantiteForge);
        recipe.Register();
        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica) && ThorMerica.TryFind("ThoriumBar", out ModItem ThoriumBar))
        {
            recipe.AddIngredient(ThoriumBar.Type, 45);
        }
    }
}