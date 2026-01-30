using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations;

//[AutoloadEquip(EquipType.Beard)]
public class MonarchyPeaceAgreement : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Slimes, Bees, and Hornets should be friendly");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.npcTypeNoAggro[NPCID.QueenSlimeBoss] = true;
        player.npcTypeNoAggro[NPCID.QueenSlimeMinionBlue] = true;
        player.npcTypeNoAggro[NPCID.QueenSlimeMinionPink] = true;
        player.npcTypeNoAggro[NPCID.QueenSlimeMinionPurple] = true;
        player.npcTypeNoAggro[NPCID.KingSlime] = true;
        player.npcTypeNoAggro[NPCID.SlimeSpiked] = true;
        player.npcTypeNoAggro[NPCID.QueenBee] = true;
        player.npcTypeNoAggro[NPCID.BlueSlime] = true;
        player.npcTypeNoAggro[NPCID.IceSlime] = true;
        player.npcTypeNoAggro[NPCID.SandSlime] = true;
        player.npcTypeNoAggro[NPCID.SpikedIceSlime] = true;
        player.npcTypeNoAggro[NPCID.SpikedJungleSlime] = true;
        player.npcTypeNoAggro[NPCID.MotherSlime] = true;
        player.npcTypeNoAggro[NPCID.LavaSlime] = true;
        player.npcTypeNoAggro[NPCID.DungeonSlime] = true;
        player.npcTypeNoAggro[NPCID.GoldenSlime] = true;
        player.npcTypeNoAggro[NPCID.UmbrellaSlime] = true;
        player.npcTypeNoAggro[NPCID.ShimmerSlime] = true;
        player.npcTypeNoAggro[NPCID.SlimeMasked] = true;
        player.npcTypeNoAggro[NPCID.SlimeRibbonGreen] = true;
        player.npcTypeNoAggro[NPCID.SlimeRibbonRed] = true;
        player.npcTypeNoAggro[NPCID.SlimeRibbonWhite] = true;
        player.npcTypeNoAggro[NPCID.SlimeRibbonYellow] = true;
        player.npcTypeNoAggro[NPCID.ToxicSludge] = true;
        player.npcTypeNoAggro[NPCID.CorruptSlime] = true;
        player.npcTypeNoAggro[NPCID.Slimer] = true;
        player.npcTypeNoAggro[NPCID.Crimslime] = true;
        player.npcTypeNoAggro[NPCID.IlluminantSlime] = true;
        player.npcTypeNoAggro[NPCID.RainbowSlime] = true;
        player.npcTypeNoAggro[NPCID.Bee] = true;
        player.npcTypeNoAggro[NPCID.BeeSmall] = true;
        player.npcTypeNoAggro[NPCID.Hornet] = true;
        player.npcTypeNoAggro[NPCID.HornetFatty] = true;
        player.npcTypeNoAggro[NPCID.HornetHoney] = true;
        player.npcTypeNoAggro[NPCID.HornetLeafy] = true;
        player.npcTypeNoAggro[NPCID.HornetSpikey] = true;
        player.npcTypeNoAggro[NPCID.HornetStingy] = true;
        player.npcTypeNoAggro[NPCID.MossHornet] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.KingSlimeTrophy);
        recipe.AddIngredient(ItemID.QueenSlimeTrophy);
        recipe.AddIngredient(ItemID.QueenBeeTrophy);
        recipe.AddIngredient<FriendCore>();
        recipe.AddIngredient<Paper>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}