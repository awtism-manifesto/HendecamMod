
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations;

//[AutoloadEquip(EquipType.Beard)]
public class RoyalGnome : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 1000);
        Item.rare = ItemRarityID.LightPurple;
        Item.accessory = true;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Gnomes should be friendly");
        tooltips.Add(line);
    }
    public override void UpdateEquip(Player player)
    {
        player.npcTypeNoAggro[NPCID.Gnome] = true;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.GardenGnome, 1);
        recipe.AddIngredient<FriendCore>(1);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}