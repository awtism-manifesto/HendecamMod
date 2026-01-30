using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations;

//[AutoloadEquip(EquipType.Beard)]
public class SeasonalPeaceAgreement : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Seasonal enemies should be friendly");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.npcTypeNoAggro[NPCID.Raven] = true;
        player.npcTypeNoAggro[NPCID.HoppinJack] = true;
        player.npcTypeNoAggro[NPCID.SlimeMasked] = true;
        player.npcTypeNoAggro[NPCID.SlimeRibbonGreen] = true;
        player.npcTypeNoAggro[NPCID.SlimeRibbonRed] = true;
        player.npcTypeNoAggro[NPCID.SlimeRibbonWhite] = true;
        player.npcTypeNoAggro[NPCID.SlimeRibbonYellow] = true;
        player.npcTypeNoAggro[NPCID.ZombieDoctor] = true;
        player.npcTypeNoAggro[NPCID.ZombieSuperman] = true;
        player.npcTypeNoAggro[NPCID.ZombiePixie] = true;
        player.npcTypeNoAggro[NPCID.SkeletonTopHat] = true;
        player.npcTypeNoAggro[NPCID.SkeletonAstonaut] = true;
        player.npcTypeNoAggro[NPCID.SkeletonAlien] = true;
        player.npcTypeNoAggro[NPCID.ZombieXmas] = true;
        player.npcTypeNoAggro[NPCID.ZombieSweater] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.EverscreamTrophy);
        recipe.AddIngredient(ItemID.MourningWoodTrophy);
        recipe.AddIngredient<FriendCore>();
        recipe.AddIngredient<Paper>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}