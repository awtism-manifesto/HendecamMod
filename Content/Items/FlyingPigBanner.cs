using HendecamMod.Content.Tiles;
using Terraria.Enums;

namespace HendecamMod.Content.Items;

public class FlyingPigBanner : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(TileType<BannerTile>(), (int)BannerTile.StyleID.FlyingPig);
        Item.width = 10;
        Item.height = 24;
        Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(silver: 10));
    }
}