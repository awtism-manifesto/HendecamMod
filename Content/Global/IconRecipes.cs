using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Furniture;
using HendecamMod.Content.Items.Icons;
using HendecamMod.Content.Items;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Items.Accessories.Rampart;

namespace HendecamMod.Content.Global;

public class IconRecipes : ModSystem
{
    public override void AddRecipes()
    {
        Recipe A40 = Recipe.Create(ItemID.SlimySaddle);
        A40.AddIngredient<Items.Icons.KingSlimeIcon>();
        A40.Register();

        Recipe A41 = Recipe.Create(ItemID.NinjaHood);
        A41.AddIngredient<Items.Icons.KingSlimeIcon>();
        A41.Register();

        Recipe A42 = Recipe.Create(ItemID.NinjaShirt);
        A42.AddIngredient<Items.Icons.KingSlimeIcon>();
        A42.Register();

        Recipe A43 = Recipe.Create(ItemID.NinjaPants);
        A43.AddIngredient<Items.Icons.KingSlimeIcon>();
        A43.Register();

        Recipe A44 = Recipe.Create(ItemID.SlimeHook);
        A44.AddIngredient<Items.Icons.KingSlimeIcon>();
        A44.Register();

        Recipe A45 = Recipe.Create(ItemID.SlimeGun);
        A45.AddIngredient<Items.Icons.KingSlimeIcon>();
        A45.Register();

        Recipe A46 = Recipe.Create(ItemID.KingSlimeMask);
        A46.AddIngredient<Items.Icons.KingSlimeIcon>();
        A46.Register();

        Recipe AE46 = Recipe.Create(ItemID.Trimarang);
        AE46.AddIngredient<Items.Icons.KingSlimeIcon>();
        AE46.Register();

        Recipe LickingMyBalls67 = Recipe.Create(ModContent.ItemType<SuperSamuraiSlicer>());
        LickingMyBalls67.AddIngredient<KingSlimeIcon>();
        LickingMyBalls67.Register();

        Recipe LickingMyBalls131 = Recipe.Create(ModContent.ItemType<Bergentrucking>());
        LickingMyBalls131.AddIngredient<KingSlimeIcon>();
        LickingMyBalls131.Register();

        Recipe AEE46 = Recipe.Create(ItemID.Katana);
        AEE46.AddIngredient<Items.Icons.KingSlimeIcon>();
        AEE46.Register();

        Recipe A47 = Recipe.Create(ItemID.KingSlimeTrophy);
        A47.AddIngredient<Items.Icons.KingSlimeIcon>();
        A47.Register();

        Recipe A48 = Recipe.Create(ItemID.Binoculars);
        A48.AddIngredient<Items.Icons.EyeOfCthulhuIcon>();
        A48.Register();

        Recipe A49 = Recipe.Create(ItemID.EyeMask);
        A49.AddIngredient<Items.Icons.EyeOfCthulhuIcon>();
        A49.Register();

        Recipe LickingMyBalls1231 = Recipe.Create(ModContent.ItemType<EyeRifle>());
        LickingMyBalls1231.AddIngredient<EyeOfCthulhuIcon>();
        LickingMyBalls1231.Register();

        Recipe LickingMyBalls12341 = Recipe.Create(ModContent.ItemType<EyePoker>());
        LickingMyBalls12341.AddIngredient<EyeOfCthulhuIcon>();
        LickingMyBalls12341.Register();

        Recipe A50 = Recipe.Create(ItemID.EyeofCthulhuTrophy);
        A50.AddIngredient<Items.Icons.EyeOfCthulhuIcon>();
        A50.Register();

        Recipe Ass50 = Recipe.Create(ItemID.VampireFrogStaff);
        Ass50.AddIngredient<Items.Icons.EyeOfCthulhuIcon>();
        Ass50.Register();

        Recipe Ass505 = Recipe.Create(ItemID.BloodRainBow);
        Ass505.AddIngredient<Items.Icons.EyeOfCthulhuIcon>();
        Ass505.Register();

        Recipe A51 = Recipe.Create(ItemID.EatersBone);
        A51.AddIngredient<Items.Icons.EaterOfWorldsIcon>();
        A51.Register();

        Recipe A52 = Recipe.Create(ItemID.EaterMask);
        A52.AddIngredient<Items.Icons.EaterOfWorldsIcon>();
        A52.Register();

        Recipe A53 = Recipe.Create(ItemID.EaterofWorldsTrophy);
        A53.AddIngredient<Items.Icons.EaterOfWorldsIcon>();
        A53.Register();

        Recipe A54 = Recipe.Create(ItemID.BoneRattle);
        A54.AddIngredient<Items.Icons.BrainOfCthulhuIcon>();
        A54.Register();

        Recipe LickingMyBalls123415 = Recipe.Create(ModContent.ItemType<GoodGrades>());
        LickingMyBalls123415.AddIngredient<BrainOfCthulhuIcon>();
        LickingMyBalls123415.Register();

        Recipe A55 = Recipe.Create(ItemID.BrainMask);
        A55.AddIngredient<Items.Icons.BrainOfCthulhuIcon>();
        A55.Register();

        Recipe A56 = Recipe.Create(ItemID.BrainofCthulhuTrophy);
        A56.AddIngredient<Items.Icons.BrainOfCthulhuIcon>();
        A56.Register();

        Recipe A57 = Recipe.Create(ItemID.BeeGun);
        A57.AddIngredient<Items.Icons.QueenBeeIcon>();
        A57.Register();

        Recipe A58 = Recipe.Create(ItemID.BeeKeeper);
        A58.AddIngredient<Items.Icons.QueenBeeIcon>();
        A58.Register();

        Recipe A59 = Recipe.Create(ItemID.BeesKnees);
        A59.AddIngredient<Items.Icons.QueenBeeIcon>();
        A59.Register();

        Recipe A60 = Recipe.Create(ItemID.HiveWand);
        A60.AddIngredient<Items.Icons.QueenBeeIcon>();
        A60.Register();

        Recipe A61 = Recipe.Create(ItemID.BeeHat);
        A61.AddIngredient<Items.Icons.QueenBeeIcon>();
        A61.Register();

        Recipe A62 = Recipe.Create(ItemID.BeeShirt);
        A62.AddIngredient<Items.Icons.QueenBeeIcon>();
        A62.Register();

        Recipe A63 = Recipe.Create(ItemID.BeePants);
        A63.AddIngredient<Items.Icons.QueenBeeIcon>();
        A63.Register();

        Recipe A64 = Recipe.Create(ItemID.HoneyComb);
        A64.AddIngredient<Items.Icons.QueenBeeIcon>();
        A64.Register();

        Recipe A65 = Recipe.Create(ItemID.Nectar);
        A65.AddIngredient<Items.Icons.QueenBeeIcon>();
        A65.Register();

        Recipe A66 = Recipe.Create(ItemID.HoneyedGoggles);
        A66.AddIngredient<Items.Icons.QueenBeeIcon>();
        A66.Register();

        Recipe A67 = Recipe.Create(ItemID.Beenade, 125);
        A67.AddIngredient<Items.Icons.QueenBeeIcon>();
        A67.Register();

        Recipe LickingMyBalls313 = Recipe.Create(ModContent.ItemType<PocketBees>());
        LickingMyBalls313.AddIngredient<QueenBeeIcon>();
        LickingMyBalls313.Register();

        Recipe A68 = Recipe.Create(ItemID.BeeMask);
        A68.AddIngredient<Items.Icons.QueenBeeIcon>();
        A68.Register();

        Recipe A69 = Recipe.Create(ItemID.QueenBeeTrophy);
        A69.AddIngredient<Items.Icons.QueenBeeIcon>();
        A69.Register();

        Recipe A70 = Recipe.Create(ItemID.SkeletronMask);
        A70.AddIngredient<Items.Icons.SkeletronIcon>();
        A70.Register();

        Recipe A71 = Recipe.Create(ItemID.SkeletronHand);
        A71.AddIngredient<Items.Icons.SkeletronIcon>();
        A71.Register();

        Recipe A72 = Recipe.Create(ItemID.BookofSkulls);
        A72.AddIngredient<Items.Icons.SkeletronIcon>();
        A72.Register();

        Recipe A73 = Recipe.Create(ItemID.SkeletronTrophy);
        A73.AddIngredient<Items.Icons.SkeletronIcon>();
        A73.Register();

        Recipe LickingMyBalls31 = Recipe.Create(ModContent.ItemType<SkeletonKey>());
        LickingMyBalls31.AddIngredient<Items.Icons.SkeletronIcon>();
        LickingMyBalls31.Register();

        Recipe A74 = Recipe.Create(ItemID.ChippysCouch);
        A74.AddIngredient<Items.Icons.SkeletronIcon>();
        A74.Register();

        Recipe A75 = Recipe.Create(ItemID.ChesterPetItem);
        A75.AddIngredient<Items.Icons.DeerclopsIcon>();
        A75.Register();

        Recipe A76 = Recipe.Create(ItemID.Eyebrella);
        A76.AddIngredient<Items.Icons.DeerclopsIcon>();
        A76.Register();

        Recipe A77 = Recipe.Create(ItemID.DizzyHat);
        A77.AddIngredient<Items.Icons.DeerclopsIcon>();
        A77.Register();

        Recipe A78 = Recipe.Create(ItemID.PewMaticHorn);
        A78.AddIngredient<Items.Icons.DeerclopsIcon>();
        A78.Register();

        Recipe A79 = Recipe.Create(ItemID.WeatherPain);
        A79.AddIngredient<Items.Icons.DeerclopsIcon>();
        A79.Register();

        Recipe A80 = Recipe.Create(ItemID.LucyTheAxe);
        A80.AddIngredient<Items.Icons.DeerclopsIcon>();
        A80.Register();

        Recipe A81 = Recipe.Create(ItemID.DeerclopsMask);
        A81.AddIngredient<Items.Icons.DeerclopsIcon>();
        A81.Register();

        Recipe LickingMyBalls21 = Recipe.Create(ModContent.ItemType<FlinxsFurblade>());
        LickingMyBalls21.AddIngredient<Items.Icons.DeerclopsIcon>();
        LickingMyBalls21.Register();

        Recipe LickingMyBalls1 = Recipe.Create(ModContent.ItemType<CaribousCatastrophe>());
        LickingMyBalls1.AddIngredient<Items.Icons.DeerclopsIcon>();
        LickingMyBalls1.Register();

        Recipe A82 = Recipe.Create(ItemID.DeerclopsTrophy);
        A82.AddIngredient<Items.Icons.DeerclopsIcon>();
        A82.Register();

        Recipe A83 = Recipe.Create(ItemID.BreakerBlade);
        A83.AddIngredient<Items.Icons.WallOfFleshIcon>();
        A83.Register();

        Recipe A84 = Recipe.Create(ItemID.ClockworkAssaultRifle);
        A84.AddIngredient<Items.Icons.WallOfFleshIcon>();
        A84.Register();

        Recipe A85 = Recipe.Create(ItemID.LaserRifle);
        A85.AddIngredient<Items.Icons.WallOfFleshIcon>();
        A85.Register();

        Recipe A86 = Recipe.Create(ItemID.FireWhip);
        A86.AddIngredient<Items.Icons.WallOfFleshIcon>();
        A86.Register();

        Recipe A87 = Recipe.Create(ItemID.WarriorEmblem);
        A87.AddIngredient<Items.Icons.WallOfFleshIcon>();
        A87.Register();

        Recipe A88 = Recipe.Create(ItemID.RangerEmblem);
        A88.AddIngredient<Items.Icons.WallOfFleshIcon>();
        A88.Register();

        Recipe A89 = Recipe.Create(ItemID.SorcererEmblem);
        A89.AddIngredient<Items.Icons.WallOfFleshIcon>();
        A89.Register();

        Recipe A90 = Recipe.Create(ItemID.SummonerEmblem);
        A90.AddIngredient<Items.Icons.WallOfFleshIcon>();
        A90.Register();

        Recipe LickingMyBalls231 = Recipe.Create(ModContent.ItemType<StupidEmblem>());
        LickingMyBalls231.AddIngredient<WallOfFleshIcon>();
        LickingMyBalls231.Register();

        Recipe LickingMyBalls2431 = Recipe.Create(ModContent.ItemType<StalingradSpewer>());
        LickingMyBalls2431.AddIngredient<WallOfFleshIcon>();
        LickingMyBalls2431.Register();


        Recipe A91 = Recipe.Create(ItemID.FleshMask);
        A91.AddIngredient<Items.Icons.WallOfFleshIcon>();
        A91.Register();

        Recipe A92 = Recipe.Create(ItemID.WallofFleshTrophy);
        A92.AddIngredient<Items.Icons.WallOfFleshIcon>();
        A92.Register();

        Recipe A93 = Recipe.Create(ItemID.CrystalNinjaHelmet);
        A93.AddIngredient<Items.Icons.QueenSlimeIcon>();
        A93.Register();

        Recipe A94 = Recipe.Create(ItemID.CrystalNinjaChestplate);
        A94.AddIngredient<Items.Icons.QueenSlimeIcon>();
        A94.Register();

        Recipe A95 = Recipe.Create(ItemID.CrystalNinjaLeggings);
        A95.AddIngredient<Items.Icons.QueenSlimeIcon>();
        A95.Register();

        Recipe LickingMyBalls26431 = Recipe.Create(ModContent.ItemType<MaximGelgun>());
        LickingMyBalls26431.AddIngredient<QueenSlimeIcon>();
        LickingMyBalls26431.Register();

        Recipe A96 = Recipe.Create(ItemID.Smolstar);
        A96.AddIngredient<Items.Icons.QueenSlimeIcon>();
        A96.Register();

        Recipe A97 = Recipe.Create(ItemID.QueenSlimeMountSaddle);
        A97.AddIngredient<Items.Icons.QueenSlimeIcon>();
        A97.Register();

        Recipe A98 = Recipe.Create(ItemID.QueenSlimeHook);
        A98.AddIngredient<Items.Icons.QueenSlimeIcon>();
        A98.Register();

        Recipe A99 = Recipe.Create(ItemID.QueenSlimeMask);
        A99.AddIngredient<Items.Icons.QueenSlimeIcon>();
        A99.Register();

        Recipe A100 = Recipe.Create(ItemID.QueenSlimeTrophy);
        A100.AddIngredient<Items.Icons.QueenSlimeIcon>();
        A100.Register();

        Recipe A101 = Recipe.Create(ItemID.TwinMask);
        A101.AddIngredient<Items.Icons.TheTwinsIcon>();
        A101.Register();

        Recipe LickingMyBalls263431 = Recipe.Create(ModContent.ItemType<ElementalFlameCore>());
        LickingMyBalls263431.AddIngredient<TheTwinsIcon>();
        LickingMyBalls263431.Register();

        Recipe A102 = Recipe.Create(ItemID.RetinazerTrophy);
        A102.AddIngredient<Items.Icons.TheTwinsIcon>();
        A102.Register();

        Recipe A103 = Recipe.Create(ItemID.SpazmatismTrophy);
        A103.AddIngredient<Items.Icons.TheTwinsIcon>();
        A103.Register();

        Recipe A104 = Recipe.Create(ItemID.DestroyerMask);
        A104.AddIngredient<Items.Icons.TheDestroyerIcon>();
        A104.Register();

        Recipe A105 = Recipe.Create(ItemID.DestroyerTrophy);
        A105.AddIngredient<Items.Icons.TheDestroyerIcon>();
        A105.Register();

        Recipe A106 = Recipe.Create(ItemID.SkeletronPrimeMask);
        A106.AddIngredient<Items.Icons.SkeletronPrimeIcon>();
        A106.Register();

        Recipe A107 = Recipe.Create(ItemID.SkeletronPrimeTrophy);
        A107.AddIngredient<Items.Icons.SkeletronPrimeIcon>();
        A107.Register();

        Recipe A108 = Recipe.Create(ItemID.VenusMagnum);
        A108.AddIngredient<Items.Icons.PlanteraIcon>();
        A108.Register();

        Recipe A109 = Recipe.Create(ItemID.NettleBurst);
        A109.AddIngredient<Items.Icons.PlanteraIcon>();
        A109.Register();

        Recipe A110 = Recipe.Create(ItemID.LeafBlower);
        A110.AddIngredient<Items.Icons.PlanteraIcon>();
        A110.Register();

        Recipe A111 = Recipe.Create(ItemID.FlowerPow);
        A111.AddIngredient<Items.Icons.PlanteraIcon>();
        A111.Register();

        Recipe A112 = Recipe.Create(ItemID.WaspGun);
        A112.AddIngredient<Items.Icons.PlanteraIcon>();
        A112.Register();

        Recipe A113 = Recipe.Create(ItemID.Seedler);
        A113.AddIngredient<Items.Icons.PlanteraIcon>();
        A113.Register();

        Recipe A114 = Recipe.Create(ItemID.PygmyStaff);
        A114.AddIngredient<Items.Icons.PlanteraIcon>();
        A114.Register();

        Recipe LickingMyBalls1263431 = Recipe.Create(ModContent.ItemType<SeedBomber>());
        LickingMyBalls1263431.AddIngredient<PlanteraIcon>();
        LickingMyBalls1263431.Register();

        Recipe LickingMyBalls12634311 = Recipe.Create(ModContent.ItemType<OtherworldlySixPack>());
        LickingMyBalls12634311.AddIngredient<PlanteraIcon>();
        LickingMyBalls12634311.Register();

        Recipe A115 = Recipe.Create(ItemID.ThornHook);
        A115.AddIngredient<Items.Icons.PlanteraIcon>();
        A115.Register();

        Recipe A116 = Recipe.Create(ItemID.TheAxe);
        A116.AddIngredient<Items.Icons.PlanteraIcon>();
        A116.Register();

        Recipe A117 = Recipe.Create(ItemID.Seedling);
        A117.AddIngredient<Items.Icons.PlanteraIcon>();
        A117.Register();

        Recipe A118 = Recipe.Create(ItemID.PlanteraMask);
        A118.AddIngredient<Items.Icons.PlanteraIcon>();
        A118.Register();

        Recipe A119 = Recipe.Create(ItemID.PlanteraTrophy);
        A119.AddIngredient<Items.Icons.PlanteraIcon>();
        A119.Register();

        Recipe A120 = Recipe.Create(ItemID.Picksaw);
        A120.AddIngredient<Items.Icons.GolemIcon>();
        A120.Register();

        Recipe A121 = Recipe.Create(ItemID.Stynger);
        A121.AddIngredient<Items.Icons.GolemIcon>();
        A121.Register();

        Recipe A122 = Recipe.Create(ItemID.StyngerBolt, 100);
        A122.AddIngredient<Items.Icons.GolemIcon>();
        A122.Register();

        Recipe A123 = Recipe.Create(ItemID.PossessedHatchet);
        A123.AddIngredient<Items.Icons.GolemIcon>();
        A123.Register();

        Recipe A124 = Recipe.Create(ItemID.SunStone);
        A124.AddIngredient<Items.Icons.GolemIcon>();
        A124.Register();

        Recipe A125 = Recipe.Create(ItemID.EyeoftheGolem);
        A125.AddIngredient<Items.Icons.GolemIcon>();
        A125.Register();

        Recipe A126 = Recipe.Create(ItemID.HeatRay);
        A126.AddIngredient<Items.Icons.GolemIcon>();
        A126.Register();

        Recipe A127 = Recipe.Create(ItemID.StaffofEarth);
        A127.AddIngredient<Items.Icons.GolemIcon>();
        A127.Register();

        Recipe A128 = Recipe.Create(ItemID.GolemFist);
        A128.AddIngredient<Items.Icons.GolemIcon>();
        A128.Register();

        Recipe A129 = Recipe.Create(ItemID.GolemMask);
        A129.AddIngredient<Items.Icons.GolemIcon>();
        A129.Register();

        Recipe A130 = Recipe.Create(ItemID.GolemTrophy);
        A130.AddIngredient<Items.Icons.GolemIcon>();
        A130.Register();

        Recipe A131 = Recipe.Create(ItemID.FairyQueenMagicItem);
        A131.AddIngredient<Items.Icons.EmpressOfLightIcon>();
        A131.Register();

        Recipe A132 = Recipe.Create(ItemID.PiercingStarlight);
        A132.AddIngredient<Items.Icons.EmpressOfLightIcon>();
        A132.Register();

        Recipe A133 = Recipe.Create(ItemID.RainbowWhip);
        A133.AddIngredient<Items.Icons.EmpressOfLightIcon>();
        A133.Register();

        Recipe A134 = Recipe.Create(ItemID.FairyQueenRangedItem);
        A134.AddIngredient<Items.Icons.EmpressOfLightIcon>();
        A134.Register();

        Recipe A135 = Recipe.Create(ItemID.RainbowWings);
        A135.AddIngredient<Items.Icons.EmpressOfLightIcon>();
        A135.Register();

        Recipe A136 = Recipe.Create(ItemID.HallowBossDye);
        A136.AddIngredient<Items.Icons.EmpressOfLightIcon>();
        A136.Register();

        Recipe A137 = Recipe.Create(ItemID.SparkleGuitar);
        A137.AddIngredient<Items.Icons.EmpressOfLightIcon>();
        A137.Register();

        Recipe A138 = Recipe.Create(ItemID.RainbowCursor);
        A138.AddIngredient<Items.Icons.EmpressOfLightIcon>();
        A138.Register();

        Recipe A139 = Recipe.Create(ItemID.FairyQueenMask);
        A139.AddIngredient<Items.Icons.EmpressOfLightIcon>();
        A139.Register();

        Recipe A140 = Recipe.Create(ItemID.FairyQueenTrophy);
        A140.AddIngredient<Items.Icons.EmpressOfLightIcon>();
        A140.Register();

        Recipe A141 = Recipe.Create(ItemID.BubbleGun);
        A141.AddIngredient<Items.Icons.DukeFishronIcon>();
        A141.Register();

        Recipe A142 = Recipe.Create(ItemID.Flairon);
        A142.AddIngredient<Items.Icons.DukeFishronIcon>();
        A142.Register();

        Recipe A143 = Recipe.Create(ItemID.RazorbladeTyphoon);
        A143.AddIngredient<Items.Icons.DukeFishronIcon>();
        A143.Register();

        Recipe A144 = Recipe.Create(ItemID.TempestStaff);
        A144.AddIngredient<Items.Icons.DukeFishronIcon>();
        A144.Register();

        Recipe A145 = Recipe.Create(ItemID.Tsunami);
        A145.AddIngredient<Items.Icons.DukeFishronIcon>();
        A145.Register();

        Recipe A146 = Recipe.Create(ItemID.FishronWings);
        A146.AddIngredient<Items.Icons.DukeFishronIcon>();
        A146.Register();

        Recipe A147 = Recipe.Create(ItemID.DukeFishronMask);
        A147.AddIngredient<Items.Icons.DukeFishronIcon>();
        A147.Register();

        Recipe A148 = Recipe.Create(ItemID.DukeFishronTrophy);
        A148.AddIngredient<Items.Icons.DukeFishronIcon>();
        A148.Register();

        Recipe A149 = Recipe.Create(ItemID.BossMaskCultist);
        A149.AddIngredient<Items.Icons.LunaticCultistIcon>();
        A149.Register();

        Recipe A150 = Recipe.Create(ItemID.AncientCultistTrophy);
        A150.AddIngredient<Items.Icons.LunaticCultistIcon>();
        A150.Register();

        Recipe A151 = Recipe.Create(ItemID.Meowmere);
        A151.AddIngredient<Items.Icons.MoonLordIcon>();
        A151.Register();

        Recipe A152 = Recipe.Create(ItemID.Terrarian);
        A152.AddIngredient<Items.Icons.MoonLordIcon>();
        A152.Register();

        Recipe A153 = Recipe.Create(ItemID.StarWrath);
        A153.AddIngredient<Items.Icons.MoonLordIcon>();
        A153.Register();

        Recipe A154 = Recipe.Create(ItemID.SDMG);
        A154.AddIngredient<Items.Icons.MoonLordIcon>();
        A154.Register();

        Recipe A155 = Recipe.Create(ItemID.Celeb2);
        A155.AddIngredient<Items.Icons.MoonLordIcon>();
        A155.Register();

        Recipe A156 = Recipe.Create(ItemID.LastPrism);
        A156.AddIngredient<Items.Icons.MoonLordIcon>();
        A156.Register();

        Recipe A157 = Recipe.Create(ItemID.LunarFlareBook);
        A157.AddIngredient<Items.Icons.MoonLordIcon>();
        A157.Register();

        Recipe A158 = Recipe.Create(ItemID.RainbowCrystalStaff);
        A158.AddIngredient<Items.Icons.MoonLordIcon>();
        A158.Register();

        Recipe A159 = Recipe.Create(ItemID.MoonlordTurretStaff);
        A159.AddIngredient<Items.Icons.MoonLordIcon>();
        A159.Register();


        Recipe aaaaaaaaaaaaa2 = Recipe.Create(ModContent.ItemType<TheMoon>());
        aaaaaaaaaaaaa2.AddIngredient<MoonLordIcon>();
        aaaaaaaaaaaaa2.Register();
        Recipe aaaaaaaaaaaaa22 = Recipe.Create(ModContent.ItemType<AmalgamatedFragment>());
        aaaaaaaaaaaaa22.AddIngredient<MoonLordIcon>();
        aaaaaaaaaaaaa22.Register();
        Recipe aaaaaaaaaaaaa222 = Recipe.Create(ModContent.ItemType<LeechRepellant>());
        aaaaaaaaaaaaa222.AddIngredient<MoonLordIcon>();
        aaaaaaaaaaaaa222.Register();

        Recipe A160 = Recipe.Create(ItemID.BossMaskMoonlord);
        A160.AddIngredient<Items.Icons.MoonLordIcon>();
        A160.Register();

        Recipe A161 = Recipe.Create(ItemID.MoonLordTrophy);
        A161.AddIngredient<Items.Icons.MoonLordIcon>();
        A161.Register();

        Recipe Balls0 = Recipe.Create(ItemID.SquireShield);
        Balls0.AddIngredient<Items.Icons.DarkMageIcon>();
        Balls0.Register();

        Recipe Balls1 = Recipe.Create(ItemID.ApprenticeScarf);
        Balls1.AddIngredient<Items.Icons.DarkMageIcon>();
        Balls1.Register();

        Recipe Balls2 = Recipe.Create(ItemID.WarTable);
        Balls2.AddIngredient<Items.Icons.DarkMageIcon>();
        Balls2.Register();

        Recipe Balls3 = Recipe.Create(ItemID.SquireShield);
        Balls3.AddIngredient<Items.Icons.DarkMageIcon>();
        Balls3.Register();

        Recipe aaaaaaaaaaaaa = Recipe.Create(ModContent.ItemType<ArcaneDartgun>());
        aaaaaaaaaaaaa.AddIngredient<DarkMageIcon>();
        aaaaaaaaaaaaa.Register();

        Recipe Balls4 = Recipe.Create(ItemID.DD2PetDragon);
        Balls4.AddIngredient<Items.Icons.DarkMageIcon>();
        Balls4.Register();

        Recipe Balls5 = Recipe.Create(ItemID.DD2PetGato);
        Balls5.AddIngredient<Items.Icons.DarkMageIcon>();
        Balls5.Register();

        Recipe Balls6 = Recipe.Create(ItemID.BossMaskDarkMage);
        Balls6.AddIngredient<Items.Icons.DarkMageIcon>();
        Balls6.Register();

        Recipe Balls7 = Recipe.Create(ItemID.BossTrophyDarkmage);
        Balls7.AddIngredient<Items.Icons.DarkMageIcon>();
        Balls7.Register();

        Recipe Balls8 = Recipe.Create(ItemID.HuntressBuckler);
        Balls8.AddIngredient<Items.Icons.OgreIcon>();
        Balls8.Register();

        Recipe Balls9 = Recipe.Create(ItemID.MonkBelt);
        Balls9.AddIngredient<Items.Icons.OgreIcon>();
        Balls9.Register();

        Recipe BallsQ = Recipe.Create(ItemID.BookStaff);
        BallsQ.AddIngredient<Items.Icons.OgreIcon>();
        BallsQ.Register();

        Recipe BallsW = Recipe.Create(ItemID.DD2PhoenixBow);
        BallsW.AddIngredient<Items.Icons.OgreIcon>();
        BallsW.Register();

        Recipe BallsE = Recipe.Create(ItemID.DD2SquireDemonSword);
        BallsE.AddIngredient<Items.Icons.OgreIcon>();
        BallsE.Register();

        Recipe BallsR = Recipe.Create(ItemID.MonkStaffT1);
        BallsR.AddIngredient<Items.Icons.OgreIcon>();
        BallsR.Register();

        Recipe BallsT = Recipe.Create(ItemID.MonkStaffT2);
        BallsT.AddIngredient<Items.Icons.OgreIcon>();
        BallsT.Register();

        Recipe eaaaaaaaaaaaaab = Recipe.Create(ModContent.ItemType<TrenboloneAcetate>());
        eaaaaaaaaaaaaab.AddIngredient<OgreIcon>();
        eaaaaaaaaaaaaab.Register();

        Recipe BallsY = Recipe.Create(ItemID.DD2PetGhost);
        BallsY.AddIngredient<Items.Icons.OgreIcon>();
        BallsY.Register();

        Recipe BallsI = Recipe.Create(ItemID.BossTrophyOgre);
        BallsI.AddIngredient<Items.Icons.OgreIcon>();
        BallsI.Register();

        Recipe BallsO = Recipe.Create(ItemID.DD2BetsyBow);
        BallsO.AddIngredient<Items.Icons.BetsyIcon>();
        BallsO.Register();

        Recipe BallsP = Recipe.Create(ItemID.MonkStaffT3);
        BallsP.AddIngredient<Items.Icons.BetsyIcon>();
        BallsP.Register();

        Recipe eaaaaaaaaaaaaa = Recipe.Create(ModContent.ItemType<PhotonShotgun>());
        eaaaaaaaaaaaaa.AddIngredient<BetsyIcon>();
        eaaaaaaaaaaaaa.Register();

        Recipe BallsA = Recipe.Create(ItemID.ApprenticeStaffT3);
        BallsA.AddIngredient<Items.Icons.BetsyIcon>();
        BallsA.Register();

        Recipe BallsS = Recipe.Create(ItemID.DD2SquireBetsySword);
        BallsS.AddIngredient<Items.Icons.BetsyIcon>();
        BallsS.Register();

        Recipe BallsD = Recipe.Create(ItemID.BetsyWings);
        BallsD.AddIngredient<Items.Icons.BetsyIcon>();
        BallsD.Register();

        Recipe BallsF = Recipe.Create(ItemID.BossMaskBetsy);
        BallsF.AddIngredient<Items.Icons.BetsyIcon>();
        BallsF.Register();

        Recipe BallsG = Recipe.Create(ItemID.BossTrophyBetsy);
        BallsG.AddIngredient<Items.Icons.BetsyIcon>();
        BallsG.Register();

        Recipe BallsH = Recipe.Create(ItemID.CoinGun);
        BallsH.AddIngredient<Items.Icons.FlyingDutchmanIcon>();
        BallsH.Register();

        Recipe BallsJ = Recipe.Create(ItemID.LuckyCoin);
        BallsJ.AddIngredient<Items.Icons.FlyingDutchmanIcon>();
        BallsJ.Register();

        Recipe beaaaaaaaaaaaaab = Recipe.Create(ModContent.ItemType<BigBuddy>());
        beaaaaaaaaaaaaab.AddIngredient<FlyingDutchmanIcon>();
        beaaaaaaaaaaaaab.Register();

        Recipe tbeaaaaaaaaaaaaab = Recipe.Create(ModContent.ItemType<Bundlebuss>());
        tbeaaaaaaaaaaaaab.AddIngredient<FlyingDutchmanIcon>();
        tbeaaaaaaaaaaaaab.Register();

        Recipe BallsK = Recipe.Create(ItemID.DiscountCard);
        BallsK.AddIngredient<Items.Icons.FlyingDutchmanIcon>();
        BallsK.Register();

        Recipe BallsL = Recipe.Create(ItemID.PirateMinecart);
        BallsL.AddIngredient<Items.Icons.FlyingDutchmanIcon>();
        BallsL.Register();

        Recipe BallsZ = Recipe.Create(ItemID.Cutlass);
        BallsZ.AddIngredient<Items.Icons.FlyingDutchmanIcon>();
        BallsZ.Register();

        Recipe BallsX = Recipe.Create(ItemID.FlyingDutchmanTrophy);
        BallsX.AddIngredient<Items.Icons.FlyingDutchmanIcon>();
        BallsX.Register();

        Recipe BallsC = Recipe.Create(ItemID.SpookyHook);
        BallsC.AddIngredient<Items.Icons.MouringWoodIcon>();
        BallsC.Register();

        Recipe BallsV = Recipe.Create(ItemID.SpookyTwig);
        BallsV.AddIngredient<Items.Icons.MouringWoodIcon>();
        BallsV.Register();

        Recipe BallsB = Recipe.Create(ItemID.StakeLauncher);
        BallsB.AddIngredient<Items.Icons.MouringWoodIcon>();
        BallsB.Register();

        Recipe BallsN = Recipe.Create(ItemID.CursedSapling);
        BallsN.AddIngredient<Items.Icons.MouringWoodIcon>();
        BallsN.Register();

        Recipe BallsM = Recipe.Create(ItemID.NecromanticScroll);
        BallsM.AddIngredient<Items.Icons.MouringWoodIcon>();
        BallsM.Register();

        Recipe BallsAA = Recipe.Create(ItemID.MourningWoodTrophy);
        BallsAA.AddIngredient<Items.Icons.MouringWoodIcon>();
        BallsAA.Register();

        Recipe Ballfart = Recipe.Create(ItemID.CandyCornRifle);
        Ballfart.AddIngredient<Items.Icons.PumpkingIcon>();
        Ballfart.Register();

        Recipe Ballfarting = Recipe.Create(ItemID.JackOLanternLauncher);
        Ballfarting.AddIngredient<Items.Icons.PumpkingIcon>();
        Ballfarting.Register();

        Recipe Ballfart1 = Recipe.Create(ItemID.TheHorsemansBlade);
        Ballfart1.AddIngredient<Items.Icons.PumpkingIcon>();
        Ballfart1.Register();

        Recipe Ballfart2 = Recipe.Create(ItemID.BatScepter);
        Ballfart2.AddIngredient<Items.Icons.PumpkingIcon>();
        Ballfart2.Register();

        Recipe Ballfart3 = Recipe.Create(ItemID.RavenStaff);
        Ballfart3.AddIngredient<Items.Icons.PumpkingIcon>();
        Ballfart3.Register();

        Recipe Ballfart4 = Recipe.Create(ItemID.ScytheWhip);
        Ballfart4.AddIngredient<Items.Icons.PumpkingIcon>();
        Ballfart4.Register();

        Recipe Riverfuckingdied = Recipe.Create(ItemID.PumpkingTrophy);
        Riverfuckingdied.AddIngredient<Items.Icons.PumpkingIcon>();
        Riverfuckingdied.Register();

        Recipe Massivejewtits = Recipe.Create(ItemID.ChristmasTreeSword);
        Massivejewtits.AddIngredient<Items.Icons.EverscreamIcon>();
        Massivejewtits.Register();

        Recipe Ballfartass = Recipe.Create(ItemID.ChristmasHook);
        Ballfartass.AddIngredient<Items.Icons.EverscreamIcon>();
        Ballfartass.Register();

        Recipe laserpiss = Recipe.Create(ItemID.Razorpine);
        laserpiss.AddIngredient<Items.Icons.EverscreamIcon>();
        laserpiss.Register();

        Recipe nineleven = Recipe.Create(ItemID.FestiveWings);
        nineleven.AddIngredient<Items.Icons.EverscreamIcon>();
        nineleven.Register();

        Recipe imgay = Recipe.Create(ItemID.EverscreamTrophy);
        imgay.AddIngredient<Items.Icons.EverscreamIcon>();
        imgay.Register();

        Recipe Alpinehatestheelfmelter = Recipe.Create(ItemID.ElfMelter);
        Alpinehatestheelfmelter.AddIngredient<Items.Icons.SantankIcon>();
        Alpinehatestheelfmelter.Register();

        Recipe gattlinggun = Recipe.Create(ItemID.ChainGun);
        gattlinggun.AddIngredient<Items.Icons.SantankIcon>();
        gattlinggun.Register();

        Recipe blitz = Recipe.Create(ItemID.SantaNK1Trophy);
        blitz.AddIngredient<Items.Icons.SantankIcon>();
        blitz.Register();

        Recipe blizzardof78 = Recipe.Create(ItemID.BlizzardStaff);
        blizzardof78.AddIngredient<Items.Icons.IceQueenIcon>();
        blizzardof78.Register();

        Recipe lemonster = Recipe.Create(ItemID.SnowmanCannon);
        lemonster.AddIngredient<Items.Icons.IceQueenIcon>();
        lemonster.Register();

        Recipe santa = Recipe.Create(ItemID.NorthPole);
        santa.AddIngredient<Items.Icons.IceQueenIcon>();
        santa.Register();

        Recipe brome = Recipe.Create(ItemID.BabyGrinchMischiefWhistle);
        brome.AddIngredient<Items.Icons.IceQueenIcon>();
        brome.Register();

        Recipe chudtasmic = Recipe.Create(ItemID.ReindeerBells);
        chudtasmic.AddIngredient<Items.Icons.IceQueenIcon>();
        chudtasmic.Register();

        Recipe saygex = Recipe.Create(ItemID.IceQueenTrophy);
        saygex.AddIngredient<Items.Icons.IceQueenIcon>();
        saygex.Register();
    }
}