using System.Collections.Generic;

namespace HendecamMod.Content.Items.Materials;

public class FireDiamond : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
    {
        // Common Properties
        Item.width = 32; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.rare = ItemRarityID.Blue; // The color that the item's name will be in-game.
        Item.value = 500;
        Item.maxStack = 9999;
        Item.alpha = 50;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "An old currency used by demons");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(2);
        recipe.AddIngredient(ItemID.Diamond);
        recipe.AddIngredient(ItemID.LivingFireBlock);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Diamond);
        recipe.AddIngredient(ItemID.Hellstone);
        recipe.Register();
    }
}