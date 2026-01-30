using HendecamMod.Content.Tiles;

namespace HendecamMod.Content.Items.Placeables;

public class Pearlclay : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<PearlclayPlaced>());
        Item.width = 12;
        Item.height = 12;
    }
}