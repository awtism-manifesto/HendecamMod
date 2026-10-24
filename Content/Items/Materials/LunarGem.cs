using System.Collections.Generic;

namespace HendecamMod.Content.Items.Materials;

public class LunarGem : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.rare = ItemRarityID.Blue;
        Item.value = 500;
        Item.maxStack = 9999;
    }
}