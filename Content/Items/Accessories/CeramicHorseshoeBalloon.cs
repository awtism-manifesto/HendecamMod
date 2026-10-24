using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using Terraria.Enums;

namespace HendecamMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.Balloon)]
public class CeramicHorseshoeBalloon : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToAccessory(20, 26);
        Item.SetShopValues(ItemRarityColor.LightRed4, Item.buyPrice(silver: 999));
        Item.defense = 3;
    }

    public override void UpdateAccessory(Player player, bool showVisual)
    {
        player.GetJumpState<SimpleExtraJump>().Enable();
        player.jumpBoost = true;
        player.jumpSpeedBoost = 0.49f;
        player.noFallDmg = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<DarkGrayHorseshoeBalloon>();
        recipe.AddIngredient<CeramicSheet>(25);

        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
        recipe = CreateRecipe();

        recipe.AddIngredient<CarbonDioxideCeram>();
        recipe.AddIngredient(ItemID.LuckyHorseshoe);

        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}