using HendecamMod.Content.Tiles.Blocks;

namespace HendecamMod.Content.Items.Placeables;

public class PromethiumOre : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<PromethiumOreTile>());
        Item.width = 12;
        Item.height = 12;
        Item.rare = ItemRarityID.Purple;
        Item.value = 3500;
    }
}