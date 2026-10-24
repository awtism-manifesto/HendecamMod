using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class UraniumCube : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 26; // Hitbox width of the item.
        Item.height = 26; // Hitbox height of the item.
        Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.
        Item.value = 152000;
        Item.maxStack = 1;
        Item.accessory = true;
        Item.defense = 5;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.maxFallSpeed = player.maxFallSpeed * Main.rand.NextFloat(-0.5f, 5f);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<CubicMold>();
        recipe.AddIngredient<UraniumBar>(12);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}