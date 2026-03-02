using System.Collections.Generic;

namespace HendecamMod.Content.Items.Materials;

public class Paper : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.rare = ItemRarityID.White; 
        Item.value = 25;
        Item.maxStack = 9999;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
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
        Recipe recipe = CreateRecipe(10);
        recipe.AddIngredient(ItemID.Wood);
        recipe.AddTile(TileID.Sawmill);
        recipe.Register();

        if (ModLoader.TryGetMod("VitalityMod", out Mod VitalMerica) && VitalMerica.TryFind("Paper", out ModItem Paper))
        {
            recipe = CreateRecipe();
            recipe.AddIngredient(Paper.Type);
            recipe.Register();
        }
    }
}