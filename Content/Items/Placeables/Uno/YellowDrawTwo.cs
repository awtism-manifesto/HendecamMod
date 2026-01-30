namespace HendecamMod.Content.Items.Placeables.Uno;

public class YellowDrawTwo : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
        ItemID.Sets.SortingPriorityMaterials[Type] = 2;
        Item.rare = ItemRarityID.Yellow;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Uno.YellowDrawTwo>());
        Item.width = 20;
        Item.height = 20;
        Item.value = 1;
    }
}