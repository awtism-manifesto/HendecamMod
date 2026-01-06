using HendecamMod.Content.Items.Accessories.Rampart;
using HendecamMod.Content.Items.Consumables;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;


namespace HendecamMod.Common.Global
    {
    public class MagUrLazy : GlobalNPC
        {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
            {
            if (npc.type == NPCID.Spazmatism)
                {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ElementalFlameCore>(), chanceDenominator: 2));
                }
            if (npc.type == NPCID.MoonLordCore)
                {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LeechRepellant>(), chanceDenominator: 2));
                }
            if (npc.type == NPCID.WallofFlesh)
                {
                npcLoot.Add(ItemDropRule.Common(ItemID.DemonHeart, 2));
                }
            if (npc.type == NPCID.ElfCopter)
                {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CandyHeart>(), chanceDenominator: 10));
                }
            if (npc.type == NPCID.ElfArcher)
                {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CandyHeart>(), chanceDenominator: 10));
                }
            }

        }
    }