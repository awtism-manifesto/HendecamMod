using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class RiverCube : BaseCube
{
    protected override int Rarity => ItemRarityID.Blue;
    protected override int Value => 1;
    protected override int Defense => -1;

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.maxFallSpeed *= 128f;
        player.fallStart = (int)(player.fallStart * 0.01f);
        player.stairFall = true;
    }

    public override void AddRecipes() => CreateCubeRecipe(ItemID.CrabBanner,6);
}
