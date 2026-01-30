using HendecamMod.Content.Tiles;
using Terraria.Enums;

namespace HendecamMod.Content.Items.Placeables;

public class PearlsandSlimeBanner : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<EnemyBanner>(), (int)EnemyBanner.StyleID.PearlsandSlime);
        Item.width = 10;
        Item.height = 24;
        Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(silver: 10));
    }
}