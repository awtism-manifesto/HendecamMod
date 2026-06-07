using HendecamMod.Content.Items;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Items.Consumables;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Items.Tools;
using HendecamMod.Content.Items.Weapons.Magic;
using HendecamMod.Content.Items.Weapons.Melee;
using HendecamMod.Content.Items.Weapons.Multiclass;
using HendecamMod.Content.Items.Weapons.Ranger;
using HendecamMod.Content.Items.Weapons.Stupid;
using HendecamMod.Content.Items.Weapons.VapeItems;
using Terraria.GameContent.ItemDropRules;

namespace HendecamMod.Content.Global;

public class HendecamDrops : GlobalNPC
{
    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {

        if (npc.type == NPCID.PirateShip)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BigBuddy>(), chanceDenominator: 8));
          
        }

        if (npc.type == NPCID.PirateCaptain)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<ShatteredKeyboard>(), chanceDenominator: 15));
            npcLoot.Add(ItemDropRule.Common(ItemType<CaptainsCannon>(), chanceDenominator: 5));
            npcLoot.Add(ItemDropRule.Common(ItemType<Bundlebuss>(), chanceDenominator: 10));
          
        }

      

        if (npc.type == NPCID.MoonLordCore)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<TheMoon>(), chanceDenominator: 4));
            npcLoot.Add(ItemDropRule.Common(ItemType<AmalgamatedFragment>(), chanceDenominator: 1));
        }

        if (npc.type == NPCID.GreenSlime)
        {

            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 100, 1, 1));
        }
        if (npc.type == NPCID.BlueSlime)
        {

            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 100, 1, 1));
        }
        if (npc.type == NPCID.RedSlime)
        {

            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 100, 1, 1));
        }
        if (npc.type == NPCID.PurpleSlime)
        {

            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 100, 1, 1));
        }
        if (npc.type == NPCID.JungleSlime)
        {

            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 100, 1, 1));
        }
        if (npc.type == NPCID.SlimeSpiked)
        {

            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 100, 1, 1));
        }
        if (npc.type == NPCID.MotherSlime)
        {

            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 100, 1, 1));
        }
        if (npc.type == NPCID.BabySlime)
        {

            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 100, 1, 1));
        }
        if (npc.type == NPCID.Hellhound)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SmallSausageSpammer>(), chanceDenominator: 20));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 150, 1, 1));
        }

        if (npc.type == NPCID.DD2Betsy)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<PhotonShotgun>(), chanceDenominator: 2));
        }

        if (npc.type == NPCID.Crab)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<TheFishStick>(), chanceDenominator: 20));
            npcLoot.Add(ItemDropRule.Common(ItemType<CollarOfTheDamned>(), chanceDenominator: 50));
            npcLoot.Add(ItemDropRule.Common(ItemType<PlasticScrap>(), 20, 4, 9));
        }

        if (npc.type == NPCID.Nymph)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<Heartache>(), chanceDenominator: 3));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 75, 1, 1));
        }

        if (npc.type == NPCID.Moth)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<Casanova>(), chanceDenominator: 4));
        }

        if (npc.type == NPCID.MossHornet)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<Casanova>(), chanceDenominator: 50));
        }

        if (npc.type == NPCID.BigMossHornet)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<Casanova>(), chanceDenominator: 50));
        }

        if (npc.type == NPCID.LittleMossHornet)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<Casanova>(), chanceDenominator: 50));
        }

        if (npc.type == NPCID.GiantMossHornet)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<Casanova>(), chanceDenominator: 50));
        }

        if (npc.type == NPCID.TinyMossHornet)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<Casanova>(), chanceDenominator: 50));
        }

        if (npc.type == NPCID.DoctorBones)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<DeadSoldiersRifle>(), chanceDenominator: 4));
        }

        if (npc.type == NPCID.PinkJellyfish)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<TheFishStick>(), chanceDenominator: 12));
            npcLoot.Add(ItemDropRule.Common(ItemType<PlasticScrap>(), 15, 4, 9));
        }

        if (npc.type == NPCID.BoneSerpentHead)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<GiantBone>(), chanceDenominator: 100));
            npcLoot.Add(ItemDropRule.Common(ItemType<LuckyCigarette>(), chanceDenominator: 20));
        }

        if (npc.type == NPCID.HallowBoss)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<PrismaticBullet>(), 1, 1449, 2749));
        }

        if (npc.type == NPCID.IceGolem)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<TheIcebreaker>(), 7));
            npcLoot.Add(ItemDropRule.Common(ItemType<IcicleMinigun>(), 7));
        }

        if (npc.type == NPCID.BloodNautilus)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<EcologicalOvershot>(), 2));
        }

        if (npc.type == NPCID.GoblinSummoner)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<Shadowflame>(), 1, 21, 41));
            npcLoot.Add(ItemDropRule.Common(ItemID.TatteredCloth, 2, 4, 12));
            npcLoot.Add(ItemDropRule.Common(ItemID.GoblinBattleStandard));
        }

        if (npc.type == NPCID.Zombie)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BadGrades>(), chanceDenominator: 50));
            npcLoot.Add(ItemDropRule.Common(ItemID.BloodyMachete, 125));
        }

        if (npc.type == NPCID.BaldZombie)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BadGrades>(), chanceDenominator: 50));
            npcLoot.Add(ItemDropRule.Common(ItemID.BloodyMachete, 125));
        }

        if (npc.type == NPCID.BigBaldZombie)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BadGrades>(), chanceDenominator: 50));
            npcLoot.Add(ItemDropRule.Common(ItemID.BloodyMachete, 125));
        }

        if (npc.type == NPCID.SmallBaldZombie)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BadGrades>(), chanceDenominator: 50));
            npcLoot.Add(ItemDropRule.Common(ItemID.BloodyMachete, 125));
        }

        if (npc.type == NPCID.ZombieDoctor)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BadGrades>(), chanceDenominator: 50));
            npcLoot.Add(ItemDropRule.Common(ItemID.BloodyMachete, 125));
        }

        if (npc.type == NPCID.ZombieEskimo)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BadGrades>(), chanceDenominator: 50));
            npcLoot.Add(ItemDropRule.Common(ItemID.RedRyder, 20));
            npcLoot.Add(ItemDropRule.Common(ItemID.BloodyMachete, 125));
        }

        if (npc.type == NPCID.ArmedZombieEskimo)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BadGrades>(), chanceDenominator: 50));
            npcLoot.Add(ItemDropRule.Common(ItemID.RedRyder, 20));
            npcLoot.Add(ItemDropRule.Common(ItemID.BloodyMachete, 125));
        }

        if (npc.type == NPCID.ZombieRaincoat)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BadGrades>(), chanceDenominator: 50));
            npcLoot.Add(ItemDropRule.Common(ItemID.BloodyMachete, 125));
        }

        if (npc.type == NPCID.PincushionZombie)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BadGrades>(), chanceDenominator: 50));
            npcLoot.Add(ItemDropRule.Common(ItemID.BloodyMachete, 125));
        }

        if (npc.type == NPCID.BigPincushionZombie)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BadGrades>(), chanceDenominator: 50));
            npcLoot.Add(ItemDropRule.Common(ItemID.BloodyMachete, 125));
        }

        if (npc.type == NPCID.TorchZombie)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BadGrades>(), chanceDenominator: 50));
            npcLoot.Add(ItemDropRule.Common(ItemID.BloodyMachete, 125));
        }

        if (npc.type == NPCID.ArmedTorchZombie)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BadGrades>(), chanceDenominator: 50));
            npcLoot.Add(ItemDropRule.Common(ItemID.BloodyMachete, 125));
        }

        if (npc.type == NPCID.ArmedZombieSlimed)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BadGrades>(), chanceDenominator: 50));
            npcLoot.Add(ItemDropRule.Common(ItemID.BloodyMachete, 125));
        }

        if (npc.type == NPCID.ArmedZombiePincussion)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BadGrades>(), chanceDenominator: 50));
            npcLoot.Add(ItemDropRule.Common(ItemID.BloodyMachete, 125));
        }

        if (npc.type == NPCID.Skeleton)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BadGrades>(), chanceDenominator: 20));
            npcLoot.Add(ItemDropRule.Common(ItemType<SkeletonKey>(), chanceDenominator: 2500));
        }

        if (npc.type == NPCID.AngryBones)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BadGrades>(), chanceDenominator: 20));
            npcLoot.Add(ItemDropRule.Common(ItemType<GiantBone>(), chanceDenominator: 50));
            npcLoot.Add(ItemDropRule.Common(ItemType<SkeletonKey>(), chanceDenominator: 250));
        }

        if (npc.type == NPCID.AngryBonesBig)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BadGrades>(), chanceDenominator: 20));
            npcLoot.Add(ItemDropRule.Common(ItemType<GiantBone>(), chanceDenominator: 50));
            npcLoot.Add(ItemDropRule.Common(ItemType<SkeletonKey>(), chanceDenominator: 250));
        }

        if (npc.type == NPCID.AngryBonesBigHelmet)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BadGrades>(), chanceDenominator: 20));
            npcLoot.Add(ItemDropRule.Common(ItemType<GiantBone>(), chanceDenominator: 50));
            npcLoot.Add(ItemDropRule.Common(ItemType<SkeletonKey>(), chanceDenominator: 250));
        }

        if (npc.type == NPCID.AngryBonesBigMuscle)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BadGrades>(), chanceDenominator: 10));
            npcLoot.Add(ItemDropRule.Common(ItemType<GiantBone>(), chanceDenominator: 48));
            npcLoot.Add(ItemDropRule.Common(ItemType<SkeletonKey>(), chanceDenominator: 250));
        }

        if (npc.type == NPCID.CursedSkull)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<GiantBone>(), chanceDenominator: 150));
            npcLoot.Add(ItemDropRule.Common(ItemType<SkeletonKey>(), chanceDenominator: 225));
        }

        if (npc.type == NPCID.Bee)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<Libeerator>(), chanceDenominator: 40));
        }

        if (npc.type == NPCID.BeeSmall)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<Libeerator>(), chanceDenominator: 40));
        }

        if (npc.type == NPCID.DarkCaster)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<WaterflameSword>(), chanceDenominator: 15));
            npcLoot.Add(ItemDropRule.Common(ItemType<TheCommunistManifesto>(), chanceDenominator: 25));
            npcLoot.Add(ItemDropRule.Common(ItemType<SkeletonKey>(), chanceDenominator: 225));
            npcLoot.Add(ItemDropRule.Common(ItemID.MagicCuffs, 20));
        }

        if (npc.type == NPCID.DesertScorpionWalk)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SandyScorpion>(), chanceDenominator: 11));
        }

        if (npc.type == NPCID.DesertScorpionWall)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SandyScorpion>(), chanceDenominator: 11));
        }

        if (npc.type == NPCID.SandShark)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SandyScorpion>(), chanceDenominator: 20));
        }

        if (npc.type == NPCID.SandsharkCorrupt)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SandyScorpion>(), chanceDenominator: 20));
        }

        if (npc.type == NPCID.SandsharkCrimson)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SandyScorpion>(), chanceDenominator: 20));
        }

        if (npc.type == NPCID.SandsharkHallow)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SandyScorpion>(), chanceDenominator: 19));
        }

        if (npc.type == NPCID.DungeonSlime)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<Liquidation>(), chanceDenominator: 25));
            npcLoot.Add(ItemDropRule.Common(ItemType<SkeletonKey>(), chanceDenominator: 175));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 100, 1, 1));
        }

        if (npc.type == NPCID.RedDevil)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<CardinalSin>(), chanceDenominator: 19));
        }

        if (npc.type == NPCID.Unicorn)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<UnicornPoacher>(), chanceDenominator: 19));
        }

        if (npc.type == NPCID.GiantTortoise)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<ShatteredKeyboard>(), chanceDenominator: 15));
        }

        if (npc.type == NPCID.Nurse)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<Heartache>(), chanceDenominator: 25));
            npcLoot.Add(ItemDropRule.Common(ItemType<FreeHealthcare>(), chanceDenominator: 5));
        }

        if (npc.type == NPCID.Cyborg)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<DvdLogo>(), chanceDenominator: 2));
        }

        if (npc.type == NPCID.Nutcracker)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<CrackedNuts>(), chanceDenominator: 15));
        }

        if (npc.type == NPCID.NutcrackerSpinning)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<CrackedNuts>(), chanceDenominator: 12));
        }

        if (npc.type == NPCID.Mimic)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BowlingBaller>(), chanceDenominator: 7));
        }

        if (npc.type == NPCID.DiabolistRed)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<ShatteredKeyboard>(), chanceDenominator: 25));
        }

        if (npc.type == NPCID.Gastropod)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<ShatteredKeyboard>(), chanceDenominator: 90));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 100, 1, 1));
        }

        if (npc.type == NPCID.SkeletonArcher)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<ShatteredKeyboard>(), chanceDenominator: 70));
        }

        if (npc.type == NPCID.RuneWizard)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<ShatteredKeyboard>(), chanceDenominator: 5));
            npcLoot.Add(ItemDropRule.Common(ItemType<RunicRaygun>(), chanceDenominator: 2));
            npcLoot.Add(ItemDropRule.Common(ItemType<RunicCodex>(), chanceDenominator: 2));
        }

        if (npc.type == NPCID.QueenBee)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<PocketBees>(), chanceDenominator: 5));
        }

        if (npc.type == NPCID.SantaNK1)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<GagGifter>(), chanceDenominator: 6));

            npcLoot.Add(ItemDropRule.Common(ItemType<Bergentrucking>(), chanceDenominator: 9));
        }

        if (npc.type == NPCID.PirateDeadeye)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<ShatteredKeyboard>(), chanceDenominator: 66));

            npcLoot.Add(ItemDropRule.Common(ItemType<Bundlebuss>(), chanceDenominator: 20));
        }

        if (npc.type == NPCID.Scutlix)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<PulsePistols>(), chanceDenominator: 18));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 100, 1, 1));
        }

        if (npc.type == NPCID.RayGunner)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<PulsePistols>(), chanceDenominator: 18));
            npcLoot.Add(ItemDropRule.Common(ItemType<MartianDrugs>(), chanceDenominator: 50));
        }
        if (npc.type == NPCID.GrayGrunt)
        {

            npcLoot.Add(ItemDropRule.Common(ItemType<MartianDrugs>(), chanceDenominator: 50));
        }
        if (npc.type == NPCID.MartianEngineer)
        {

            npcLoot.Add(ItemDropRule.Common(ItemType<MartianDrugs>(), chanceDenominator: 50));
        }
        if (npc.type == NPCID.MartianOfficer)
        {

            npcLoot.Add(ItemDropRule.Common(ItemType<MartianDrugs>(), chanceDenominator: 40));
        }
        if (npc.type == NPCID.GigaZapper)
        {

            npcLoot.Add(ItemDropRule.Common(ItemType<MartianDrugs>(), chanceDenominator: 50));
        }
        if (npc.type == NPCID.BrainScrambler)
        {

            npcLoot.Add(ItemDropRule.Common(ItemType<MartianDrugs>(), chanceDenominator: 50));
        }

        if (npc.type == NPCID.MartianSaucerCore)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<PulsePistols>(), chanceDenominator: 7));
            npcLoot.Add(ItemDropRule.Common(ItemType<MartianDronegun>(), chanceDenominator: 7));
            npcLoot.Add(ItemDropRule.ByCondition(new PostMoonlordDrop(), ItemType<FissionDrive>(), chanceDenominator: 4, chanceNumerator: 3));
        }

        if (npc.type == NPCID.Angler)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new HardmodeDrop(), ItemType<Six7Gun>(), chanceDenominator: 7, chanceNumerator: 6));
        }

        if (npc.type == NPCID.Golem)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new PostMoonlordDrop(), ItemType<FissionDrive>(), chanceDenominator: 5, chanceNumerator: 4));
        }

        if (npc.type == NPCID.TheDestroyer)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new PostMoonlordDrop(), ItemType<FissionDrive>(), chanceDenominator: 5, chanceNumerator: 3));
        }

        if (npc.type == NPCID.SkeletronPrime)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new PostMoonlordDrop(), ItemType<FissionDrive>(), chanceDenominator: 5, chanceNumerator: 3));
        }

        if (npc.type == NPCID.Retinazer)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new PostMoonlordDrop(), ItemType<FissionDrive>(), chanceDenominator: 10, chanceNumerator: 3));
        }

        if (npc.type == NPCID.Spazmatism)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new PostMoonlordDrop(), ItemType<FissionDrive>(), chanceDenominator: 10, chanceNumerator: 3));
            npcLoot.Add(ItemDropRule.Common(ItemType<AutismOrb>(), chanceDenominator: 5));
        }

        if (npc.type == NPCID.ScutlixRider)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<PulsePistols>(), chanceDenominator: 18));
        }

        if (npc.type == NPCID.Necromancer)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BrokenHeroGun>(), chanceDenominator: 20));
        }

        if (npc.type == NPCID.BlueArmoredBones)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BrokenHeroGun>(), chanceDenominator: 25));
            npcLoot.Add(ItemDropRule.Common(ItemID.ShroomiteBar, 4, 2, 4));
        }

        if (npc.type == NPCID.Lavabat)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.LivingFireBlock, 5, 25, 54));
        }

        if (npc.type == NPCID.HellArmoredBones)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.HellstoneBar, 2, 2, 4));
        }

        if (npc.type == NPCID.RustyArmoredBonesAxe)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.MeteoriteBar, 1, 2, 4));
        }

        if (npc.type == NPCID.RustyArmoredBonesFlail)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.MeteoriteBar, 3, 2, 4));
        }

        if (npc.type == NPCID.RustyArmoredBonesSword)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.MeteoriteBar, 4, 2, 4));
        }

        if (npc.type == NPCID.RustyArmoredBonesSwordNoArmor)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.MeteoriteBar, 2, 2, 4));
        }

        if (npc.type == NPCID.HellArmoredBonesMace)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.HellstoneBar, 3, 2, 4));
        }

        if (npc.type == NPCID.HellArmoredBonesSword)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.HellstoneBar, 4, 2, 4));
        }

        if (npc.type == NPCID.HellArmoredBonesSpikeShield)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.HellstoneBar, 4, 2, 4));
        }

        if (npc.type == NPCID.BlueArmoredBonesNoPants)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BrokenHeroGun>(), chanceDenominator: 20));
            npcLoot.Add(ItemDropRule.Common(ItemID.ShroomiteBar, 1, 2, 4));
        }

        if (npc.type == NPCID.BlueArmoredBonesMace)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BrokenHeroGun>(), chanceDenominator: 25));
            npcLoot.Add(ItemDropRule.Common(ItemID.ShroomiteBar, 2, 2, 4));
        }

        if (npc.type == NPCID.BlueArmoredBonesSword)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BrokenHeroGun>(), chanceDenominator: 25));
            npcLoot.Add(ItemDropRule.Common(ItemID.ShroomiteBar, 2, 2, 4));
        }

        if (npc.type == NPCID.SkeletonSniper)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BrokenHeroGun>(), chanceDenominator: 9));
        }

        if (npc.type == NPCID.AncientCultistSquidhead)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<StupidFuckingPickaxe>(), chanceDenominator: 2555));
        }

        if (npc.type == NPCID.BigMimicHallow)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<DartShotgun>(), chanceDenominator: 5));
        }

        if (npc.type == NPCID.TacticalSkeleton)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BrokenHeroGun>(), chanceDenominator: 15));
        }

        if (npc.type == NPCID.AngryTrapper)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<ShatteredKeyboard>(), chanceDenominator: 60));
        }

        if (npc.type == NPCID.Wraith)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<ShatteredKeyboard>(), chanceDenominator: 115));
        }

        if (npc.type == NPCID.KingSlime)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SuperSamuraiSlicer>(), chanceDenominator: 4));
            npcLoot.Add(ItemDropRule.Common(ItemID.Trimarang, 9));
            npcLoot.Add(ItemDropRule.Common(ItemID.Katana, 2));
            npcLoot.Add(ItemDropRule.Common(ItemType<Bergentrucking>(), chanceDenominator: 5));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 5, 1, 1));
        }

        if (npc.type == NPCID.SkeletronHead)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SkeletonKey>(), chanceDenominator: 8));
        }

        if (npc.type == NPCID.DungeonGuardian)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SkeletonKey>(), chanceDenominator: 2));
        }

        if (npc.type == NPCID.DD2DarkMageT1)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<ArcaneDartgun>(), chanceDenominator: 5));
        }

        if (npc.type == NPCID.GoblinPeon)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.TatteredCloth, 3, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 150, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemType<ScreamingSoyjak>(), 180, 1, 1));
        }

        if (npc.type == NPCID.GoblinThief)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.TatteredCloth, 3, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 150, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemType<ScreamingSoyjak>(), 144, 1, 1));
        }

        if (npc.type == NPCID.GoblinWarrior)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.TatteredCloth, 3, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 150, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemType<ScreamingSoyjak>(), 96, 1, 1));
        }

        if (npc.type == NPCID.GoblinSorcerer)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.TatteredCloth, 3, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 150, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemType<ScreamingSoyjak>(), 111, 1, 1));
        }

        if (npc.type == NPCID.GoblinArcher)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.TatteredCloth, 3, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 150, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemType<ScreamingSoyjak>(), 90, 1, 1));
        }

        if (npc.type == NPCID.DD2OgreT2)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<TrenboloneAcetate>(), chanceDenominator: 3));
        }

        if (npc.type == NPCID.DD2OgreT3)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<TrenboloneAcetate>(), chanceDenominator: 5));
        }

        if (npc.type == NPCID.DD2DarkMageT3)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<ArcaneDartgun>(), chanceDenominator: 6));
        }

        if (npc.type == NPCID.DemonEye)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Lens, 2));
        }

        if (npc.type == NPCID.DemonEyeSpaceship)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Lens, 2));
        }

        if (npc.type == NPCID.DemonEye2)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Lens, 2));
        }

        if (npc.type == NPCID.QueenSlimeBoss)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<MaximGelgun>(), chanceDenominator: 3));
            npcLoot.Add(ItemDropRule.Common(ItemType<QueensShank>(), chanceDenominator: 4));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 5, 1, 1));
        }

        if (npc.type == NPCID.BrainofCthulhu)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<GoodGrades>(), chanceDenominator: 2));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 5, 1, 1));
        }

        if (npc.type == NPCID.Plantera)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SeedBomber>(), chanceDenominator: 7));
            npcLoot.Add(ItemDropRule.Common(ItemType<OtherworldlySixPack>(), chanceDenominator: 5));
        }

        if (npc.type == NPCID.EyeofCthulhu)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.BlackLens, 2, 2, 3));
            npcLoot.Add(ItemDropRule.Common(ItemType<EyeRifle>(), chanceDenominator: 4));
            npcLoot.Add(ItemDropRule.Common(ItemType<EyePoker>(), chanceDenominator: 4));
            npcLoot.Add(ItemDropRule.Common(ItemID.VampireFrogStaff, 4));
            npcLoot.Add(ItemDropRule.Common(ItemID.BloodRainBow, 4));
            npcLoot.Add(ItemDropRule.Common(ItemID.Lens, 1, 1, 5));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 4, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.BlackLens, 5));
            npcLoot.Add(ItemDropRule.Common(ItemID.BloodMoonStarter, 1));
        }

        if (npc.type == NPCID.EyeballFlyingFish)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.BlackLens, 1, 1, 10));
            npcLoot.Add(ItemDropRule.Common(ItemType<EyeRifle>(), chanceDenominator: 8));
            npcLoot.Add(ItemDropRule.Common(ItemType<EyePoker>(), chanceDenominator: 8));
        }

        if (npc.type == NPCID.Deerclops)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<CaribousCatastrophe>(), chanceDenominator: 3));
            npcLoot.Add(ItemDropRule.Common(ItemType<FlinxsFurblade>(), chanceDenominator: 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.FlinxFur, 1, 6, 15));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 10, 1, 1));
        }

        if (npc.type == NPCID.SnowFlinx)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.FlinxFur, 1, 1, 2));
        }

        if (npc.type == NPCID.ManEater)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Stinger, 2, 2, 3));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 150, 1, 1));
        }

        if (npc.type == NPCID.Snatcher)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Stinger, 3, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 150, 1, 1));
        }

        if (npc.type == NPCID.LittleHornetLeafy)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Vine, 3, 1, 2));
        }

        if (npc.type == NPCID.HornetLeafy)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Vine, 2, 2, 2));
        }

        if (npc.type == NPCID.BigHornetLeafy)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Vine, 1, 2, 3));
        }

        if (npc.type == NPCID.SandElemental)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Nazar, 5));
        }

        if (npc.type == NPCID.Demon)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.DemonConch, 25));
            npcLoot.Add(ItemDropRule.Common(ItemType<LuckyCigarette>(), chanceDenominator: 20));
        }

        if (npc.type == NPCID.VoodooDemon)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.DemonConch, 20));
            npcLoot.Add(ItemDropRule.Common(ItemType<LuckyCigarette>(), chanceDenominator: 20));
        }

        if (npc.type == NPCID.Butcher)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.HamBat, 20));
        }

        if (npc.type == NPCID.Harpy)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.CreativeWings, 35));
        }

        if (npc.type == NPCID.Derpling)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Uzi, 30));
        }

        if (npc.type == NPCID.MeteorHead)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Meteorite, 2, 1, 3));
        }

        if (npc.type == NPCID.BigMimicCrimson)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.SoulofNight, 1, 9, 14));
        }

        if (npc.type == NPCID.BigMimicCorruption)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.SoulofNight, 1, 9, 14));
        }

        if (npc.type == NPCID.BigMimicHallow)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.SoulofLight, 1, 9, 14));
        }

        if (npc.type == NPCID.Shark)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<TheFishStick>(), chanceDenominator: 7));
            npcLoot.Add(ItemDropRule.Common(ItemID.Minishark, 10));
            npcLoot.Add(ItemDropRule.Common(ItemType<PlasticScrap>(), 25, 4, 9));
        }

        if (npc.type == NPCID.Squid)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<TheFishStick>(), chanceDenominator: 9));
            npcLoot.Add(ItemDropRule.Common(ItemType<PlasticScrap>(), 15, 4, 9));
        }

        if (npc.type == NPCID.UndeadViking)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.RedRyder, 15));
            npcLoot.Add(ItemDropRule.Common(ItemID.FlinxFur, 8, 1, 3));
        }

        if (npc.type == NPCID.Werewolf)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Sextant, 20));
        }

        if (npc.type == NPCID.TacticalSkeleton)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.OnyxBlaster, 20));
        }

        if (npc.type == NPCID.SpikedJungleSlime)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.DPSMeter, 33));
            npcLoot.Add(ItemDropRule.Common(ItemType<VerdantClaymore>(), chanceDenominator: 25));
            npcLoot.Add(ItemDropRule.Common(ItemID.JungleSpores, 3, 2, 4));
        }

        if (npc.type == NPCID.Poltergeist)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Ectoplasm, 3, 1, 4));
        }

        if (npc.type == NPCID.SeekerHead)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.WormTooth, 2, 2, 4));
        }

        if (npc.type == NPCID.SpikedIceSlime)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.BlizzardinaBottle, 20));
            npcLoot.Add(ItemDropRule.Common(ItemID.IceBlade, 33));
        }

        if (npc.type == NPCID.SandSlime)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.SandstorminaBottle, 20));
        }

        if (npc.type == NPCID.AngryNimbus)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.WeatherRadio, 20));
        }

        if (npc.type == NPCID.Crawdad)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.FeralClaws, 10));
        }

        if (npc.type == NPCID.WalkingAntlion)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.SandBoots, 20));
        }

        if (npc.type == NPCID.GiantWalkingAntlion)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.SandBoots, 15));
        }

        if (npc.type == NPCID.Corruptor)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Toxikarp, 33));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 150, 1, 1));
        }

        if (npc.type == NPCID.Herpling)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Bladetongue, 33));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 150, 1, 1));
        }

        if (npc.type == NPCID.FlyingFish)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.FishermansGuide, 100));
        }

        if (npc.type == NPCID.Hellbat)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<LuckyCigarette>(), chanceDenominator: 20));
        }


        if (npc.type == NPCID.FireImp)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<LuckyCigarette>(), chanceDenominator: 20));
        }

        if (npc.type == NPCID.LavaSlime)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<LuckyCigarette>(), chanceDenominator: 20));
        }

        if (npc.type == NPCID.BoneLee)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.MasterNinjaGear, 60));
        }

        if (npc.type == NPCID.Drippler)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Terragrim, 200));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 125, 1, 1));
        }
        if (npc.type == NPCID.DrManFly)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BrokenHeroVape>(), chanceDenominator: 10));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 25, 1, 1));
        }

        if (npc.type == NPCID.WallofFlesh)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<StalingradSpewer>(), chanceDenominator: 3));
            npcLoot.Add(ItemDropRule.Common(ItemType<StupidEmblem>(), chanceDenominator: 4));
            npcLoot.Add(ItemDropRule.Common(ItemID.Lens, 1, 2, 12));
            npcLoot.Add(ItemDropRule.Common(ItemID.ShadowScale, 2, 10, 25));
            npcLoot.Add(ItemDropRule.Common(ItemID.TissueSample, 2, 10, 25));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 1));
            npcLoot.Add(ItemDropRule.Common(ItemType<MantleBooster>(), chanceDenominator: 1));
        }
        if (npc.type == NPCID.Demon)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<FireDiamond>(), 4, 2, 7));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 150, 1, 1));
        }

        if (npc.type == NPCID.FireImp)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<FireDiamond>(), 4, 2, 7));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 150, 1, 1));
        }

        if (npc.type == NPCID.Hellbat)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<FireDiamond>(), 4, 2, 7));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 150, 1, 1));
        }

        if (npc.type == NPCID.LavaSlime)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<FireDiamond>(), 4, 2, 7));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 100, 1, 1));
        }

        if (npc.type == NPCID.Lavabat)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<FireDiamond>(), 4, 2, 7));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 150, 1, 1));
        }

        if (npc.type == NPCID.BoneSerpentHead)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<FireDiamond>(), 4, 2, 7));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 150, 1, 1));
        }

        if (npc.type == NPCID.RedDevil)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<FireDiamond>(), 4, 2, 7));
            npcLoot.Add(ItemDropRule.Common(ItemType<SoulOfHeight>(), chanceDenominator: 3));

        }

        if (npc.type == NPCID.VoodooDemon)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<FireDiamond>(), chanceDenominator: 4, 2, 7));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 150, 1, 1));
        }

        if (npc.type == NPCID.Harpy)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<LunarGem>(), chanceDenominator: 3, 1, 4));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 150, 1, 1));
        }

        if (npc.type == NPCID.WyvernBody)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<LunarGem>(), chanceDenominator: 2, 5, 12));
        }

        if (npc.type == NPCID.Zombie)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<LunarGem>(), chanceDenominator: 10, 3, 5));
        }

        if (npc.type == NPCID.DemonEye)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<LunarGem>(), chanceDenominator: 10, 3, 5));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 150, 1, 1));
        }

        if (npc.type == NPCID.PossessedArmor)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SteelBar>(), chanceDenominator: 5, 1, 3));

        }

        if (npc.type == NPCID.Corruptor)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SoulOfHeight>(), chanceDenominator: 3));
        }

        if (npc.type == NPCID.FloatyGross)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SoulOfHeight>(), chanceDenominator: 3));
        }

        if (npc.type == NPCID.GiantBat)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SoulOfHeight>(), chanceDenominator: 3));
        }

        if (npc.type == NPCID.GiantFlyingFox)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SoulOfHeight>(), chanceDenominator: 3));
        }

        if (npc.type == NPCID.Gastropod)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SoulOfHeight>(), chanceDenominator: 3));
        }

        if (npc.type == NPCID.IlluminantBat)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SoulOfHeight>(), chanceDenominator: 3));
        }

        if (npc.type == NPCID.IceElemental)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SoulOfHeight>(), chanceDenominator: 3));
        }

        if (npc.type == NPCID.Lavabat)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SoulOfHeight>(), chanceDenominator: 3));
        }

        if (npc.type == NPCID.MossHornet)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SoulOfHeight>(), chanceDenominator: 3));
        }

        if (npc.type == NPCID.Pixie)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SoulOfHeight>(), chanceDenominator: 3));
        }

        if (npc.type == NPCID.Wraith)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SoulOfHeight>(), chanceDenominator: 3));
        }

        if (npc.type == NPCID.WanderingEye)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SoulOfHeight>(), chanceDenominator: 3));
        }

        if (npc.type == NPCID.DarkCaster)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.WaterBolt, 30));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 150, 1, 1));
        }

        if (npc.type == NPCID.BlackSlime)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 100, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemType<CoalLump>(), 1, 2, 7));
            npcLoot.Add(ItemDropRule.Common(ItemType<CrudeOil>(), 5, 5, 15));
        }

        if (npc.type == NPCID.SandSlime)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.SandBlock, 1, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 100, 1, 1));
        }

        if (npc.type == NPCID.AngryNimbus)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.RainCloud, 3, 20, 35));
        }

        if (npc.type == NPCID.IceMimic)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.SnowGlobe, 3));
        }

        if (npc.type == NPCID.IceSlime)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.SnowBlock, 1, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 100, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.HandWarmer, 100));
        }

        if (npc.type == NPCID.MeteorHead)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.BlackLens, 10, 2, 5));
        }

        if (npc.type == NPCID.SpikedIceSlime)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.IceBlock, 1, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 100, 1, 1));
        }

        if (npc.type == NPCID.CrimsonAxe)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.BloodLustCluster, 6));
        }

        if (npc.type == NPCID.ArmedZombieEskimo)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.HandWarmer, 100));
        }

        if (npc.type == NPCID.ZombieEskimo)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.HandWarmer, 100));
        }

        if (npc.type == NPCID.IceElemental)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.HandWarmer, 100));
        }

        if (npc.type == NPCID.Wolf)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.HandWarmer, 100));
        }

        if (npc.type == NPCID.IceGolem)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.HandWarmer, 100));
        }

        if (npc.type == NPCID.TwiggyZombie)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<WoodenStick>(), 4, 2, 5));
        }

        if (npc.type == NPCID.ArmedZombieTwiggy)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<WoodenStick>(), 4, 2, 5));
        }

        if (npc.type == NPCID.BigTwiggyZombie)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<WoodenStick>(), 4, 2, 6));
        }

        if (npc.type == NPCID.SmallTwiggyZombie)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<WoodenStick>(), 4, 2, 4));
        }

        if (npc.type == NPCID.FlyingFish)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.SharkFin, 66));
            npcLoot.Add(ItemDropRule.Common(ItemType<MonsterStemCells>(), 150, 1, 1));
        }

        if (npc.type == NPCID.Angler)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.MagicConch));
        }

        if (npc.type == NPCID.HoppinJack)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Pumpkin, 4, 12, 28));
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 3, 7));
        }

        if (npc.type == NPCID.MoonLordCore)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.FragmentNebula, 4, 6, 30));
            npcLoot.Add(ItemDropRule.Common(ItemID.FragmentVortex, 4, 6, 30));
            npcLoot.Add(ItemDropRule.Common(ItemID.FragmentStardust, 4, 6, 30));
            npcLoot.Add(ItemDropRule.Common(ItemID.FragmentSolar, 4, 6, 30));
        }

        if (npc.type == NPCID.RuneWizard)
        {
        }
    }
}