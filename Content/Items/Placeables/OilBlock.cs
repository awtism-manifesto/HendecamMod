using System.Collections.Generic;

namespace HendecamMod.Content.Items.Placeables;

public class OilBlock : ModItem
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
        Item.rare = ItemRarityID.Blue; // The color that the item's name will be in-game.
        Item.value = 1150;
        Item.maxStack = 9999;
        Item.DefaultToPlaceableTile(TileType<Tiles.OilDeposit>());
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        if (ModLoader.TryGetMod("Macrocosm", out Mod MacroMerica) && MacroMerica.TryFind("OilShale", out ModItem OilShale))
        {
            recipe = CreateRecipe();

            recipe.AddIngredient(OilShale.Type);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}