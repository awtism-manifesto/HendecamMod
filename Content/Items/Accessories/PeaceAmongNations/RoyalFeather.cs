using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations;

//[AutoloadEquip(EquipType.Beard)]
public class RoyalFeather : ModItem
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
        var line = new TooltipLine(Mod, "Face", "The residents of wind and sky should be friendly");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.npcTypeNoAggro[NPCID.Harpy] = true;
        player.npcTypeNoAggro[NPCID.WyvernBody] = true;
        player.npcTypeNoAggro[NPCID.WyvernBody2] = true;
        player.npcTypeNoAggro[NPCID.WyvernBody3] = true;
        player.npcTypeNoAggro[NPCID.WyvernHead] = true;
        player.npcTypeNoAggro[NPCID.WyvernLegs] = true;
        player.npcTypeNoAggro[NPCID.WyvernTail] = true;
        player.npcTypeNoAggro[NPCID.MartianProbe] = true;
        player.npcTypeNoAggro[NPCID.WindyBalloon] = true;
        player.npcTypeNoAggro[NPCID.Dandelion] = true;
        player.npcTypeNoAggro[NPCID.LadyBug] = true;
        player.npcTypeNoAggro[NPCID.GoldLadyBug] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.GiantHarpyFeather);
        recipe.AddIngredient<FriendCore>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}