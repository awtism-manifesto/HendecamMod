using System.Collections.Generic;

namespace HendecamMod.Content.Items.Materials;

public class PurifiedSalt : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 32; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1.22f;
        Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
        Item.value = 6750;
        Item.maxStack = 9999;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(9);

        recipe.AddIngredient(ItemID.PixieDust, 2);
        recipe.AddIngredient(ItemID.SoulofLight);
        recipe.AddIngredient<RockSalt>(9);
        recipe.AddTile(TileID.Furnaces);
        recipe.Register();
    }
}