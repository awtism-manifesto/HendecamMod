using HendecamMod.Content.Tiles;
using Terraria.Enums;

namespace HendecamMod.Content.Items.Placeables;

public class LycosporeSlimeBanner : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(TileType<BannerTile>(), (int)BannerTile.StyleID.LycosporeSlime);
        Item.width = 10;
        Item.height = 24;
        Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(silver: 10));
    }
}