using System.Collections.Generic;
using HendecamMod.Content.Tiles;

namespace HendecamMod.Content.Items;

public class TurboDartTrap : ModItem
{
    public override void SetStaticDefaults()
    {
        // Registers a vertical animation with 4 frames and each one will last 5 ticks (1/12 second)

        Item.ResearchUnlockCount = 10; // Configure the amount of this item that's needed to research it in Journey mode.
    }

    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 32; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1f;
        Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
        Item.value = 41000;
        Item.maxStack = 9999;
        Item.DefaultToPlaceableTile(ModContent.TileType<TurboDartTrapTile>());
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Shoots fast carbon fiber darts");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Can fire as fast as a 1/4 second timer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

       
        recipe.AddIngredient(ItemID.CobaltBar, 4);
        recipe.AddIngredient<Kevlar>(4);
        recipe.AddIngredient(ItemID.DartTrap);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        recipe = CreateRecipe();


        recipe.AddIngredient(ItemID.PalladiumBar, 4);
        recipe.AddIngredient<Kevlar>(4);
        recipe.AddIngredient(ItemID.DartTrap);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}