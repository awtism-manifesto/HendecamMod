using HendecamMod.Content.Items;
using HendecamMod.Content.Items.Placeables;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace HendecamMod.Content.NPCs;

// Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
public class MorbiumWarrior : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.PossessedArmor];

        NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.PossessedArmor;

        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers
        {
            Velocity = 1f 
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement("\"They're here to usher in Morbin Time. Fast and erratic. Do not trust them, they were locked away in the dungeon for a reason\" "),

            new FlavorTextBestiaryInfoElement("")
        });
    }

    public override void SetDefaults()
    {
        NPC.width = 24;
        NPC.height = 36;
        NPC.damage = 82;
        NPC.defense = 39;
        NPC.lifeMax = 985;
        NPC.HitSound = SoundID.NPCHit4;
        NPC.DeathSound = SoundID.NPCDeath39;
        NPC.value = 6700f;
        NPC.knockBackResist = 0f;
        NPC.aiStyle = NPCAIStyleID.Fighter; 
        AIType = NPCID.WalkingAntlion;
        AnimationType = NPCID.PossessedArmor; 
        Banner = Type;
        BannerItem = ModContent.ItemType<MorbiumWarriorBanner>();
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MorbiumOre>(), 1, 11, 33));
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (NPC.downedPlantBoss)
        {
            return SpawnCondition.Dungeon.Chance * 0.55f;
        }

        return SpawnCondition.DesertCave.Chance * 0.00f;
    }
}