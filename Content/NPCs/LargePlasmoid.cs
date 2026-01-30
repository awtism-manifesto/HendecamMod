using HendecamMod.Common.Systems;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Dusts;
using HendecamMod.Content.Items;
using Humanizer;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace HendecamMod.Content.NPCs;

// Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
public class LargePlasmoid : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Harpy];

        NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.Pixie;

        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
        { // Influences how the NPC looks in the Bestiary
            Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<RadPoisoning>()] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<RadPoisoning2>()] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<RadPoisoning3>()] = true;
    }

    public override void SetDefaults()
    {
        NPC.width = 150;
        NPC.height = 100;
        NPC.damage = 89;
        NPC.defense = 5;
        NPC.lifeMax = 5950;
        NPC.HitSound = SoundID.NPCHit44;
        NPC.DeathSound = SoundID.NPCDeath39;
        NPC.value = 95000f;
        NPC.knockBackResist = 0.03f;
        NPC.aiStyle = 63; // slime ai
        NPC.noGravity = true;
        AIType = NPCID.Flocko; // Use vanilla zombie's type when executing AI code. (This also means it will try to despawn during daytime)
        AnimationType = NPCID.Harpy; // Use vanilla zombie's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
        NPC.despawnEncouraged = false;
        NPC.noTileCollide = true;
        Banner = Type;
        BannerItem = ModContent.ItemType<LargePlasmoidBanner>();

        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica))
        {
            NPC.lifeMax = 5950;
            NPC.damage = 96;
        }
    }
    public override void AI()
    {
        Lighting.AddLight(NPC.Center, 1.5f, 0.575f, 2f);
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
        for (int i = 0; i < 10; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, ModContent.DustType<PlutoniumDust>());
            dust.noGravity = true;
            dust.velocity *= 9.5f;
            dust.scale *= 1.55f;
        }

        int buffType = ModContent.BuffType<RadPoisoning2>();
        // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

        int timeToAdd = (int)(Main.rand.NextFloat(6, 7) * 30); // This makes it 5 seconds, one second is 60 ticks
        target.AddBuff(buffType, timeToAdd);
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
			
				

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("\"Like their normal-sized counterparts, Large Plasmoids feed on radiation. Unlike normal Plasmoids, however, they seem to return to the edge of space when it's daytime\" "),

				// You can add multiple elements if you really wanted to
				// You can also use localization keys (see Localization/en-US.lang)
				new FlavorTextBestiaryInfoElement("")
        });
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PlutoniumOre>(), 1, 121, 166));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheXRay>(), 10));
        npcLoot.Add(ItemDropRule.Common(ItemID.FallenStar, 4, 5, 10));

        npcLoot.Add(ItemDropRule.Common(ItemID.SoulofFlight, 3, 15, 30));


    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (!Main.dayTime & ApacheElfShipDown.downedApacheElfShip & NPC.downedEmpressOfLight)
        {
            return SpawnCondition.Sky.Chance * 0.02f;
        }
        if (!Main.dayTime & ApacheElfShipDown.downedApacheElfShip)
        {
            return SpawnCondition.Sky.Chance * 0.095f;
        }
        else
            return SpawnCondition.Sky.Chance * 0f;
    }

}