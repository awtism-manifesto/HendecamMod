using HendecamMod.Content.Tiles;
using Terraria.Enums;

namespace HendecamMod.Content.Items.Placeables;

public class PoisonSlimeBanner : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<EnemyBanner>(), (int)EnemyBanner.StyleID.PoisonSlime);
        Item.width = 10;
        Item.height = 24;
        Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(silver: 10));
    }
}