using HendecamMod.Content.NPCs;
using HendecamMod.Content.Tiles;
using Terraria.Enums;

namespace HendecamMod.Content.Items;

public class UnstablePlasmoidBanner : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.KillsToBanner[NPCType<UnstablePlasmoid>()] = 25;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(TileType<BannerTile>(), (int)BannerTile.StyleID.UnstablePlasmoid);
        Item.width = 10;
        Item.height = 24;
        Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(silver: 10));
    }
}