using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class SilverCube : BaseCube
{
    protected override int Rarity => ItemRarityID.Blue;
    protected override int Value => 37000;
    protected override int Defense => 3;

    public override void UpdateAccessory(Player player, bool hideVisual) =>
                    player.maxFallSpeed *= 2f;

    public override void AddRecipes() => CreateCubeRecipe(ItemID.SilverBar);
}
