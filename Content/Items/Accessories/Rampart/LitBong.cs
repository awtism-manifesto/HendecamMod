using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class LitBong : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 2000);
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
    }

    public override void UpdateEquip(Player player)
    {
        player.buffImmune[BuffID.ChaosState] = true;
        player.ClearBuff(BuffID.PotionSickness);
        player.buffImmune[BuffID.NoBuilding] = true;
        player.buffImmune[BuffID.WindPushed] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<Lightbulb>();

        recipe.AddIngredient<EmptyBong>();
        recipe.AddIngredient<WeedLeaves>(3);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}