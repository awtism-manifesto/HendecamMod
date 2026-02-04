using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Placeables;

public class CultistCyclotron : ModItem
{
    public override void SetDefaults()
    {
        // Common Properties
        Item.width = 20; // Hitbox width of the item.
        Item.height = 20; // Hitbox height of the item.
        Item.rare = ItemRarityID.Red; // The color that the item's name will be in-game.
        Item.value = 7500000;
        Item.maxStack = 9999;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.useTime = 15;
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.CultistCyclotronPlaced>());
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Used for crafting higher-tier radioactive items");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Also functions as an Ancient Manipulator")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<UraniumBar>(30);
        recipe.AddIngredient<PlutoniumBar>(15);
        recipe.AddIngredient<AstatineOre>(135);
        recipe.AddIngredient(ItemID.LunarCraftingStation);
        recipe.AddTile(TileID.AdamantiteForge);
        recipe.Register();
    }
}