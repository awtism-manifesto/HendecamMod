namespace HendecamMod.Content.Items.Placeables.Uno;

public class BlueThree : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
        ItemID.Sets.SortingPriorityMaterials[Type] = 2;
        Item.rare = ItemRarityID.Blue;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Uno.BlueThree>());
        Item.width = 20;
        Item.height = 20;
        Item.value = 1;
    }
}