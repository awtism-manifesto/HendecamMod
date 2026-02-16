using HendecamMod.Content.Items;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Items.Accessories.NormalOnes;
using HendecamMod.Content.Items.Accessories.PeaceAmongNations;
using HendecamMod.Content.Items.Accessories.Rampart;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Items.Weapons;

namespace HendecamMod.Common.Systems;

public class RiverRecipes : ModSystem
{
    public override void AddRecipes()
    {
        Recipe fraudplat = Recipe.Create(ItemID.PlatinumCoin);
        fraudplat.AddIngredient(ItemID.PlatinumBar, 10);
        fraudplat.AddTile(TileID.Autohammer);
        fraudplat.Register();
        Recipe fraudgold = Recipe.Create(ItemID.GoldCoin);
        fraudgold.AddIngredient(ItemID.GoldBar, 10);
        fraudgold.AddTile(TileID.Autohammer);
        fraudgold.Register();
        Recipe fraudsilv = Recipe.Create(ItemID.SilverCoin);
        fraudsilv.AddIngredient(ItemID.SilverBar, 10);
        fraudsilv.AddTile(TileID.Autohammer);
        fraudsilv.Register();
        Recipe fraudcopp = Recipe.Create(ItemID.CopperCoin);
        fraudcopp.AddIngredient(ItemID.CopperBar, 10);
        fraudcopp.AddTile(TileID.Autohammer);
        fraudcopp.Register();

        Recipe flamespectre = Recipe.Create(ItemID.SpectreBoots);
        flamespectre.AddIngredient(ItemID.FlameWakerBoots);
        flamespectre.AddIngredient(ItemID.RocketBoots);
        flamespectre.AddTile(TileID.TinkerersWorkbench);
        flamespectre.Register();
        Recipe flowerspectre = Recipe.Create(ItemID.SpectreBoots);
        flowerspectre.AddIngredient(ItemID.FlowerBoots);
        flowerspectre.AddIngredient(ItemID.RocketBoots);
        flowerspectre.AddTile(TileID.TinkerersWorkbench);
        flowerspectre.Register();
        Recipe althotskull = Recipe.Create(ItemID.MoltenSkullRose);
        althotskull.AddIngredient(ItemID.ObsidianSkull);
        althotskull.AddIngredient<MoltenRose>();
        althotskull.AddTile(TileID.TinkerersWorkbench);
        althotskull.Register();
        Recipe althotboots = Recipe.Create(ItemID.LavaWaders);
        althotboots.AddIngredient(ItemID.ObsidianWaterWalkingBoots);
        althotboots.AddIngredient<MoltenRose>();
        althotboots.AddTile(TileID.TinkerersWorkbench);
        althotboots.Register();

        Recipe unshimmerstone = Recipe.Create(ItemID.CopperBar);
        unshimmerstone.AddIngredient<StoneBar>(10);
        unshimmerstone.AddTile(TileID.AlchemyTable);
        unshimmerstone.Register();
        Recipe unshimmercopper = Recipe.Create(ItemID.TinBar);
        unshimmercopper.AddIngredient(ItemID.CopperBar, 10);
        unshimmercopper.AddTile(TileID.AlchemyTable);
        unshimmercopper.Register();
        Recipe unshimmertin = Recipe.Create(ItemID.IronBar);
        unshimmertin.AddIngredient(ItemID.TinBar, 10);
        unshimmertin.AddTile(TileID.AlchemyTable);
        unshimmertin.Register();
        Recipe unshimmeriron = Recipe.Create(ItemID.LeadBar);
        unshimmeriron.AddIngredient(ItemID.IronBar, 10);
        unshimmeriron.AddTile(TileID.AlchemyTable);
        unshimmeriron.Register();
        Recipe unshimmerlead = Recipe.Create(ItemID.SilverBar);
        unshimmerlead.AddIngredient(ItemID.LeadBar, 10);
        unshimmerlead.AddTile(TileID.AlchemyTable);
        unshimmerlead.Register();
        Recipe unshimmersilver = Recipe.Create(ItemID.TungstenBar);
        unshimmersilver.AddIngredient(ItemID.SilverBar, 10);
        unshimmersilver.AddTile(TileID.AlchemyTable);
        unshimmersilver.Register();
        Recipe unshimmertungsten = Recipe.Create(ItemID.GoldBar);
        unshimmertungsten.AddIngredient(ItemID.TungstenBar, 10);
        unshimmertungsten.AddTile(TileID.AlchemyTable);
        unshimmertungsten.Register();
        Recipe unshimmergold = Recipe.Create(ItemID.PlatinumBar);
        unshimmergold.AddIngredient(ItemID.GoldBar, 10);
        unshimmergold.AddTile(TileID.AlchemyTable);
        unshimmergold.Register();
        Recipe unshimmerplatinum = Recipe.Create(ItemID.CobaltBar);
        unshimmerplatinum.AddIngredient(ItemID.PlatinumBar, 10);
        unshimmerplatinum.AddTile(TileID.AlchemyTable);
        unshimmerplatinum.AddTile(TileID.AdamantiteForge);
        unshimmerplatinum.Register();
        Recipe unshimmercobalt = Recipe.Create(ItemID.PalladiumBar);
        unshimmercobalt.AddIngredient(ItemID.CobaltBar, 10);
        unshimmercobalt.AddTile(TileID.AlchemyTable);
        unshimmercobalt.AddTile(TileID.AdamantiteForge);
        unshimmercobalt.Register();
        Recipe unshimmerpalladium = Recipe.Create(ItemID.MythrilBar);
        unshimmerpalladium.AddIngredient(ItemID.PalladiumBar, 10);
        unshimmerpalladium.AddTile(TileID.AlchemyTable);
        unshimmerpalladium.AddTile(TileID.AdamantiteForge);
        unshimmerpalladium.Register();
        Recipe unshimmermythril = Recipe.Create(ItemID.OrichalcumBar);
        unshimmermythril.AddIngredient(ItemID.MythrilBar, 10);
        unshimmermythril.AddTile(TileID.AlchemyTable);
        unshimmermythril.AddTile(TileID.AdamantiteForge);
        unshimmermythril.Register();
        Recipe unshimmerorichalcum = Recipe.Create(ItemID.AdamantiteBar);
        unshimmerorichalcum.AddIngredient(ItemID.OrichalcumBar, 10);
        unshimmerorichalcum.AddTile(TileID.AlchemyTable);
        unshimmerorichalcum.AddTile(TileID.AdamantiteForge);
        unshimmerorichalcum.Register();
        Recipe unshimmeradamantite = Recipe.Create(ItemID.TitaniumBar);
        unshimmeradamantite.AddIngredient(ItemID.AdamantiteBar, 10);
        unshimmeradamantite.AddTile(TileID.AlchemyTable);
        unshimmeradamantite.AddTile(TileID.AdamantiteForge);
        unshimmeradamantite.Register();
        Recipe unshimmertitanium = Recipe.Create(ItemID.ChlorophyteBar);
        unshimmertitanium.AddIngredient(ItemID.TitaniumBar, 10);
        unshimmertitanium.AddTile(TileID.AlchemyTable);
        unshimmertitanium.AddTile(TileID.Autohammer);
        unshimmertitanium.Register();
        Recipe unshimmerchlorophyte = Recipe.Create(ItemID.LunarBar);
        unshimmerchlorophyte.AddIngredient(ItemID.ChlorophyteBar, 10);
        unshimmerchlorophyte.AddTile(TileID.AlchemyTable);
        unshimmerchlorophyte.AddTile(TileID.VoidMonolith);
        unshimmerchlorophyte.Register();

        Recipe ankhshield = Recipe.Create(ItemID.AnkhShield);
        ankhshield.AddIngredient<RiotShield>();
        ankhshield.AddIngredient<SecondDinner>();
        ankhshield.AddTile(TileID.TinkerersWorkbench);
        ankhshield.AddTile(TileID.AdamantiteForge);
        ankhshield.Register();
        Recipe royalgel = Recipe.Create(ItemID.RoyalGel);
        royalgel.AddIngredient(ItemID.SlimeStaff);
        royalgel.AddIngredient<FriendCore>();
        royalgel.AddTile(TileID.TinkerersWorkbench);
        royalgel.AddTile(TileID.AlchemyTable);
        royalgel.Register();

        Recipe spectrerecipe = Recipe.Create(ItemID.SpectreBoots);
        spectrerecipe.AddIngredient(ItemID.SandBoots);
        spectrerecipe.AddIngredient(ItemID.HermesBoots);
        spectrerecipe.AddIngredient(ItemID.FlurryBoots);
        spectrerecipe.AddTile(TileID.TinkerersWorkbench);
        spectrerecipe.Register();
    }

    public override void PostAddRecipes()
    {
        for (int i = 0; i < Recipe.numRecipes; i++)
        {
            Recipe recipe = Main.recipe[i];
            if (recipe.HasIngredient(ItemID.ArmorBracing) && recipe.HasResult(ItemID.AnkhCharm))
            {
                recipe.DisableRecipe();
            }

            if (recipe.HasIngredient(ItemID.ObsidianShield) && recipe.HasResult(ItemID.AnkhShield))
            {
                recipe.DisableRecipe();
            }

            if (recipe.HasIngredient(ItemID.RocketBoots) && recipe.HasResult(ItemID.SpectreBoots))
            {
                recipe.DisableRecipe();
            }

            if (recipe.HasResult(ItemID.TerrasparkBoots))
            {
                recipe.RemoveIngredient(ItemID.LavaWaders);
                recipe.AddIngredient(ItemID.HellfireTreads);
                recipe.AddIngredient(ItemID.AmphibianBoots);
                recipe.AddIngredient(ItemID.FairyBoots);
                recipe.AddIngredient(ItemID.LuckyHorseshoe);
            }

            if (recipe.HasResult(ItemID.PDA))
            {


                recipe.AddIngredient(ModContent.ItemType<IQTest>());
            }

            if (recipe.HasResult(ItemID.HellfireTreads))
            {
                recipe.RemoveIngredient(ItemID.SpectreBoots);
                recipe.AddIngredient(ItemID.LavaWaders);
            }

            if (recipe.HasResult(ItemID.FairyBoots))
            {
                recipe.RemoveIngredient(ItemID.SpectreBoots);
                recipe.AddIngredient(ItemID.RocketBoots);
            }


            if (ModLoader.TryGetMod("PixelGunsTest", out Mod PixelMerica) && PixelMerica.TryFind("Ultimatum", out ModItem Ultimatum))
            {
                if (recipe.HasResult(Ultimatum.Type))
                {
                    recipe.AddIngredient(ModContent.ItemType<BigBuddy>());
                    recipe.AddIngredient(ModContent.ItemType<IcicleMinigun>());
                    recipe.AddIngredient(ModContent.ItemType<SolarRayRifle>());
                    recipe.AddIngredient(ModContent.ItemType<BlackMamba>());
                }
            }

        }
    }
}

