using HendecamMod.Content.Tiles.PlayingCards;
namespace HendecamMod.Content.Items.Placeables.PlayingCards;

public class TwoSpades : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
        ItemID.Sets.SortingPriorityMaterials[Type] = 2;
        Item.rare = ItemRarityID.Blue;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(TileType<Tiles.PlayingCards.TwoSpades>());
        Item.width = 20;
        Item.height = 20;
        Item.value = 1;
    }
}