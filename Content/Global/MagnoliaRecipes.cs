using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Global;

public class MagnoliaRecipes : ModSystem
{
    public override void AddRecipes()
    {
        Recipe recipe = Recipe.Create(ItemID.SkyFracture);
        recipe.AddIngredient(ItemID.MagicMissile);
        recipe.AddIngredient<LunarGem>(4);
        recipe.AddIngredient<SoulOfHeight>(16);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();

        Recipe A162 = Recipe.Create(ItemID.Gel, 50);
        A162.AddIngredient(ItemID.SlimeHook);
        A162.AddTile(TileID.Solidifier);
        A162.Register();

        Recipe A1 = Recipe.Create(ItemID.CopperPickaxe);
        A1.AddIngredient(ItemID.CopperBar, 8);
        A1.AddIngredient<WoodenStick>(2);
        A1.AddTile(TileID.Anvils);
        A1.Register();

        Recipe A2 = Recipe.Create(ItemID.TinPickaxe);
        A2.AddIngredient(ItemID.TinBar, 8);
        A2.AddIngredient<WoodenStick>(2);
        A2.AddTile(TileID.Anvils);
        A2.Register();

        Recipe A3 = Recipe.Create(ItemID.IronPickaxe);
        A3.AddIngredient(ItemID.IronBar, 10);
        A3.AddIngredient<WoodenStick>(2);
        A3.AddTile(TileID.Anvils);
        A3.Register();

        Recipe A4 = Recipe.Create(ItemID.LeadPickaxe);
        A4.AddIngredient(ItemID.LeadBar, 10);
        A4.AddIngredient<WoodenStick>(2);
        A4.AddTile(TileID.Anvils);
        A4.Register();

        Recipe A5 = Recipe.Create(ItemID.SilverPickaxe);
        A5.AddIngredient(ItemID.SilverBar, 10);
        A5.AddIngredient<WoodenStick>(2);
        A5.AddTile(TileID.Anvils);
        A5.Register();

        Recipe A6 = Recipe.Create(ItemID.TungstenPickaxe);
        A6.AddIngredient(ItemID.TungstenBar, 10);
        A6.AddIngredient<WoodenStick>(2);
        A6.AddTile(TileID.Anvils);
        A6.Register();

        Recipe A7 = Recipe.Create(ItemID.GoldPickaxe);
        A7.AddIngredient(ItemID.GoldBar, 10);
        A7.AddIngredient<WoodenStick>(2);
        A7.AddTile(TileID.Anvils);
        A7.Register();

        Recipe A8 = Recipe.Create(ItemID.PlatinumPickaxe);
        A8.AddIngredient(ItemID.PlatinumBar, 10);
        A8.AddIngredient<WoodenStick>(2);
        A8.AddTile(TileID.Anvils);
        A8.Register();

        Recipe A9 = Recipe.Create(ItemID.CopperAxe);
        A9.AddIngredient(ItemID.CopperBar, 6);
        A9.AddIngredient<WoodenStick>(2);
        A9.AddTile(TileID.Anvils);
        A9.Register();

        Recipe A10 = Recipe.Create(ItemID.TinAxe);
        A10.AddIngredient(ItemID.TinBar, 6);
        A10.AddIngredient<WoodenStick>(2);
        A10.AddTile(TileID.Anvils);
        A10.Register();

        Recipe A11 = Recipe.Create(ItemID.IronAxe);
        A11.AddIngredient(ItemID.IronBar, 8);
        A11.AddIngredient<WoodenStick>(2);
        A11.AddTile(TileID.Anvils);
        A11.Register();

        Recipe A12 = Recipe.Create(ItemID.LeadAxe);
        A12.AddIngredient(ItemID.LeadBar, 8);
        A12.AddIngredient<WoodenStick>(2);
        A12.AddTile(TileID.Anvils);
        A12.Register();

        Recipe A13 = Recipe.Create(ItemID.SilverAxe);
        A13.AddIngredient(ItemID.SilverBar, 8);
        A13.AddIngredient<WoodenStick>(2);
        A13.AddTile(TileID.Anvils);
        A13.Register();

        Recipe A14 = Recipe.Create(ItemID.TungstenAxe);
        A14.AddIngredient(ItemID.TungstenBar, 8);
        A14.AddIngredient<WoodenStick>(2);
        A14.AddTile(TileID.Anvils);
        A14.Register();

        Recipe A15 = Recipe.Create(ItemID.GoldAxe);
        A15.AddIngredient(ItemID.GoldBar, 9);
        A15.AddIngredient<WoodenStick>(2);
        A15.AddTile(TileID.Anvils);
        A15.Register();

        Recipe A16 = Recipe.Create(ItemID.PlatinumAxe);
        A16.AddIngredient(ItemID.PlatinumBar, 9);
        A16.AddIngredient<WoodenStick>(2);
        A16.AddTile(TileID.Anvils);
        A16.Register();

        Recipe A17 = Recipe.Create(ItemID.CopperHammer);
        A17.AddIngredient(ItemID.CopperBar, 8);
        A17.AddIngredient<WoodenStick>(2);
        A17.AddTile(TileID.Anvils);
        A17.Register();

        Recipe A18 = Recipe.Create(ItemID.TinHammer);
        A18.AddIngredient(ItemID.TinBar, 8);
        A18.AddIngredient<WoodenStick>(2);
        A18.AddTile(TileID.Anvils);
        A18.Register();

        Recipe A19 = Recipe.Create(ItemID.IronHammer);
        A19.AddIngredient(ItemID.IronBar, 8);
        A19.AddIngredient<WoodenStick>(2);
        A19.AddTile(TileID.Anvils);
        A19.Register();

        Recipe A20 = Recipe.Create(ItemID.LeadHammer);
        A20.AddIngredient(ItemID.LeadBar, 8);
        A20.AddIngredient<WoodenStick>(2);
        A20.AddTile(TileID.Anvils);
        A20.Register();

        Recipe A21 = Recipe.Create(ItemID.SilverHammer);
        A21.AddIngredient(ItemID.SilverBar, 8);
        A21.AddIngredient<WoodenStick>(2);
        A21.AddTile(TileID.Anvils);
        A21.Register();

        Recipe A22 = Recipe.Create(ItemID.TungstenHammer);
        A22.AddIngredient(ItemID.TungstenBar, 8);
        A22.AddIngredient<WoodenStick>(2);
        A22.AddTile(TileID.Anvils);
        A22.Register();

        Recipe A23 = Recipe.Create(ItemID.GoldHammer);
        A23.AddIngredient(ItemID.GoldBar, 8);
        A23.AddIngredient<WoodenStick>(2);
        A23.AddTile(TileID.Anvils);
        A23.Register();

        Recipe A24 = Recipe.Create(ItemID.PlatinumHammer);
        A24.AddIngredient(ItemID.PlatinumBar, 8);
        A24.AddIngredient<WoodenStick>(2);
        A24.AddTile(TileID.Anvils);
        A24.Register();

        Recipe A25 = Recipe.Create(ItemID.Flamarang);
        A25.AddIngredient<FireDiamond>(8);
        A25.AddIngredient(ItemID.HellstoneBar, 2);
        A25.AddIngredient(ItemID.EnchantedBoomerang);
        A25.AddTile(TileID.Anvils);
        A25.Register();

        Recipe A26 = Recipe.Create(ItemID.PhoenixBlaster);
        A26.AddIngredient<FireDiamond>(8);
        A26.AddIngredient(ItemID.Handgun);
        A26.AddTile(TileID.Anvils);
        A26.Register();

        Recipe A27 = Recipe.Create(ItemID.RainbowRod);
        A27.AddIngredient(ItemID.CrystalShard, 15);
        A27.AddIngredient(ItemID.UnicornHorn, 2);
        A27.AddIngredient(ItemID.PixieDust, 10);
        A27.AddIngredient<SoulOfHeight>(8);
        A27.AddIngredient(ItemID.RainbowBrick, 25);
        A27.AddTile(TileID.Anvils);
        A27.Register();

        Recipe A28 = Recipe.Create(ItemID.Mushroom);
        A28.AddIngredient(ItemID.GlowingMushroom);
        A28.Register();

        Recipe A29 = Recipe.Create(ItemID.Present);
        A29.AddIngredient(ItemID.GoodieBag);
        A29.Register();

        Recipe A30 = Recipe.Create(ItemID.GoodieBag);
        A30.AddIngredient(ItemID.Present);
        A30.Register();

        Recipe A31 = Recipe.Create(ItemID.ImpStaff);
        A31.AddIngredient<FireDiamond>(15);
        A31.AddIngredient(ItemID.HellstoneBar, 10);
        A31.AddTile(TileID.Anvils);
        A31.Register();

        Recipe A32 = Recipe.Create(ItemID.FlameWakerBoots);
        A32.AddIngredient(ItemID.SpectreBoots);
        A32.AddIngredient<FireDiamond>(5);
        A32.AddTile(TileID.TinkerersWorkbench);
        A32.Register();

        Recipe A33 = Recipe.Create(ItemID.CloudinaBottle);
        A33.AddIngredient(ItemID.Bottle);
        A33.AddIngredient(ItemID.Cloud, 25);
        A33.AddTile(TileID.Anvils);
        A33.Register();

        Recipe A34 = Recipe.Create(ItemID.SailfishBoots);
        A34.AddIngredient(ItemID.BottledWater);
        A34.AddIngredient(ItemID.HermesBoots);
        A34.AddTile(TileID.Anvils);
        A34.Register();

        Recipe A35 = Recipe.Create(ItemID.FlurryBoots);
        A35.AddIngredient(ItemID.SnowBlock, 15);
        A35.AddIngredient(ItemID.HermesBoots);
        A35.AddTile(TileID.Anvils);
        A35.Register();

        Recipe A36 = Recipe.Create(ItemID.SailfishBoots);
        A36.AddIngredient(ItemID.WaterBucket, 2);
        A36.AddIngredient(ItemID.HermesBoots);
        A36.AddTile(TileID.Anvils);
        A36.Register();

        Recipe A37 = Recipe.Create(ItemID.BandofRegeneration);
        A37.AddIngredient(ItemID.LifeCrystal);
        A37.AddIngredient(ItemID.GoldBar, 4);
        A37.AddTile(TileID.Anvils);
        A37.Register();

        Recipe A38 = Recipe.Create(ItemID.BandofRegeneration);
        A38.AddIngredient(ItemID.LifeCrystal);
        A38.AddIngredient(ItemID.PlatinumBar, 4);
        A38.AddTile(TileID.Anvils);
        A38.Register();

        Recipe A39 = Recipe.Create(ItemID.BandofStarpower);
        A39.AddIngredient(ItemID.ManaCrystal);
        A39.AddIngredient<LunarGem>(3);
        A39.AddIngredient(ItemID.SilverBar, 4);
        A39.AddTile(TileID.Anvils);
        A39.Register();

        Recipe A163 = Recipe.Create(ItemID.Bone, 40);
        A163.AddIngredient(ItemID.SkeletronHand);
        A163.Register();

        Recipe A164 = Recipe.Create(ItemID.CandyCaneBlock, 75);
        A164.AddIngredient(ItemID.CandyCaneHook);
        A164.Register();

        Recipe A165 = Recipe.Create(ItemID.GreenCandyCaneBlock, 75);
        A165.AddIngredient(ItemID.CandyCaneHook);
        A165.Register();

        Recipe A166 = Recipe.Create(ItemID.SoulofLight, 25);
        A166.AddIngredient(ItemID.IlluminantHook);
        A166.AddTile<CobaltWorkBenchPlaced>();
        A166.Register();

        Recipe A191 = Recipe.Create(ItemID.SoulofLight, 25);
        A191.AddIngredient(ItemID.IlluminantHook);
        A191.AddTile<PalladiumWorkBenchPlaced>();
        A191.Register();

        Recipe A167 = Recipe.Create(ItemID.SoulofNight, 25);
        A167.AddIngredient(ItemID.WormHook);
        A167.AddTile<CobaltWorkBenchPlaced>();
        A167.Register();

        Recipe A168 = Recipe.Create(ItemID.SoulofNight, 25);
        A168.AddIngredient(ItemID.TendonHook);
        A168.AddTile<CobaltWorkBenchPlaced>();
        A168.Register();

        Recipe A169 = Recipe.Create(ItemID.GlowingMushroom, 500);
        A169.AddIngredient(ItemID.Hammush);
        A169.AddTile<CobaltWorkBenchPlaced>();
        A169.Register();

        Recipe A192 = Recipe.Create(ItemID.SoulofNight, 25);
        A192.AddIngredient(ItemID.WormHook);
        A192.AddTile<PalladiumWorkBenchPlaced>();
        A192.Register();

        Recipe A193 = Recipe.Create(ItemID.SoulofNight, 25);
        A193.AddIngredient(ItemID.TendonHook);
        A193.AddTile<PalladiumWorkBenchPlaced>();
        A193.Register();

        Recipe A194 = Recipe.Create(ItemID.GlowingMushroom, 500);
        A194.AddIngredient(ItemID.Hammush);
        A194.AddTile<PalladiumWorkBenchPlaced>();
        A194.Register();

        Recipe A170 = Recipe.Create(ItemID.Shroomerang);
        A170.AddIngredient(ItemID.EnchantedBoomerang);
        A170.AddIngredient(ItemID.GlowingMushroom, 100);
        A170.AddTile(TileID.Anvils);
        A170.Register();

        Recipe A171 = Recipe.Create(ItemID.IceBoomerang);
        A171.AddIngredient(ItemID.EnchantedBoomerang);
        A171.AddIngredient(ItemID.IceBlock, 50);
        A171.AddTile(TileID.Anvils);
        A171.Register();

        Recipe A172 = Recipe.Create(ItemID.LivingFireBlock);
        A172.AddIngredient(ItemID.Torch, 25);
        A172.AddIngredient<FireDiamond>();
        A172.Register();

        Recipe A173 = Recipe.Create(ItemID.BoneWand);
        A173.AddIngredient(ItemID.Bone, 25);
        A173.AddTile(TileID.BoneWelder);
        A173.Register();

        Recipe A175 = Recipe.Create(ItemID.Mace);
        A175.AddIngredient(ItemID.IronBar, 8);
        A175.AddTile(TileID.Anvils);
        A175.Register();

        Recipe A176 = Recipe.Create(ItemID.Starfury);
        A176.AddIngredient<LunarGem>(12);
        A176.AddIngredient(ItemID.GoldBar, 10);
        A176.AddIngredient(ItemID.FallenStar, 2);
        A176.AddTile(TileID.Anvils);
        A176.Register();

        Recipe A177 = Recipe.Create(ItemID.Starfury);
        A177.AddIngredient<LunarGem>(12);
        A177.AddIngredient(ItemID.PlatinumBar, 10);
        A177.AddIngredient(ItemID.FallenStar, 2);
        A177.AddTile(TileID.Anvils);
        A177.Register();

        Recipe A178 = Recipe.Create(ItemID.EnchantedBoomerang);
        A178.AddIngredient<LunarGem>(2);
        A178.AddIngredient(ItemID.FallenStar);
        A178.AddIngredient(ItemID.WoodenBoomerang);
        A178.Register();

        Recipe A179 = Recipe.Create(ItemID.EnchantedNightcrawler, 2);
        A179.AddIngredient<LunarGem>(3);
        A179.AddIngredient(ItemID.EnchantedNightcrawler);
        A179.Register();

        Recipe A180 = Recipe.Create(ItemID.HellfireArrow, 150);
        A180.AddIngredient<FireDiamond>(3);
        A180.AddIngredient(ItemID.WoodenArrow, 100);
        A180.AddTile(TileID.WorkBenches);
        A180.Register();

        Recipe A181 = Recipe.Create(ItemID.Leather);
        A181.AddIngredient(ItemID.Vertebrae, 5);
        A181.AddTile(TileID.WorkBenches);
        A181.Register();

        Recipe A182 = Recipe.Create(ItemID.GiantHarpyFeather);
        A182.AddIngredient(ItemID.Feather, 150);
        A182.AddTile(TileID.SkyMill);
        A182.Register();

        Recipe A183 = Recipe.Create(ItemID.Book);
        A183.AddIngredient<Paper>(30);
        A183.AddIngredient(ItemID.Leather, 2);
        A183.AddTile(TileID.WorkBenches);
        A183.Register();

        Recipe A184 = Recipe.Create(ItemID.Leather, 7);
        A184.AddIngredient(ItemID.BlandWhip);
        A184.Register();

        Recipe A185 = Recipe.Create(ItemID.PaperAirplaneA, 2);
        A185.AddIngredient<Paper>();
        A185.Register();

        Recipe A186 = Recipe.Create(ItemID.PaperAirplaneB, 2);
        A186.AddIngredient<Paper>();
        A186.Register();

        Recipe A187 = Recipe.Create(ItemID.LuckyHorseshoe);
        A187.AddIngredient<SoulOfHeight>(3);
        A187.AddIngredient(ItemID.GoldBar, 4);
        A187.AddTile(TileID.Anvils);
        A187.Register();

        Recipe A188 = Recipe.Create(ItemID.LuckyHorseshoe);
        A188.AddIngredient<SoulOfHeight>(3);
        A188.AddIngredient(ItemID.PlatinumBar, 4);
        A188.AddTile(TileID.Anvils);
        A188.Register();

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

        Recipe A47 = Recipe.Create(ItemID.KingSlimeTrophy);
        A47.AddIngredient<Items.Icons.KingSlimeIcon>();
        A47.Register();

        Recipe A48 = Recipe.Create(ItemID.Binoculars);
        A48.AddIngredient<Items.Icons.EyeOfCthulhuIcon>();
        A48.Register();

        Recipe A49 = Recipe.Create(ItemID.EyeMask);
        A49.AddIngredient<Items.Icons.EyeOfCthulhuIcon>();
        A49.Register();

        Recipe A50 = Recipe.Create(ItemID.EyeofCthulhuTrophy);
        A50.AddIngredient<Items.Icons.EyeOfCthulhuIcon>();
        A50.Register();

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
        A54.AddIngredient<Items.Icons.EaterOfWorldsIcon>();
        A54.Register();

        Recipe A55 = Recipe.Create(ItemID.BrainMask);
        A55.AddIngredient<Items.Icons.EaterOfWorldsIcon>();
        A55.Register();

        Recipe A56 = Recipe.Create(ItemID.BrainofCthulhuTrophy);
        A56.AddIngredient<Items.Icons.EaterOfWorldsIcon>();
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

        Recipe A67 = Recipe.Create(ItemID.Beenade, 30);
        A67.AddIngredient<Items.Icons.QueenBeeIcon>();
        A67.Register();

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

        Recipe A160 = Recipe.Create(ItemID.BossMaskMoonlord);
        A160.AddIngredient<Items.Icons.MoonLordIcon>();
        A160.Register();

        Recipe A161 = Recipe.Create(ItemID.MoonLordTrophy);
        A161.AddIngredient<Items.Icons.MoonLordIcon>();
        A161.Register();

        Recipe A189 = Recipe.Create(ItemID.PirateMap, 2);
        A189.AddIngredient<Paper>(30);
        A189.AddIngredient(ItemID.PirateMap);
        A189.Register();

        Recipe A190 = Recipe.Create(ItemID.PixieDust, 10);
        A190.AddIngredient(ItemID.GoldDust, 10);
        A190.AddIngredient(ItemID.SoulofLight);
        A190.AddTile<PalladiumWorkBenchPlaced>();
        A190.Register();

        Recipe A195 = Recipe.Create(ItemID.PixieDust, 10);
        A195.AddIngredient(ItemID.GoldDust, 10);
        A195.AddIngredient(ItemID.SoulofLight);
        A195.AddTile<CobaltWorkBenchPlaced>();
        A195.Register();
    }
}