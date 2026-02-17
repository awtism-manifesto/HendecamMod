using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Tiles.Furniture;

namespace HendecamMod.Content.Global;

public class AzuriteDungeonRecipes : ModSystem
{
    public override void AddRecipes()
    {
        Recipe recipe = Recipe.Create(ItemID.AquaScepter);
        recipe.AddIngredient<AzuriteBar>(10);
        recipe.AddIngredient(ItemID.BottledWater, 30);
        recipe.AddIngredient(ItemID.FallenStar, 3);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();

        Recipe recipe1 = Recipe.Create(ItemID.BlueMoon);
        recipe1.AddIngredient<AzuriteBar>(15);
        recipe1.AddIngredient(ItemID.Mace);

        recipe1.AddTile(TileID.Anvils);
        recipe1.Register();
        Recipe recipe12 = Recipe.Create(ItemID.Muramasa);
        recipe12.AddIngredient<AzuriteBar>(10);
        recipe12.AddIngredient(ItemID.Katana);

        recipe12.AddTile(TileID.Anvils);
        recipe12.Register();

        Recipe recipe123 = Recipe.Create(ItemID.CobaltShield);
        recipe123.AddIngredient<AzuriteBar>(10);

        recipe123.AddIngredient<DefenseShield>();

        recipe123.AddTile<CobaltWorkBenchPlaced>(); // Palladium also counts as cobalt workbench

        recipe123.Register();
        

        Recipe recipe1234 = Recipe.Create(ItemID.WaterBolt);
        recipe1234.AddIngredient<AzuriteBar>(10);
        recipe1234.AddIngredient(ItemID.FallenStar, 3);
        recipe1234.AddIngredient(ItemID.Book);

        recipe1234.AddTile(TileID.Bookcases);
        recipe1234.Register();

        Recipe recipe12345 = Recipe.Create(ItemID.MagicMissile);
        recipe12345.AddIngredient<AzuriteBar>(10);
        recipe12345.AddIngredient<WoodenStick>(5);
        recipe12345.AddIngredient(ItemID.FallenStar, 3);
        recipe12345.AddTile(TileID.Anvils);
        recipe12345.Register();
    }
}