using HendecamMod.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items;

public class PyriteOre : ModItem
{
    public override void SetDefaults()
    {
        Item.useTime = 10;
        Item.useAnimation = 10;
        Item.autoReuse = true;
        Item.rare = ItemRarityID.White;
        Item.value = Item.buyPrice(copper: 555);
        Item.consumable = true;
        Item.maxStack = 9999;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.DefaultToPlaceableTile(ModContent.TileType<PyriteOreTile>(), 0);
    }
}