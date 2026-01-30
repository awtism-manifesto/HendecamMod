using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations;

//[AutoloadEquip(EquipType.Beard)]
public class FriendCore : ModItem
{
    public override void SetDefaults()
    {
        Item.maxStack = Item.CommonMaxStack;
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 500);
        Item.rare = ItemRarityID.LightPurple;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "A basis for peace with enemies");
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(10);
        recipe.AddIngredient(ItemID.RoyalGel);
        recipe.AddIngredient(ItemID.LifeCrystal);
        recipe.AddIngredient(ItemID.LovePotion);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}