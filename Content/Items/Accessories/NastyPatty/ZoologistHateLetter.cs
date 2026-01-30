using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
public class ZoologistHateLetter : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 4000);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Grants 3hp/s, Light, 25% Crit Chance, and Double Movement Speed"));
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "No longer gain effects from Pets, Light Pets, Summons, or Mounts"));
    }

    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<NastyRegen>().NastyEffect = true;
        player.GetModPlayer<NastyLight>().NastyEffect = true;
        player.GetModPlayer<NastyCrit>().NastyEffect = true;
        player.GetModPlayer<NastyMovement>().NastyEffect = true;
        player.ClearBuff(BuffID.Flamingo);
        player.ClearBuff(BuffID.Rudolph);
        player.ClearBuff(BuffID.WitchBroom);
        player.maxMinions = 0;
        player.ClearBuff(BuffID.BabyBird);
        player.ClearBuff(BuffID.BabySlime);
        player.ClearBuff(BuffID.DeadlySphere);
        player.ClearBuff(BuffID.StormTiger);
        player.ClearBuff(BuffID.Smolstar);
        player.ClearBuff(BuffID.FlinxMinion);
        player.ClearBuff(BuffID.HornetMinion);
        player.ClearBuff(BuffID.ImpMinion);
        player.ClearBuff(BuffID.PirateMinion);
        player.ClearBuff(BuffID.Pygmies);
        player.ClearBuff(BuffID.Ravens);
        player.ClearBuff(BuffID.BatOfLight);
        player.ClearBuff(BuffID.SharknadoMinion);
        player.ClearBuff(BuffID.SpiderMinion);
        player.ClearBuff(BuffID.StardustMinion);
        player.ClearBuff(BuffID.StardustDragonMinion);
        player.ClearBuff(BuffID.EmpressBlade);
        player.ClearBuff(BuffID.TwinEyesMinion);
        player.ClearBuff(BuffID.UFOMinion);
        player.ClearBuff(BuffID.VampireFrog);
        player.ClearBuff(BuffID.BasiliskMount);
        player.ClearBuff(BuffID.BeeMount);
        player.ClearBuff(BuffID.BunnyMount);
        player.ClearBuff(BuffID.CuteFishronMount);
        player.ClearBuff(BuffID.DarkHorseMount);
        player.ClearBuff(BuffID.DarkMageBookMount);
        player.ClearBuff(BuffID.DrillMount);
        player.ClearBuff(BuffID.GolfCartMount);
        player.ClearBuff(BuffID.LavaSharkMount);
        player.ClearBuff(BuffID.MajesticHorseMount);
        player.ClearBuff(BuffID.PaintedHorseMount);
        player.ClearBuff(BuffID.PigronMount);
        player.ClearBuff(BuffID.PirateShipMount);
        player.ClearBuff(BuffID.PogoStickMount);
        player.ClearBuff(BuffID.QueenSlimeMount);
        player.ClearBuff(BuffID.SantankMount);
        player.ClearBuff(BuffID.ScutlixMount);
        player.ClearBuff(BuffID.SlimeMount);
        player.ClearBuff(BuffID.SpookyWoodMount);
        player.ClearBuff(BuffID.TurtleMount);
        player.ClearBuff(BuffID.UFOMount);
        player.ClearBuff(BuffID.UnicornMount);
        player.ClearBuff(BuffID.WallOfFleshGoatMount);
        player.ClearBuff(BuffID.WolfMount);
        player.ClearBuff(BuffID.CrimsonHeart);
        player.ClearBuff(BuffID.FairyBlue);
        player.ClearBuff(BuffID.FairyGreen);
        player.ClearBuff(BuffID.FairyRed);
        player.ClearBuff(BuffID.FairyQueenPet);
        player.ClearBuff(BuffID.PetDD2Ghost);
        player.ClearBuff(BuffID.PumpkingPet);
        player.ClearBuff(BuffID.MagicLantern);
        player.ClearBuff(BuffID.ShadowOrb);
        player.ClearBuff(BuffID.SuspiciousTentacle);
        player.ClearBuff(BuffID.GolemPet);
        player.ClearBuff(BuffID.Wisp);
        player.ClearBuff(BuffID.PetBunny);
        player.ClearBuff(BuffID.PetDD2Dragon);
        player.ClearBuff(BuffID.PetDD2Gato);
        player.ClearBuff(BuffID.PetLizard);
        player.ClearBuff(BuffID.PetParrot);
        player.ClearBuff(BuffID.PetSapling);
        player.ClearBuff(BuffID.PetSpider);
        player.ClearBuff(BuffID.PetTurtle);
        player.ClearBuff(BuffID.BerniePet);
        player.ClearBuff(BuffID.BlueChickenPet);
        player.ClearBuff(BuffID.BrainOfCthulhuPet);
        player.ClearBuff(BuffID.ChesterPet);
        player.ClearBuff(BuffID.DD2BetsyPet);
        player.ClearBuff(BuffID.DD2OgrePet);
        player.ClearBuff(BuffID.DeerclopsPet);
        player.ClearBuff(BuffID.DestroyerPet);
        player.ClearBuff(BuffID.DualSlimePet);
        player.ClearBuff(BuffID.DukeFishronPet);
        player.ClearBuff(BuffID.EaterOfWorldsPet);
        player.ClearBuff(BuffID.EverscreamPet);
        player.ClearBuff(BuffID.EyeOfCthulhuPet);
        player.ClearBuff(BuffID.GlommerPet);
        player.ClearBuff(BuffID.IceQueenPet);
        player.ClearBuff(BuffID.JunimoPet);
        player.ClearBuff(BuffID.KingSlimePet);
        player.ClearBuff(BuffID.LunaticCultistPet);
        player.ClearBuff(BuffID.MartianPet);
        player.ClearBuff(BuffID.MoonLordPet);
        player.ClearBuff(BuffID.PigPet);
        player.ClearBuff(BuffID.PlanteraPet);
        player.ClearBuff(BuffID.QueenSlimePet);
        player.ClearBuff(BuffID.SkeletronPet);
        player.ClearBuff(BuffID.SkeletronPrimePet);
        player.ClearBuff(BuffID.TwinsPet);
        player.ClearBuff(BuffID.BabyDinosaur);
        player.ClearBuff(BuffID.BabyEater);
        player.ClearBuff(BuffID.BabyGrinch);
        player.ClearBuff(BuffID.BabyHornet);
        player.ClearBuff(BuffID.BabyImp);
        player.ClearBuff(BuffID.BabyPenguin);
        player.ClearBuff(BuffID.BabyRedPanda);
        player.ClearBuff(BuffID.BabySkeletronHead);
        player.ClearBuff(BuffID.BabySnowman);
        player.ClearBuff(BuffID.BabyTruffle);
        player.ClearBuff(BuffID.BabyWerewolf);
        player.ClearBuff(BuffID.BlackCat);
        player.ClearBuff(BuffID.CavelingGardener);
        player.ClearBuff(BuffID.CompanionCube);
        player.ClearBuff(BuffID.CursedSapling);
        player.ClearBuff(BuffID.DirtiestBlock);
        player.ClearBuff(BuffID.DynamiteKitten);
        player.ClearBuff(BuffID.UpbeatStar);
        player.ClearBuff(BuffID.EyeballSpring);
        player.ClearBuff(BuffID.FennecFox);
        player.ClearBuff(BuffID.GlitteryButterfly);
        player.ClearBuff(BuffID.LilHarpy);
        player.ClearBuff(BuffID.MiniMinotaur);
        player.ClearBuff(BuffID.Plantero);
        player.ClearBuff(BuffID.Puppy);
        player.ClearBuff(BuffID.ShadowMimic);
        player.ClearBuff(BuffID.SharkPup);
        player.ClearBuff(BuffID.Spiffo);
        player.ClearBuff(BuffID.Squashling);
        player.ClearBuff(BuffID.TikiSpirit);
        player.ClearBuff(BuffID.VoltBunny);
        player.ClearBuff(BuffID.ZephyrFish);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<PetaAutograph>();
        recipe.AddIngredient<CeaseAndDesist>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}