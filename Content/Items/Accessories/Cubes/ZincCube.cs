using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Rarities;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class ZincCube : BaseCube
{
    protected override int Rarity => (ModLoader.TryGetMod("Avalon", out Mod Avalon)) ? Item.rare = ItemRarityID.Blue: RarityType<BurntOrange>();
    protected override int Value => 36250;
    protected override int Defense => 3;

    public override void UpdateAccessory(Player player, bool hideVisual) =>
        player.maxFallSpeed *= 2f;

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<CubicMold>();
        recipe.AddIngredient(ItemID.AlphabetStatueZ, 1);
        recipe.AddIngredient(ItemID.AlphabetStatueI, 1);
        recipe.AddIngredient(ItemID.AlphabetStatueN, 1);
        recipe.AddIngredient(ItemID.AlphabetStatueC, 1);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();

        if (ModLoader.TryGetMod("Avalon", out Mod Avalon) && Avalon.TryFind("ZincBar", out ModItem ZincBar))
        {
            CreateCubeRecipe(ZincBar.Type);
        }
    }
}
