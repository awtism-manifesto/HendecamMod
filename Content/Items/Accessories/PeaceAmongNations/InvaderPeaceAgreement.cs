using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations;

//[AutoloadEquip(EquipType.Beard)]
public class InvaderPeaceAgreement : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Invaders should be friendly");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.npcTypeNoAggro[NPCID.GoblinArcher] = true;
        player.npcTypeNoAggro[NPCID.GoblinPeon] = true;
        player.npcTypeNoAggro[NPCID.GoblinScout] = true;
        player.npcTypeNoAggro[NPCID.GoblinSorcerer] = true;
        player.npcTypeNoAggro[NPCID.GoblinSummoner] = true;
        player.npcTypeNoAggro[NPCID.GoblinThief] = true;
        player.npcTypeNoAggro[NPCID.GoblinWarrior] = true;
        player.npcTypeNoAggro[NPCID.ShadowFlameApparition] = true;
        player.npcTypeNoAggro[NPCID.DD2DrakinT2] = true;
        player.npcTypeNoAggro[NPCID.DD2DrakinT3] = true;
        player.npcTypeNoAggro[NPCID.DD2AttackerTest] = true;
        player.npcTypeNoAggro[NPCID.DD2Bartender] = true;
        player.npcTypeNoAggro[NPCID.DD2Betsy] = true;
        player.npcTypeNoAggro[NPCID.DD2DarkMageT1] = true;
        player.npcTypeNoAggro[NPCID.DD2DarkMageT3] = true;
        player.npcTypeNoAggro[NPCID.DD2EterniaCrystal] = true;
        player.npcTypeNoAggro[NPCID.DD2GoblinBomberT1] = true;
        player.npcTypeNoAggro[NPCID.DD2GoblinBomberT2] = true;
        player.npcTypeNoAggro[NPCID.DD2GoblinBomberT3] = true;
        player.npcTypeNoAggro[NPCID.DD2GoblinT1] = true;
        player.npcTypeNoAggro[NPCID.DD2GoblinT2] = true;
        player.npcTypeNoAggro[NPCID.DD2GoblinT3] = true;
        player.npcTypeNoAggro[NPCID.DD2JavelinstT1] = true;
        player.npcTypeNoAggro[NPCID.DD2JavelinstT2] = true;
        player.npcTypeNoAggro[NPCID.DD2JavelinstT3] = true;
        player.npcTypeNoAggro[NPCID.DD2KoboldFlyerT2] = true;
        player.npcTypeNoAggro[NPCID.DD2KoboldFlyerT3] = true;
        player.npcTypeNoAggro[NPCID.DD2KoboldWalkerT2] = true;
        player.npcTypeNoAggro[NPCID.DD2KoboldWalkerT3] = true;
        player.npcTypeNoAggro[NPCID.DD2LanePortal] = true;
        player.npcTypeNoAggro[NPCID.DD2LightningBugT3] = true;
        player.npcTypeNoAggro[NPCID.DD2OgreT2] = true;
        player.npcTypeNoAggro[NPCID.DD2OgreT3] = true;
        player.npcTypeNoAggro[NPCID.DD2SkeletonT1] = true;
        player.npcTypeNoAggro[NPCID.DD2SkeletonT3] = true;
        player.npcTypeNoAggro[NPCID.DD2WitherBeastT2] = true;
        player.npcTypeNoAggro[NPCID.DD2WitherBeastT3] = true;
        player.npcTypeNoAggro[NPCID.DD2WyvernT1] = true;
        player.npcTypeNoAggro[NPCID.DD2WyvernT2] = true;
        player.npcTypeNoAggro[NPCID.DD2WyvernT3] = true;
        player.npcTypeNoAggro[NPCID.MisterStabby] = true;
        player.npcTypeNoAggro[NPCID.SnowBalla] = true;
        player.npcTypeNoAggro[NPCID.SnowmanGangsta] = true;
        player.npcTypeNoAggro[NPCID.Parrot] = true;
        player.npcTypeNoAggro[NPCID.PirateCaptain] = true;
        player.npcTypeNoAggro[NPCID.PirateCorsair] = true;
        player.npcTypeNoAggro[NPCID.PirateCrossbower] = true;
        player.npcTypeNoAggro[NPCID.PirateDeadeye] = true;
        player.npcTypeNoAggro[NPCID.PirateDeckhand] = true;
        player.npcTypeNoAggro[NPCID.PirateGhost] = true;
        player.npcTypeNoAggro[NPCID.PirateShip] = true;
        player.npcTypeNoAggro[NPCID.PirateShipCannon] = true;
        player.npcTypeNoAggro[NPCID.Mothron] = true;
        player.npcTypeNoAggro[NPCID.MothronEgg] = true;
        player.npcTypeNoAggro[NPCID.MothronSpawn] = true;
        player.npcTypeNoAggro[NPCID.Butcher] = true;
        player.npcTypeNoAggro[NPCID.CreatureFromTheDeep] = true;
        player.npcTypeNoAggro[NPCID.DeadlySphere] = true;
        player.npcTypeNoAggro[NPCID.DrManFly] = true;
        player.npcTypeNoAggro[NPCID.Eyezor] = true;
        player.npcTypeNoAggro[NPCID.Frankenstein] = true;
        player.npcTypeNoAggro[NPCID.Fritz] = true;
        player.npcTypeNoAggro[NPCID.Nailhead] = true;
        player.npcTypeNoAggro[NPCID.Psycho] = true;
        player.npcTypeNoAggro[NPCID.Reaper] = true;
        player.npcTypeNoAggro[NPCID.SwampThing] = true;
        player.npcTypeNoAggro[NPCID.ThePossessed] = true;
        player.npcTypeNoAggro[NPCID.Vampire] = true;
        player.npcTypeNoAggro[NPCID.VampireBat] = true;
        player.npcTypeNoAggro[NPCID.BrainScrambler] = true;
        player.npcTypeNoAggro[NPCID.GigaZapper] = true;
        player.npcTypeNoAggro[NPCID.GrayGrunt] = true;
        player.npcTypeNoAggro[NPCID.MartianDrone] = true;
        player.npcTypeNoAggro[NPCID.MartianEngineer] = true;
        player.npcTypeNoAggro[NPCID.MartianOfficer] = true;
        player.npcTypeNoAggro[NPCID.MartianProbe] = true;
        player.npcTypeNoAggro[NPCID.MartianSaucer] = true;
        player.npcTypeNoAggro[NPCID.MartianSaucerCannon] = true;
        player.npcTypeNoAggro[NPCID.MartianSaucerCore] = true;
        player.npcTypeNoAggro[NPCID.MartianSaucerTurret] = true;
        player.npcTypeNoAggro[NPCID.MartianTurret] = true;
        player.npcTypeNoAggro[NPCID.MartianWalker] = true;
        player.npcTypeNoAggro[NPCID.RayGunner] = true;
        player.npcTypeNoAggro[NPCID.Scutlix] = true;
        player.npcTypeNoAggro[NPCID.ScutlixRider] = true;
        player.npcTypeNoAggro[NPCID.HeadlessHorseman] = true;
        player.npcTypeNoAggro[NPCID.Hellhound] = true;
        player.npcTypeNoAggro[NPCID.Poltergeist] = true;
        player.npcTypeNoAggro[NPCID.Scarecrow1] = true;
        player.npcTypeNoAggro[NPCID.Scarecrow10] = true;
        player.npcTypeNoAggro[NPCID.Scarecrow2] = true;
        player.npcTypeNoAggro[NPCID.Scarecrow3] = true;
        player.npcTypeNoAggro[NPCID.Scarecrow4] = true;
        player.npcTypeNoAggro[NPCID.Scarecrow5] = true;
        player.npcTypeNoAggro[NPCID.Scarecrow6] = true;
        player.npcTypeNoAggro[NPCID.Scarecrow7] = true;
        player.npcTypeNoAggro[NPCID.Scarecrow8] = true;
        player.npcTypeNoAggro[NPCID.Scarecrow9] = true;
        player.npcTypeNoAggro[NPCID.Splinterling] = true;
        player.npcTypeNoAggro[NPCID.Pumpking] = true;
        player.npcTypeNoAggro[NPCID.PumpkingBlade] = true;
        player.npcTypeNoAggro[NPCID.ElfArcher] = true;
        player.npcTypeNoAggro[NPCID.ElfCopter] = true;
        player.npcTypeNoAggro[NPCID.Flocko] = true;
        player.npcTypeNoAggro[NPCID.GingerbreadMan] = true;
        player.npcTypeNoAggro[NPCID.Krampus] = true;
        player.npcTypeNoAggro[NPCID.Nutcracker] = true;
        player.npcTypeNoAggro[NPCID.NutcrackerSpinning] = true;
        player.npcTypeNoAggro[NPCID.PresentMimic] = true;
        player.npcTypeNoAggro[NPCID.Yeti] = true;
        player.npcTypeNoAggro[NPCID.ZombieElf] = true;
        player.npcTypeNoAggro[NPCID.ZombieElfBeard] = true;
        player.npcTypeNoAggro[NPCID.ZombieElfGirl] = true;
        player.npcTypeNoAggro[NPCID.Everscream] = true;
        player.npcTypeNoAggro[NPCID.SantaNK1] = true;
        player.npcTypeNoAggro[NPCID.Everscream] = true;
        player.npcTypeNoAggro[NPCID.IceQueen] = true;
        player.npcTypeNoAggro[NPCID.LunarTowerNebula] = true;
        player.npcTypeNoAggro[NPCID.LunarTowerSolar] = true;
        player.npcTypeNoAggro[NPCID.LunarTowerStardust] = true;
        player.npcTypeNoAggro[NPCID.LunarTowerVortex] = true;
        player.npcTypeNoAggro[NPCID.NebulaBrain] = true;
        player.npcTypeNoAggro[NPCID.NebulaBeast] = true;
        player.npcTypeNoAggro[NPCID.NebulaHeadcrab] = true;
        player.npcTypeNoAggro[NPCID.NebulaSoldier] = true;
        player.npcTypeNoAggro[NPCID.SolarCorite] = true;
        player.npcTypeNoAggro[NPCID.SolarCrawltipedeBody] = true;
        player.npcTypeNoAggro[NPCID.SolarCrawltipedeHead] = true;
        player.npcTypeNoAggro[NPCID.SolarCrawltipedeTail] = true;
        player.npcTypeNoAggro[NPCID.SolarDrakomire] = true;
        player.npcTypeNoAggro[NPCID.SolarDrakomireRider] = true;
        player.npcTypeNoAggro[NPCID.SolarFlare] = true;
        player.npcTypeNoAggro[NPCID.SolarGoop] = true;
        player.npcTypeNoAggro[NPCID.SolarSolenian] = true;
        player.npcTypeNoAggro[NPCID.SolarSpearman] = true;
        player.npcTypeNoAggro[NPCID.SolarSroller] = true;
        player.npcTypeNoAggro[NPCID.StardustCellBig] = true;
        player.npcTypeNoAggro[NPCID.StardustCellSmall] = true;
        player.npcTypeNoAggro[NPCID.StardustJellyfishBig] = true;
        player.npcTypeNoAggro[NPCID.StardustJellyfishSmall] = true;
        player.npcTypeNoAggro[NPCID.StardustSoldier] = true;
        player.npcTypeNoAggro[NPCID.StardustSpiderBig] = true;
        player.npcTypeNoAggro[NPCID.StardustSpiderSmall] = true;
        player.npcTypeNoAggro[NPCID.StardustWormBody] = true;
        player.npcTypeNoAggro[NPCID.StardustWormHead] = true;
        player.npcTypeNoAggro[NPCID.StardustWormTail] = true;
        player.npcTypeNoAggro[NPCID.VortexHornet] = true;
        player.npcTypeNoAggro[NPCID.VortexHornetQueen] = true;
        player.npcTypeNoAggro[NPCID.VortexLarva] = true;
        player.npcTypeNoAggro[NPCID.VortexRifleman] = true;
        player.npcTypeNoAggro[NPCID.VortexSoldier] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.BloodMoonMonolith);
        recipe.AddIngredient(ItemID.ShadowFlameHexDoll);
        recipe.AddIngredient(ItemID.MothronWings);
        recipe.AddIngredient(ItemID.PumpkingTrophy);
        recipe.AddIngredient(ItemID.IceQueenTrophy);
        recipe.AddIngredient(ItemID.MartianSaucerTrophy);
        recipe.AddIngredient(ItemID.BossTrophyBetsy);
        recipe.AddIngredient(ItemID.FlyingDutchmanTrophy);
        recipe.AddIngredient(ItemID.CelestialSigil);
        recipe.AddIngredient<FriendCore>();
        recipe.AddIngredient<Paper>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}