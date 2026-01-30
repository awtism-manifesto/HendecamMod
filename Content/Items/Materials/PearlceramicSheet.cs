using System.Collections.Generic;
using HendecamMod.Content.Items.Placeables;

namespace HendecamMod.Content.Items.Materials;

public class PearlceramicSheet : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 32; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1.22f;
        Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
        Item.value = 2250;
        Item.maxStack = 9999;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(5);

        recipe.AddIngredient(ItemID.Bone, 2);
        recipe.AddIngredient<Pearlclay>(10);
        recipe.AddTile(TileID.AdamantiteForge);
        recipe.Register();

        recipe = CreateRecipe(10);

        recipe.AddIngredient(ItemID.SoulofLight);
        recipe.AddIngredient<Pearlclay>();
        recipe.AddIngredient<CeramicSheet>(10);
        recipe.AddTile(TileID.AdamantiteForge);
        recipe.Register();
    }
}