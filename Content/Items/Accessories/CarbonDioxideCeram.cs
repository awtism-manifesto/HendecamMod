using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using Terraria.Enums;

namespace HendecamMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.Balloon)]
public class CarbonDioxideCeram : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToAccessory(20, 26);
        Item.SetShopValues(ItemRarityColor.LightRed4, Item.buyPrice(silver: 799));
        Item.defense = 2;
    }

    public override void UpdateAccessory(Player player, bool showVisual)
    {
        player.GetJumpState<SimpleExtraJump>().Enable();
        player.jumpBoost = true;
        player.jumpSpeedBoost = 0.48f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<CarbonDioxideBalloon>();
        recipe.AddIngredient<CeramicSheet>(25);

        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}