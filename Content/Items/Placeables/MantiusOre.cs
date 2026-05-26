using HendecamMod.Content.Tiles.Blocks;

namespace HendecamMod.Content.Items.Placeables;

public class MantiusOre : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;

       
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(TileType<MantiusOrePlaced>());
        Item.width = 12;
        Item.height = 12;
        Item.rare = ItemRarityID.Orange;
        Item.value = 5195;
    }
}