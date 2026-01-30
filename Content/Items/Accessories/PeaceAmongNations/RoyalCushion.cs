using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations;

//[AutoloadEquip(EquipType.Beard)]
public class RoyalCushion : ModItem
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
        var line = new TooltipLine(Mod, "Face", "All that are creepy and crawly should be friendly");
        tooltips.Add(line);
    }
    public override void UpdateEquip(Player player)
    {
        player.npcTypeNoAggro[NPCID.BloodNautilus] = true;
        player.npcTypeNoAggro[NPCID.LarvaeAntlion] = true;
        player.npcTypeNoAggro[NPCID.Bee] = true;
        player.npcTypeNoAggro[NPCID.CochinealBeetle] = true;
        player.npcTypeNoAggro[NPCID.Crab] = true;
        player.npcTypeNoAggro[NPCID.Crawdad] = true;
        player.npcTypeNoAggro[NPCID.Crawdad2] = true;
        player.npcTypeNoAggro[NPCID.CyanBeetle] = true;
        player.npcTypeNoAggro[NPCID.Worm] = true;
        player.npcTypeNoAggro[NPCID.GiantWormBody] = true;
        player.npcTypeNoAggro[NPCID.GiantWormHead] = true;
        player.npcTypeNoAggro[NPCID.GiantWormTail] = true;
        player.npcTypeNoAggro[NPCID.LacBeetle] = true;
        player.npcTypeNoAggro[NPCID.MushiLadybug] = true;
        player.npcTypeNoAggro[NPCID.SeaSnail] = true;
        player.npcTypeNoAggro[NPCID.TombCrawlerBody] = true;
        player.npcTypeNoAggro[NPCID.TombCrawlerHead] = true;
        player.npcTypeNoAggro[NPCID.TombCrawlerTail] = true;
        player.npcTypeNoAggro[NPCID.AnglerFish] = true;
        player.npcTypeNoAggro[NPCID.DiggerBody] = true;
        player.npcTypeNoAggro[NPCID.DiggerHead] = true;
        player.npcTypeNoAggro[NPCID.DiggerTail] = true;
        player.npcTypeNoAggro[NPCID.DuneSplicerBody] = true;
        player.npcTypeNoAggro[NPCID.DuneSplicerHead] = true;
        player.npcTypeNoAggro[NPCID.DuneSplicerTail] = true;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.WhoopieCushion, 1);
        recipe.AddIngredient<FriendCore>(1);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}


// Balls