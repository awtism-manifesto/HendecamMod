using HendecamMod.Content.Items;
using HendecamMod.Content.Items.Placeables;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace HendecamMod.Content.NPCs;

// Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
public class MorbiumWarrior : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.PossessedArmor];

        NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.PossessedArmor;

        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
        { // Influences how the NPC looks in the Bestiary
            Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
			
				

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("\"They're here to usher in Morbin Time. Fast and erratic. Do not trust them.\" "),

				// You can add multiple elements if you really wanted to
				// You can also use localization keys (see Localization/en-US.lang)
				new FlavorTextBestiaryInfoElement("")
        });
    }
    public override void SetDefaults()
    {
        NPC.width = 24;
        NPC.height = 36;
        NPC.damage = 91;
        NPC.defense = 13;
        NPC.lifeMax = 1670;
        NPC.HitSound = SoundID.NPCHit4;
        NPC.DeathSound = SoundID.NPCDeath39;
        NPC.value = 6700f;
        NPC.knockBackResist = 0f;
        NPC.aiStyle = 3; // slime ai

        AIType = NPCID.WalkingAntlion; // Use vanilla zombie's type when executing AI code. (This also means it will try to despawn during daytime)
        AnimationType = NPCID.PossessedArmor; // Use vanilla zombie's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
        Banner = Type;
        BannerItem = ModContent.ItemType<OilMonsterBanner>();

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
        else
            return SpawnCondition.DesertCave.Chance * 0.00f;

    }

}