using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Furniture;

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