using System.Collections.Generic;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items.Consumables;

/// <summary>
///     A potion that applies the ExampleWeaponImbue buff to the player.
///     See also ExampleWeaponImbue and ExampleWeaponEnchantmentPlayer.
/// </summary>
public class FlaskOfShadowflame : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;

        ItemID.Sets.DrinkParticleColors[Type] =
        [
            new Color(242, 20, 250),
            new Color(220, 110, 255),
            new Color(210, 170, 255)
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
        Item.buffType = BuffType<WeaponImbueShadowflame>();
        Item.buffTime = Item.flaskTime;
        Item.value = Item.sellPrice(0, 0, 5);
        Item.rare = ItemRarityID.LightRed;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.BottledWater)
            .AddIngredient<Shadowflame>(2)
            .AddTile(TileID.ImbuingStation)
            .Register();
    }
}