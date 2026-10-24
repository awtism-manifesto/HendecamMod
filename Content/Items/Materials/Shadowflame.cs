using System.Collections.Generic;

namespace HendecamMod.Content.Items.Materials;

public class Shadowflame : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 32; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1.22f;
        Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
        Item.value = 1499;
        Item.maxStack = 9999;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(50);
        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica) && CalMerica.TryFind("TheFirstShadowflame", out ModItem TheFirstShadowflame))
        {
            recipe.AddIngredient(TheFirstShadowflame.Type);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.Register();
        }
    }
}