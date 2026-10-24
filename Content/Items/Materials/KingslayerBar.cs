using System.Collections.Generic;

namespace HendecamMod.Content.Items.Materials;

public class KingslayerBar : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;

        // Dust that will appear in these colors when the item with ItemUseStyleID.DrinkLiquid is used
        ItemID.Sets.DrinkParticleColors[Type] = new Color[3]
        {
            new Color(102, 144, 229),
            new Color(72, 63, 44),
            new Color(173, 167, 118)
        };
    }

    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 32;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.useAnimation = 10;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.UseSound = SoundID.Item3;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.rare = ItemRarityID.Green;
        Item.value = Item.buyPrice(silver: 71);
        Item.buffType = BuffID.Slimed; // Specify an existing buff to be applied when used.
        Item.buffTime = 690; // The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(2);
        recipe.AddIngredient(ItemID.GoldBar, 2);
        recipe.AddIngredient(ItemID.PlatinumBar, 2);
        recipe.AddIngredient(ItemID.Gel, 5);
        recipe.AddTile(TileID.Solidifier);
        recipe.Register();

        // recipe.AddIngredient(PyriteBar.Type, 2);
        recipe = CreateRecipe(3);
        recipe.AddIngredient<PyriteBar>(2);
        recipe.AddIngredient(ItemID.PlatinumBar, 2);
        recipe.AddIngredient(ItemID.Gel, 5);
        recipe.AddTile(TileID.Solidifier);
        recipe.Register();

        recipe = CreateRecipe(3);
        recipe.AddIngredient<PyriteBar>(2);
        recipe.AddIngredient(ItemID.GoldBar, 2);
        recipe.AddIngredient(ItemID.Gel, 5);
        recipe.AddTile(TileID.Solidifier);
        recipe.Register();
    }
}