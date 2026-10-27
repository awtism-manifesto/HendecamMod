using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class MeteorCube : BaseCube
{
    protected override int Rarity => ItemRarityID.Blue;
    protected override int Value => 87000;
    protected override int Defense => 5;

    public override void UpdateAccessory(Player player, bool hideVisual) =>
                    player.maxFallSpeed *= 4f;

    public override void AddRecipes() => CreateCubeRecipe(ItemID.MeteoriteBar);
}