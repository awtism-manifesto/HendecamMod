namespace HendecamMod.Content.Items.Placeables.PlayingCards;

public class SevenHearts : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
        ItemID.Sets.SortingPriorityMaterials[Type] = 2;
        Item.rare = ItemRarityID.Blue;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(TileType<Tiles.PlayingCards.SevenHearts>());
        Item.width = 20;
        Item.height = 20;
        Item.value = 1;
    }
}