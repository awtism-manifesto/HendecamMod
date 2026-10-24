using System.Collections.Generic;

namespace HendecamMod.Content.Items.Materials;

public class AutismOrb : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
        ItemID.Sets.ItemIconPulse[Item.type] = true;
    }

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.rare = ItemRarityID.Blue;
        Item.value = 160000;
        Item.maxStack = 9999;
    }
}