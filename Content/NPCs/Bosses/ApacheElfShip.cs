using HendecamMod.Common.Systems;
using HendecamMod.Content.Global;
using HendecamMod.Content.Items;
using HendecamMod.Content.Items.Consumables;
using HendecamMod.Content.NPCs.Town.Alpine;
using HendecamMod.Content.Projectiles.Enemies.Boss;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;

namespace HendecamMod.Content.NPCs.Bosses;

[AutoloadBossHead]
public class ApacheElfShip : ModNPC
{


    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 4;
        NPCID.Sets.MPAllowedEnemies[Type] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.BoneJavelin] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.TentacleSpike] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.BloodButcherer] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Shimmer] = true;
        if (Main.hardMode)
        {
            NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
        }
    }

    public override void SetDefaults()
    {
        NPC.damage = 80;
        NPC.defense = 45;
        NPC.lifeMax = 69500;

        NPC.width = 100;
        NPC.height = 100;

        NPC.knockBackResist = 0;

        NPC.HitSound = SoundID.NPCHit4;
        NPC.DeathSound = SoundID.NPCDeath14;

        NPC.noGravity = true;
        NPC.noTileCollide = true;

        NPC.SpawnWithHigherTime(30);
        NPC.boss = true;
        NPC.npcSlots = 10f;

        NPC.aiStyle = NPCAIStyleID.ElfCopter;
        if (!Main.dedServ)
        {
            {
                Music = MusicID.OtherworldlyUGHallow;
            }
        }
    }

    public override void FindFrame(int frameHeight)
    {
        Main.npcFrameCount[Type] = 4;
        AnimationType = NPCID.BlazingWheel;
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        // Do NOT misuse the ModifyNPCLoot and OnKill hooks: the former is only used for registering drops, the latter for everything else

        // The order in which you add loot will appear as such in the Bestiary. To mirror vanilla boss order:
        // 1. Trophy (1/10 chance)
        // 2. Classic Mode ("not expert")
        // 3. Expert Mode (usually just the treasure bag)
        // 4. Master Mode (relic first, pet last, everything else in between)

        LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

        // notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MinionBossMask>(), 7));
        int itemType = ItemType<Fivenato>();
        var parameters = new DropOneByOne.Parameters
        {
            ChanceNumerator = 1,
            ChanceDenominator = 1,
            MinimumStackPerChunkBase = 4,
            MaximumStackPerChunkBase = 6,
            MinimumItemDropsCount = 40,
            MaximumItemDropsCount = 60,
        };

        notExpertRule.OnSuccess(new DropOneByOne(itemType, parameters));

        npcLoot.Add(notExpertRule);

        npcLoot.Add(ItemDropRule.BossBag(ItemType<AlpineTreasureBag>()));

        // npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Placeable.Furniture.MinionBossRelic>()));

        // npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<MinionBossPetItem>(), 4));
    }

    public override void OnKill()
    {
        var source2 = NPC.GetSource_FromAI();
        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("AGH!"), new Color(185, 105, 105));


        AlpineNPCRespawnSystem.unlockedAlpineSpawn = true;
        NPC.NewNPCDirect(source2, (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<Alpine>(), NPC.whoAmI);
        if (!BossDownedSystem.downedApacheElfShip)
        {
            BossDownedSystem.downedApacheElfShip = true;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                GetInstance<PlutoniumSystem>().BlessWorldWithPlutonium();
            }
        }

    }

    public override bool CanHitPlayer(Player target, ref int cooldownSlot)
    {
        cooldownSlot = ImmunityCooldownID.Bosses;
        return true;
    }

    // No more 'private int nextSpawnTick, tickCounter;'

    public override void AI()
    {
        // Constants for state machine
        const int STATE_IDLE = 0;
        const int STATE_ATTACK = 1;

        // 1. Run the Elf Copter movement (base AI style)
        base.AI();

        // 2. Despawn if no target AND not in attack (server only)
        if (!NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[0] == STATE_IDLE)
        {
            NPC.SetDefaults(0);
            NPC.active = false;
            if (BossDownedSystem.downedApacheElfShip)
            {
                var source = NPC.GetSource_FromAI();
                AlpineNPCRespawnSystem.unlockedAlpineSpawn = true;
                NPC.NewNPCDirect(source, (int)NPC.Center.X, (int)NPC.Center.Y,
                    NPCType<Alpine>(), NPC.whoAmI);
            }
            return;
        }

        // 3. Unlock alpine spawn only once (server only)
        if (AlpineNPCRespawnSystem.unlockedAlpineSpawn && Main.netMode != NetmodeID.MultiplayerClient)
            AlpineNPCRespawnSystem.unlockedAlpineSpawn = false;

        // Helper function to reset to idle state
        void FinishAttack()
        {
            NPC.ai[0] = STATE_IDLE;
            NPC.ai[1] = 0;          // tick counter
            NPC.ai[2] = Main.rand.Next(300, 500); // next spawn tick
            NPC.ai[3] = 0;          // loop counter
        }

        // Helper to convert milliseconds to frames (60 fps)
        int DelayFrames(float milliseconds)
        {
            return (int)Math.Max(1, milliseconds / 1000f * 60f);
        }

        // ------------------------------------------------------------------
        // IDLE STATE
        // ------------------------------------------------------------------
        if (NPC.ai[0] == STATE_IDLE)
        {
            // Initialize idle state on first entry
            if (NPC.ai[2] == 0)
            {
                NPC.ai[2] = Main.rand.Next(300, 500);   // nextSpawnTick
                NPC.ai[1] = 0;                          // tickCounter
            }

            NPC.ai[1]++;   // increment frame counter

            if (NPC.ai[1] >= NPC.ai[2])
            {
                // Time to choose an attack
                NPC.TargetClosest();
                if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    // Choose attack (0-4)
                    int attackType = Main.rand.Next(5);
                    NPC.ai[2] = attackType;          // store attack type
                    NPC.ai[3] = 0;                   // initialize loop counter
                    NPC.ai[0] = STATE_ATTACK;        // switch to attack state
                    NPC.ai[1] = 0;                   // reset timer
                }
                else
                {
                    // No target – reset idle timer
                    NPC.ai[2] = Main.rand.Next(300, 500);
                    NPC.ai[1] = 0;
                }
            }
            return;
        }

        // ------------------------------------------------------------------
        // ATTACK STATE
        // ------------------------------------------------------------------
        if (NPC.ai[0] == STATE_ATTACK)
        {
            // Ensure we still have a valid target
            NPC.TargetClosest();
            if (!NPC.HasValidTarget)
            {
                FinishAttack();
                return;
            }

            // If timer > 0, count it down and wait
            if (NPC.ai[1] > 0)
            {
                NPC.ai[1]--;
                return;
            }

            // Timer reached 0 – perform next step of the current attack
            int attackType = (int)NPC.ai[2];
            int spawnsRemaining = (int)NPC.ai[3];
            var source = NPC.GetSource_FromAI();
            Vector2 center = NPC.Center;
            Vector2 targetPos = Main.player[NPC.target].Center;

            // Difficulty scaling
            int difficulty = 1;
            if (Main.expertMode) difficulty++;
            if (Main.masterMode) difficulty++;
            if (Main.getGoodWorld) difficulty++;
            if (Main.zenithWorld) difficulty++;
            if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica))
            {
                difficulty++;
            }
            if (ModLoader.TryGetMod("InfernalEclipseAPI", out Mod HiAkira))
            {
                difficulty++;
            }
            if (ModLoader.TryGetMod("TerrorMod", out Mod Terror))
            {
                difficulty += 3;
            }

                switch (attackType)
            {
                case 0: // Presents
                    if (spawnsRemaining == 0)
                    {
                        // Initialize attack
                        ChatHelper.BroadcastChatMessage(
                            NetworkText.FromLiteral("I'm preparing a few ''gifts'' for you."),
                            new Color(185, 105, 105));
                        spawnsRemaining = 10 * difficulty;
                        NPC.ai[3] = spawnsRemaining;
                        NPC.ai[1] = DelayFrames(500);
                        break;
                    }

                    // Spawn one present projectile
                    Vector2 dirToPlayer = targetPos - center;
                    Projectile.NewProjectile(source, center, dirToPlayer * 0.01f,
                        ProjectileID.Present, 75, 0f, Main.myPlayer);
                    SoundEngine.PlaySound(SoundID.Item42, center);

                    spawnsRemaining--;
                    NPC.ai[3] = spawnsRemaining;

                    if (spawnsRemaining > 0)
                        NPC.ai[1] = DelayFrames(335 / difficulty * 2);
                    else
                        FinishAttack();
                    break;

                case 1: // Plasma bullets
                    int bulletsPerOuterLoop = 5 * difficulty; // Declared once here

                    if (spawnsRemaining == 0)
                    {
                        ChatHelper.BroadcastChatMessage(
                            NetworkText.FromLiteral("Get ready to eat plasma, asswipe."),
                            new Color(185, 105, 105));
                        // Total bullets = (5*difficulty)^2
                        int totalBullets = bulletsPerOuterLoop * bulletsPerOuterLoop;
                        spawnsRemaining = totalBullets;
                        NPC.ai[3] = spawnsRemaining;
                        NPC.ai[1] = DelayFrames(500);
                        break;
                    }

                    // Spawn one plasma bullet
                    Vector2 dir = targetPos - center;
                    Projectile.NewProjectile(source, center, dir * 0.01f,
                        ProjectileType<ApexPlasmaBulletHostile>(), 67, 0f, Main.myPlayer);
                    SoundEngine.PlaySound(SoundID.Item42, center);
                    SoundEngine.PlaySound(SoundID.Item99, center);
                    SoundEngine.PlaySound(SoundID.Item114, center);

                    spawnsRemaining--;
                    NPC.ai[3] = spawnsRemaining;

                    bool isEndOfOuter = (spawnsRemaining % bulletsPerOuterLoop) == 0;
                    if (spawnsRemaining > 0)
                    {
                        NPC.ai[1] = DelayFrames( 80 / difficulty * 2);
                        if (isEndOfOuter)
                            NPC.ai[1] += DelayFrames(550);
                    }
                    else
                    {
                        FinishAttack();
                    }
                    break;

                case 2: // Summon Santa NK1
                    if (spawnsRemaining == 0)
                    {
                        ChatHelper.BroadcastChatMessage(
                            NetworkText.FromLiteral("Needing ground support, send an NK1."),
                            new Color(185, 105, 105));
                        SoundEngine.PlaySound(SoundID.Item94, center);
                        // Choose left/right once for this entire attack
                        int summonXOffset = Main.rand.NextBool() ? 3000 : -3000;
                        NPC.ai[1] = summonXOffset; // temporarily store offset in timer slot (safe because timer is 0 here)
                        NPC.ai[1] = DelayFrames(500);
                        spawnsRemaining = Math.Max(1,  1 * difficulty - 2);
                        NPC.ai[3] = spawnsRemaining;
                        break;
                    }

                    // Spawn one NK1
                    int summonOffset = (int)NPC.ai[1]; // retrieve stored offset
                    int xPos = (int)center.X + summonOffset + Main.rand.Next(-100, 100);
                    int yPos = (int)center.Y + Main.rand.Next(-500, 500);
                    // Clamp to world bounds
                    xPos = (int)Math.Clamp(xPos, 50, Main.maxTilesX * 16 - 50);
                    yPos = (int)Math.Clamp(yPos, 50, Main.maxTilesY * 16 - 50);
                    NPC.NewNPCDirect(source, xPos, yPos, NPCID.SantaNK1, NPC.whoAmI);

                    spawnsRemaining--;
                    NPC.ai[3] = spawnsRemaining;

                    if (spawnsRemaining > 0)
                        NPC.ai[1] = DelayFrames(8100 / difficulty * 2);
                    else
                        FinishAttack();
                    break;

                case 3: // Summon Elf Copter squad
                    if (spawnsRemaining == 0)
                    {
                        ChatHelper.BroadcastChatMessage(
                            NetworkText.FromLiteral("Requesting backup, send a squad to my location."),
                            new Color(185, 105, 105));
                        SoundEngine.PlaySound(SoundID.Item94, center);
                        // Store summon offset for this attack
                        int summonXOffset = Main.rand.NextBool() ? 3000 : -3000;
                        NPC.ai[1] = summonXOffset;
                        NPC.ai[1] = DelayFrames(500);
                        spawnsRemaining = 4 * difficulty;
                        NPC.ai[3] = spawnsRemaining;
                        break;
                    }

                    // Spawn one Elf Copter
                    int summonOffset2 = (int)NPC.ai[1];
                    int xPos2 = (int)center.X + summonOffset2 + Main.rand.Next(-300, 300);
                    int yPos2 = (int)center.Y + Main.rand.Next(-700, 700);
                    // Clamp to world bounds
                    xPos2 = (int)Math.Clamp(xPos2, 50, Main.maxTilesX * 16 - 50);
                    yPos2 = (int)Math.Clamp(yPos2, 50, Main.maxTilesY * 16 - 50);
                    NPC.NewNPCDirect(source, xPos2, yPos2, NPCID.ElfCopter, NPC.whoAmI);

                    spawnsRemaining--;
                    NPC.ai[3] = spawnsRemaining;

                    if (spawnsRemaining > 0)
                        NPC.ai[1] = DelayFrames(450 / difficulty * 2);
                    else
                        FinishAttack();
                    break;

                case 4: // Missiles
                    if (spawnsRemaining == 0)
                    {
                        ChatHelper.BroadcastChatMessage(
                            NetworkText.FromLiteral("Missiles primed, target locked."),
                            new Color(185, 105, 105));
                        SoundEngine.PlaySound(SoundID.Item94, center);
                        spawnsRemaining = 5 * difficulty;
                        NPC.ai[3] = spawnsRemaining;
                        NPC.ai[1] = DelayFrames(500);
                        break;
                    }

                    // Spawn one missile
                    Vector2 dirMissile = targetPos - center;
                    Projectile.NewProjectile(source, center, dirMissile * 20f,
                        ProjectileID.Missile, 96, 0f, Main.myPlayer);
                    SoundEngine.PlaySound(SoundID.Item42, center);

                    spawnsRemaining--;
                    NPC.ai[3] = spawnsRemaining;

                    if (spawnsRemaining > 0)
                        NPC.ai[1] = DelayFrames(220 / difficulty * 2);
                    else
                        FinishAttack();
                    break;
            }
        }
    }
}