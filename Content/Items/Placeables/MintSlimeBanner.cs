using HendecamMod.Content.Tiles;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Placeables;

public class MintSlimeBanner : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<EnemyBanner>(), (int)EnemyBanner.StyleID.MintSlime);
        Item.width = 10;
        Item.height = 24;
        Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(silver: 10));
    }
}