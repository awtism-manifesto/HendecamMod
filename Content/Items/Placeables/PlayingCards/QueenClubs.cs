namespace HendecamMod.Content.Items.Placeables.Uno;

public class QueenClubs : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
        ItemID.Sets.SortingPriorityMaterials[Type] = 2;
        Item.rare = ItemRarityID.Blue;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(TileType<Tiles.Uno.QueenClubs>());
        Item.width = 20;
        Item.height = 20;
        Item.value = 1;
    }
}