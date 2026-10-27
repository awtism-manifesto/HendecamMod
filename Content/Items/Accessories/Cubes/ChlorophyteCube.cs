using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class ChlorophyteCube : BaseCube
{
    protected override int Rarity => ItemRarityID.Lime;
    protected override int Value => 285000;
    protected override int Defense => 5;

    public override void UpdateAccessory(Player player, bool hideVisual) =>
        player.statLifeMax2 += 75;


    public override void AddRecipes() => CreateCubeRecipe(ItemID.ChlorophyteBar);
}