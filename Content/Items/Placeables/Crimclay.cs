using HendecamMod.Content.Tiles;

namespace HendecamMod.Content.Items.Placeables;

public class Crimclay : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<CrimclayPlaced>());
        Item.width = 12;
        Item.height = 12;
    }
}