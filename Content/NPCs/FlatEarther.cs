using HendecamMod.Content.Items;
using HendecamMod.Content.Items.Placeables;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace HendecamMod.Content.NPCs;

// Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
public class FlatEarther : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.BaldZombie];

        

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
            new FlavorTextBestiaryInfoElement("\"They're stupid idiots who dig in their butt constantly and think helping the adderall sniffing cultist will prove their lobotomized ass flat earth theories correct or something idk, these guys are just REALLY dumb and show up during the pillars so that's what i think and i'm the developer of the mod so i guess that is true and oh my god this description is way too long\" "),

            new FlavorTextBestiaryInfoElement("")
        });
    }

    public override void SetDefaults()
    {
        NPC.width = 24;
        NPC.height = 36;
        NPC.damage = 101;
        NPC.defense = 18;
        NPC.lifeMax = 2125;
        NPC.HitSound = SoundID.PlayerHit;
        NPC.DeathSound = SoundID.NPCDeath39;
        NPC.value = 17500f;
        NPC.knockBackResist = 0f;
        NPC.aiStyle = NPCAIStyleID.Fighter; 
        AIType = NPCID.WalkingAntlion;
        AnimationType = NPCID.BaldZombie; 
        Banner = Type;
        BannerItem = ModContent.ItemType<FlatEartherBanner>();
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FragmentFlatEarth>(), 1, 1, 4));
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (NPC.TowerActiveNebula || NPC.TowerActiveVortex || NPC.TowerActiveStardust || NPC.TowerActiveSolar)
        {
            return SpawnCondition.Overworld.Chance * 0.67f;
        }

        return SpawnCondition.DesertCave.Chance * 0.00f;
    }
}