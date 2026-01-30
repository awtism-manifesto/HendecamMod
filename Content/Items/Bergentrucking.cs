using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items;

public class Bergentrucking : ModItem
{
    public override void SetStaticDefaults()
    {
        // Registers a vertical animation with 4 frames and each one will last 5 ticks (1/12 second)

        Item.ResearchUnlockCount = 1; // Configure the amount of this item that's needed to research it in Journey mode.
    }

    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 32; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1f;
        Item.rare = ItemRarityID.Cyan; // The color that the item's name will be in-game.
        Item.value = 69000;
        Item.maxStack = 9999;
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.BergentruckingTile>());
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "DRIVIN’ IN MY CAR RIGHT AFTER A BEER  ");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "HEY THAT BUMP IS SHAPED LIKE A DEER?")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "DUI??? HOW ABOUT YOU DIE? I’LL GO A HUNDRED MILES AN HOUR")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "LITTLE DO YOU KNOW I’M FILLED UP ON GAS")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "IMA GET YOUR FOUNTAIN MAKING ASS")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "PULVERIZE THIS FUCK WITH MY BERGEN TRUCK")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "IT SEEMS YOU’RE OUT OF LUCK")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "TRUCK!!!!!!!")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<BlankCanvas>();
        recipe.AddIngredient(ItemID.Minecart);
        recipe.AddIngredient<Beer>(7);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}