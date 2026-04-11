using HendecamMod.Content.Items.Weapons;
using HendecamMod.Content.Items.Weapons.Multiclass;
using Terraria.GameContent.ItemDropRules;

namespace HendecamMod.Content.Items.Consumables;

public class AlpineTreasureBag : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.BossBag[Type] = true;
        // ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
        // This makes boss bags not drop dev armor
        Item.ResearchUnlockCount = 3;
    }

    public override void SetDefaults()
    {
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.width = 24;
        Item.height = 24;
        Item.rare = ItemRarityID.Purple;
        Item.expert = true;
    }

    public override bool CanRightClick()
    {
        return true;
    }

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDropRule.Common(ItemID.GreaterHealingPotion, 1, 7, 15));
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<ApexPlasmaCannon>(), 3));
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<RazorRotors>(), 3));
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<AlpinePlushieGlitterbomb>(), 3));
        itemLoot.Add(ItemDropRule.Common(ItemType<fivenato>(), 1, 100, 1000));
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<CandyHeart>()));
    }
}