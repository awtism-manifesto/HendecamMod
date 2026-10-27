using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class AstatineCube : BaseCube
{
    protected override int Rarity => ItemRarityID.Red;
    protected override int Value => 1230000;
    protected override int Defense => 15;

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.maxFallSpeed *= Main.rand.NextFloat(-0.5f, 5f);
        player.accRunSpeed *= Main.rand.NextFloat(0.01f, 5f);
        player.moveSpeed *= Main.rand.NextFloat(0.01f, 5f);
        player.runAcceleration *= Main.rand.NextFloat(0.01f, 5f);
    }

    public override void AddRecipes() =>
        CreateCubeRecipe(ModContent.ItemType<AstatineBar>(), 12, ModContent.TileType<CultistCyclotronPlaced>());
}