using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class MorbiumCube : BaseCube
{
    protected override int Rarity => ItemRarityID.Yellow;
    protected override int Value => 181000;
    protected override int Defense => 25;

    public override void UpdateAccessory(Player player, bool hideVisual) =>
                player.maxFallSpeed *= -1.1f;

    public override void AddRecipes() => CreateCubeRecipe(ModContent.ItemType<MorbiumBar>());
}
