using HendecamMod.Content.Items.Accessories.NormalOnes;
using HendecamMod.Content.Items.Accessories.PeaceAmongNations;
using HendecamMod.Content.Items.Accessories.Rampart;
using HendecamMod.Content.Items.Placeables;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Common.Systems;

public class RiverRecipes : ModSystem
{
    public override void AddRecipes()
    {
        Recipe fraudplat = Recipe.Create(ItemID.PlatinumCoin, 1);
        fraudplat.AddIngredient(ItemID.PlatinumBar, 10);
        fraudplat.AddTile(TileID.Autohammer);
        fraudplat.Register();
        Recipe fraudgold = Recipe.Create(ItemID.GoldCoin, 1);
        fraudgold.AddIngredient(ItemID.GoldBar, 10);
        fraudgold.AddTile(TileID.Autohammer);
        fraudgold.Register();
        Recipe fraudsilv = Recipe.Create(ItemID.SilverCoin, 1);
        fraudsilv.AddIngredient(ItemID.SilverBar, 10);
        fraudsilv.AddTile(TileID.Autohammer);
        fraudsilv.Register();
        Recipe fraudcopp = Recipe.Create(ItemID.CopperCoin, 1);
        fraudcopp.AddIngredient(ItemID.CopperBar, 10);
        fraudcopp.AddTile(TileID.Autohammer);
        fraudcopp.Register();

        Recipe flamespectre = Recipe.Create(ItemID.SpectreBoots, 1);
        flamespectre.AddIngredient(ItemID.FlameWakerBoots, 1);
        flamespectre.AddIngredient(ItemID.RocketBoots, 1);
        flamespectre.AddTile(TileID.TinkerersWorkbench);
        flamespectre.Register();
        Recipe flowerspectre = Recipe.Create(ItemID.SpectreBoots, 1);
        flowerspectre.AddIngredient(ItemID.FlowerBoots, 1);
        flowerspectre.AddIngredient(ItemID.RocketBoots, 1);
        flowerspectre.AddTile(TileID.TinkerersWorkbench);
        flowerspectre.Register();
        Recipe althotskull = Recipe.Create(ItemID.MoltenSkullRose, 1);
        althotskull.AddIngredient(ItemID.ObsidianSkull, 1);
        althotskull.AddIngredient<MoltenRose>(1);
        althotskull.AddTile(TileID.TinkerersWorkbench);
        althotskull.Register();
        Recipe althotboots = Recipe.Create(ItemID.LavaWaders, 1);
        althotboots.AddIngredient(ItemID.ObsidianWaterWalkingBoots, 1);
        althotboots.AddIngredient<MoltenRose>(1);
        althotboots.AddTile(TileID.TinkerersWorkbench);
        althotboots.Register();

        Recipe unshimmerstone = Recipe.Create(ItemID.CopperBar, 1);
        unshimmerstone.AddIngredient<StoneBar>(10);
        unshimmerstone.AddTile(TileID.AlchemyTable);
        unshimmerstone.Register();
        Recipe unshimmercopper = Recipe.Create(ItemID.TinBar, 1);
        unshimmercopper.AddIngredient(ItemID.CopperBar, 10);
        unshimmercopper.AddTile(TileID.AlchemyTable);
        unshimmercopper.Register();
        Recipe unshimmertin = Recipe.Create(ItemID.IronBar, 1);
        unshimmertin.AddIngredient(ItemID.TinBar, 10);
        unshimmertin.AddTile(TileID.AlchemyTable);
        unshimmertin.Register();
        Recipe unshimmeriron = Recipe.Create(ItemID.LeadBar, 1);
        unshimmeriron.AddIngredient(ItemID.IronBar, 10);
        unshimmeriron.AddTile(TileID.AlchemyTable);
        unshimmeriron.Register();
        Recipe unshimmerlead = Recipe.Create(ItemID.SilverBar, 1);
        unshimmerlead.AddIngredient(ItemID.LeadBar, 10);
        unshimmerlead.AddTile(TileID.AlchemyTable);
        unshimmerlead.Register();
        Recipe unshimmersilver = Recipe.Create(ItemID.TungstenBar, 1);
        unshimmersilver.AddIngredient(ItemID.SilverBar, 10);
        unshimmersilver.AddTile(TileID.AlchemyTable);
        unshimmersilver.Register();
        Recipe unshimmertungsten = Recipe.Create(ItemID.GoldBar, 1);
        unshimmertungsten.AddIngredient(ItemID.TungstenBar, 10);
        unshimmertungsten.AddTile(TileID.AlchemyTable);
        unshimmertungsten.Register();
        Recipe unshimmergold = Recipe.Create(ItemID.PlatinumBar, 1);
        unshimmergold.AddIngredient(ItemID.GoldBar, 10);
        unshimmergold.AddTile(TileID.AlchemyTable);
        unshimmergold.Register();
        Recipe unshimmerplatinum = Recipe.Create(ItemID.CobaltBar, 1);
        unshimmerplatinum.AddIngredient(ItemID.PlatinumBar, 10);
        unshimmerplatinum.AddTile(TileID.AlchemyTable);
        unshimmerplatinum.AddTile(TileID.AdamantiteForge);
        unshimmerplatinum.Register();
        Recipe unshimmercobalt = Recipe.Create(ItemID.PalladiumBar, 1);
        unshimmercobalt.AddIngredient(ItemID.CobaltBar, 10);
        unshimmercobalt.AddTile(TileID.AlchemyTable);
        unshimmercobalt.AddTile(TileID.AdamantiteForge);
        unshimmercobalt.Register();
        Recipe unshimmerpalladium = Recipe.Create(ItemID.MythrilBar, 1);
        unshimmerpalladium.AddIngredient(ItemID.PalladiumBar, 10);
        unshimmerpalladium.AddTile(TileID.AlchemyTable);
        unshimmerpalladium.AddTile(TileID.AdamantiteForge);
        unshimmerpalladium.Register();
        Recipe unshimmermythril = Recipe.Create(ItemID.OrichalcumBar, 1);
        unshimmermythril.AddIngredient(ItemID.MythrilBar, 10);
        unshimmermythril.AddTile(TileID.AlchemyTable);
        unshimmermythril.AddTile(TileID.AdamantiteForge);
        unshimmermythril.Register();
        Recipe unshimmerorichalcum = Recipe.Create(ItemID.AdamantiteBar, 1);
        unshimmerorichalcum.AddIngredient(ItemID.OrichalcumBar, 10);
        unshimmerorichalcum.AddTile(TileID.AlchemyTable);
        unshimmerorichalcum.AddTile(TileID.AdamantiteForge);
        unshimmerorichalcum.Register();
        Recipe unshimmeradamantite = Recipe.Create(ItemID.TitaniumBar, 1);
        unshimmeradamantite.AddIngredient(ItemID.AdamantiteBar, 10);
        unshimmeradamantite.AddTile(TileID.AlchemyTable);
        unshimmeradamantite.AddTile(TileID.AdamantiteForge);
        unshimmeradamantite.Register();
        Recipe unshimmertitanium = Recipe.Create(ItemID.ChlorophyteBar, 1);
        unshimmertitanium.AddIngredient(ItemID.TitaniumBar, 10);
        unshimmertitanium.AddTile(TileID.AlchemyTable);
        unshimmertitanium.AddTile(TileID.Autohammer);
        unshimmertitanium.Register();
        Recipe unshimmerchlorophyte = Recipe.Create(ItemID.LunarBar, 1);
        unshimmerchlorophyte.AddIngredient(ItemID.ChlorophyteBar, 10);
        unshimmerchlorophyte.AddTile(TileID.AlchemyTable);
        unshimmerchlorophyte.AddTile(TileID.VoidMonolith);
        unshimmerchlorophyte.Register();

        Recipe ankhshield = Recipe.Create(ItemID.AnkhShield, 1);
        ankhshield.AddIngredient<RiotShield>(1);
        ankhshield.AddIngredient<SecondDinner>(1);
        ankhshield.AddTile(TileID.TinkerersWorkbench);
        ankhshield.AddTile(TileID.AdamantiteForge);
        ankhshield.Register();
        Recipe royalgel = Recipe.Create(ItemID.RoyalGel, 1);
        royalgel.AddIngredient(ItemID.SlimeStaff, 1);
        royalgel.AddIngredient<FriendCore>(1);
        royalgel.AddTile(TileID.TinkerersWorkbench);
        royalgel.AddTile(TileID.AlchemyTable);
        royalgel.Register();

        Recipe spectrerecipe = Recipe.Create(ItemID.SpectreBoots, 1);
        spectrerecipe.AddIngredient(ItemID.SandBoots, 1);
        spectrerecipe.AddIngredient(ItemID.HermesBoots, 1);
        spectrerecipe.AddIngredient(ItemID.FlurryBoots, 1);
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
                recipe.AddIngredient(ItemID.HellfireTreads, 1);
                recipe.AddIngredient(ItemID.AmphibianBoots, 1);
                recipe.AddIngredient(ItemID.FairyBoots, 1);
                recipe.AddIngredient(ItemID.LuckyHorseshoe, 1);
            }
            if (recipe.HasResult(ItemID.HellfireTreads))
            {
                recipe.RemoveIngredient(ItemID.SpectreBoots);
                recipe.AddIngredient(ItemID.LavaWaders, 1);
            }
            if (recipe.HasResult(ItemID.FairyBoots))
            {
                recipe.RemoveIngredient(ItemID.SpectreBoots);
                recipe.AddIngredient(ItemID.RocketBoots, 1);
            }
        }
    }
}

