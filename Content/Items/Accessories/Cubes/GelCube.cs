using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class GelCube : BaseCube
{
    protected override int Rarity => ItemRarityID.Blue;
    protected override int Value => 5000;

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.AddBuff(BuffID.Slimed, 2);
        player.slippy2 = true;
    }
    public override void AddRecipes() => CreateCubeRecipe(ItemID.Gel, 120);
}