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

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "WARNING: CERTAIN EFFECTS IN HENDECAM MOD MAY CAUSE ISSUES FOR PEOPLE WITH EPILEPSY!!")
        {
            Color = new Color(255, 0, 0)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Specifically, Stupid-class weapons have an effect called the Lobotometer.")
        {
            Color = new Color(255, 0, 0)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "The Lobotometer applies an increasingly annoying rainbow shader to the player's screen as Stupid weapons are used more.")
        {
            Color = new Color(255, 0, 0)
        };
        tooltips.Add(line);
        if (Main.zenithWorld)
        {
            line = new TooltipLine(Mod, "Face", "The shader is MUCH more intense when playing in a GetFixedBoi world.")
            {
                Color = new Color(255, 0, 0)
            };
            tooltips.Add(line);
        }
       
        line = new TooltipLine(Mod, "Face", "Certain Omni-class weapons also use this mechanic.")
        {
            Color = new Color(255, 0, 0)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "We have NOT made an 'Accessible Lobotometer' config **yet** but it will be added soon. My sincerest apologies to any epileptic folks playing the mod.")
        {
            Color = new Color(255, 0, 0)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "If you actually read all this, you can right-click this item to get the Signed Epilepsy Warning, which sells for a pretty penny or shimmers into Monster Stem Cells")
        {
            Color = new Color(255, 0, 0)
        };
        tooltips.Add(line);
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