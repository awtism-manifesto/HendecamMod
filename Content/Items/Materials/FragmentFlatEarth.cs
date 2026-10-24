using System.Collections.Generic;

namespace HendecamMod.Content.Items.Materials;

public class FragmentFlatEarth : ModItem
{
    public override void SetStaticDefaults()
    {
        // Registers a vertical animation with 4 frames and each one will last 5 ticks (1/12 second)
        ItemID.Sets.ItemIconPulse[Item.type] = true; // The item pulses while in the player's inventory
        ItemID.Sets.ItemNoGravity[Item.type] = true; // Makes the item have no gravity

        Item.ResearchUnlockCount = 25; // Configure the amount of this item that's needed to research it in Journey mode.
    }

    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 32; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1.22f;
        Item.rare = ItemRarityID.Cyan; // The color that the item's name will be in-game.
        Item.value = 2000;
        Item.maxStack = 9999;
    }

    public override void AddRecipes()
    {
        Recipe
            recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.FragmentNebula);
        recipe.AddIngredient(ItemID.FragmentSolar);
        recipe.AddIngredient(ItemID.FragmentStardust);
        recipe.AddIngredient(ItemID.FragmentVortex);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}