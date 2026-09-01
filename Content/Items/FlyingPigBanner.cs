using HendecamMod.Content.NPCs;
using HendecamMod.Content.Tiles;
using Terraria.Enums;

namespace HendecamMod.Content.Items;

public class FlyingPigBanner : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.KillsToBanner[NPCType<FlyingPig>()] = 10;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(TileType<BannerTile>(), (int)BannerTile.StyleID.FlyingPig);
        Item.width = 10;
        Item.height = 24;
        Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(silver: 10));
    }
}