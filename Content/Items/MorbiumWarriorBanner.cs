using Terraria.Enums;
using Terraria.ModLoader;
using Terraria;
using HendecamMod.Content.Tiles;

namespace HendecamMod.Content.Items;

public class MorbiumWarriorBanner : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<BannerTile>(), (int)BannerTile.StyleID.MorbiumWarrior);
        Item.width = 10;
        Item.height = 24;
        Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(silver: 10));
    }
}
