using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace HendecamMod.Content.NPCs;

public class LunarElemental : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.GraniteFlyer];

        NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.GraniteFlyer;

        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers
        {
            Velocity = 1f 
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
    }

    public override void SetDefaults()
    {
        NPC.width = 34;
        NPC.height = 40;
        NPC.damage = 27;
        NPC.defense = 8;
        NPC.lifeMax = 105;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.value = 550f;
        NPC.knockBackResist = 0.7f;
        NPC.aiStyle = NPCAIStyleID.GraniteElemental; //Don't just type NPCAIStyleID.GraniteElemental like a dumbass

        AIType = NPCID.GraniteFlyer;
        AnimationType = NPCID.GraniteFlyer;
        Banner = Type;
        BannerItem = ModContent.ItemType<LunarElementalBanner>();
    }

    public override void AI()
    {
        Lighting.AddLight(NPC.Center, 0.1f, 0.67f, 0.67f);
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LunarGem>(), 1, 2, 5));
        npcLoot.Add(ItemDropRule.Common(ItemID.CelestialMagnet, 20));
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        return SpawnCondition.Sky.Chance * 0.55f;
    }
}