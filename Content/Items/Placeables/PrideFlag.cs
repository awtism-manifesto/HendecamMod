using System.Collections.Generic;

namespace HendecamMod.Content.Items.Placeables;

public class PrideFlag : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 32; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.rare = ItemRarityID.White; // The color that the item's name will be in-game.
        Item.value = 2950;
        Item.maxStack = 9999;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.autoReuse = true;
        Item.DefaultToPlaceableTile(TileType<Tiles.PrideFlagPlaced>());
    }

    // Another method of hiding can be done if you want to hide just one line.
    // tooltips.FirstOrDefault(x => x.Mod == "ExampleMod" && x.Name == "Verbose:RemoveMe")?.Hide();

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<BlankFlag>();
        recipe.AddIngredient(ItemID.Wood, 2);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}