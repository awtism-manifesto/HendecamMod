using HendecamMod.Common.Systems;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.Beard)]
public class PhatBlunt : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 22;
        Item.height = 18;
        Item.value = Item.sellPrice(gold: 7);
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
    }

    public override void UpdateEquip(Player player)
    {
        player.statLifeMax2 = (int)(player.statLifeMax2 * 1.1f);
        player.lifeRegen += 3;
        player.breathMax = 167;
        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();
        loboPlayer.LobotometerShaderStrength *= 0.9f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<LuckyCigarette>();
        recipe.AddIngredient<WeedLeaves>(28);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}