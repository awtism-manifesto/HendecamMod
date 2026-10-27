using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class UraniumCube : BaseCube
{
    protected override int Rarity => ItemRarityID.Green;
    protected override int Value => 152000;
    protected override int Defense => 5;

    public override void UpdateAccessory(Player player, bool hideVisual) =>
                    player.maxFallSpeed *= Main.rand.NextFloat(-0.5f, 5f);

    public override void AddRecipes() => CreateCubeRecipe(ModContent.ItemType<UraniumBar>());
}
