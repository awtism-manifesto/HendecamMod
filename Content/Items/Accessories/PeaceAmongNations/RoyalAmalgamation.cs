
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations;

//[AutoloadEquip(EquipType.Beard)]
public class RoyalAmalgamation : ModItem
    {
    public override void SetDefaults()
        {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 4000);
        Item.rare = ItemRarityID.LightPurple;
        Item.accessory = true;
        }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
        var line = new TooltipLine(Mod, "Face", "Various enemies across Terraria should be friendly");
        tooltips.Add(line);
        }
    public override void UpdateEquip(Player player)
        {
        player.npcTypeNoAggro[NPCID.Gnome] = true;
        player.npcTypeNoAggro[NPCID.CaveBat] = true;
        player.npcTypeNoAggro[NPCID.SporeBat] = true;
        player.npcTypeNoAggro[NPCID.JungleBat] = true;
        player.npcTypeNoAggro[NPCID.Hellbat] = true;
        player.npcTypeNoAggro[NPCID.IceBat] = true;
        player.npcTypeNoAggro[NPCID.GiantBat] = true;
        player.npcTypeNoAggro[NPCID.IlluminantBat] = true;
        player.npcTypeNoAggro[NPCID.Lavabat] = true;
        player.npcTypeNoAggro[NPCID.Slimer] = true;
        player.npcTypeNoAggro[NPCID.GiantFlyingFox] = true;
        player.npcTypeNoAggro[NPCID.VampireBat] = true;
        player.npcTypeNoAggro[NPCID.AnomuraFungus] = true;
        player.npcTypeNoAggro[NPCID.FungiBulb] = true;
        player.npcTypeNoAggro[NPCID.FungiSpore] = true;
        player.npcTypeNoAggro[NPCID.FungoFish] = true;
        player.npcTypeNoAggro[NPCID.GiantFungiBulb] = true;
        player.npcTypeNoAggro[NPCID.SporeBat] = true;
        player.npcTypeNoAggro[NPCID.SporeSkeleton] = true;
        player.npcTypeNoAggro[NPCID.MushiLadybug] = true;
        player.npcTypeNoAggro[NPCID.ZombieMushroom] = true;
        player.npcTypeNoAggro[NPCID.Vulture] = true;
        player.npcTypeNoAggro[NPCID.Antlion] = true;
        player.npcTypeNoAggro[NPCID.Mummy] = true;
        player.npcTypeNoAggro[NPCID.LightMummy] = true;
        player.npcTypeNoAggro[NPCID.DarkMummy] = true;
        player.npcTypeNoAggro[NPCID.BloodMummy] = true;
        player.npcTypeNoAggro[NPCID.Golfer] = true;
        player.npcTypeNoAggro[NPCID.GolferRescue] = true;
        player.npcTypeNoAggro[NPCID.FlyingAntlion] = true;
        player.npcTypeNoAggro[NPCID.GiantFlyingAntlion] = true;
        player.npcTypeNoAggro[NPCID.GiantWalkingAntlion] = true;
        player.npcTypeNoAggro[NPCID.BloodNautilus] = true;
        player.npcTypeNoAggro[NPCID.LarvaeAntlion] = true;
        player.npcTypeNoAggro[NPCID.WalkingAntlion] = true;
        player.npcTypeNoAggro[NPCID.SandSlime] = true;
        player.npcTypeNoAggro[NPCID.DesertBeast] = true;
        player.npcTypeNoAggro[NPCID.DesertDjinn] = true;
        player.npcTypeNoAggro[NPCID.DesertGhoul] = true;
        player.npcTypeNoAggro[NPCID.DesertGhoulCorruption] = true;
        player.npcTypeNoAggro[NPCID.DesertGhoulCrimson] = true;
        player.npcTypeNoAggro[NPCID.DesertGhoulHallow] = true;
        player.npcTypeNoAggro[NPCID.DesertLamiaDark] = true;
        player.npcTypeNoAggro[NPCID.DesertLamiaLight] = true;
        player.npcTypeNoAggro[NPCID.DesertScorpionWalk] = true;
        player.npcTypeNoAggro[NPCID.DesertScorpionWall] = true;
        player.npcTypeNoAggro[NPCID.SandElemental] = true;
        player.npcTypeNoAggro[NPCID.SandShark] = true;
        player.npcTypeNoAggro[NPCID.SandsharkCorrupt] = true;
        player.npcTypeNoAggro[NPCID.SandsharkCrimson] = true;
        player.npcTypeNoAggro[NPCID.SandsharkHallow] = true;
        player.npcTypeNoAggro[NPCID.TombCrawlerBody] = true;
        player.npcTypeNoAggro[NPCID.TombCrawlerHead] = true;
        player.npcTypeNoAggro[NPCID.TombCrawlerTail] = true;
        player.npcTypeNoAggro[NPCID.DuneSplicerBody] = true;
        player.npcTypeNoAggro[NPCID.DuneSplicerHead] = true;
        player.npcTypeNoAggro[NPCID.DuneSplicerTail] = true;
        player.npcTypeNoAggro[NPCID.Tumbleweed] = true;
        player.npcTypeNoAggro[NPCID.GraniteFlyer] = true;
        player.npcTypeNoAggro[NPCID.GraniteGolem] = true;
        player.npcTypeNoAggro[NPCID.GreekSkeleton] = true;
        player.npcTypeNoAggro[NPCID.Medusa] = true;
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
        player.npcTypeNoAggro[NPCID.Mimic] = true;
        player.npcTypeNoAggro[NPCID.BigMimicCorruption] = true;
        player.npcTypeNoAggro[NPCID.BigMimicCrimson] = true;
        player.npcTypeNoAggro[NPCID.BigMimicHallow] = true;
        player.npcTypeNoAggro[NPCID.BigMimicJungle] = true;
        player.npcTypeNoAggro[NPCID.IceMimic] = true;
        player.npcTypeNoAggro[NPCID.PresentMimic] = true;
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

        }
    public override void AddRecipes()
        {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<RoyalChest>(1);
        recipe.AddIngredient<RoyalCushion>(1);
        recipe.AddIngredient<RoyalFeather>(1);
        recipe.AddIngredient<RoyalGeode>(1);
        recipe.AddIngredient<RoyalGnome>(1);
        recipe.AddIngredient<RoyalMandibles>(1);
        recipe.AddIngredient<RoyalMushroom>(1);
        recipe.AddIngredient<RoyalWing>(1);
        recipe.AddIngredient(ItemID.RoyalGel);
        recipe.AddIngredient<FriendCore>(1);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
        }
    }
