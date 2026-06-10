using HendecamMod.Common.Systems;
using HendecamMod.Content.Tiles.Blocks;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Placeables;

public class GogBlock : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 2500;
    }

    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 20; // Hitbox width of the item.
        Item.height = 20; // Hitbox height of the item.
        Item.rare = ItemRarityID.Blue; // The color that the item's name will be in-game.
        Item.value = 0;
        Item.maxStack = 9999;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.useTime = 15;
        Item.DefaultToPlaceableTile(TileType<GogBlockPlaced>());
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Can spread to ANY tile, and spreads exponentially fast");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Will not spread unless all five gog enablers, enable gog enablers, and experimental features are enabled in the configs")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "FINAL WARNING!!! THIS BLOCK CAN AND WILL TAKE OVER AND DESTROY YOUR WORLD IF PLACED")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Bluer than blue...")
        {
            OverrideColor = new Color(15, 15, 105)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        var config = GetInstance<ZeGogEnablers>();
        var config2 = GetInstance<HendecamExperimentalConfig>();
        var config3 = GetInstance<HendecamConfig>();

        if (config.GogEnabler1 && config.GogEnabler2 && config.GogEnabler3 && config.GogEnabler4 &&
           config.GogEnabler5 && config2.EnableGogEnablers && config3.EnableExperimentalFeatures)
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<AzuriteOre>(200);
            recipe.AddIngredient(ItemID.BlueDye, 20);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }
    }
}