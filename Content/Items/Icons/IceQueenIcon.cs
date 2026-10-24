using System.Collections.Generic;
using HendecamMod.Content.Rarities;

namespace HendecamMod.Content.Items.Icons;

public class IceQueenIcon : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 3;
    }

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.rare = RarityType<DarkGreen>();
        Item.value = 750000;
        Item.maxStack = 9999;
    }
}