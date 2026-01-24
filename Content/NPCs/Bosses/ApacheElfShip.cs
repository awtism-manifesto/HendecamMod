using HendecamMod.Common.Systems;
using HendecamMod.Content.Items;
using HendecamMod.Content.Items.Consumables;
using HendecamMod.Content.NPCs.Town.Alpine;
using HendecamMod.Content.Projectiles.Enemies.Boss;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace HendecamMod.Content.NPCs.Bosses
    {
    [AutoloadBossHead]
    public class ApacheElfShip : ModNPC
        {
        private int tickCounter = 0;
        private int nextSpawnTick = 0;
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
            NPC.lifeMax = 75000;

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
            int itemType = ModContent.ItemType<fivenato>();
            var parameters = new DropOneByOne.Parameters()
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

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<AlpineTreasureBag>()));

            // npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Placeable.Furniture.MinionBossRelic>()));

            // npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<MinionBossPetItem>(), 4));
        }
        public override void OnKill()
            {
            var source2 = NPC.GetSource_FromAI();
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("AGH!"), new Color(185, 105, 105));
            NPC.SetEventFlagCleared(ref ApacheElfShipDown.downedApacheElfShip, -1);
            AlpineNPCRespawnSystem.unlockedAlpineSpawn = true;
            NPC.NewNPCDirect(source2, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Alpine>(), NPC.whoAmI);
            }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
            {
            cooldownSlot = ImmunityCooldownID.Bosses;
            return true;
            }
        public async override void AI()
            {
            if (!NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                {
                NPC.SetDefaults(0);
                NPC.active = false;
                if (ApacheElfShipDown.downedApacheElfShip == true)
                    {
                    var source2 = NPC.GetSource_FromAI();
                    AlpineNPCRespawnSystem.unlockedAlpineSpawn = true;
                    NPC.NewNPCDirect(source2, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Alpine>(), NPC.whoAmI);
                    }
                }
                {
                if (nextSpawnTick == 0)
                    {
                    AlpineNPCRespawnSystem.unlockedAlpineSpawn = false;
                    nextSpawnTick = Main.rand.Next(300, 500);
                    }

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
                        AlpineNPCRespawnSystem.unlockedAlpineSpawn = false;
                        direction.Normalize();
                        float speed = 20f;
                        int damage = NPC.damage;
                        int choice = Main.rand.Next(2);
                        int SummonX = 0;
                        int p = 0;
                        int b = 0;
                        int Difficulty = 1;
                        Difficulty = 1;
                        if (Main.expertMode == true)
                            {
                            Difficulty += 1;
                            }
                        if (Main.masterMode == true)
                            {
                            Difficulty += 1;
                            }
                        if (Main.getGoodWorld == true)
                            {
                            Difficulty += 1;
                            }
                        if (Main.getGoodWorld == true & Main.drunkWorld == true)
                            {
                            Difficulty += 1;
                            }

                        switch (choice)
                            {
                            case 0:
                            SummonX = 3000;
                            p = 0;
                            break;
                            case 1:
                            SummonX = -3000;
                            p = 0;
                            break;
                            }
                        switch (Main.rand.Next(5))
                            {
                            case 0:
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("I'm preparing a few ''gifts'' for you."), new Color(185, 105, 105));
                            await Task.Delay(500 / Difficulty);
                            for (p = 0; p < 10 * Difficulty; p++)
                                {
                                Vector2 position0 = NPC.Center;
                                Vector2 targetPosition0 = Main.player[NPC.target].Center;
                                Vector2 direction0 = targetPosition0 - position;
                                Projectile.NewProjectile(source, position0, direction0 * 0.01f, ProjectileID.Present, 75, 0f, Main.myPlayer);
                                await Task.Delay(400 / Difficulty);
                                }
                            break;
                            case 1:
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Get ready to eat plasma, asswipe."), new Color(185, 105, 105));
                            await Task.Delay(500 / Difficulty);
                            for (b = 0; b < 5 * Difficulty; b++)
                                {
                                for (p = 0; p < 5 * Difficulty; p++)
                                    {
                                    Vector2 position1 = NPC.Center;
                                    Vector2 targetPosition1 = Main.player[NPC.target].Center;
                                    Vector2 direction1 = targetPosition1 - position1;
                                    SoundEngine.PlaySound(SoundID.Item42, position1);
                                    SoundEngine.PlaySound(SoundID.Item99, position1);
                                    SoundEngine.PlaySound(SoundID.Item114, position1);
                                    Projectile.NewProjectile(source, position1, direction1 * 0.01f, ModContent.ProjectileType<ApexPlasmaBulletHostile>(), 75, 0f, Main.myPlayer);
                                    await Task.Delay(50 / Difficulty);
                                    }
                                await Task.Delay(500 / Difficulty);
                                }
                            break;
                            case 2:

                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Needing ground support, send an NK1."), new Color(185, 105, 105));
                            await Task.Delay(500 / Difficulty);
                            SoundEngine.PlaySound(SoundID.Item94, position);
                            for (p = 0; p < 1 * Difficulty; p++)
                                {
                                NPC.NewNPCDirect(source, (int)NPC.Center.X + SummonX + Main.rand.Next(-100, 100), (int)NPC.Center.Y + Main.rand.Next(-500, 500), NPCID.SantaNK1, NPC.whoAmI);
                                }
                            await Task.Delay(10000 / Difficulty);
                            break;
                            case 3:
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Requesting backup, send a squad to my location."), new Color(185, 105, 105));
                            await Task.Delay(500 / Difficulty);
                            SoundEngine.PlaySound(SoundID.Item94, position);
                            for (p = 0; p < 5 * Difficulty; p++)
                                {
                                NPC.NewNPCDirect(source, (int)NPC.Center.X + SummonX + Main.rand.Next(-100, 100), (int)NPC.Center.Y + Main.rand.Next(-500, 500), NPCID.ElfCopter, NPC.whoAmI);
                                }
                            break;
                            case 4:
                            SoundEngine.PlaySound(SoundID.Item94, position);
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Missiles primed, target locked."), new Color(185, 105, 105));
                            await Task.Delay(500 / Difficulty);
                            for (p = 0; p < 5 * Difficulty; p++)
                                {
                                Vector2 position4 = NPC.Center;
                                Vector2 targetPosition4 = Main.player[NPC.target].Center;
                                Vector2 direction4 = targetPosition4 - position4;
                                Projectile.NewProjectile(source, position4, direction4 * speed, ProjectileID.Missile, 80, 0f, Main.myPlayer);
                                await Task.Delay(200 / Difficulty);
                                }
                            break;
                            }
                        }

                    tickCounter = 0;
                    nextSpawnTick = Main.rand.Next(300, 500);

                    }
                }
            }
        }
    }
