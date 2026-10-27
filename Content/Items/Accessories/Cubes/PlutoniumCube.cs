using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class PlutoniumCube : BaseCube
{
    protected override int Rarity => ItemRarityID.LightPurple;
    protected override int Value => 181000;
    protected override int Defense => 10;

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.accRunSpeed *= Main.rand.NextFloat(0.01f, 5f);
        player.moveSpeed *= Main.rand.NextFloat(0.01f, 5f);
        player.runAcceleration *= Main.rand.NextFloat(0.01f, 5f);
    }

    public override void AddRecipes() => CreateCubeRecipe(ModContent.ItemType<PlutoniumBar>());
}
