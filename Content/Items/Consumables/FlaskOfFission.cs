using System.Collections.Generic;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Placeables;

namespace HendecamMod.Content.Items.Consumables;

public class FlaskOfFission : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;

        ItemID.Sets.DrinkParticleColors[Type] =
        [
            new Color(85, 25, 255),
            new Color(210, 115, 255),
            new Color(225, 185, 255)
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
        Item.buffType = BuffType<WeaponImbueFission>();
        Item.buffTime = Item.flaskTime;
        Item.value = Item.sellPrice(0, 0, 3, 33);
        Item.rare = ItemRarityID.LightRed;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient<FlaskOfRadiation>()
            .AddIngredient<PlutoniumOre>()
            .AddTile(TileID.ImbuingStation)
            .Register();
    }
}