using HendecamMod.Content.Global;
using HendecamMod.Content.Items;
using HendecamMod.Content.Items.Accessories;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace HendecamMod.Content.NPCs;

public class FlyingPig : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.PigronHallow];

        NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.ExplosiveBunny;

        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers
        {
            Velocity = 1f 
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
    }

    public override void SetDefaults()
    {
        NPC.width = 70;
        NPC.height = 50;
        NPC.damage = 22;
        NPC.defense = 8;
        NPC.lifeMax = 165;
        NPC.HitSound = SoundID.NPCHit10;
        NPC.DeathSound = SoundID.NPCDeath20;
        NPC.value = 99999f;
        NPC.knockBackResist = 0.525f;
        NPC.aiStyle = NPCAIStyleID.DemonEye;
        NPC.noGravity = true;
        NPC.despawnEncouraged = false;

        AIType = NPCID.PigronHallow; // Use vanilla zombie's type when executing AI code. (This also means it will try to despawn during daytime)
        AnimationType = NPCID.PigronHallow; // Use vanilla zombie's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
        Banner = Type;
        BannerItem = ModContent.ItemType<FlyingPigBanner>();
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement("\"Terrarian folklore says that these strange, pig-like creatures with mouths sewn shut are reincarnations of evil and greedy people who've died. This form is a punishment for those souls.\" "),
            new FlavorTextBestiaryInfoElement("")
        });
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ItemID.Ruby, 3, 5, 10));
        npcLoot.Add(ItemDropRule.Common(ItemID.Diamond, 3, 4, 8));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<KulakWings>(), 20));
        npcLoot.Add(ItemDropRule.Common(ItemID.GoldBar, 2, 13, 21));
        npcLoot.Add(ItemDropRule.Common(ItemID.PlatinumBar, 2, 12, 19));
        npcLoot.Add(ItemDropRule.ByCondition(new HardmodeDrop(), ItemID.CoinGun, chanceDenominator: 4999, chanceNumerator: 2));
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        return SpawnCondition.Overworld.Chance * 0.0095f;
    }
}