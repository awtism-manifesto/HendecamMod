using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.NormalOnes;

//[AutoloadEquip(EquipType.Beard)]
public class MoltenRose : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 825);
        Item.rare = ItemRarityID.Green;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Reduces damage from touching lava, and provides 7 seconds of lava immunity"));
    }

    public override void UpdateEquip(Player player)
    {
        player.lavaRose = true;
        player.lavaMax += 210;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.ObsidianRose);
        recipe.AddIngredient(ItemID.LavaCharm);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}