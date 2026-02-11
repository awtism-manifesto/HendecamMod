using System.Collections.Generic;

namespace HendecamMod.Content.Items;

public class PlasticScrap : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 32; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1.22f;
        Item.rare = ItemRarityID.Blue; // The color that the item's name will be in-game.
        Item.value = 10;
        Item.maxStack = 9999;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "probably useless lol");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "sike use recipe browser chud")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
       

        recipe.AddIngredient<CrudeOil>(3);

        recipe.AddTile(TileID.Furnaces);
        recipe.Register();

        if (ModLoader.TryGetMod("Macrocosm", out Mod MacroMerica) && MacroMerica.TryFind("Plastic", out ModItem Plastic))
        {
            recipe = CreateRecipe(20);

            recipe.AddIngredient(Plastic.Type);

            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.Register();
        }
        if (ModLoader.TryGetMod("AwfulGarbageMod", out Mod AwfulMerica) && AwfulMerica.TryFind("Garbage", out ModItem Garbage))
        {
            recipe = CreateRecipe();
            recipe.AddIngredient(Garbage.Type, 10);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
        
    }
}