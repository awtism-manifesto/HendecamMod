using HendecamMod.Content.Items.Other;
using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;

namespace HendecamMod.Content.Items.Consumables;

public class EpilepsyWarning : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
        ItemID.Sets.SortingPriorityMaterials[Type] = 2;
        Item.rare = ItemRarityID.Expert;
    }

    public override void SetDefaults()
    {
       
        Item.width = 20;
        Item.height = 20;
        Item.value = 2;
    }

    public override bool CanRightClick()
    {
        return true;
    }

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<SignedEpilepsyWarning>()));
    }
}