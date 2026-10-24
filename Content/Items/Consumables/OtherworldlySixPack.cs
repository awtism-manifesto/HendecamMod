using System.Collections.Generic;

namespace HendecamMod.Content.Items.Consumables;

public class OtherworldlySixPack : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;

        // Dust that will appear in these colors when the item with ItemUseStyleID.DrinkLiquid is used
        ItemID.Sets.DrinkParticleColors[Type] = new Color[3]
        {
            new Color(255, 120, 255),
            new Color(111, 245, 6),
            new Color(122, 251, 114)
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
        Item.maxStack = 1;
        Item.consumable = true;
        Item.rare = ItemRarityID.Lime;
        Item.value = Item.buyPrice(silver: 11500);
        Item.buffType = BuffType<Buffs.OtherworldPoisoning>(); // Specify an existing buff to be applied when used.
        Item.buffTime = 69000; // The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }
    // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
}