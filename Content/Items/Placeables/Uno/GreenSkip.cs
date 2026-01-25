using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Placeables.Uno;

public class GreenSkip : ModItem
    {
    public override void SetStaticDefaults()
        {
        Item.ResearchUnlockCount = 25;
        ItemID.Sets.SortingPriorityMaterials[Type] = 2;
        Item.rare = ItemRarityID.Green;
        }
    public override void SetDefaults()
        {
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Uno.GreenSkip>());
        Item.width = 20;
        Item.height = 20;
        Item.value = 1;
        }
    }
