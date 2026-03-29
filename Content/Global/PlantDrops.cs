using HendecamMod.Content.Items;

namespace HendecamMod.Content.Global;

public class PlantDrops : GlobalTile
{
    public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        // Check if the tile destroyed is a leaf tile
        if (type == TileID.CorruptThorns || type == TileID.CrimsonThorns)
        {
            if (!fail && !effectOnly)
            {
                // Roll a 1 in 25 chance
                if (Main.rand.Next(15) == 0)
                {
                    // Spawn your custom item at the tile position
                    Item.NewItem(null, i * 16, j * 16, 16, 16, ModContent.ItemType<WeedLeaves>(), Main.rand.Next(2, 4));
                }
            }
        }

        if (type == TileID.JungleThorns)
        {
            if (!fail && !effectOnly)
            {
                // Roll a 1 in 25 chance
                if (Main.rand.Next(20) == 0)
                {
                    // Spawn your custom item at the tile position
                    Item.NewItem(null, i * 16, j * 16, 16, 16, ModContent.ItemType<WeedLeaves>(), Main.rand.Next(2, 4));
                }
            }
        }
        if (type == TileID.JungleVines)
        {
            if (!fail && !effectOnly)
            {
                // Roll a 1 in 25 chance
                if (Main.rand.Next(33) == 0)
                {
                    // Spawn your custom item at the tile position
                    Item.NewItem(null, i * 16, j * 16, 16, 16, ModContent.ItemType<WeedLeaves>(), Main.rand.Next(1, 2));
                }
            }
        }

        // Check if the tile destroyed is a leaf tile
        if (type == TileID.CorruptVines || type == TileID.CrimsonVines)
        {
            if (!fail && !effectOnly)
            {
                // Roll a 1 in 25 chance
                if (Main.rand.Next(20) == 0)
                {
                    // Spawn your custom item at the tile position
                    Item.NewItem(null, i * 16, j * 16, 16, 16, ModContent.ItemType<WeedLeaves>(), Main.rand.Next(1, 3));
                }
            }
        }
    }
}