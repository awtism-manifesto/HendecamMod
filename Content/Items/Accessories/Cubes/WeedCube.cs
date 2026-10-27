using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class WeedCube : BaseCube
{
    protected override int Rarity => ItemRarityID.Green;
    protected override int Value => 45000;
    protected override int Defense => 4;

    public override void UpdateAccessory(Player player, bool hideVisual) =>
                    player.maxFallSpeed *= 2f;

    public override void AddRecipes() => CreateCubeRecipe(ModContent.ItemType<WeedLeaves>());
}
