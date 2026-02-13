using HendecamMod.Content.Buffs;
using HendecamMod.Content.Dusts;
using HendecamMod.Content.Global;
using HendecamMod.Content.Items;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace HendecamMod.Content.NPCs;

// Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
public class Plasmoid : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Pixie];

        NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.Pixie;

        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers
        {
            // Influences how the NPC looks in the Bestiary
            Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<RadPoisoning>()] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<RadPoisoning2>()] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<RadPoisoning3>()] = true;
    }

    public override void SetDefaults()
    {
        NPC.width = 30;
        NPC.height = 30;
        NPC.damage = 41;
        NPC.defense = 3;
        NPC.lifeMax = 285;
        NPC.HitSound = SoundID.NPCHit44;
        NPC.DeathSound = SoundID.NPCDeath39;
        NPC.value = 10000f;
        NPC.knockBackResist = 0.35f;
        NPC.aiStyle = NPCAIStyleID.Bat; 
        NPC.noTileCollide = true;
        AIType = NPCID.CaveBat; // Use vanilla zombie's type when executing AI code. (This also means it will try to despawn during daytime)
        AnimationType = NPCID.Pixie; // Use vanilla zombie's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
        Banner = Type;
        BannerItem = ModContent.ItemType<PlasmoidBanner>();
        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica))
        {
            NPC.lifeMax = 325;
            NPC.damage = 45;
        }
    }

    public override void AI()
    {
        Lighting.AddLight(NPC.Center, 0.33f, 1, 0.33f);
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
        for (int i = 0; i < 7; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, ModContent.DustType<UraniumDust>());
            dust.noGravity = true;
            dust.velocity *= 7.5f;
            dust.scale *= 1.25f;
        }

        int buffType = ModContent.BuffType<RadPoisoning>();
        // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

        int timeToAdd = (int)(Main.rand.NextFloat(6, 7) * 30); // This makes it 5 seconds, one second is 60 ticks
        target.AddBuff(buffType, timeToAdd);
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            // Sets your NPC's flavor text in the bestiary.
            new FlavorTextBestiaryInfoElement("\"These odd, barely-corporeal creatures appear to feed on radiation. Their intelligence level is unknown\" "),

            // You can add multiple elements if you really wanted to
            // You can also use localization keys (see Localization/en-US.lang)
            new FlavorTextBestiaryInfoElement("")
        });
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ItemID.FallenStar, 4, 2, 5));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<UraniumOre>(), 1, 34, 69));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PlasmoidWand>(), 15));
        npcLoot.Add(ItemDropRule.ByCondition(new HardmodeDrop(), ModContent.ItemType<RadBullet>(), 4, 40, 120));
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (NPC.downedMechBoss1 & NPC.downedMechBoss2 & NPC.downedMechBoss3 & NPC.downedEmpressOfLight)
        {
            return SpawnCondition.Sky.Chance * 0.13f;
        }

        if (NPC.downedMechBoss1 & NPC.downedMechBoss2 & NPC.downedMechBoss3)
        {
            return SpawnCondition.Sky.Chance * 0.15f;
        }

        if (NPC.downedDeerclops & NPC.downedBoss2)
        {
            return SpawnCondition.Sky.Chance * 0.51f;
        }

        if (NPC.downedDeerclops)
        {
            return SpawnCondition.Sky.Chance * 0.43f;
        }

        if (NPC.downedBoss2)
        {
            return SpawnCondition.Sky.Chance * 0.43f;
        }

        return SpawnCondition.Sky.Chance * 0.00f;
    }
}