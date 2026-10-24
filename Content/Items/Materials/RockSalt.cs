using System.Collections.Generic;

namespace HendecamMod.Content.Items.Materials;

public class RockSalt : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 32; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1.22f;
        Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.
        Item.value = 1440;
        Item.maxStack = 9999;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(5);

        recipe.AddIngredient(ItemID.PurificationPowder, 2);
        recipe.AddIngredient(ItemID.DemoniteBar);
        recipe.AddTile(TileID.Furnaces);
        recipe.Register();

        recipe = CreateRecipe(5);

        recipe.AddIngredient(ItemID.PurificationPowder, 2);
        recipe.AddIngredient(ItemID.CrimtaneBar);
        recipe.AddTile(TileID.Furnaces);
        recipe.Register();

        recipe = CreateRecipe(5);

        recipe.AddIngredient<FireDiamond>(2);
        recipe.AddTile(TileID.Furnaces);

        recipe.Register();
    }
}