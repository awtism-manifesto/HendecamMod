using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class MintalCube : BaseCube
{
    protected override int Rarity => ItemRarityID.LightRed;
    protected override int Value => 181000;
    protected override int Defense => 5;

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.accRunSpeed *= 20f;
        player.moveSpeed *= 20f;
    }
        

    public override void AddRecipes() => CreateCubeRecipe(ModContent.ItemType<MintalBar>());
}
