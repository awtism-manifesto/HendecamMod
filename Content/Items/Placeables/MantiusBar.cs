using HendecamMod.Content.Tiles.Furniture;

namespace HendecamMod.Content.Items.Placeables;

public class MantiusBar : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
        ItemID.Sets.SortingPriorityMaterials[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(TileType<MantiusBarPlaced>());
        Item.width = 20;
        Item.height = 20;
        Item.value = 14150;
    }

    public override void AddRecipes()
    {
       
    }
}