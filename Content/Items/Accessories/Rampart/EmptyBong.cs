using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class EmptyBong : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 1000);
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
    }

    public override void UpdateEquip(Player player)
    {
        player.buffImmune[BuffID.ChaosState] = true;
        player.ClearBuff(BuffID.PotionSickness);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.RodOfHarmony);
        recipe.AddIngredient<Placebo>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}