using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Consumables;

public class FlaskOfFusion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;

        ItemID.Sets.DrinkParticleColors[Type] =
        [
            new Color(255, 25, 55),
            new Color(255, 115, 150),
            new Color(255, 85, 160)
        ];
    }

    public override void SetDefaults()
    {
        Item.UseSound = SoundID.Item3;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.useTurn = true;
        Item.useAnimation = 14;
        Item.useTime = 14;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.width = 14;
        Item.height = 24;
        Item.buffType = BuffType<WeaponImbueFusion>();
        Item.buffTime = Item.flaskTime;
        Item.value = Item.sellPrice(0, 0, 3, 33);
        Item.rare = ItemRarityID.Red;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient<FlaskOfFission>()
            .AddIngredient<AstatineOre>()
            .AddTile<CultistCyclotronPlaced>()
            .Register();
    }
}