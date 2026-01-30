using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations;

//[AutoloadEquip(EquipType.Beard)]
public class SlushyPeaceAgreement : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Members of the Cold and Wet should be friendly");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.npcTypeNoAggro[NPCID.Deerclops] = true;
        player.npcTypeNoAggro[NPCID.AngryNimbus] = true;
        player.npcTypeNoAggro[NPCID.FlyingFish] = true;
        player.npcTypeNoAggro[NPCID.IceGolem] = true;
        player.npcTypeNoAggro[NPCID.RainbowSlime] = true;
        player.npcTypeNoAggro[NPCID.ZombieRaincoat] = true;
        player.npcTypeNoAggro[NPCID.UmbrellaSlime] = true;
        player.npcTypeNoAggro[NPCID.IceSlime] = true;
        player.npcTypeNoAggro[NPCID.ZombieEskimo] = true;
        player.npcTypeNoAggro[NPCID.ArmedZombieEskimo] = true;
        player.npcTypeNoAggro[NPCID.CorruptPenguin] = true;
        player.npcTypeNoAggro[NPCID.CrimsonPenguin] = true;
        player.npcTypeNoAggro[NPCID.IceElemental] = true;
        player.npcTypeNoAggro[NPCID.Wolf] = true;
        player.npcTypeNoAggro[NPCID.IceBat] = true;
        player.npcTypeNoAggro[NPCID.SnowFlinx] = true;
        player.npcTypeNoAggro[NPCID.IceSlime] = true;
        player.npcTypeNoAggro[NPCID.UndeadViking] = true;
        player.npcTypeNoAggro[NPCID.CyanBeetle] = true;
        player.npcTypeNoAggro[NPCID.ArmoredViking] = true;
        player.npcTypeNoAggro[NPCID.IceTortoise] = true;
        player.npcTypeNoAggro[NPCID.IcyMerman] = true;
        player.npcTypeNoAggro[NPCID.IceMimic] = true;
        player.npcTypeNoAggro[NPCID.PigronCorruption] = true;
        player.npcTypeNoAggro[NPCID.PigronCrimson] = true;
        player.npcTypeNoAggro[NPCID.PigronHallow] = true;
        player.npcTypeNoAggro[NPCID.IceElemental] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DeerclopsTrophy);
        recipe.AddIngredient<FriendCore>();
        recipe.AddIngredient<Paper>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}