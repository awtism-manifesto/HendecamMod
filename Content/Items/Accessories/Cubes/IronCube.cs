using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class IronCube : BaseCube
{
    protected override int Rarity => ItemRarityID.Blue;
    protected override int Value => 28000;
    protected override int Defense => 10;

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.maxFallSpeed *= 0.5f;
        player.accRunSpeed *= 0.5f;
        player.moveSpeed *= 0.5f;
    }

    public override void AddRecipes() => CreateCubeRecipe(ItemID.IronBar);
}