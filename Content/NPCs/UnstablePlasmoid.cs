using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace HendecamMod.Content.NPCs
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class UnstablePlasmoid : ModNPC
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
            NPC.width = 60;
            NPC.height = 60;
            NPC.damage = 128;
            NPC.defense = 1;
            NPC.lifeMax = 11990;
            NPC.HitSound = SoundID.NPCHit44;
            NPC.DeathSound = SoundID.NPCDeath39;
            NPC.value = 300000f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 63; // slime ai
            NPC.noGravity = true;
            NPC.despawnEncouraged = false;
            NPC.noTileCollide = true;
            AIType = NPCID.Flocko; // Use vanilla zombie's type when executing AI code. (This also means it will try to despawn during daytime)
            AnimationType = NPCID.Harpy; // Use vanilla zombie's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
            Banner = Type;
            BannerItem = ModContent.ItemType<UnstablePlasmoidBanner>();
            if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica))
            {
                NPC.lifeMax = 13295;
                NPC.damage = 145;
            }
        }
        public override void AI()
        {
            Lighting.AddLight(NPC.Center, 2.5f, 0.25f, 0.8f);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            // Here we can make things happen if this NPC hits a player via its hitbox (not projectiles it shoots, this is handled in the projectile code usually)
            // Common use is applying buffs/debuffs:
            int buffType = ModContent.BuffType<RadPoisoning>();
            int buff2Type = ModContent.BuffType<RadPoisoning2>();
            int buff3Type = ModContent.BuffType<RadPoisoning3>();
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

            int timeToAdd = (int)(Main.rand.NextFloat(6, 7) * 30); // This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);
            int time2Add = (int)(Main.rand.NextFloat(6, 7) * 30); // This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buff2Type, time2Add);
            int time3Add = (int)(Main.rand.NextFloat(6, 7) * 30); // This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buff3Type, time3Add);
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
			
				

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("\"TerMerican folklore says that even the Moon Lord trembles in fear at the destructive potential of Unstable Plasmoids. Studying them may be the key to major technological advancements...\" "),

				// You can add multiple elements if you really wanted to
				// You can also use localization keys (see Localization/en-US.lang)
				new FlavorTextBestiaryInfoElement("")
            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AstatineOre>(), 1, 191, 251));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AstatineMarksmanRifle>(), 18));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AstatinePolearm>(), 18));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PlutoniumOre>(), 4, 101, 179));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<UraniumOre>(), 4, 41, 79));
            npcLoot.Add(ItemDropRule.Common(ItemID.SoulofFlight, 4, 19, 39));
            npcLoot.Add(ItemDropRule.Common(ItemID.FallenStar, 4, 15, 25));
           
          
           
            npcLoot.Add(ItemDropRule.Common(ItemID.Ectoplasm, 4, 9, 19));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!Main.dayTime  & NPC.downedEmpressOfLight)
            {
                return SpawnCondition.Sky.Chance * 0.07f;
            }
            else
                return SpawnCondition.Sky.Chance * 0f;
        }

    }
}