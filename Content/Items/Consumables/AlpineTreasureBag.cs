using HendecamMod.Content.Items;
using HendecamMod.Content.Items.Weapons;
using HendecamMod.Content.NPCs.Bosses;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Consumables
{
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

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ApexPlasmaCannon>(), 5));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<fivenato>(), 1, 60, 70));
        }
    }
}
