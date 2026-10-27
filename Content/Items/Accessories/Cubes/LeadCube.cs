using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public class LeadCube : IronCube
{
    protected override int Value => 29000;
    public override void AddRecipes() => CreateCubeRecipe(ItemID.LeadBar);
}