using System.Collections.Generic;

namespace HendecamMod.Content.Items.Consumables;

public class CandyHeart : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;

        ItemID.Sets.DrinkParticleColors[Type] = new Color[3]
        {
            new Color(92, 42, 9),
            new Color(51, 24, 6),
            new Color(122, 51, 4)
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
        Item.rare = ItemRarityID.LightPurple;
        Item.value = Item.buyPrice(silver: 3);
        Item.buffType = BuffID.WellFed3;
        Item.buffTime = 300;
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }
}