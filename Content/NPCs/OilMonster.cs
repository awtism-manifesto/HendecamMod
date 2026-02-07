using HendecamMod.Content.Items;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace HendecamMod.Content.NPCs;

public class OilMonster : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.BloodZombie];

        NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.Demon;

        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers
        {
            Velocity = 1f 
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            // Sets your NPC's flavor text in the bestiary.
            new FlavorTextBestiaryInfoElement("\"Super Spoopy, Extremely Oopy, and INCREDIBLY goopy, these speedy little bastards will ruin your day if you're not careful\" "),

            // You can add multiple elements if you really wanted to
            // You can also use localization keys (see Localization/en-US.lang)
            new FlavorTextBestiaryInfoElement("")
        });
    }

    public override void SetDefaults()
    {
        NPC.width = 24;
        NPC.height = 36;
        NPC.damage = 79;
        NPC.defense = 13;
        NPC.lifeMax = 515;
        NPC.HitSound = SoundID.NPCHit41;
        NPC.DeathSound = SoundID.NPCDeath39;
        NPC.value = 9770f;
        NPC.knockBackResist = 0.2f;
        NPC.aiStyle = NPCAIStyleID.Fighter; 

        AIType = NPCID.WalkingAntlion; // Use vanilla zombie's type when executing AI code. (This also means it will try to despawn during daytime)
        AnimationType = NPCID.BloodZombie; // Use vanilla zombie's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
        Banner = Type;
        BannerItem = ModContent.ItemType<OilMonsterBanner>();
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RefinedOil>(), 2, 9, 19));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CrudeOil>(), 1, 31, 61));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FerrousThornSmooth>(), 33));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FerrousThornSpiky>(), 33));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheOilBaron>(), 16));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OilMonsterStaff>(), 25));
        npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 49));
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (Main.hardMode)
        {
            return SpawnCondition.OverworldDayDesert.Chance * 0.6f;
        }

        return SpawnCondition.DesertCave.Chance * 0.00f;
    }
}