using System.Collections.Generic;

namespace HendecamMod.Content.Items.Materials;

public class WoodenStick : ModItem
    {
    public override void SetStaticDefaults()
        {
        Item.ResearchUnlockCount = 25;
        }

    public override void SetDefaults()
        {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 32; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.rare = ItemRarityID.White; // The color that the item's name will be in-game.
        Item.value = 10;
        Item.maxStack = 64;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.autoReuse = true;
        }

    public override void AddRecipes()
        {
        Recipe recipe = CreateRecipe(8);
        recipe.AddRecipeGroup("Wood");
        recipe.Register();
        }
    }