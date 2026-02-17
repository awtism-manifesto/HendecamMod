using Terraria.DataStructures;

namespace HendecamMod.Common.Systems;

public class RiverFish : ModPlayer
{
    public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
    {
        bool cheater = !Main.hardMode;
        bool inShimmer = !attempt.inLava && !attempt.inHoney && Player.ZoneShimmer;
        bool trashRod = attempt.playerFishingConditions.PoleItemType == ItemID.WoodFishingPole;
        bool fishRod = attempt.playerFishingConditions.PoleItemType == ItemID.ReinforcedFishingPole;
        bool itemRod = attempt.playerFishingConditions.PoleItemType == ItemID.FisherofSouls || attempt.playerFishingConditions.PoleItemType == ItemID.Fleshcatcher;
        bool potionRod = attempt.playerFishingConditions.PoleItemType == ItemID.ScarabFishingRod;
        bool enemyRod = attempt.playerFishingConditions.PoleItemType == ItemID.BloodFishingRod;
        bool baitRod = attempt.playerFishingConditions.PoleItemType == ItemID.FiberglassFishingPole;
        bool crateRod = attempt.playerFishingConditions.PoleItemType == ItemID.MechanicsRod;
        bool merchantRod = attempt.playerFishingConditions.PoleItemType == ItemID.SittingDucksFishingRod;
        bool questRod = attempt.playerFishingConditions.PoleItemType == ItemID.HotlineFishingHook;
        bool goldRod = attempt.playerFishingConditions.PoleItemType == ItemID.GoldenFishingRod;
        if (cheater && inShimmer)
        {
            itemDrop = ItemID.TinShortswordOld;
        }
        else
        {
            if (inShimmer && trashRod)
            {
                int choice = Main.rand.Next(4);
                switch (choice)
                {
                    case 0:
                        itemDrop = ItemID.OldShoe;
                        break;
                    case 1:
                        itemDrop = ItemID.FishingSeaweed;
                        break;
                    case 2:
                        itemDrop = ItemID.TinCan;
                        break;
                    case 3:
                        itemDrop = ItemID.JojaCola;
                        break;
                }
            }

            if (inShimmer && fishRod)
            {
                int choice = Main.rand.Next(9);
                switch (choice)
                {
                    case 0:
                        itemDrop = ItemID.AtlanticCod;
                        break;
                    case 1:
                        itemDrop = ItemID.Bass;
                        break;
                    case 2:
                        itemDrop = ItemID.Flounder;
                        break;
                    case 3:
                        itemDrop = ItemID.RedSnapper;
                        break;
                    case 4:
                        itemDrop = ItemID.RockLobster;
                        break;
                    case 5:
                        itemDrop = ItemID.Salmon;
                        break;
                    case 6:
                        itemDrop = ItemID.Shrimp;
                        break;
                    case 7:
                        itemDrop = ItemID.Trout;
                        break;
                    case 8:
                        itemDrop = ItemID.Tuna;
                        break;
                }
            }

            if (inShimmer && itemRod)
            {
                int choice = Main.rand.Next(23);
                switch (choice)
                {
                    case 0:
                        itemDrop = ItemID.FrogLeg;
                        break;
                    case 1:
                        itemDrop = ItemID.BalloonPufferfish;
                        break;
                    case 2:
                        itemDrop = ItemID.BombFish;
                        break;
                    case 3:
                        itemDrop = ItemID.PurpleClubberfish;
                        break;
                    case 4:
                        itemDrop = ItemID.ReaverShark;
                        break;
                    case 5:
                        itemDrop = ItemID.Rockfish;
                        break;
                    case 6:
                        itemDrop = ItemID.SawtoothShark;
                        break;
                    case 7:
                        itemDrop = ItemID.FrostDaggerfish;
                        break;
                    case 8:
                        itemDrop = ItemID.Swordfish;
                        break;
                    case 9:
                        itemDrop = ItemID.ZephyrFish;
                        break;
                    case 10:
                        itemDrop = ItemID.Honeyfin;
                        break;
                    case 11:
                        itemDrop = ItemID.Toxikarp;
                        break;
                    case 12:
                        itemDrop = ItemID.Bladetongue;
                        break;
                    case 13:
                        itemDrop = ItemID.CrystalSerpent;
                        break;
                    case 14:
                        itemDrop = ItemID.ScalyTruffle;
                        break;
                    case 15:
                        itemDrop = ItemID.ObsidianSwordfish;
                        break;
                    case 16:
                        itemDrop = ItemID.AlchemyTable;
                        break;
                    case 17:
                        itemDrop = ItemID.Oyster;
                        break;
                    case 18:
                        itemDrop = ItemID.BottomlessBucket;
                        break;
                    case 19:
                        itemDrop = ItemID.LavaAbsorbantSponge;
                        break;
                    case 20:
                        itemDrop = ItemID.DemonConch;
                        break;
                    case 21:
                        itemDrop = ItemID.DreadoftheRedSea;
                        break;
                    case 22:
                        itemDrop = ItemID.LadyOfTheLake;
                        break;
                }
            }

            if (inShimmer && potionRod)
            {
                int choice = Main.rand.Next(14);
                switch (choice)
                {
                    case 0:
                        itemDrop = ItemID.ArmoredCavefish;
                        break;
                    case 1:
                        itemDrop = ItemID.ChaosFish;
                        break;
                    case 2:
                        itemDrop = ItemID.CrimsonTigerfish;
                        break;
                    case 3:
                        itemDrop = ItemID.Damselfish;
                        break;
                    case 4:
                        itemDrop = ItemID.DoubleCod;
                        break;
                    case 5:
                        itemDrop = ItemID.Ebonkoi;
                        break;
                    case 6:
                        itemDrop = ItemID.FlarefinKoi;
                        break;
                    case 7:
                        itemDrop = ItemID.Hemopiranha;
                        break;
                    case 8:
                        itemDrop = ItemID.Obsidifish;
                        break;
                    case 9:
                        itemDrop = ItemID.PrincessFish;
                        break;
                    case 10:
                        itemDrop = ItemID.Prismite;
                        break;
                    case 11:
                        itemDrop = ItemID.SpecularFish;
                        break;
                    case 12:
                        itemDrop = ItemID.Stinkfish;
                        break;
                    case 13:
                        itemDrop = ItemID.VariegatedLardfish;
                        break;
                }

                return;
            }

            if (inShimmer && enemyRod)
            {
                int choice = Main.rand.Next(5);
                switch (choice)
                {
                    case 0:
                        int npc1 = NPCID.EyeballFlyingFish;
                        npcSpawn = npc1;
                        break;
                    case 1:
                        int npc2 = NPCID.ZombieMerman;
                        npcSpawn = npc2;
                        break;
                    case 2:
                        int npc3 = NPCID.GoblinShark;
                        npcSpawn = npc3;
                        break;
                    case 3:
                        int npc4 = NPCID.BloodEelHead;
                        npcSpawn = npc4;
                        break;
                    case 4:
                        int npc5 = NPCID.BloodNautilus;
                        npcSpawn = npc5;
                        break;
                }

                itemDrop = -1;
            }

            if (inShimmer && baitRod)
            {
                int choice = Main.rand.Next(50);
                switch (choice)
                {
                    case 0:
                        itemDrop = ItemID.ApprenticeBait;
                        break;
                    case 1:
                        itemDrop = ItemID.JourneymanBait;
                        break;
                    case 2:
                        itemDrop = ItemID.MasterBait;
                        break;
                    case 3:
                        itemDrop = ItemID.BlackDragonfly;
                        break;
                    case 4:
                        itemDrop = ItemID.BlueDragonfly;
                        break;
                    case 5:
                        itemDrop = ItemID.GreenDragonfly;
                        break;
                    case 6:
                        itemDrop = ItemID.OrangeDragonfly;
                        break;
                    case 7:
                        itemDrop = ItemID.RedDragonfly;
                        break;
                    case 8:
                        itemDrop = ItemID.YellowDragonfly;
                        break;
                    case 9:
                        itemDrop = ItemID.GoldDragonfly;
                        break;
                    case 10:
                        itemDrop = ItemID.EmpressButterfly;
                        break;
                    case 11:
                        itemDrop = ItemID.GoldButterfly;
                        break;
                    case 12:
                        itemDrop = ItemID.HellButterfly;
                        break;
                    case 13:
                        itemDrop = ItemID.JuliaButterfly;
                        break;
                    case 14:
                        itemDrop = ItemID.MonarchButterfly;
                        break;
                    case 15:
                        itemDrop = ItemID.PurpleEmperorButterfly;
                        break;
                    case 16:
                        itemDrop = ItemID.RedAdmiralButterfly;
                        break;
                    case 17:
                        itemDrop = ItemID.SulphurButterfly;
                        break;
                    case 18:
                        itemDrop = ItemID.TreeNymphButterfly;
                        break;
                    case 19:
                        itemDrop = ItemID.UlyssesButterfly;
                        break;
                    case 20:
                        itemDrop = ItemID.ZebraSwallowtailButterfly;
                        break;
                    case 21:
                        itemDrop = ItemID.Snail;
                        break;
                    case 22:
                        itemDrop = ItemID.GlowingSnail;
                        break;
                    case 23:
                        itemDrop = ItemID.MagmaSnail;
                        break;
                    case 24:
                        itemDrop = ItemID.Scorpion;
                        break;
                    case 25:
                        itemDrop = ItemID.BlackScorpion;
                        break;
                    case 26:
                        itemDrop = ItemID.BlueJellyfish;
                        break;
                    case 27:
                        itemDrop = ItemID.GreenJellyfish;
                        break;
                    case 28:
                        itemDrop = ItemID.PinkJellyfish;
                        break;
                    case 29:
                        itemDrop = ItemID.LadyBug;
                        break;
                    case 30:
                        itemDrop = ItemID.GoldLadyBug;
                        break;
                    case 31:
                        itemDrop = ItemID.Grasshopper;
                        break;
                    case 32:
                        itemDrop = ItemID.GoldGrasshopper;
                        break;
                    case 33:
                        itemDrop = ItemID.Worm;
                        break;
                    case 34:
                        itemDrop = ItemID.EnchantedNightcrawler;
                        break;
                    case 35:
                        itemDrop = ItemID.GoldWorm;
                        break;
                    case 36:
                        itemDrop = ItemID.TruffleWorm;
                        break;
                    case 37:
                        itemDrop = ItemID.Firefly;
                        break;
                    case 38:
                        itemDrop = ItemID.Shimmerfly;
                        break;
                    case 39:
                        itemDrop = ItemID.Lavafly;
                        break;
                    case 40:
                        itemDrop = ItemID.Sluggy;
                        break;
                    case 41:
                        itemDrop = ItemID.Grubby;
                        break;
                    case 42:
                        itemDrop = ItemID.Buggy;
                        break;
                    case 43:
                        itemDrop = ItemID.Stinkbug;
                        break;
                    case 44:
                        itemDrop = ItemID.WaterStrider;
                        break;
                    case 45:
                        itemDrop = ItemID.GoldWaterStrider;
                        break;
                    case 46:
                        itemDrop = ItemID.Maggot;
                        break;
                    case 47:
                        itemDrop = ItemID.GoldDragonfly;
                        break;
                    case 48:
                        itemDrop = ItemID.CrabBanner;
                        break;
                    case 49:
                        itemDrop = ItemID.DirtBlock;
                        break;
                }
            }

            if (inShimmer && crateRod)
            {
                int choice = Main.rand.Next(13);
                switch (choice)
                {
                    case 0:
                        itemDrop = ItemID.WoodenCrateHard;
                        break;
                    case 1:
                        itemDrop = ItemID.IronCrateHard;
                        break;
                    case 2:
                        itemDrop = ItemID.GoldenCrateHard;
                        break;
                    case 3:
                        itemDrop = ItemID.JungleFishingCrateHard;
                        break;
                    case 4:
                        itemDrop = ItemID.FloatingIslandFishingCrateHard;
                        break;
                    case 5:
                        itemDrop = ItemID.CorruptFishingCrateHard;
                        break;
                    case 6:
                        itemDrop = ItemID.CrimsonFishingCrateHard;
                        break;
                    case 7:
                        itemDrop = ItemID.HallowedFishingCrateHard;
                        break;
                    case 8:
                        itemDrop = ItemID.DungeonFishingCrateHard;
                        break;
                    case 9:
                        itemDrop = ItemID.FrozenCrateHard;
                        break;
                    case 10:
                        itemDrop = ItemID.OasisCrateHard;
                        break;
                    case 11:
                        itemDrop = ItemID.LavaCrateHard;
                        break;
                    case 12:
                        itemDrop = ItemID.OceanCrateHard;
                        break;
                }
            }

            if (inShimmer && merchantRod)
            {
                int choice = Main.rand.Next(51);
                switch (choice)
                {
                    case 0:
                        itemDrop = ItemID.Oyster;
                        break;
                }
            }

            if (inShimmer && questRod)
            {
                int choice = Main.rand.Next(41);
                switch (choice)
                {
                    case 0:
                        itemDrop = ItemID.AmanitaFungifin;
                        break;
                    case 1:
                        itemDrop = ItemID.Angelfish;
                        break;
                    case 2:
                        itemDrop = ItemID.Batfish;
                        break;
                    case 3:
                        itemDrop = ItemID.BloodyManowar;
                        break;
                    case 4:
                        itemDrop = ItemID.Bonefish;
                        break;
                    case 5:
                        itemDrop = ItemID.BumblebeeTuna;
                        break;
                    case 6:
                        itemDrop = ItemID.Bunnyfish;
                        break;
                    case 7:
                        itemDrop = ItemID.BumblebeeTuna;
                        break;
                    case 8:
                        itemDrop = ItemID.Catfish;
                        break;
                    case 9:
                        itemDrop = ItemID.Cloudfish;
                        break;
                    case 10:
                        itemDrop = ItemID.Clownfish;
                        break;
                    case 11:
                        itemDrop = ItemID.Cursedfish;
                        break;
                    case 12:
                        itemDrop = ItemID.DemonicHellfish;
                        break;
                    case 13:
                        itemDrop = ItemID.Derpfish;
                        break;
                    case 14:
                        itemDrop = ItemID.Dirtfish;
                        break;
                    case 15:
                        itemDrop = ItemID.DynamiteFish;
                        break;
                    case 16:
                        itemDrop = ItemID.EaterofPlankton;
                        break;
                    case 17:
                        itemDrop = ItemID.FallenStarfish;
                        break;
                    case 18:
                        itemDrop = ItemID.Fishotron;
                        break;
                    case 19:
                        itemDrop = ItemID.Fishron;
                        break;
                    case 20:
                        itemDrop = ItemID.GuideVoodooFish;
                        break;
                    case 21:
                        itemDrop = ItemID.Harpyfish;
                        break;
                    case 22:
                        itemDrop = ItemID.Hungerfish;
                        break;
                    case 23:
                        itemDrop = ItemID.Ichorfish;
                        break;
                    case 24:
                        itemDrop = ItemID.InfectedScabbardfish;
                        break;
                    case 25:
                        itemDrop = ItemID.Jewelfish;
                        break;
                    case 26:
                        itemDrop = ItemID.MirageFish;
                        break;
                    case 27:
                        itemDrop = ItemID.Mudfish;
                        break;
                    case 28:
                        itemDrop = ItemID.MutantFlinxfin;
                        break;
                    case 29:
                        itemDrop = ItemID.Pengfish;
                        break;
                    case 30:
                        itemDrop = ItemID.Pixiefish;
                        break;
                    case 31:
                        itemDrop = ItemID.ScarabFish;
                        break;
                    case 32:
                        itemDrop = ItemID.ScorpioFish;
                        break;
                    case 33:
                        itemDrop = ItemID.Slimefish;
                        break;
                    case 34:
                        itemDrop = ItemID.Spiderfish;
                        break;
                    case 35:
                        itemDrop = ItemID.TheFishofCthulu;
                        break;
                    case 36:
                        itemDrop = ItemID.TropicalBarracuda;
                        break;
                    case 37:
                        itemDrop = ItemID.TundraTrout;
                        break;
                    case 38:
                        itemDrop = ItemID.UnicornFish;
                        break;
                    case 39:
                        itemDrop = ItemID.Wyverntail;
                        break;
                    case 40:
                        itemDrop = ItemID.ZombieFish;
                        break;
                }
            }

            if (inShimmer && goldRod)
            {
                itemDrop = ItemID.GoldenCarp;
            }
        }
    }

    public override void ModifyCaughtFish(Item fish)
    {
        if (Player.GetFishingConditions().BaitItemType != ItemID.EmpressButterfly && fish.rare != ItemRarityID.Quest)
        {
            if (Player.GetFishingConditions().BaitItemType != ItemID.Shimmerfly && fish.rare != ItemRarityID.Quest)
            {
                fish.stack -= 999;
            }
        }
    }
}