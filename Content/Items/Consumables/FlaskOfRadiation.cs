using System.Collections.Generic;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Placeables;

namespace HendecamMod.Content.Items.Consumables;

/// <summary>
///     A potion that applies the ExampleWeaponImbue buff to the player.
///     See also ExampleWeaponImbue and ExampleWeaponEnchantmentPlayer.
/// </summary>
public class FlaskOfRadiation : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;

        ItemID.Sets.DrinkParticleColors[Type] =
        [
            new Color(55, 255, 55),
            new Color(111, 255, 120),
            new Color(165, 255, 120)
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
        Item.buffType = BuffType<WeaponImbueRadiation>();
        Item.buffTime = Item.flaskTime;
        Item.value = Item.sellPrice(0, 0, 3, 33);
        Item.rare = ItemRarityID.LightRed;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.BottledWater)
            .AddIngredient<UraniumOre>()
            .AddTile(TileID.ImbuingStation)
            .Register();
    }
}