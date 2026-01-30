
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations;

//[AutoloadEquip(EquipType.Beard)]
public class PeaceAmongCthulhu : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Cthulhu and his parts should be friendly");
        tooltips.Add(line);
    }
    public override void UpdateEquip(Player player)
    {
        player.npcTypeNoAggro[NPCID.EyeofCthulhu] = true;
        player.npcTypeNoAggro[NPCID.WanderingEye] = true;
        player.npcTypeNoAggro[NPCID.DemonEye] = true;
        player.npcTypeNoAggro[NPCID.DemonEyeOwl] = true;
        player.npcTypeNoAggro[NPCID.DemonEyeSpaceship] = true;
        player.npcTypeNoAggro[NPCID.EyeballFlyingFish] = true;
        player.npcTypeNoAggro[NPCID.Eyezor] = true;
        player.npcTypeNoAggro[NPCID.CataractEye] = true;
        player.npcTypeNoAggro[NPCID.DialatedEye] = true;
        player.npcTypeNoAggro[NPCID.GreenEye] = true;
        player.npcTypeNoAggro[NPCID.MoonLordFreeEye] = true;
        player.npcTypeNoAggro[NPCID.PurpleEye] = true;
        player.npcTypeNoAggro[NPCID.SleepyEye] = true;
        player.npcTypeNoAggro[NPCID.Spazmatism] = true;
        player.npcTypeNoAggro[NPCID.Retinazer] = true;
        player.npcTypeNoAggro[NPCID.SkeletronPrime] = true;
        player.npcTypeNoAggro[NPCID.PrimeCannon] = true;
        player.npcTypeNoAggro[NPCID.PrimeLaser] = true;
        player.npcTypeNoAggro[NPCID.PrimeSaw] = true;
        player.npcTypeNoAggro[NPCID.PrimeVice] = true;
        player.npcTypeNoAggro[NPCID.MoonLordCore] = true;
        player.npcTypeNoAggro[NPCID.MoonLordFreeEye] = true;
        player.npcTypeNoAggro[NPCID.MoonLordHand] = true;
        player.npcTypeNoAggro[NPCID.MoonLordHead] = true;
        player.npcTypeNoAggro[NPCID.MoonLordLeechBlob] = true;
        player.npcTypeNoAggro[NPCID.CultistArcherBlue] = true;
        player.npcTypeNoAggro[NPCID.CultistArcherWhite] = true;
        player.npcTypeNoAggro[NPCID.CultistBoss] = true;
        player.npcTypeNoAggro[NPCID.CultistBossClone] = true;
        player.npcTypeNoAggro[NPCID.CultistDevote] = true;
        player.npcTypeNoAggro[NPCID.CultistTablet] = true;
        player.npcTypeNoAggro[NPCID.AncientCultistSquidhead] = true;
        player.npcTypeNoAggro[NPCID.AncientDoom] = true;
        player.npcTypeNoAggro[NPCID.CultistDragonHead] = true;
        player.npcTypeNoAggro[NPCID.CultistDragonBody1] = true;
        player.npcTypeNoAggro[NPCID.CultistDragonBody2] = true;
        player.npcTypeNoAggro[NPCID.CultistDragonBody3] = true;
        player.npcTypeNoAggro[NPCID.CultistDragonBody4] = true;
        player.npcTypeNoAggro[NPCID.CultistDragonTail] = true;
        player.npcTypeNoAggro[NPCID.Zombie] = true;
        player.npcTypeNoAggro[NPCID.ZombieEskimo] = true;
        player.npcTypeNoAggro[NPCID.ZombieDoctor] = true;
        player.npcTypeNoAggro[NPCID.ZombieElf] = true;
        player.npcTypeNoAggro[NPCID.ZombieElfBeard] = true;
        player.npcTypeNoAggro[NPCID.PincushionZombie] = true;
        player.npcTypeNoAggro[NPCID.TorchZombie] = true;
        player.npcTypeNoAggro[NPCID.ZombieElfGirl] = true;
        player.npcTypeNoAggro[NPCID.ZombieMerman] = true;
        player.npcTypeNoAggro[NPCID.ZombieElfGirl] = true;
        player.npcTypeNoAggro[NPCID.ZombieMushroom] = true;
        player.npcTypeNoAggro[NPCID.ZombieMushroomHat] = true;
        player.npcTypeNoAggro[NPCID.ZombiePixie] = true;
        player.npcTypeNoAggro[NPCID.ZombieRaincoat] = true;
        player.npcTypeNoAggro[NPCID.ZombieSuperman] = true;
        player.npcTypeNoAggro[NPCID.ZombieXmas] = true;
        player.npcTypeNoAggro[NPCID.ArmedZombie] = true;
        player.npcTypeNoAggro[NPCID.ArmedTorchZombie] = true;
        player.npcTypeNoAggro[NPCID.ZombieSweater] = true;
        player.npcTypeNoAggro[NPCID.ArmedZombieCenx] = true;
        player.npcTypeNoAggro[NPCID.ArmedZombiePincussion] = true;
        player.npcTypeNoAggro[NPCID.ArmedZombieEskimo] = true;
        player.npcTypeNoAggro[NPCID.ArmedZombieSlimed] = true;
        player.npcTypeNoAggro[NPCID.ArmedZombieSwamp] = true;
        player.npcTypeNoAggro[NPCID.ArmedZombieTwiggy] = true;
        player.npcTypeNoAggro[NPCID.BaldZombie] = true;
        player.npcTypeNoAggro[NPCID.BloodZombie] = true;
        player.npcTypeNoAggro[NPCID.FemaleZombie] = true;
        player.npcTypeNoAggro[NPCID.MaggotZombie] = true;
        player.npcTypeNoAggro[NPCID.SlimedZombie] = true;
        player.npcTypeNoAggro[NPCID.TorchZombie] = true;
        player.npcTypeNoAggro[NPCID.TwiggyZombie] = true;
        player.npcTypeNoAggro[NPCID.Skeleton] = true;
        player.npcTypeNoAggro[NPCID.SkeletonAlien] = true;
        player.npcTypeNoAggro[NPCID.SkeletonArcher] = true;
        player.npcTypeNoAggro[NPCID.SkeletonAstonaut] = true;
        player.npcTypeNoAggro[NPCID.SkeletonCommando] = true;
        player.npcTypeNoAggro[NPCID.SkeletonSniper] = true;
        player.npcTypeNoAggro[NPCID.SkeletonTopHat] = true;
        player.npcTypeNoAggro[NPCID.ArmoredSkeleton] = true;
        player.npcTypeNoAggro[NPCID.BoneThrowingSkeleton] = true;
        player.npcTypeNoAggro[NPCID.BoneThrowingSkeleton2] = true;
        player.npcTypeNoAggro[NPCID.BoneThrowingSkeleton3] = true;
        player.npcTypeNoAggro[NPCID.BoneThrowingSkeleton4] = true;
        player.npcTypeNoAggro[NPCID.DD2SkeletonT1] = true;
        player.npcTypeNoAggro[NPCID.DD2SkeletonT3] = true;
        player.npcTypeNoAggro[NPCID.GreekSkeleton] = true;
        player.npcTypeNoAggro[NPCID.HeadacheSkeleton] = true;
        player.npcTypeNoAggro[NPCID.MisassembledSkeleton] = true;
        player.npcTypeNoAggro[NPCID.SporeSkeleton] = true;
        player.npcTypeNoAggro[NPCID.TacticalSkeleton] = true;
        player.npcTypeNoAggro[NPCID.SkeletronHand] = true;
        player.npcTypeNoAggro[NPCID.SkeletronHead] = true;
        player.npcTypeNoAggro[NPCID.DungeonGuardian] = true;
        player.npcTypeNoAggro[NPCID.Ghost] = true;
        player.npcTypeNoAggro[NPCID.PirateGhost] = true;
        player.npcTypeNoAggro[NPCID.FloatyGross] = true;
        player.npcTypeNoAggro[NPCID.Wraith] = true;
        player.npcTypeNoAggro[NPCID.BoneSerpentBody] = true;
        player.npcTypeNoAggro[NPCID.BoneSerpentHead] = true;
        player.npcTypeNoAggro[NPCID.BoneSerpentTail] = true;
        player.npcTypeNoAggro[NPCID.BoneLee] = true;
        player.npcTypeNoAggro[NPCID.CursedSkull] = true;
        player.npcTypeNoAggro[NPCID.GiantCursedSkull] = true;
        player.npcTypeNoAggro[NPCID.UndeadMiner] = true;
        player.npcTypeNoAggro[NPCID.UndeadViking] = true;
        player.npcTypeNoAggro[NPCID.ArmoredViking] = true;
        player.npcTypeNoAggro[NPCID.BlueArmoredBones] = true;
        player.npcTypeNoAggro[NPCID.BlueArmoredBonesMace] = true;
        player.npcTypeNoAggro[NPCID.BlueArmoredBonesSword] = true;
        player.npcTypeNoAggro[NPCID.BlueArmoredBonesNoPants] = true;
        player.npcTypeNoAggro[NPCID.DarkCaster] = true;
        player.npcTypeNoAggro[NPCID.DarkMummy] = true;
        player.npcTypeNoAggro[NPCID.LightMummy] = true;
        player.npcTypeNoAggro[NPCID.Mummy] = true;
        player.npcTypeNoAggro[NPCID.RustyArmoredBonesAxe] = true;
        player.npcTypeNoAggro[NPCID.RustyArmoredBonesFlail] = true;
        player.npcTypeNoAggro[NPCID.RustyArmoredBonesSword] = true;
        player.npcTypeNoAggro[NPCID.RustyArmoredBonesSwordNoArmor] = true;
        player.npcTypeNoAggro[NPCID.HellArmoredBones] = true;
        player.npcTypeNoAggro[NPCID.HellArmoredBonesMace] = true;
        player.npcTypeNoAggro[NPCID.HellArmoredBonesSword] = true;
        player.npcTypeNoAggro[NPCID.HellArmoredBonesSpikeShield] = true;
        player.npcTypeNoAggro[NPCID.DiabolistRed] = true;
        player.npcTypeNoAggro[NPCID.DiabolistWhite] = true;
        player.npcTypeNoAggro[NPCID.Necromancer] = true;
        player.npcTypeNoAggro[NPCID.NecromancerArmored] = true;
        player.npcTypeNoAggro[NPCID.RaggedCaster] = true;
        player.npcTypeNoAggro[NPCID.RaggedCasterOpenCoat] = true;
        player.npcTypeNoAggro[NPCID.AngryBones] = true;
        player.npcTypeNoAggro[NPCID.AngryBonesBig] = true;
        player.npcTypeNoAggro[NPCID.AngryBonesBigHelmet] = true;
        player.npcTypeNoAggro[NPCID.AngryBonesBigMuscle] = true;
        player.npcTypeNoAggro[NPCID.DoctorBones] = true;
        player.npcTypeNoAggro[NPCID.TheBride] = true;
        player.npcTypeNoAggro[NPCID.TheGroom] = true;
        player.npcTypeNoAggro[NPCID.Reaper] = true;
        player.npcTypeNoAggro[NPCID.Eyezor] = true;
        player.npcTypeNoAggro[NPCID.CreatureFromTheDeep] = true;
        player.npcTypeNoAggro[NPCID.ThePossessed] = true;
        player.npcTypeNoAggro[NPCID.PossessedArmor] = true;
        player.npcTypeNoAggro[NPCID.Poltergeist] = true;
        player.npcTypeNoAggro[NPCID.SwampThing] = true;

    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<OcularPeaceAgreement>(1);
        recipe.AddIngredient<MechanicalPeaceAgreement>(1);
        recipe.AddIngredient<MoonlordPeaceAgreement>(1);
        recipe.AddIngredient<UndeadPeaceAgreement>(1);
        recipe.AddIngredient<FriendCore>(1);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}
