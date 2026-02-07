using HendecamMod.Content.Items.Placeables;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace HendecamMod.Content.NPCs;

public class MintSlime : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.BlueSlime];

        NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.ShimmerSlime;

        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers
        {
            Velocity = 1f 
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
    }

    public override void SetDefaults()
    {
        NPC.width = 34;
        NPC.height = 24;
        NPC.damage = 55;
        NPC.defense = 25;
        NPC.lifeMax = 170;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.value = 1075f;
        NPC.knockBackResist = 0.7f;
        NPC.aiStyle = NPCAIStyleID.Slime;

        AIType = NPCID.BlueSlime; 
        AnimationType = NPCID.GreenSlime; 
        Banner = Type;
        BannerItem = ModContent.ItemType<MintSlimeBanner>();
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        var zombieDropRules = Main.ItemDropsDB.GetRulesForNPCID(NPCID.GreenSlime, false); // false is important here
        foreach (var zombieDropRule in zombieDropRules)
        {
            npcLoot.Add(zombieDropRule);
        }
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MintalOre>(), 1, 9, 23));
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        return SpawnCondition.OverworldHallow.Chance * 0.8f;
    }
}