using System.Collections.Generic;

namespace HendecamMod.Content.Items.Placeables;

public class AlpineTrophy : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(TileType<Tiles.Furniture.AlpineTrophyPlaced>());

        Item.width = 32;
        Item.height = 32;
        Item.rare = ItemRarityID.Blue;
        Item.value = 555;
    }
}