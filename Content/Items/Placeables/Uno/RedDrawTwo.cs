namespace HendecamMod.Content.Items.Placeables.Uno;

public class RedDrawTwo : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
        ItemID.Sets.SortingPriorityMaterials[Type] = 2;
        Item.rare = ItemRarityID.Red;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Uno.RedDrawTwo>());
        Item.width = 20;
        Item.height = 20;
        Item.value = 1;
    }
}