using System.Collections.Generic;
using HendecamMod.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items;

public class BrainrotPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;

        // Dust that will appear in these colors when the item with ItemUseStyleID.DrinkLiquid is used
        ItemID.Sets.DrinkParticleColors[Type] = new Color[3]
        {
            new Color(85, 32, 102),
            new Color(157, 55, 191),
            new Color(191, 81, 224)
        };
    }

    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 26;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.useAnimation = 10;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.UseSound = SoundID.Item3;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.buyPrice(silver: 3);
        Item.buffType = ModContent.BuffType<BrainRotted>();
        Item.buffTime = 14480; // Ticks
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Increases Stupid damage by 10%");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.BottledWater);
        recipe.AddIngredient(ItemID.Deathweed);
        recipe.AddIngredient<WeedLeaves>();
        recipe.AddIngredient<PlasticScrap>();
        recipe.AddTile(TileID.Bottles);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.BottledWater);
        recipe.AddIngredient(ItemID.Deathweed);
        recipe.AddIngredient<WeedLeaves>();
        recipe.AddIngredient<PlasticScrap>();
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();

        recipe = CreateRecipe(100);
        recipe.AddIngredient(ItemID.BrainOfConfusion);

        recipe.AddTile(TileID.Kegs);
        recipe.Register();
    }
}