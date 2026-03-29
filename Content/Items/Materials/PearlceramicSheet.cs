using System.Collections.Generic;
using HendecamMod.Content.Items.Placeables;

namespace HendecamMod.Content.Items.Materials;

public class PearlceramicSheet : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.scale = 1.22f;
        Item.rare = ItemRarityID.LightRed;
        Item.value = 2250;
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