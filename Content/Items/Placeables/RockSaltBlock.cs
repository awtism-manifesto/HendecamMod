using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Placeables;

public class RockSaltBlock : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 32; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1.22f;
        Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.
        Item.value = 10250;
        Item.maxStack = 9999;
        Item.DefaultToPlaceableTile(TileType<Tiles.RockSaltTile>());
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<RockSalt>(5);

        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}