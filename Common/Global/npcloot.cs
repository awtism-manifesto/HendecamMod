using HendecamMod.Content.Items.Accessories.Rampart;
using HendecamMod.Content.Items.Consumables;
using HendecamMod.Content.Items.Weapons.Ranger;
using HendecamMod.Content.NPCs.Bosses;
using Terraria.GameContent.ItemDropRules;

namespace HendecamMod.Common.Global;

public class MagUrLazy : GlobalNPC
    {
    public override void AI(NPC npc)
        {
        if (npc.type == NPCID.ServantofCthulhu)
            {
            if (NPC.AnyNPCs(NPCType<EyesOfMichael>()))
                {
                npc.SetDefaults(0);
                npc.active = false;
                }
            }
        }
    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
        if (npc.type == NPCID.Spazmatism)
            {
            npcLoot.Add(ItemDropRule.Common(ItemType<ElementalFlameCore>(), chanceDenominator: 2));
            }

        if (npc.type == NPCID.MoonLordCore)
            {
            npcLoot.Add(ItemDropRule.Common(ItemType<LeechRepellant>(), chanceDenominator: 2));
            }

        if (npc.type == NPCID.WallofFlesh)
            {
            npcLoot.Add(ItemDropRule.Common(ItemID.DemonHeart, 2));
            }

        if (npc.type == NPCID.ElfCopter)
            {
            npcLoot.Add(ItemDropRule.Common(ItemType<CandyHeart>(), chanceDenominator: 10));
            }

        if (npc.type == NPCID.ElfArcher)
            {
            npcLoot.Add(ItemDropRule.Common(ItemType<CandyHeart>(), chanceDenominator: 10));
            npcLoot.Add(ItemDropRule.Common(ItemType<CandyCaneCompoundBow>(), chanceDenominator: 20));
            }
        }
    }