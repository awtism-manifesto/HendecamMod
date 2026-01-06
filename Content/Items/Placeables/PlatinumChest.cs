using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HendecamMod.Content.Tiles.Furniture;

namespace HendecamMod.Content.Items.Placeables
{
    public class PlatinumChest : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<PlatinumChestPlaced>());
            Item.width = 26;
            Item.height = 22;
            Item.value = 500;
        }
    }
    // Item to unlock locked platinum chests
    public class PlatinumKey : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 3;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.GoldenKey);
        }
    }
}