using System.Collections.Generic;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items.Consumables;

/// <summary>
///     A potion that applies the ExampleWeaponImbue buff to the player.
///     See also ExampleWeaponImbue and ExampleWeaponEnchantmentPlayer.
/// </summary>
public class FlaskOfOil : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;

        ItemID.Sets.DrinkParticleColors[Type] =
        [
            new Color(24, 20, 20),
            new Color(0, 20, 20),
            new Color(10, 10, 40)
        ];
    }

    public override void SetDefaults()
    {
        Item.UseSound = SoundID.Item3;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.useTurn = true;
        Item.useAnimation = 17;
        Item.useTime = 17;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.width = 14;
        Item.height = 24;
        Item.buffType = BuffType<WeaponImbueOil>();
        Item.buffTime = Item.flaskTime;
        Item.value = Item.sellPrice(0, 0, 2, 22);
        Item.rare = ItemRarityID.LightRed;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.BottledWater)
            .AddIngredient<CrudeOil>(5)
            .AddTile(TileID.ImbuingStation)
            .Register();
    }
}