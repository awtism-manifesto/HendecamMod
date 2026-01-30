using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Placeables;

public class TwinPoster : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 20; // Hitbox width of the item.
        Item.height = 20; // Hitbox height of the item.
        Item.rare = ItemRarityID.White; // The color that the item's name will be in-game.
        Item.value = 69;
        Item.maxStack = 9999;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.useTime = 15;
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.TwinPosterPlaced>());
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "We getting canceled with this one");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<BlankCanvas>();
        recipe.AddIngredient(ItemID.PaperAirplaneB);
        recipe.AddIngredient(ItemID.PaperAirplaneA);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}