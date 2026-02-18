using HendecamMod.Common.Systems;
using HendecamMod.Content.Global;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.NPCs.Enemies;
using Terraria.GameContent.ItemDropRules;

namespace HendecamMod.Content.NPCs.Bosses;

[AutoloadBossHead]
public class EyesOfMichael : ModNPC
    {
    private int nextSpawnTick;
    private int tickCounter;

    public override void SetStaticDefaults()
        {
        Main.npcFrameCount[Type] = 6;
        NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
        }

    public override void SetDefaults()
        {
        NPC.damage = 225;
        NPC.defense = 75;
        NPC.lifeMax = 300000;
        NPC.width = 60;
        NPC.height = 60;
        NPC.scale = 1f;

        NPC.oldVelocity = NPC.oldVelocity * 1.25f;
        NPC.velocity = NPC.velocity * 1.25f;
        NPC.knockBackResist = 0;

        NPC.HitSound = SoundID.NPCHit4;
        NPC.DeathSound = SoundID.NPCDeath14;

        NPC.despawnEncouraged = false;
        NPC.noGravity = true;
        NPC.noTileCollide = true;

        NPC.SpawnWithHigherTime(30);
        NPC.boss = true;
        NPC.npcSlots = 10f;

        NPC.aiStyle = NPCAIStyleID.EyeOfCthulhu;
        if (!Main.dedServ)
            {
            Music = MusicID.OtherworldlyBoss2;
            }
        }

    public override void FindFrame(int frameHeight)
        {
        Main.npcFrameCount[Type] = 6;
        AnimationType = NPCID.EyeofCthulhu;
        }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
        if (NPC.AnyNPCs(ModContent.NPCType<EyesOfGabriel>()) || NPC.AnyNPCs(ModContent.NPCType<EyesOfRaphael>()))
            {

            }
        else
            {
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            // notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MinionBossMask>(), 7));
            npcLoot.Add(ItemDropRule.ByCondition(new MichaelDrop(), ModContent.ItemType<AngelShard>(), chanceDenominator: 1, chanceNumerator: 1, minimumDropped: 67 / 10, maximumDropped: 69));
            npcLoot.Add(notExpertRule);
            // npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<AlpineTreasureBag>()));
            // npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Placeable.Furniture.MinionBossRelic>()));
            // npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<MinionBossPetItem>(), 4));
            }
        }
    public override void OnKill()
        {
        NPC.SetEventFlagCleared(ref BossDownedSystem.downedPromethiumPlasmoid, -1);
        }

    public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
        cooldownSlot = ImmunityCooldownID.Bosses;
        return true;
        }

    public override void AI()
        {
        NPC.active = true;
        if (nextSpawnTick == 0)
            {
            nextSpawnTick = Main.rand.Next(600, 600);
            }

        Lighting.AddLight(NPC.Center, 0.8f, 0.8f, 2.5f);
        tickCounter++;

        if (tickCounter == nextSpawnTick)
            {
            NPC.TargetClosest();
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                {
                var source = NPC.GetSource_FromAI();
                Vector2 position = NPC.Center;
                Vector2 targetPosition = Main.player[NPC.target].Center;
                Vector2 direction = targetPosition - position;
                NPC.NewNPCDirect(source, (int)NPC.Center.X, (int)NPC.Center.Y, (ModContent.NPCType<HeavenCloud>()), NPC.whoAmI);
                direction.Normalize();
                if (!NPC.AnyNPCs(ModContent.NPCType<EyesOfGabriel>()))
                    {

                    }
                if (!NPC.AnyNPCs(ModContent.NPCType<EyesOfRaphael>()))
                    {

                    }
                }
            tickCounter = 0;
            nextSpawnTick = Main.rand.Next(600, 600);
            }
        }
    }