using HendecamMod.Content.Tiles.Blocks;

namespace HendecamMod.Content.Items.Placeables;

public class MorbiumOre : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<MorbiumOrePlaced>());
        Item.width = 12;
        Item.height = 12;
        Item.rare = ItemRarityID.Yellow;
        Item.value = 4350;
    }
}