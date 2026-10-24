using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons.VapeItems;

public class VapeKit : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 32; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1f;
        Item.rare = ItemRarityID.White; // The color that the item's name will be in-game.
        Item.value = 9000;
        Item.maxStack = 9999;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
      
        recipe.AddIngredient<Polymer>(5);
        recipe.AddIngredient<Rubber>(10);
        recipe.AddIngredient(ItemID.Glass, 5);
        recipe.AddIngredient(ItemID.CopperBar, 4);
        recipe.AddIngredient(ItemID.Cobweb, 15);
        recipe.AddTile(TileID.Anvils);

        recipe.Register();
        recipe = CreateRecipe();

        recipe.AddIngredient<Polymer>(5);
        recipe.AddIngredient<Rubber>(10);
        recipe.AddIngredient(ItemID.Glass, 5);
        recipe.AddIngredient(ItemID.TinBar, 4);
        recipe.AddIngredient(ItemID.Cobweb, 15);
        recipe.AddTile(TileID.Anvils);

        recipe.Register();

        if (ModLoader.TryGetMod("VitalityMod", out Mod Vital) && Vital.TryFind("BronzeAlloy", out ModItem BronzeAlloy))
        {
            recipe = CreateRecipe();

            recipe.AddIngredient<Polymer>(5);
            recipe.AddIngredient<Rubber>(10);
            recipe.AddIngredient(ItemID.Glass, 5);
            recipe.AddIngredient(BronzeAlloy.Type, 3);
            recipe.AddIngredient(ItemID.Cobweb, 15);
            recipe.AddTile(TileID.Anvils);

            recipe.Register();
           
        }
    }
}