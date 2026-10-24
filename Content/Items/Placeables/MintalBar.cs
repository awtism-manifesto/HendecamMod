using System.Collections.Generic;

namespace HendecamMod.Content.Items.Placeables;

public class MintalBar : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 20; // Hitbox width of the item.
        Item.height = 20; // Hitbox height of the item.
        Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
        Item.value = 25000;
        Item.maxStack = 9999;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.useTime = 15;
        Item.DefaultToPlaceableTile(TileType<Tiles.Furniture.MintalBarPlaced>());
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<MintalOre>(5);
        recipe.AddTile(TileID.Hellforge);
        recipe.Register();
    }
}