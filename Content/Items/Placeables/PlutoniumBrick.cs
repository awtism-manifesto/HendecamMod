using System.Collections.Generic;

namespace HendecamMod.Content.Items.Placeables;

public class PlutoniumBrick : ModItem
{
    public override void SetStaticDefaults()
    {
        // Registers a vertical animation with 4 frames and each one will last 5 ticks (1/12 second)

        Item.ResearchUnlockCount = 25; // Configure the amount of this item that's needed to research it in Journey mode.
    }

    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 32; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1f;
        Item.rare = ItemRarityID.LightPurple; // The color that the item's name will be in-game.
        Item.value = 2500;
        Item.maxStack = 9999;
        Item.DefaultToPlaceableTile(TileType<Tiles.PlutoniumBrickTile>());
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(5);

        recipe.AddIngredient<PlutoniumOre>();
        recipe.AddIngredient(ItemID.StoneBlock);
        recipe.AddTile(TileID.AdamantiteForge);
        recipe.Register();
    }
}