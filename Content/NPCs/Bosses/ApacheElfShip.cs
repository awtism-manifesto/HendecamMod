using HendecamMod.Common.Systems;
using HendecamMod.Common.Systems.Particles;
using HendecamMod.Content.Global;
using HendecamMod.Content.Items.Consumables;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Items.Weapons.Ammo;
using HendecamMod.Content.NPCs.Town.AlpineNPC;
using HendecamMod.Content.Pets;
using HendecamMod.Content.Projectiles.Enemies.ApacheBoss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Content.NPCs.Bosses;

[AutoloadBossHead]
public class ApacheElfShip : ModNPC
{
    public static readonly Asset<Texture2D> bodyTexture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/Dartship_Body");
    public static readonly Asset<Texture2D> bodyGunTexture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/Dartship_BodyGun");
    public static readonly Asset<Texture2D> frontGunTexture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/Dartship_FrontGun");
    public static readonly Asset<Texture2D> topGunTexture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/Dartship_TopGun");
    public static readonly Asset<Texture2D> rotorTexture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/Dartship_Rotor");
    public static readonly Asset<Texture2D> muzzleTexture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/Dartship_Muzzle");

    public static readonly Asset<Texture2D> bodyFrontTexture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/Dartship_Body_Front");
    public static readonly Asset<Texture2D> bodyGunFrontTexture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/Dartship_BodyGun_Front");
    public static readonly Asset<Texture2D> frontGunFrontTexture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/Dartship_FrontGun_Front");
    public static readonly Asset<Texture2D> topGunFrontTexture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/Dartship_TopGun_Front");

    public override string Texture => "HendecamMod/Content/NPCs/Bosses/ApacheElfShip";

    public enum ActionState
    {
        Intro,
        Chase,
        Reposition,
        RazorRotor,
        DartshipBarrage,
        SpecPops,
        ChinookSupply,
        Downdraft,
        ShoverRocket,
        PrimeAssault,
        ComancheCommander,
        Retreat,
        Death
    }

    public ActionState AIState
    {
        get => (ActionState)NPC.ai[0];
        set => NPC.ai[0] = (float)value;
    }

    public ref float aiTimer0 => ref NPC.localAI[0];
    public ref float aiTimer1 => ref NPC.localAI[1];
    public ref float aiTimer2 => ref NPC.localAI[2];
    public ref float attackCount => ref NPC.ai[2];
    public ref float marineWhoAmI => ref NPC.ai[3];

    public int TargetFacing = -1;
    public int DesiredSide = -1;
    public float HoverTimer;

    public bool UseFrontSprites;
    public float BackgroundProgress;

    public Vector2 BodyPos;
    public Vector2 RotorPos;
    public Vector2 TailRotorPos;
    public Vector2 FrontGunPivot;
    public Vector2 BodyGunPivot;
    public Vector2 TopGunPivot;
    public Vector2 FrontGunMuzzlePos;
    public Vector2 BodyGunMuzzlePos;
    public Vector2 TopGunMuzzlePos;
    public Vector2 RopeAnchorPos;

    public Vector2 LeftBodyGunPivot;
    public Vector2 RightBodyGunPivot;
    public Vector2 LeftTopGunPivot;
    public Vector2 RightTopGunPivot;
    public Vector2 LeftBodyGunMuzzlePos;
    public Vector2 RightBodyGunMuzzlePos;
    public Vector2 LeftTopGunMuzzlePos;
    public Vector2 RightTopGunMuzzlePos;

    public Vector2 BodyRecoilOffset;
    public Vector2 FrontGunRecoil;
    public Vector2 BodyGunRecoil;
    public Vector2 TopGunRecoil;
    public float FrontGunScaleRecoil;
    public float BodyGunScaleRecoil;
    public float TopGunScaleRecoil;
    public float FrontGunMuzzleGlow;
    public float BodyGunMuzzleGlow;
    public float TopGunMuzzleGlow;

    public float GunRotation;
    public float TargetGunRotation;
    public float TopGunRotation;
    public float TargetTopGunRotation;
    public float GunRotationLerpSpeed = 0.08f;

    public const int BodyFrameCount = 6;
    public int bodyFrame;
    public int bodyFrameCounter;
    public int bodyFrameSpeed = 4;
    public bool doorGun;

    public int gunFrameSpeed = 3;
    public int frontGunFrame;
    public int frontGunFrameCounter;
    public int bodyGunFrame;
    public int bodyGunFrameCounter;
    public int topGunFrame;
    public int topGunFrameCounter;

    public bool frontGunActive;
    public bool bodyGunActive;
    public bool topGunActive;

    public const int RotorBladeCount = 4;
    public const int RotorTrailLength = 24;
    public Vector2[][] RotorTrailPos = new Vector2[RotorBladeCount][];
    public float[][] RotorTrailRot = new float[RotorBladeCount][];
    public float RotorAngle;
    public float RotorSpeed = 0.2f;
    public float RotorRadiusX = 72f;
    public float RotorRadiusY = 16f;
    public float RotorTrailArc = 1.25f;
    public float RotorTrailPersistence = 1f;
    public float RotorWidthMultiplier = 1.5f;
    public float RotorPosScale = 0.55f;
    public float RotorSizeMultiplier = 1f;
    public float RotorSpinFactor = 1f;
    public Vector2 RotorAnchorOffset = new(-4f, -40f);

    public Vector2[][] TailRotorTrailPos = new Vector2[RotorBladeCount][];
    public float[][] TailRotorTrailRot = new float[RotorBladeCount][];
    public float TailRotorAngle;
    public float TailRotorSpeed = 0.4f;
    public float TailRotorRadiusX = 14f;
    public float TailRotorRadiusY = 14f;
    public float TailRotorTrailArc = 2.6f;
    public float TailRotorTrailPersistence = 0.5f;
    public float TailRotorWidthMultiplier = 1.2f;
    public float TailRotorPosScale = 0.75f;
    public Vector2 TailRotorAnchorOffset = new(-90f, -10f);

    private int currentDrawingBlade;
    private readonly Vector2[] dashOldPositions = new Vector2[40];
    private readonly float[] dashOldRotations = new float[40];

    private float dashTrailOpacity;
    private Vector2 chinookStartPos;
    private Vector2 chinookEndPos;
    private ActionState lastAttack = ActionState.Intro;
    private readonly HashSet<ActionState> usedAttacks = [];

    private MiscShaderData rotorShader;
    public static VertexStrip vertexStrip = new();
    public VertexStrip IntroTrailStrip = new();

    private float LifeRatio => NPC.life / (float)NPC.lifeMax;
    public static bool ExpertMode => Main.expertMode;
    public static bool MasterMode => Main.masterMode;
    public static bool LegendaryMode => (Main.getGoodWorld || Main.zenithWorld) && Main.masterMode;

    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = BodyFrameCount;
        NPCID.Sets.MPAllowedEnemies[Type] = true;
        NPCID.Sets.TrailCacheLength[Type] = 40;
        NPCID.Sets.TrailingMode[Type] = 2;

        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.BoneJavelin] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.TentacleSpike] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.BloodButcherer] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Shimmer] = true;
    }

    public override void SetDefaults()
    {
        NPC.damage = 80;
        NPC.defense = 40;
        NPC.lifeMax = 40000;
        NPC.width = 140;
        NPC.height = 90;
        NPC.knockBackResist = 0f;
        NPC.HitSound = SoundID.NPCHit4;
        NPC.DeathSound = SoundID.NPCDeath14;
        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.SpawnWithHigherTime(30);
        NPC.boss = true;
        NPC.npcSlots = 10f;
        NPC.aiStyle = -1;

        for (int i = 0; i < RotorBladeCount; i++)
        {
            RotorTrailPos[i] = new Vector2[RotorTrailLength];
            RotorTrailRot[i] = new float[RotorTrailLength];
            TailRotorTrailPos[i] = new Vector2[RotorTrailLength];
            TailRotorTrailRot[i] = new float[RotorTrailLength];
        }

        AIState = ActionState.Intro;

        if (!Main.dedServ)
        {
            Music = MusicID.OtherworldlyUGHallow;
        }
    }

    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(aiTimer0);
        writer.Write(aiTimer1);
        writer.Write(aiTimer2);
        writer.Write((sbyte)TargetFacing);
        writer.Write((sbyte)DesiredSide);
        writer.Write(doorGun);
        writer.Write(UseFrontSprites);
        writer.Write(BackgroundProgress);
        writer.WriteVector2(chinookStartPos);
        writer.WriteVector2(chinookEndPos);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        aiTimer0 = reader.ReadSingle();
        aiTimer1 = reader.ReadSingle();
        aiTimer2 = reader.ReadSingle();
        TargetFacing = reader.ReadSByte();
        DesiredSide = reader.ReadSByte();
        doorGun = reader.ReadBoolean();
        UseFrontSprites = reader.ReadBoolean();
        BackgroundProgress = reader.ReadSingle();
        chinookStartPos = reader.ReadVector2();
        chinookEndPos = reader.ReadVector2();
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AlpineTrophy>(), 10));

        var notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
        var ammoParams = new DropOneByOne.Parameters
        {
            ChanceNumerator = 1,
            ChanceDenominator = 1,
            MinimumStackPerChunkBase = 4,
            MaximumStackPerChunkBase = 6,
            MinimumItemDropsCount = 40,
            MaximumItemDropsCount = 60,
        };
        notExpertRule.OnSuccess(new DropOneByOne(ModContent.ItemType<Fivenato>(), ammoParams));
        npcLoot.Add(notExpertRule);

        npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<AlpineTreasureBag>()));
        npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<AlpineRelicItem>()));
        npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<AlpinePetItem>(), 4));
    }

    public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * balance * bossAdjustment);
        NPC.damage = (int)(NPC.damage * 0.7f);
    }

    public override bool CanHitPlayer(Player target, ref int cooldownSlot)
    {
        cooldownSlot = ImmunityCooldownID.Bosses;
        return AIState is not (ActionState.Intro or ActionState.Retreat or ActionState.Death)
            && !UseFrontSprites
            && BackgroundProgress < 0.35f;
    }

    public override void OnSpawn(IEntitySource source)
    {
        aiTimer0 = 0f;
        aiTimer1 = 0f;
        aiTimer2 = 0f;
        attackCount = 0f;
        marineWhoAmI = -1f;
        RotorSpinFactor = 1f;
        UseFrontSprites = false;
        NPC.hide = false;
        BackgroundProgress = 0f;
        dashTrailOpacity = 1f;
        lastAttack = ActionState.Intro;
        usedAttacks.Clear();
        AIState = ActionState.Intro;

        int alpineIndex = NPC.FindFirstNPC(ModContent.NPCType<Alpine>());
        if (alpineIndex >= 0 && alpineIndex < Main.maxNPCs && Main.npc[alpineIndex].active)
        {
            NPC.Center = Main.npc[alpineIndex].Center;
            Main.npc[alpineIndex].active = false;
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, alpineIndex);
            }
        }

        NPC.TargetClosest(true);
        if (NPC.HasValidTarget)
        {
            Player player = Main.player[NPC.target];
            TargetFacing = player.Center.X < NPC.Center.X ? -1 : 1;
            NPC.spriteDirection = TargetFacing;
            DesiredSide = -TargetFacing;
        }

        NPC.velocity = new Vector2(TargetFacing * 4f, 6f);
        NPC.netUpdate = true;
    }

    public int ProjectileDamage(int damage)
    {
        if (Main.masterMode)
            return (int)(damage / 3.5f);
        if (Main.expertMode)
            return (int)(damage / 2.5f);
        return damage;
    }

    public override bool CheckDead()
    {
        if (AIState != ActionState.Death)
        {
            NPC.life = 1;
            NPC.dontTakeDamage = true;
            ResetBackgroundVisuals();
            aiTimer0 = 0f;
            aiTimer1 = 0f;
            AIState = ActionState.Death;
            NPC.netUpdate = true;
            return false;
        }

        bool crashed = aiTimer1 >= 340f || (aiTimer1 > 75f && (NPC.collideY || NPC.collideX || NPC.velocity.Y == 0f));
        if (crashed)
            return true;

        NPC.life = 1;
        return false;
    }

    public override void OnKill()
    {
        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("AGH!"), new Color(185, 105, 105));
        SoundEngine.PlaySound(SoundID.Item14, NPC.Center);

        if (!Main.dedServ)
        {
            Vector2[] crashPoints = [BodyPos, BodyGunPivot, TopGunPivot, FrontGunMuzzlePos];
            foreach (Vector2 pt in crashPoints)
            {
                for (int i = 0; i < 20; i++)
                {
                    Color color = Main.rand.NextFromList(Color.Orange, Color.OrangeRed, Color.SlateGray, Color.Gray);
                    ParticleSystem.SpawnParticle(
                        ParticleID.Streak,
                        pt + Main.rand.NextVector2Circular(10f, 10f),
                        Vector2.UnitY.RotatedByRandom(3f) * Main.rand.NextFloat(1.5f, 3.8f),
                        color,
                        1f,
                        Main.rand.NextFloat(0.75f, 1.15f)
                    );

                    ParticleSystem.SpawnParticle(
                        ParticleID.Gas,
                        pt + Main.rand.NextVector2Circular(12f, 12f),
                        Vector2.UnitY.RotatedByRandom(1.3f) * Main.rand.NextFloat(0.8f, 2.2f),
                        color,
                        0.9f,
                        Main.rand.NextFloat(0.55f, 0.85f),
                        data0: 0.03f
                    );
                }
            }
        }

        AlpineNPCRespawnSystem.unlockedAlpineSpawn = true;

        if (!BossDownedSystem.downedApacheElfShip)
        {
            BossDownedSystem.downedApacheElfShip = true;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                ModContent.GetInstance<PlutoniumSystem>().BlessWorldWithPlutonium();
            }
        }
    }

    public override void AI()
    {
        if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
        {
            NPC.TargetClosest();
        }

        Player player = Main.player[NPC.target];

        if (AlpineNPCRespawnSystem.unlockedAlpineSpawn && Main.netMode != NetmodeID.MultiplayerClient)
        {
            AlpineNPCRespawnSystem.unlockedAlpineSpawn = false;
        }

        HoverTimer++;
        UpdateTrailHistory();

        frontGunActive = false;
        bodyGunActive = false;
        topGunActive = false;

        if (AIState != ActionState.PrimeAssault)
        {
            UseFrontSprites = false;
            NPC.hide = false;
            BackgroundProgress = MathHelper.Lerp(BackgroundProgress, 0f, 0.15f);
            NPC.scale = MathHelper.Lerp(NPC.scale, 1f, 0.15f);

            if (AIState is not (ActionState.Intro or ActionState.Retreat or ActionState.Death))
            {
                NPC.dontTakeDamage = false;
            }
        }

        if (AIState == ActionState.Retreat)
        {
            Retreat();
        }
        else if (AIState == ActionState.Death)
        {
            Death();
        }
        else if (HasValidTarget(player))
        {
            switch (AIState)
            {
                case ActionState.Intro: Intro(player); break;
                case ActionState.Chase: Chase(player); break;
                case ActionState.Reposition: Reposition(player); break;
                case ActionState.RazorRotor: RazorRotor(player); break;
                case ActionState.DartshipBarrage: DartshipBarrage(player); break;
                case ActionState.SpecPops: SpecPops(player); break;
                case ActionState.ChinookSupply: ChinookSupply(player); break;
                case ActionState.Downdraft: Downdraft(player); break;
                case ActionState.ShoverRocket: ShoverRocket(player); break;
                case ActionState.PrimeAssault: PrimeAssault(player); break;
                case ActionState.ComancheCommander: ComancheCommander(player); break;
            }
        }

        UpdateKinematicsAndAnimations();
    }

    private bool HasValidTarget(Player player)
    {
        if (!player.active || player.dead || Vector2.Distance(NPC.Center, player.Center) > 4000f)
        {
            NPC.TargetClosest();
            player = Main.player[NPC.target];

            if (!player.active || player.dead || Vector2.Distance(NPC.Center, player.Center) > 4000f)
            {
                ResetBackgroundVisuals();
                aiTimer0 = 0f;
                aiTimer1 = 0f;
                doorGun = false;
                AIState = ActionState.Retreat;
                NPC.netUpdate = true;
                return false;
            }
        }

        if (NPC.timeLeft < 600)
            NPC.timeLeft = 600;

        return true;
    }

    private void ResetBackgroundVisuals()
    {
        UseFrontSprites = false;
        NPC.hide = false;
        BackgroundProgress = 0f;
        NPC.scale = 1f;
    }

    private void SwitchState(ActionState next)
    {
        aiTimer0 = 0f;
        attackCount = 0f;
        AIState = next;
        NPC.netUpdate = true;
    }

    private ActionState PickNextAttack()
    {
        bool phase2 = (MasterMode || LegendaryMode ) ? LifeRatio <= 0.5f : LifeRatio <= 0.6f;

        ActionState[] pool;

        if (ExpertMode || MasterMode || LegendaryMode)
        {
            pool = phase2
                ? [
                    ActionState.RazorRotor,
                    ActionState.Downdraft,
                    ActionState.ShoverRocket,
                    ActionState.ChinookSupply,
                    ActionState.DartshipBarrage,
                    ActionState.ComancheCommander,
                    ActionState.PrimeAssault,
                    ActionState.SpecPops
                ]
                : [
                    ActionState.RazorRotor,
                    ActionState.Downdraft,
                    ActionState.ShoverRocket,
                    ActionState.ChinookSupply,
                    ActionState.DartshipBarrage,
                    ActionState.ComancheCommander
                ];
        }
        else
        {
            pool = phase2
                ? [
                    ActionState.RazorRotor,
                    ActionState.Downdraft,
                    ActionState.ShoverRocket,
                    ActionState.ChinookSupply,
                    ActionState.DartshipBarrage,
                    ActionState.ComancheCommander
                ]
                : [
                    ActionState.RazorRotor,
                    ActionState.Downdraft,
                    ActionState.ShoverRocket
                ];
        }

        List<ActionState> available = [];
        foreach (ActionState attack in pool)
        {
            if (!usedAttacks.Contains(attack))
                available.Add(attack);
        }

        if (available.Count == 0)
        {
            usedAttacks.Clear();
            foreach (ActionState attack in pool)
            {
                if (pool.Length <= 1 || attack != lastAttack)
                    available.Add(attack);
            }
        }

        ActionState chosen = available[Main.rand.Next(available.Count)];
        usedAttacks.Add(chosen);
        lastAttack = chosen;
        return chosen;
    }

    private int GetComancheCount()
    {
        float hp = LifeRatio;

        if (ExpertMode || MasterMode || LegendaryMode)
        {
            if (hp <= 0.5f) return 3;
            if (hp <= 0.75f) return 2;
            return 1;
        }

        if (hp <= 0.25f) return 3;
        if (hp <= 0.5f) return 2;
        return 1;
    }

    #region Attacks
    public void Intro(Player target)
    {
        aiTimer0++;
        NPC.dontTakeDamage = true;

        if (aiTimer0 == 1f)
        {
            TargetFacing = target.Center.X < NPC.Center.X ? -1 : 1;
            NPC.spriteDirection = TargetFacing;
            DesiredSide = -TargetFacing;
            GunRotation = TargetFacing == -1 ? 0f : MathHelper.Pi;
            TopGunRotation = GunRotation;
        }

        UpdateFacingAndGuns(target, false, false);

        Vector2 hoverPos = target.Center + new Vector2(DesiredSide * 300f, -210f);

        if (aiTimer0 < 45f && NPC.Center.Y < target.Center.Y + 40f)
        {
            NPC.velocity.X = MathHelper.Lerp(NPC.velocity.X, TargetFacing * 6f, 0.08f);
            NPC.velocity.Y = Math.Min(22f, NPC.velocity.Y + 0.75f);
            NPC.rotation = MathHelper.Lerp(NPC.rotation, 0.55f * TargetFacing, 0.15f);
            return;
        }

        float dist = MoveTowards(hoverPos, 0.065f, 2f, 16f, 0.08f, stopRadius: 12f);
        float pullUpTilt = MathHelper.Clamp(NPC.velocity.X * 0.025f + NPC.velocity.Y * 0.015f * TargetFacing, -0.35f, 0.35f);
        NPC.rotation = MathHelper.Lerp(NPC.rotation, pullUpTilt, 0.12f);

        if (aiTimer0 >= 105f || (aiTimer0 >= 60f && dist < 50f))
        {
            NPC.dontTakeDamage = false;
            SwitchState(ActionState.Chase);
        }
    }

    public void Chase(Player target)
    {
        if (aiTimer0 == 0f)
            EnsureSide(target);

        doorGun = false;
        UpdateFacingAndGuns(target, false, false);
        HoverMoveTowards(target, 1f);

        aiTimer0++;
        if (aiTimer0 >= 150f && Main.netMode != NetmodeID.MultiplayerClient)
        {
            SwitchState(PickNextAttack());
        }
    }

    public void Reposition(Player target)
    {
        doorGun = false;
        UpdateFacingAndGuns(target, false, false);
        aiTimer0++;

        if (DesiredSide == 0)
            DesiredSide = -TargetFacing;

        Vector2 swoopTarget = target.Center + new Vector2(DesiredSide * 340f, -240f + MathF.Cos(HoverTimer * 0.03f) * 20f);
        float dist = MoveTowards(swoopTarget, 0.04f, 1f, 8f, 0.05f, stopRadius: 16f);
        NPC.rotation = MathHelper.Lerp(NPC.rotation, MathHelper.Clamp(NPC.velocity.X * 0.025f, -0.2f, 0.2f), 0.08f);

        if (aiTimer0 >= 150f || dist < 45f)
        {
            SwitchState(ActionState.Chase);
        }
    }

    public void RazorRotor(Player target)
    {
        if (aiTimer0 == 0f && attackCount == 0f)
            EnsureSide(target);

        aiTimer0++;

        float slashSpeed = 20f;
        float flightTime = Vector2.Distance(RotorPos, target.Center) / slashSpeed;
        Vector2 interceptPos = new(target.Center.X + target.velocity.X * flightTime, target.Bottom.Y - 10f);

        FaceTowards(interceptPos.X, 32f);

        Vector2 desiredPos = target.Center + new Vector2(-TargetFacing * 150f, -140f);
        MoveTowards(desiredPos, 0.09f, 2f, 13.5f, 0.1f, stopRadius: 0f);

        AimBodyAt(interceptPos, RotorPos, 0.12f, 1.25f);
        UpdateFacingAndGuns(target, false, false);

        if (aiTimer0 >= 30f)
        {
            aiTimer0 = 1f;
            Vector2 planeDir = new Vector2(TargetFacing, 0f).RotatedBy(NPC.rotation);
            SoundEngine.PlaySound(SoundID.Item71, RotorPos);
            BodyRecoilOffset = -planeDir * 4.5f;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = -1; i <= 1; i += 2)
                {
                    Projectile.NewProjectile(
                        NPC.GetSource_FromAI(),
                        RotorPos,
                        planeDir * slashSpeed * i,
                        ModContent.ProjectileType<RazorRotorSlash>(),
                        ProjectileDamage(NPC.damage / 2),
                        2f,
                        Main.myPlayer
                    );
                }
            }

            if (++attackCount >= 7f)
            {
                DesiredSide = -TargetFacing;
                SwitchState(ActionState.Reposition);
            }
        }
    }

    public void DartshipBarrage(Player target)
    {
        if (aiTimer0 == 0f && attackCount == 0f)
            DesiredSide = NPC.Center.X < target.Center.X ? -1 : 1;

        aiTimer0++;

        if (aiTimer0 <= 135f)
        {
            SetFacing(-DesiredSide);
            UpdateFacingAndGuns(target, false, false);

            Vector2 desiredPos = new(target.Center.X + DesiredSide * 460f, target.Center.Y + MathF.Sin(HoverTimer * 0.03f) * 12f);
            MoveTowards(desiredPos, 0.06f, 1f, 10.5f, 0.07f, stopRadius: 8f);
            NPC.rotation = MathHelper.Lerp(NPC.rotation, MathHelper.Clamp(NPC.velocity.X * 0.018f, -0.12f, 0.12f), 0.09f);
        }
        else if (aiTimer0 <= 195f)
        {
            SetFacing(-DesiredSide);
            UpdateFacingAndGuns(target, false, true);

            BrakeToStop(0.16f, 0.15f);
            NPC.rotation = MathHelper.Lerp(NPC.rotation, 0f, 0.15f);

            if (aiTimer0 == 150f)
            {
                SoundEngine.PlaySound(SoundID.Item15, BodyGunMuzzlePos);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(
                        NPC.GetSource_FromAI(),
                        BodyGunMuzzlePos,
                        new Vector2(TargetFacing, 0f),
                        ModContent.ProjectileType<DartshipTelegraph>(),
                        0,
                        0f,
                        Main.myPlayer,
                        0f,
                        0f,
                        35f
                    );
                }
            }
        }
        else if (aiTimer0 <= 275f)
        {
            SetFacing(-DesiredSide);
            UpdateFacingAndGuns(target, false, true);

            float targetVelY = MathHelper.Clamp((target.Center.Y - NPC.Center.Y) * 0.018f, -1.2f, 1.2f);
            NPC.velocity = Vector2.Lerp(NPC.velocity, new Vector2(0f, targetVelY), 0.08f);
            NPC.rotation = MathHelper.Lerp(NPC.rotation, 0f, 0.15f);

            frontGunActive = true;
            bodyGunActive = true;
            topGunActive = true;

            Vector2 forwardDir = new Vector2(TargetFacing, 0f).RotatedBy(NPC.rotation);
            Vector2 bodyShootDir = new Vector2(-1f, 0f).RotatedBy(GunRotation);

            if (aiTimer0 % 3f == 0f)
            {
                FrontGunRecoil = new Vector2(-10f * TargetFacing, 0f);
                BodyGunRecoil = -bodyShootDir * 8f;
                BodyRecoilOffset = -forwardDir * 2.5f;
                FrontGunMuzzleGlow = 1f;
                BodyGunMuzzleGlow = 1f;
                SoundEngine.PlaySound(SoundID.Item11, FrontGunPivot);
                SpawnBullet(BodyGunMuzzlePos, bodyShootDir, 20.5f, 5f);
            }

            if (aiTimer0 % 30f == 0f)
            {
                Vector2 rocketDir = new Vector2(-1f, 0f).RotatedBy(TopGunRotation);
                TopGunRecoil = -rocketDir * 12f;
                BodyRecoilOffset += -rocketDir * 3.5f;
                TopGunMuzzleGlow = 1f;
                SoundEngine.PlaySound(SoundID.Item61, TopGunPivot);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(
                        NPC.GetSource_FromAI(),
                        TopGunMuzzlePos,
                        rocketDir * 6f,
                        ModContent.ProjectileType<DartshipRocket>(),
                        ProjectileDamage(NPC.damage / 2),
                        1f,
                        Main.myPlayer,
                        0,
                        target.whoAmI
                    );
                }
            }
        }
        else if (aiTimer0 <= 300f)
        {
            UpdateFacingAndGuns(target, false, false);
            NPC.velocity = Vector2.Lerp(NPC.velocity, Vector2.Zero, 0.12f);
            NPC.rotation = MathHelper.Lerp(NPC.rotation, 0f, 0.12f);
        }
        else
        {
            aiTimer0 = 0f;
            DesiredSide = -DesiredSide;
            NPC.netUpdate = true;

            if (++attackCount >= 3f)
            {
                SwitchState(ActionState.Reposition);
            }
        }
    }

    public void SpecPops(Player target)
    {
        const float spawnDelay = 24f;
        const float maxStateTime = 340f;
        const float doorCloseTime = 20f;

        if (aiTimer0 == 0f)
            EnsureSide(target);

        aiTimer0++;

        bool marineActive = IsMarineActive();
        doorGun = aiTimer0 <= spawnDelay || marineActive;
        UpdateFacingAndGuns(target, false, false);

        Vector2 hoverPos = target.Center + new Vector2(DesiredSide * 260f, -260f + MathF.Sin(HoverTimer * 0.03f) * 14f);
        MoveTowards(hoverPos, 0.045f, 0.5f, 5.5f, 0.05f, stopRadius: 10f);
        NPC.rotation = MathHelper.Lerp(NPC.rotation, MathHelper.Clamp(NPC.velocity.X * 0.02f, -0.14f, 0.14f), 0.08f);

        if (aiTimer0 == spawnDelay && Main.netMode != NetmodeID.MultiplayerClient)
        {
            int idx = NPC.NewNPC(
                NPC.GetSource_FromAI(),
                (int)RopeAnchorPos.X,
                (int)RopeAnchorPos.Y,
                ModContent.NPCType<DartshipMarine>(),
                ai0: NPC.whoAmI
            );
            marineWhoAmI = idx;

            if (idx >= 0 && idx < Main.maxNPCs)
            {
                Main.npc[idx].realLife = NPC.whoAmI;
                Main.npc[idx].netUpdate = true;
            }
            NPC.netUpdate = true;
        }

        if (aiTimer0 >= maxStateTime && marineActive && Main.netMode != NetmodeID.MultiplayerClient)
        {
            if (Main.npc[(int)marineWhoAmI].ModNPC is DartshipMarine marine)
                marine.TriggerRetract();
        }

        if (aiTimer0 > spawnDelay && !marineActive)
        {
            attackCount++;
            if (attackCount >= doorCloseTime)
            {
                doorGun = false;
                marineWhoAmI = -1f;
                DesiredSide = -DesiredSide;
                SwitchState(ActionState.Reposition);
            }
        }
    }
    public void ChinookSupply(Player target)
    {
        float attackAmount = 3;
        attackAmount = LegendaryMode ? 6 : (MasterMode ? 5 : ExpertMode ? 4 : 3);
        if (aiTimer0 == 0f)
            EnsureSide(target);

        aiTimer0++;
        int dashDir = -DesiredSide;

        if (aiTimer0 == 1f && Main.netMode != NetmodeID.MultiplayerClient)
        {
            float slope = Main.rand.NextFloat(0.06f, 0.14f) * (Main.rand.NextBool() ? 1f : -1f);
            Vector2 lineDir = new Vector2(dashDir, 0f).RotatedBy(slope);
            Vector2 lineCenter = target.Center + new Vector2(0f, -310f);
            const float halfSpan = 760f;

            chinookStartPos = lineCenter - lineDir * halfSpan;
            chinookEndPos = lineCenter + lineDir * halfSpan;

            int pointType = ModContent.ProjectileType<DartshipPatrolPoint>();
            Projectile.NewProjectile(NPC.GetSource_FromAI(), chinookStartPos, Vector2.Zero, pointType, 0, 0f, Main.myPlayer, NPC.whoAmI, 0f, 0f);
            Projectile.NewProjectile(NPC.GetSource_FromAI(), chinookStartPos, Vector2.Zero, pointType, 0, 0f, Main.myPlayer, NPC.whoAmI, chinookEndPos.X, chinookEndPos.Y);
            NPC.netUpdate = true;
        }

        Vector2 dashVector = chinookEndPos - chinookStartPos;
        float dashLength = MathF.Max(1f, dashVector.Length());
        Vector2 dashLineDir = dashVector / dashLength;
        float dashAngle = dashLineDir.ToRotation();
        float linePlaneRot = MathHelper.Clamp(
            dashDir == -1 ? MathHelper.WrapAngle(dashAngle + MathHelper.Pi) : MathHelper.WrapAngle(dashAngle),
            -0.6f,
            0.6f
        );

        int crateInterval = LegendaryMode ? 4 : MasterMode ? 5 : ExpertMode ? 6 : 7;

        if (aiTimer0 <= 95f)
        {
            doorGun = false;
            UpdateFacingAndGuns(target, false, false);

            float dist = MoveTowards(chinookStartPos, 0.085f, 2f, 22f, 0.1f, stopRadius: 10f);
            NPC.rotation = MathHelper.Lerp(NPC.rotation, MathHelper.Clamp(NPC.velocity.X * 0.02f, -0.15f, 0.15f), 0.1f);

            if (dist < 32f && aiTimer0 >= 52f)
                aiTimer0 = 96f;
            else if (aiTimer0 == 95f && dist > 64f)
                aiTimer0 = 94f;
        }
        else if (aiTimer0 <= 120f)
        {
            doorGun = true;
            SetFacing(dashDir);
            AlignGunsForward();

            Vector2 holdCorrection = (chinookStartPos - NPC.Center) * 0.12f;
            NPC.velocity = Vector2.Lerp(NPC.velocity, holdCorrection, 0.22f);
            NPC.rotation = MathHelper.Lerp(NPC.rotation, linePlaneRot - dashDir * 0.12f, 0.15f);

            if (aiTimer0 == 120f)
                SoundEngine.PlaySound(SoundID.Item89, NPC.Center);
        }
        else if (aiTimer0 <= 200f)
        {
            doorGun = true;
            SetFacing(dashDir);
            AlignGunsForward();

            float along = Vector2.Dot(NPC.Center - chinookStartPos, dashLineDir);
            Vector2 closestOnLine = chinookStartPos + dashLineDir * MathHelper.Clamp(along, 0f, dashLength);
            Vector2 lineCorrection = (closestOnLine - NPC.Center) * 0.2f;

            NPC.velocity = Vector2.Lerp(NPC.velocity, dashLineDir * 24f + lineCorrection, 0.24f);
            NPC.rotation = MathHelper.Lerp(NPC.rotation, linePlaneRot + dashDir * 0.2f, 0.2f);

            if ((int)aiTimer0 % crateInterval == 0)
            {
                SoundEngine.PlaySound(SoundID.Item42, RopeAnchorPos);
                BodyRecoilOffset += new Vector2(0f, -3f);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(
                        NPC.GetSource_FromAI(),
                        RopeAnchorPos,
                        new Vector2(NPC.velocity.X * 0.18f, 3.5f),
                        ModContent.ProjectileType<ChinookSupplyCrate>(),
                        ProjectileDamage(NPC.damage / 2),
                        2f,
                        Main.myPlayer
                    );
                }
            }

            if (along >= dashLength - 24f)
                aiTimer0 = 200f;
        }
        else if (aiTimer0 <= 238f)
        {
            if (aiTimer0 == 201f && Main.netMode != NetmodeID.MultiplayerClient)
                FadePatrolPoints();

            doorGun = false;
            UpdateFacingAndGuns(target, false, false);
            NPC.velocity *= 0.88f;
            NPC.rotation = MathHelper.Lerp(NPC.rotation, 0f, 0.12f);
        }
        else
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
                FadePatrolPoints();

            aiTimer0 = 0f;
            DesiredSide = -DesiredSide;
            NPC.netUpdate = true;

            if (++attackCount >= attackAmount)
            {
                SwitchState(ActionState.Reposition);
            }
        }
    }

    public void Downdraft(Player target)
    {
        if (aiTimer0 == 0f && attackCount == 0f)
            EnsureSide(target);

        aiTimer0++;
        doorGun = false;

        int burstCount = 3;
        const float moveFastTicks = 60f;
        const float pauseTelegraphTicks = 22f;
        const float blowTicks = 150f;
        const float recoveryTicks = 20f;

        const float telegraphStartTick = moveFastTicks + 4f;
        const float blowStartTick = moveFastTicks + pauseTelegraphTicks;
        const float blowEndTick = blowStartTick + blowTicks;
        const float cycleTotalTime = blowEndTick + recoveryTicks;

        FaceTowards(target.Center.X, 60f);
        UpdateFacingAndGuns(target, false, false);

        if (aiTimer0 <= moveFastTicks)
        {
            float leadX = MathHelper.Clamp(target.velocity.X * 22f, -260f, 260f);
            Vector2 desiredPos = new(target.Center.X + leadX, target.Center.Y - 300f + MathF.Sin(HoverTimer * 0.04f) * 10f);
            MoveTowards(desiredPos, 0.11f, 2f, 18.5f, 0.12f, stopRadius: 8f);
            NPC.rotation = MathHelper.Lerp(NPC.rotation, MathHelper.Clamp(NPC.velocity.X * 0.018f, -0.16f, 0.16f), 0.12f);
        }
        else if (aiTimer0 <= blowStartTick)
        {
            BrakeToStop(0.18f, 0.2f);
            NPC.rotation = MathHelper.Lerp(NPC.rotation, 0f, 0.16f);

            if (aiTimer0 == telegraphStartTick && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(
                    NPC.GetSource_FromAI(),
                    RotorPos,
                    Vector2.Zero,
                    ModContent.ProjectileType<DowndraftTelegraph>(),
                    0,
                    0f,
                    Main.myPlayer,
                    ai0: MathHelper.PiOver2,
                    ai1: NPC.whoAmI,
                    ai2: pauseTelegraphTicks
                );
            }

            if (aiTimer0 == blowStartTick && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(
                    NPC.GetSource_FromAI(),
                    NPC.Bottom,
                    Vector2.Zero,
                    ModContent.ProjectileType<DowndraftColumn>(),
                    ProjectileDamage((int)(NPC.damage * 0.6f)),
                    0f,
                    Main.myPlayer,
                    ai0: NPC.whoAmI,
                    ai1: blowTicks
                );
            }
        }
        else if (aiTimer0 <= blowEndTick)
        {
            Vector2 toDesired = new Vector2(target.Center.X, target.Center.Y - 300f) - NPC.Center;
            Vector2 trackVel = new(
                MathHelper.Clamp(toDesired.X * 0.025f, -3.2f, 3.2f),
                MathHelper.Clamp(toDesired.Y * 0.04f, -3.5f, 3.5f)
            );
            NPC.velocity = Vector2.Lerp(NPC.velocity, trackVel, 0.06f);
            NPC.rotation = MathHelper.Lerp(NPC.rotation, MathHelper.Clamp(NPC.velocity.X * 0.015f, -0.08f, 0.08f), 0.12f);
        }
        else if (aiTimer0 <= cycleTotalTime)
        {
            NPC.velocity *= 0.88f;
            NPC.rotation = MathHelper.Lerp(NPC.rotation, 0f, 0.12f);
        }
        else
        {
            aiTimer0 = 0f;
            NPC.netUpdate = true;

            if (++attackCount >= burstCount)
            {
                DesiredSide = -TargetFacing;
                SwitchState(ActionState.Reposition);
            }
        }
    }

    public void ShoverRocket(Player target)
    {
        if (aiTimer0 == 0f && attackCount == 0f)
            EnsureSide(target);

        aiTimer0++;
        doorGun = false;

        const int burstCount = 3;
        const float spawnTick = 10f;
        const float reticleSpawnTime = 40f;
        const float lockTime = 40f;
        const float holdDuration = spawnTick + reticleSpawnTime + lockTime + 22f;
        const float cycleTotalTime = holdDuration + 28f;

        FaceTowards(target.Center.X, 60f);

        if (aiTimer0 == spawnTick && Main.netMode != NetmodeID.MultiplayerClient)
        {
            Projectile.NewProjectile(
                NPC.GetSource_FromAI(),
                BodyPos,
                Vector2.Zero,
                ModContent.ProjectileType<ShovingRocket>(),
                ProjectileDamage((int)(NPC.damage * 0.85f)),
                12f,
                Main.myPlayer,
                ai0: NPC.whoAmI,
                ai1: reticleSpawnTime,
                ai2: lockTime
            );
        }

        if (aiTimer0 <= holdDuration)
        {
            bool isLocking = aiTimer0 >= spawnTick + reticleSpawnTime;
            Vector2 desiredPos = target.Center + new Vector2(-TargetFacing * 280f, -210f);
            MoveTowards(desiredPos, 0.055f, 0.5f, isLocking ? 2f : 8.5f, 0.08f, stopRadius: 10f);

            AimBodyAt(target.Center, BodyPos, 0.14f, 0.65f);
            UpdateFacingAndGuns(target, true, false);
        }
        else if (aiTimer0 <= cycleTotalTime)
        {
            UpdateFacingAndGuns(target, false, false);
            NPC.velocity *= 0.9f;
            NPC.rotation = MathHelper.Lerp(NPC.rotation, 0f, 0.12f);
        }
        else
        {
            aiTimer0 = 0f;
            NPC.netUpdate = true;

            if (++attackCount >= burstCount)
            {
                DesiredSide = -TargetFacing;
                SwitchState(ActionState.Reposition);
            }
        }
    }

    public void PrimeAssault(Player target)
    {
        const float bodyLeadTicks = 18f;
        const float orbLeadTicks = 40f;
        const float rocketLeadTicks = 44f;
        const float maxTrackSpeed = 12.5f;
        const float trackResponsiveness = 0.065f;
        const float velocitySmoothness = 0.075f;
        const float playerVelInheritance = 0.8f;
        const float orbFireInterval = 10f;
        const float rocketFireInterval = 60f;

        if (aiTimer0 == 0f)
        {
            EnsureSide(target);
            NPC.netUpdate = true;
        }

        aiTimer0++;
        doorGun = false;
        NPC.dontTakeDamage = true;

        if (aiTimer0 <= 55f)
        {
            UseFrontSprites = false;
            NPC.hide = false;

            float progress = Utils.GetLerpValue(1f, 55f, aiTimer0, true);
            BackgroundProgress = MathHelper.SmoothStep(0f, 1f, progress);
            NPC.scale = MathHelper.Lerp(1f, 0.72f, BackgroundProgress);

            Vector2 predictedTarget = target.Center + target.velocity * (bodyLeadTicks * 0.75f);
            MoveTowards(predictedTarget, trackResponsiveness, 0.5f, maxTrackSpeed, velocitySmoothness, target.velocity * playerVelInheritance, stopRadius: 0f);
            NPC.rotation = MathHelper.Lerp(NPC.rotation, MathHelper.Clamp(NPC.velocity.X * 0.015f, -0.12f, 0.12f), 0.12f);
            UpdateFacingAndGuns(target, false, false);

            if (aiTimer0 == 55f)
                NPC.netUpdate = true;
        }
        else if (aiTimer0 <= 235f)
        {
            UseFrontSprites = true;
            BackgroundProgress = 1f;
            NPC.scale = 0.72f;

            Vector2 weaveOffset = new(MathF.Sin(HoverTimer * 0.025f) * 45f, MathF.Sin(HoverTimer * 0.04f) * 25f);
            Vector2 predictedCenter = target.Center + target.velocity * bodyLeadTicks + weaveOffset;
            MoveTowards(predictedCenter, trackResponsiveness, 1f, maxTrackSpeed, velocitySmoothness, target.velocity * playerVelInheritance, stopRadius: 6f);
            NPC.rotation = MathHelper.Lerp(NPC.rotation, MathHelper.Clamp(NPC.velocity.X * 0.014f, -0.12f, 0.12f), 0.1f);

            frontGunActive = true;
            bodyGunActive = true;
            topGunActive = true;

            if (aiTimer0 % 6f == 0f)
            {
                FrontGunScaleRecoil = -0.24f;
                FrontGunMuzzleGlow = 1f;
            }

            if (aiTimer0 % orbFireInterval == 0f)
            {
                BodyGunScaleRecoil = -0.3f;
                BodyGunMuzzleGlow = 1f;
                SoundEngine.PlaySound(SoundID.Item11, BodyPos);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int orbDamage = ProjectileDamage(NPC.damage / 3);
                    Vector2[] gunMuzzles = [LeftBodyGunMuzzlePos, RightBodyGunMuzzlePos];
                    Vector2 leadOffset = Vector2.Clamp(target.velocity * orbLeadTicks, new Vector2(-520f, -360f), new Vector2(520f, 360f));
                    Vector2 interceptBase = Vector2.Lerp(NPC.Center, target.Center + leadOffset, 0.9f);

                    for (int i = 0; i < gunMuzzles.Length; i++)
                    {
                        float sideSign = i == 0 ? -1f : 1f;
                        Vector2 spread = new(sideSign * Main.rand.NextFloat(12f, 36f), Main.rand.NextFloat(-22f, 22f));
                        Vector2 impactPos = interceptBase + spread;

                        Projectile.NewProjectile(
                            NPC.GetSource_FromAI(),
                            gunMuzzles[i],
                            Vector2.Zero,
                            ModContent.ProjectileType<DartshipPlasmaOrb>(),
                            orbDamage,
                            1.5f,
                            Main.myPlayer,
                            ai0: 0f,
                            ai1: impactPos.X,
                            ai2: impactPos.Y
                        );
                    }
                }
            }

            if (aiTimer0 % rocketFireInterval == 0f)
            {
                TopGunScaleRecoil = -0.36f;
                TopGunMuzzleGlow = 1f;
                SoundEngine.PlaySound(SoundID.Item61, BodyPos);

                int shotIndex = (int)(aiTimer0 / rocketFireInterval);
                bool fireRightSide = shotIndex % 2 == 0;
                Vector2 rocketMuzzle = fireRightSide ? RightTopGunMuzzlePos : LeftTopGunMuzzlePos;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 rocketLead = Vector2.Clamp(target.velocity * rocketLeadTicks, new Vector2(-560f, -400f), new Vector2(560f, 400f));
                    Vector2 rocketTarget = target.Center + rocketLead;

                    Projectile rocket = Projectile.NewProjectileDirect(
                        NPC.GetSource_FromAI(),
                        rocketMuzzle,
                        Vector2.Zero,
                        ModContent.ProjectileType<DartshipHomingRocket>(),
                        ProjectileDamage(NPC.damage / 2),
                        2.5f,
                        Main.myPlayer,
                        ai0: target.whoAmI,
                        ai1: rocketTarget.X,
                        ai2: rocketTarget.Y
                    );
                    rocket.localAI[1] = fireRightSide ? 1f : -1f;
                }
            }

            if (aiTimer0 == 235f)
                NPC.netUpdate = true;
        }
        else if (aiTimer0 <= 280f)
        {
            UseFrontSprites = true;
            BackgroundProgress = 1f;
            NPC.scale = 0.72f;

            Vector2 sidePos = target.Center + target.velocity * 12f + new Vector2(DesiredSide * 320f, -210f);
            MoveTowards(sidePos, 0.08f, 1.5f, 15f, 0.09f, target.velocity * 0.35f, stopRadius: 10f);
            NPC.rotation = MathHelper.Lerp(NPC.rotation, MathHelper.Clamp(NPC.velocity.X * 0.015f, -0.14f, 0.14f), 0.1f);

            if (aiTimer0 == 280f)
                NPC.netUpdate = true;
        }
        else if (aiTimer0 <= 325f)
        {
            UseFrontSprites = false;
            NPC.hide = false;

            float returnT = Utils.GetLerpValue(280f, 325f, aiTimer0, true);
            BackgroundProgress = MathHelper.SmoothStep(1f, 0f, returnT);
            NPC.scale = MathHelper.Lerp(1f, 0.72f, BackgroundProgress);

            if (returnT >= 0.65f)
                NPC.dontTakeDamage = false;

            SetFacing(-DesiredSide);
            HoverMoveTowards(target, 1.15f);
            UpdateFacingAndGuns(target, false, false);
        }
        else
        {
            ResetBackgroundVisuals();
            NPC.dontTakeDamage = false;
            SwitchState(ActionState.Reposition);
        }
    }

    public void ComancheCommander(Player target)
    {
        const float fireStartTick = 45f;
        const float fireEndTick = 325f;
        const float totalDuration = 355f;

        if (aiTimer0 == 0f)
            EnsureSide(target);

        aiTimer0++;
        doorGun = false;

        if (aiTimer0 == 1f && Main.netMode != NetmodeID.MultiplayerClient)
        {
            Projectile.NewProjectile(
                NPC.GetSource_FromAI(),
                target.Center,
                Vector2.Zero,
                ModContent.ProjectileType<DartshipPointer>(),
                0,
                0f,
                Main.myPlayer,
                ai0: NPC.whoAmI,
                ai1: target.whoAmI,
                ai2: totalDuration
            );

            Vector2[] spawnOffsets =
            [
                new Vector2(-TargetFacing * 700f, -260f),
                new Vector2(-TargetFacing * 740f, 220f),
                new Vector2(-TargetFacing * 860f, -20f)
            ];

            int[] slots = GetComancheCount() switch
            {
                1 => [2],
                2 => [0, 1],
                _ => [0, 1, 2]
            };

            foreach (int slot in slots)
            {
                Vector2 spawnPos = NPC.Center + spawnOffsets[slot];
                int idx = NPC.NewNPC(
                    NPC.GetSource_FromAI(),
                    (int)spawnPos.X,
                    (int)spawnPos.Y,
                    ModContent.NPCType<ElfComanche>(),
                    ai0: NPC.whoAmI,
                    ai1: slot,
                    ai2: 0f
                );

                if (idx >= 0 && idx < Main.maxNPCs)
                    Main.npc[idx].netUpdate = true;
            }
            NPC.netUpdate = true;
        }

        Projectile pointer = FindActivePointer();
        Vector2 pointerPos = pointer?.Center ?? target.Center;

        FaceTowards(pointerPos.X, 24f);
        AimBodyAt(pointerPos, BodyPos, 0.16f, 1.3f);
        AlignGunsForward();

        MoveTowards(pointerPos, 0.055f, 2f, 8.5f, 0.08f, stopRadius: 14f);

        if (aiTimer0 == fireStartTick && Main.netMode != NetmodeID.MultiplayerClient)
            SetComancheCommand(2f);

        if (aiTimer0 >= fireStartTick && aiTimer0 <= fireEndTick)
        {
            frontGunActive = true;

            if (aiTimer0 % 8f == 0f)
            {
                Vector2 lookDir = new Vector2(TargetFacing, 0f).RotatedBy(NPC.rotation);
                FrontGunRecoil = new Vector2(-12f * TargetFacing, 0f);
                BodyRecoilOffset = -lookDir * 2.5f;
                FrontGunMuzzleGlow = 1f;
                SoundEngine.PlaySound(SoundID.Item11, FrontGunPivot);
                SpawnBullet(FrontGunMuzzlePos, lookDir, 18.5f, 2.5f);
            }
        }

        if (aiTimer0 == fireEndTick && Main.netMode != NetmodeID.MultiplayerClient)
            SetComancheCommand(0f);

        if (aiTimer0 >= totalDuration)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                DismissComanches();
                pointer?.Kill();
            }

            DesiredSide = -TargetFacing;
            SwitchState(ActionState.Reposition);
        }
    }

    public void Retreat()
    {
        NPC.dontTakeDamage = true;

        if (IsMarineActive())
        {
            doorGun = true;
            NPC.velocity *= 0.9f;
            NPC.rotation = MathHelper.Lerp(NPC.rotation, 0f, 0.1f);
            return;
        }

        doorGun = false;
        aiTimer0++;
        AlignGunsForward();

        NPC.velocity.X = MathHelper.Lerp(NPC.velocity.X, -TargetFacing * 10f, 0.04f);
        NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, -10f, 0.04f);
        NPC.rotation = MathHelper.Lerp(NPC.rotation, MathHelper.Clamp(NPC.velocity.X * 0.025f, -0.2f, 0.2f), 0.08f);

        if (aiTimer0 >= 90f && Main.netMode != NetmodeID.MultiplayerClient)
        {
            if (BossDownedSystem.downedApacheElfShip)
            {
                AlpineNPCRespawnSystem.unlockedAlpineSpawn = true;
                NPC.NewNPCDirect(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Alpine>(), NPC.whoAmI);
            }
            NPC.active = false;
            NPC.netUpdate = true;
        }
    }

    public void Death()
    {
        NPC.dontTakeDamage = true;

        if (IsMarineActive())
        {
            doorGun = true;
            NPC.velocity *= 0.88f;
            NPC.rotation = MathHelper.Lerp(NPC.rotation, 0f, 0.12f);
            return;
        }

        doorGun = false;
        aiTimer1++;

        float neutral = TargetFacing == -1 ? 0f : MathHelper.Pi;
        float droopOffset = TargetFacing == -1 ? -0.65f : 0.65f;

        if (aiTimer1 == 1f)
        {
            BodyGunRecoil = new Vector2(-14f * TargetFacing, -6f);
            BodyRecoilOffset = new Vector2(-8f * TargetFacing, -4f);
            SpawnGunExplosion(BodyGunPivot);
        }

        if (aiTimer1 == 18f)
        {
            TopGunRecoil = new Vector2(-16f * TargetFacing, -8f);
            BodyRecoilOffset = new Vector2(-10f * TargetFacing, -5f);
            SpawnGunExplosion(TopGunPivot);
        }

        if (aiTimer1 >= 1f)
            SpawnBurningGasTrail(BodyGunPivot);
        if (aiTimer1 >= 18f)
            SpawnBurningGasTrail(TopGunPivot);

        if (aiTimer1 <= 65f)
        {
            bodyFrame = 5;
            NPC.velocity *= 0.9f;
            NPC.rotation = MathHelper.Lerp(NPC.rotation, -0.1f * TargetFacing, 0.1f);

            TargetGunRotation = neutral + NPC.rotation + droopOffset;
            TargetTopGunRotation = neutral + NPC.rotation + droopOffset * 1.15f;
            GunRotationLerpSpeed = 0.15f;
            RotorSpinFactor = MathHelper.Lerp(1f, 0.45f, aiTimer1 / 65f);
            return;
        }

        bodyFrame = 4;
        RotorSpinFactor = MathHelper.Max(0.35f, RotorSpinFactor - 0.003f);

        if (aiTimer1 == 66f)
        {
            SpawnGunExplosion(BodyPos);
            NPC.velocity = new Vector2(TargetFacing * 3.5f, -2.5f);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.NewNPCDirect(
                    NPC.GetSource_FromAI(),
                    (int)BodyPos.X,
                    (int)(BodyPos.Y - 16f),
                    ModContent.NPCType<FallingAlpine>()
                );
            }
            NPC.netUpdate = true;
        }

        NPC.noTileCollide = false;
        NPC.velocity.X *= 0.992f;
        NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y + 0.16f, -10f, 7.5f);

        float fallNoseDown = TargetFacing == -1 ? -1.15f : 1.15f;
        NPC.rotation = MathHelper.Lerp(NPC.rotation, fallNoseDown, 0.045f);

        TargetGunRotation = neutral + NPC.rotation + droopOffset;
        TargetTopGunRotation = neutral + NPC.rotation + droopOffset;

        if (NPC.collideY || NPC.collideX || NPC.velocity.Y == 0f || aiTimer1 >= 340f)
        {
            NPC.life = 0;
            NPC.HitEffect();
            NPC.checkDead();
        }
    }
    #endregion

    #region Helpers
    private float MoveTowards(Vector2 goal, float responsiveness, float minSpeed, float maxSpeed, float accelLerp, Vector2 inheritVel = default, float stopRadius = 10f)
    {
        Vector2 toGoal = goal - NPC.Center;
        float dist = toGoal.Length();

        Vector2 desiredVel = dist > stopRadius && dist > 0.01f
            ? (toGoal / dist) * MathHelper.Clamp(dist * responsiveness, minSpeed, maxSpeed) + inheritVel
            : inheritVel;

        NPC.velocity = Vector2.Lerp(NPC.velocity, desiredVel, accelLerp);
        return dist;
    }

    private void BrakeToStop(float lerp, float snapThreshold)
    {
        NPC.velocity = Vector2.Lerp(NPC.velocity, Vector2.Zero, lerp);
        if (NPC.velocity.Length() < snapThreshold)
            NPC.velocity = Vector2.Zero;
    }

    private void SetFacing(int newFacing)
    {
        if (newFacing != -1 && newFacing != 1)
            return;

        if (TargetFacing != newFacing)
        {
            TargetFacing = newFacing;
            NPC.rotation = MathHelper.Clamp(-NPC.rotation, -MathHelper.PiOver2, MathHelper.PiOver2);
            GunRotation = MathHelper.WrapAngle(MathHelper.Pi - GunRotation);
            TopGunRotation = MathHelper.WrapAngle(MathHelper.Pi - TopGunRotation);
        }

        NPC.spriteDirection = TargetFacing;
    }

    private void FaceTowards(float targetX, float deadzone)
    {
        if (MathF.Abs(targetX - NPC.Center.X) > deadzone)
            SetFacing(targetX < NPC.Center.X ? -1 : 1);
        else
            NPC.spriteDirection = TargetFacing;
    }

    private void AimBodyAt(Vector2 worldTarget, Vector2 pivot, float rotLerp, float maxTilt)
    {
        float aimAngle = (worldTarget - pivot).ToRotation();
        float rawPlaneRot = TargetFacing == -1
            ? MathHelper.WrapAngle(aimAngle + MathHelper.Pi)
            : MathHelper.WrapAngle(aimAngle);
        float clampedRot = MathHelper.Clamp(rawPlaneRot, -maxTilt, maxTilt);
        NPC.rotation = MathHelper.Lerp(NPC.rotation, clampedRot, rotLerp);
    }

    private void AlignGunsForward()
    {
        float neutralAngle = TargetFacing == -1 ? 0f : MathHelper.Pi;
        TargetGunRotation = neutralAngle + NPC.rotation;
        TargetTopGunRotation = neutralAngle + NPC.rotation;
    }

    private void EnsureSide(Player target)
    {
        if (DesiredSide is not (-1 or 1))
            DesiredSide = NPC.Center.X < target.Center.X ? -1 : 1;
    }

    private void SpawnBullet(Vector2 origin, Vector2 direction, float speed, float spreadDegrees)
    {
        if (Main.netMode == NetmodeID.MultiplayerClient)
            return;

        Vector2 vel = direction.RotatedByRandom(MathHelper.ToRadians(spreadDegrees)) * speed;
        Projectile p = Projectile.NewProjectileDirect(
            NPC.GetSource_FromAI(),
            origin,
            vel,
            ModContent.ProjectileType<DartshipBullet>(),
            ProjectileDamage(NPC.damage / 3),
            1f,
            Main.myPlayer
        );
        p.hostile = true;
        p.friendly = false;
        p.tileCollide = false;
    }

    private Projectile FindActivePointer()
    {
        int pointerType = ModContent.ProjectileType<DartshipPointer>();
        for (int i = 0; i < Main.maxProjectiles; i++)
        {
            Projectile proj = Main.projectile[i];
            if (proj.active && proj.type == pointerType && (int)proj.ai[0] == NPC.whoAmI)
                return proj;
        }
        return null;
    }

    private void FadePatrolPoints()
    {
        int patrolType = ModContent.ProjectileType<DartshipPatrolPoint>();
        for (int i = 0; i < Main.maxProjectiles; i++)
        {
            Projectile proj = Main.projectile[i];
            if (proj.active && proj.type == patrolType && (int)proj.ai[0] == NPC.whoAmI)
            {
                proj.ai[0] = -2f;
                proj.netUpdate = true;
            }
        }
    }

    private void DismissComanches() => SetComancheCommand(1f);

    private void SetComancheCommand(float commandState)
    {
        int comancheType = ModContent.NPCType<ElfComanche>();
        for (int i = 0; i < Main.maxNPCs; i++)
        {
            NPC npc = Main.npc[i];
            if (npc.active && npc.type == comancheType && (int)npc.ai[0] == NPC.whoAmI)
            {
                npc.ai[2] = commandState;
                npc.netUpdate = true;
            }
        }
    }

    private bool IsMarineActive()
    {
        int idx = (int)marineWhoAmI;
        return idx >= 0 && idx < Main.maxNPCs && Main.npc[idx].active && Main.npc[idx].type == ModContent.NPCType<DartshipMarine>();
    }

    private void UpdateFacingAndGuns(Player target, bool trackBodyGun, bool trackTopGun)
    {
        FaceTowards(target.Center.X, 90f);

        float neutralAngle = TargetFacing == -1 ? 0f : MathHelper.Pi;
        float defaultForwardAngle = neutralAngle + NPC.rotation;
        bool farEnough = Vector2.Distance(target.Center, BodyPos) >= 80f;

        TargetGunRotation = trackBodyGun && farEnough
            ? (target.Center - BodyGunPivot).ToRotation() + MathHelper.Pi
            : defaultForwardAngle;

        TargetTopGunRotation = trackTopGun && farEnough
            ? (target.Center - TopGunPivot).ToRotation() + MathHelper.Pi
            : defaultForwardAngle;
    }

    private void HoverMoveTowards(Player target, float speedMultiplier = 1f, bool rotateBodyToVelocity = true)
    {
        EnsureSide(target);

        Vector2 desiredPos = target.Center + new Vector2(DesiredSide * 300f, -210f + MathF.Sin(HoverTimer * 0.025f) * 18f);
        Vector2 toDesired = desiredPos - NPC.Center;
        float distToDesired = toDesired.Length();
        float distToPlayer = Vector2.Distance(NPC.Center, target.Center);

        float distT = Utils.GetLerpValue(180f, 950f, distToDesired, true);
        float maxSpeed = MathHelper.Lerp(7.5f, 24f, distT) * speedMultiplier;

        if (distToPlayer < 220f && distToDesired < 260f)
            maxSpeed *= MathHelper.Clamp(distToPlayer / 220f, 0.35f, 1f);

        float responsiveness = MathHelper.Lerp(0.045f, 0.095f, distT);
        float accelLerp = MathHelper.Lerp(0.055f, 0.14f, distT);
        Vector2 inheritVel = distToDesired > 12f ? target.velocity * MathHelper.Lerp(0.25f, 0.75f, distT) : Vector2.Zero;

        MoveTowards(desiredPos, responsiveness, 1f, maxSpeed, accelLerp, inheritVel, stopRadius: 12f);

        if (rotateBodyToVelocity)
            NPC.rotation = MathHelper.Lerp(NPC.rotation, MathHelper.Clamp(NPC.velocity.X * 0.022f, -0.24f, 0.24f), 0.09f);
    }

    private void UpdateTrailHistory()
    {
        if (dashOldPositions[^1] == Vector2.Zero)
        {
            for (int i = 0; i < dashOldPositions.Length; i++)
            {
                dashOldPositions[i] = NPC.position;
                dashOldRotations[i] = NPC.rotation + MathHelper.PiOver2;
            }
        }

        for (int i = dashOldPositions.Length - 1; i > 0; i--)
        {
            dashOldPositions[i] = dashOldPositions[i - 1];
            dashOldRotations[i] = dashOldRotations[i - 1];
        }

        dashOldPositions[0] = NPC.position;
        dashOldRotations[0] = NPC.rotation + MathHelper.PiOver2;
    }

    private static void UpdateGunAnimation(bool active, ref int counter, ref int frame, int speed, int frameCount)
    {
        if (!active)
        {
            frame = 0;
            counter = 0;
            return;
        }

        if (++counter > speed)
        {
            counter = 0;
            frame = (frame + 1) % frameCount;
        }
    }

    private void UpdateKinematicsAndAnimations()
    {
        bool overdriveRotor = AIState is ActionState.RazorRotor or ActionState.Downdraft or ActionState.PrimeAssault;
        RotorSizeMultiplier = MathHelper.Lerp(RotorSizeMultiplier, overdriveRotor ? 1.1f : 1f, 0.1f);

        float baseRotorSpeed = overdriveRotor ? 0.3f : 0.2f;
        float baseRotorArc = overdriveRotor ? 1.5f : 1.25f;

        RotorSpeed = baseRotorSpeed * RotorSpinFactor;
        TailRotorSpeed = 0.4f * RotorSpinFactor;

        RotorRadiusX = 72f * RotorSizeMultiplier;
        RotorRadiusY = 16f * RotorSizeMultiplier;
        RotorWidthMultiplier = 1.5f * RotorSizeMultiplier;
        RotorAnchorOffset = UseFrontSprites ? new Vector2(0f, -38f) : new Vector2(-4f, -40f);

        RotorTrailArc = MathHelper.Max(0.25f, baseRotorArc * RotorSpinFactor);
        TailRotorTrailArc = MathHelper.Max(0.5f, 2.6f * RotorSpinFactor);

        FrontGunScaleRecoil = MathHelper.Lerp(FrontGunScaleRecoil, 0f, 0.35f);
        BodyGunScaleRecoil = MathHelper.Lerp(BodyGunScaleRecoil, 0f, 0.35f);
        TopGunScaleRecoil = MathHelper.Lerp(TopGunScaleRecoil, 0f, 0.28f);

        FrontGunMuzzleGlow = MathHelper.Max(0f, FrontGunMuzzleGlow - 0.12f);
        BodyGunMuzzleGlow = MathHelper.Max(0f, BodyGunMuzzleGlow - 0.12f);
        TopGunMuzzleGlow = MathHelper.Max(0f, TopGunMuzzleGlow - 0.08f);

        BodyPos = NPC.Center + BodyRecoilOffset;
        GunRotation = Utils.AngleLerp(GunRotation, TargetGunRotation, GunRotationLerpSpeed);
        TopGunRotation = Utils.AngleLerp(TopGunRotation, TargetTopGunRotation, GunRotationLerpSpeed);

        if (UseFrontSprites)
        {
            FrontGunPivot = BodyPos + (new Vector2(0f, 16f) * NPC.scale).RotatedBy(NPC.rotation);
            LeftBodyGunPivot = BodyPos + (new Vector2(-28f, 10f) * NPC.scale).RotatedBy(NPC.rotation);
            RightBodyGunPivot = BodyPos + (new Vector2(28f, 10f) * NPC.scale).RotatedBy(NPC.rotation);
            LeftTopGunPivot = BodyPos + (new Vector2(-24f, -18f) * NPC.scale).RotatedBy(NPC.rotation);
            RightTopGunPivot = BodyPos + (new Vector2(24f, -18f) * NPC.scale).RotatedBy(NPC.rotation);

            BodyGunPivot = LeftBodyGunPivot;
            TopGunPivot = LeftTopGunPivot;
            RotorPos = BodyPos + (RotorAnchorOffset * NPC.scale).RotatedBy(NPC.rotation);
            TailRotorPos = BodyPos;
            RopeAnchorPos = BodyPos + (new Vector2(0f, 16f) * NPC.scale).RotatedBy(NPC.rotation);

            Vector2 bodyMuzzleOffset = (new Vector2(0f, 8f) * NPC.scale).RotatedBy(NPC.rotation);
            FrontGunMuzzlePos = FrontGunPivot + (new Vector2(0f, 10f) * NPC.scale).RotatedBy(NPC.rotation);
            LeftBodyGunMuzzlePos = LeftBodyGunPivot + bodyMuzzleOffset;
            RightBodyGunMuzzlePos = RightBodyGunPivot + bodyMuzzleOffset;
            LeftTopGunMuzzlePos = LeftTopGunPivot + bodyMuzzleOffset;
            RightTopGunMuzzlePos = RightTopGunPivot + bodyMuzzleOffset;

            BodyGunMuzzlePos = LeftBodyGunMuzzlePos;
            TopGunMuzzlePos = LeftTopGunMuzzlePos;
        }
        else
        {
            FrontGunPivot = BodyPos + (new Vector2(66f * TargetFacing, 8f) * NPC.scale).RotatedBy(NPC.rotation) + (FrontGunRecoil * NPC.scale).RotatedBy(NPC.rotation);
            BodyGunPivot = BodyPos + (new Vector2(-12f * TargetFacing, 8f) * NPC.scale).RotatedBy(NPC.rotation) + BodyGunRecoil;
            TopGunPivot = BodyPos + (new Vector2(-40f * TargetFacing, -20f) * NPC.scale).RotatedBy(NPC.rotation) + TopGunRecoil;
            RotorPos = BodyPos + (new Vector2(RotorAnchorOffset.X * TargetFacing, RotorAnchorOffset.Y) * NPC.scale).RotatedBy(NPC.rotation);
            TailRotorPos = BodyPos + (new Vector2(TailRotorAnchorOffset.X * TargetFacing, TailRotorAnchorOffset.Y) * NPC.scale).RotatedBy(NPC.rotation);
            RopeAnchorPos = BodyPos + (new Vector2(12f * TargetFacing, 14f) * NPC.scale).RotatedBy(NPC.rotation);

            FrontGunMuzzlePos = FrontGunPivot + new Vector2(20f * TargetFacing, -2f).RotatedBy(NPC.rotation) * NPC.scale;
            BodyGunMuzzlePos = BodyGunPivot + new Vector2(-34f, 0f).RotatedBy(GunRotation) * NPC.scale;
            TopGunMuzzlePos = TopGunPivot + new Vector2(-28f, -4f * TargetFacing).RotatedBy(TopGunRotation) * NPC.scale;

            LeftBodyGunPivot = RightBodyGunPivot = BodyGunPivot;
            LeftTopGunPivot = RightTopGunPivot = TopGunPivot;
            LeftBodyGunMuzzlePos = RightBodyGunMuzzlePos = BodyGunMuzzlePos;
            LeftTopGunMuzzlePos = RightTopGunMuzzlePos = TopGunMuzzlePos;
        }

        BodyRecoilOffset = Vector2.Lerp(BodyRecoilOffset, Vector2.Zero, 0.14f);
        FrontGunRecoil = Vector2.Lerp(FrontGunRecoil, Vector2.Zero, 0.22f);
        BodyGunRecoil = Vector2.Lerp(BodyGunRecoil, Vector2.Zero, 0.4f);
        TopGunRecoil = Vector2.Lerp(TopGunRecoil, Vector2.Zero, 0.4f);

        bool wantsDashTrail =
            AIState == ActionState.Intro
            || HoverTimer <= 170f
            || (AIState == ActionState.ChinookSupply && aiTimer0 > 120f && aiTimer0 <= 205f)
            || (AIState == ActionState.Downdraft && aiTimer0 >= 1f && aiTimer0 <= 46f)
            || (AIState == ActionState.PrimeAssault && aiTimer0 >= 1f && aiTimer0 <= 55f);

        dashTrailOpacity = wantsDashTrail
            ? MathHelper.Min(1f, dashTrailOpacity + 0.12f)
            : MathHelper.Max(0f, dashTrailOpacity - 0.025f);

        if (AIState != ActionState.Death || aiTimer1 == 0f)
        {
            if (++bodyFrameCounter > bodyFrameSpeed)
            {
                bodyFrameCounter = 0;
                if (doorGun && bodyFrame < 4) bodyFrame++;
                else if (!doorGun && bodyFrame > 0) bodyFrame--;
            }
        }

        UpdateGunAnimation(frontGunActive, ref frontGunFrameCounter, ref frontGunFrame, gunFrameSpeed, 3);
        UpdateGunAnimation(bodyGunActive, ref bodyGunFrameCounter, ref bodyGunFrame, gunFrameSpeed, UseFrontSprites ? 4 : 3);
        UpdateGunAnimation(topGunActive, ref topGunFrameCounter, ref topGunFrame, gunFrameSpeed, 3);

        int spinDir = UseFrontSprites ? 1 : TargetFacing;
        RotorAngle = MathHelper.WrapAngle(RotorAngle + RotorSpeed * spinDir);
        TailRotorAngle = MathHelper.WrapAngle(TailRotorAngle + TailRotorSpeed * spinDir);

        for (int b = 0; b < RotorBladeCount; b++)
        {
            float bladeOffset = b * MathHelper.PiOver2;

            for (int i = 0; i < RotorTrailLength; i++)
            {
                float progress = i / (float)(RotorTrailLength - 1);

                float mainAngle = RotorAngle + bladeOffset - progress * RotorTrailArc * spinDir;
                Vector2 mainWorldVec = new Vector2(MathF.Cos(mainAngle) * RotorRadiusX, MathF.Sin(mainAngle) * RotorRadiusY).RotatedBy(NPC.rotation) * NPC.scale;
                RotorTrailPos[b][i] = RotorPos + mainWorldVec * RotorPosScale;
                RotorTrailRot[b][i] = mainWorldVec.ToRotation() + MathHelper.PiOver2;

                float tailAngle = TailRotorAngle + bladeOffset - progress * TailRotorTrailArc * spinDir;
                Vector2 tailWorldVec = new Vector2(MathF.Cos(tailAngle) * TailRotorRadiusX, MathF.Sin(tailAngle) * TailRotorRadiusY).RotatedBy(NPC.rotation) * NPC.scale;
                TailRotorTrailPos[b][i] = TailRotorPos + tailWorldVec * TailRotorPosScale;
                TailRotorTrailRot[b][i] = tailWorldVec.ToRotation() + MathHelper.PiOver2;
            }
        }
    }

    private static void SpawnGunExplosion(Vector2 pos)
    {
        SoundEngine.PlaySound(SoundID.Item14, pos);
        if (Main.dedServ)
            return;

        for (int i = 0; i < 6; i++)
        {
            Color color = Main.rand.NextFromList(Color.Orange, Color.OrangeRed, Color.SlateGray, Color.Gray);
            ParticleSystem.SpawnParticle(
                ParticleID.Streak,
                pos,
                Vector2.UnitY.RotatedByRandom(3f) * Main.rand.NextFloat(1.5f, 3.2f),
                color,
                1f,
                Main.rand.NextFloat(0.65f, 0.95f)
            );

            ParticleSystem.SpawnParticle(
                ParticleID.Gas,
                pos,
                Vector2.UnitY.RotatedByRandom(1.3f) * Main.rand.NextFloat(0.8f, 2f),
                color,
                0.9f,
                Main.rand.NextFloat(0.45f, 0.7f),
                data0: 0.03f
            );
        }
    }

    private void SpawnBurningGasTrail(Vector2 pos)
    {
        if (Main.dedServ)
            return;

        Color gasColor = Main.rand.NextFromList(Color.Orange, Color.OrangeRed, Color.SlateGray, Color.Gray);
        Vector2 driftVel = -NPC.velocity * 0.2f + Vector2.UnitY.RotatedByRandom(1.1f) * Main.rand.NextFloat(0.5f, 1.4f);

        ParticleSystem.SpawnParticle(
            ParticleID.Gas,
            pos + Main.rand.NextVector2Circular(4f, 4f),
            driftVel,
            gasColor,
            0.8f,
            Main.rand.NextFloat(0.65f, 0.85f),
            data0: 0.04f
        );
    }
    #endregion

    #region Drawing
    private Color IntroStripColors(float progressOnStrip)
    {
        Color result = Color.Lerp(Color.LightCyan, Color.SlateGray, progressOnStrip);
        result.A /= 2;
        return result * (1f - progressOnStrip) * dashTrailOpacity;
    }

    private float IntroStripWidth(float progressOnStrip) => 28f * NPC.scale;

    private Color MainRotorStripColors(float progressOnStrip)
    {
        Color baseCol = Color.Lerp(new Color(115, 130, 144, 255), new Color(35, 32, 46, 200), BackgroundProgress * 0.7f);
        baseCol.A /= 3;
        return baseCol * MathF.Pow(1f - progressOnStrip, RotorTrailPersistence);
    }

    private Color TailRotorStripColors(float progressOnStrip)
    {
        Color result = new(115, 130, 144, 255);
        result.A /= 3;
        return result * MathF.Pow(1f - progressOnStrip, TailRotorTrailPersistence);
    }

    private float MainRotorStripWidth(float progressOnStrip)
    {
        int spinDir = UseFrontSprites ? 1 : TargetFacing;
        float sampleAngle = RotorAngle + currentDrawingBlade * MathHelper.PiOver2 - progressOnStrip * RotorTrailArc * spinDir;
        Vector2 localVec = new(MathF.Cos(sampleAngle) * RotorRadiusX, MathF.Sin(sampleAngle) * RotorRadiusY);
        return localVec.Length() * RotorWidthMultiplier * NPC.scale;
    }

    private float TailRotorStripWidth(float progressOnStrip)
    {
        float sampleAngle = TailRotorAngle + currentDrawingBlade * MathHelper.PiOver2 - progressOnStrip * TailRotorTrailArc * TargetFacing;
        Vector2 localVec = new(MathF.Cos(sampleAngle) * TailRotorRadiusX, MathF.Sin(sampleAngle) * TailRotorRadiusY);
        return localVec.Length() * TailRotorWidthMultiplier * NPC.scale;
    }

    public static void DrawMuzzleFlashEffect(SpriteBatch spriteBatch, Vector2 muzzleWorldPos, float shootAngle, float glowValue, float baseScale, Vector2 velocity, Vector2 screenPos)
    {
        if (glowValue <= 0f)
            return;

        float animProgress = 1f - glowValue;
        float scaleMult = animProgress < 0.2f ? animProgress / 0.2f : (1f - animProgress) / 0.8f;
        scaleMult = MathF.Sin(scaleMult * MathHelper.PiOver2);

        Texture2D tex = muzzleTexture.Value;
        Vector2 flashOrigin = new(tex.Width / 2f, tex.Height);
        Vector2 stretch = new(1f, 1f + velocity.Length() * 0.04f);

        spriteBatch.Draw(
            tex,
            muzzleWorldPos - screenPos,
            null,
            new Color(255, 255, 255, 40),
            shootAngle + MathHelper.PiOver2,
            flashOrigin,
            stretch * (baseScale * scaleMult),
            SpriteEffects.None,
            0f
        );
    }

    public static void DrawFrontMuzzleFlashEffect(SpriteBatch spriteBatch, Vector2 muzzleWorldPos, float shootAngle, float glowValue, float baseScale, Vector2 screenPos)
    {
        if (glowValue <= 0f)
            return;

        Texture2D effectTexture = Terraria.GameContent.TextureAssets.Extra[ExtrasID.SharpTears].Value;
        Vector2 drawPosition = muzzleWorldPos - screenPos;
        Vector2 origin = effectTexture.Size() / 2f;
        Vector2 glowScale = new Vector2(1.5f, 0.1f) * (2f * baseScale * MathF.Cos(MathHelper.PiOver2 * glowValue));
        float opacity = MathF.Sqrt(glowValue);

        spriteBatch.Draw(effectTexture, drawPosition, null, new Color(255, 255, 255, 0) * opacity, shootAngle, origin, glowScale, SpriteEffects.None, 0f);
        spriteBatch.Draw(effectTexture, drawPosition, null, new Color(255, 184, 34, 0) * opacity, shootAngle + MathHelper.PiOver2, origin, glowScale, SpriteEffects.None, 0f);
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        if (BodyPos == Vector2.Zero)
            UpdateKinematicsAndAnimations();

        if (BackgroundProgress > 0f)
            drawColor = Color.Lerp(drawColor, new Color(28, 22, 34, 220), BackgroundProgress * 0.78f);

        if (dashTrailOpacity > 0.01f)
        {
            GameShaders.Misc["LightDisc"].Apply(null);
            IntroTrailStrip.PrepareStrip(dashOldPositions, dashOldRotations, IntroStripColors, IntroStripWidth, NPC.Size * 0.5f - screenPos, dashOldPositions.Length, true);
            IntroTrailStrip.DrawTrail();
            Main.pixelShader.CurrentTechnique.Passes[0].Apply();
        }

        rotorShader ??= new MiscShaderData(Main.VertexPixelShaderRef, "MagicMissile")
            .UseProjectionMatrix(true)
            .UseImage0("Images/Extra_" + 201)
            .UseImage1("Images/Extra_" + 193)
            .UseImage2("Images/Extra_" + 252)
            .UseSaturation(-2.8f)
            .UseOpacity(2f);

        rotorShader.Apply(null);

        for (int b = 0; b < RotorBladeCount; b++)
        {
            currentDrawingBlade = b;
            vertexStrip.PrepareStrip(RotorTrailPos[b], RotorTrailRot[b], MainRotorStripColors, MainRotorStripWidth, -screenPos, RotorTrailLength, true);
            vertexStrip.DrawTrail();
        }

        if (!UseFrontSprites)
        {
            for (int b = 0; b < RotorBladeCount - 1; b += 2)
            {
                currentDrawingBlade = b;
                vertexStrip.PrepareStrip(TailRotorTrailPos[b], TailRotorTrailRot[b], TailRotorStripColors, TailRotorStripWidth, -screenPos, RotorTrailLength, true);
                vertexStrip.DrawTrail();
            }
        }

        spriteBatch.End();
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

        int mIdx = (int)marineWhoAmI;
        if (mIdx >= 0 && mIdx < Main.maxNPCs)
        {
            NPC marineNPC = Main.npc[mIdx];
            if (marineNPC.active && marineNPC.type == ModContent.NPCType<DartshipMarine>() && marineNPC.ModNPC is DartshipMarine marine)
            {
                Color marineColor = Lighting.GetColor(marineNPC.Center.ToTileCoordinates());
                marine.DrawMarineAndRope(spriteBatch, screenPos, marineColor);
            }
        }

        Texture2D activeBodyTex = UseFrontSprites ? bodyFrontTexture.Value : bodyTexture.Value;
        Texture2D activeFrontGunTex = UseFrontSprites ? frontGunFrontTexture.Value : frontGunTexture.Value;
        Texture2D activeBodyGunTex = UseFrontSprites ? bodyGunFrontTexture.Value : bodyGunTexture.Value;
        Texture2D activeTopGunTex = UseFrontSprites ? topGunFrontTexture.Value : topGunTexture.Value;

        Rectangle bodyRect = UseFrontSprites ? activeBodyTex.Frame(1, 1, 0, 0) : activeBodyTex.Frame(1, BodyFrameCount, 0, bodyFrame);
        Rectangle frontGunRect = activeFrontGunTex.Frame(1, 3, 0, frontGunFrame % 3);
        Rectangle bodyGunRect = UseFrontSprites ? activeBodyGunTex.Frame(1, 4, 0, bodyGunFrame % 4) : activeBodyGunTex.Frame(1, 3, 0, bodyGunFrame % 3);
        Rectangle topGunRect = activeTopGunTex.Frame(1, 3, 0, topGunFrame % 3);

        if (UseFrontSprites)
        {
            DrawFrontPass(spriteBatch, screenPos, drawColor, activeBodyTex, activeFrontGunTex, activeBodyGunTex, activeTopGunTex, bodyRect, frontGunRect, bodyGunRect, topGunRect);
        }
        else
        {
            DrawSidePass(spriteBatch, screenPos, drawColor, activeBodyTex, activeFrontGunTex, activeBodyGunTex, activeTopGunTex, bodyRect, frontGunRect, bodyGunRect, topGunRect);
        }

        return false;
    }

    private void DrawFrontPass(
        SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor,
        Texture2D bodyTex, Texture2D frontTex, Texture2D bodyGunTex, Texture2D topGunTex,
        Rectangle bodyRect, Rectangle frontRect, Rectangle bodyGunRect, Rectangle topGunRect)
    {
        Vector2 bodyOrigin = bodyRect.Size() / 2f;
        Vector2 frontOrigin = frontRect.Size() / 2f;
        Vector2 bodyGunOrigin = bodyGunRect.Size() / 2f;
        Vector2 topGunOrigin = topGunRect.Size() / 2f;
        float flashAngle = NPC.rotation + MathHelper.PiOver2;

        float frontScale = NPC.scale * (1f + FrontGunScaleRecoil);
        float bodyGunScale = NPC.scale * (1f + BodyGunScaleRecoil);
        float topGunScale = NPC.scale * (1f + TopGunScaleRecoil);

        spriteBatch.Draw(topGunTex, LeftTopGunPivot - screenPos, topGunRect, drawColor, NPC.rotation, topGunOrigin, topGunScale, SpriteEffects.None, 0f);
        spriteBatch.Draw(topGunTex, RightTopGunPivot - screenPos, topGunRect, drawColor, NPC.rotation, topGunOrigin, topGunScale, SpriteEffects.FlipHorizontally, 0f);
        DrawFrontMuzzleFlashEffect(spriteBatch, LeftTopGunMuzzlePos, flashAngle, TopGunMuzzleGlow, 1.15f * topGunScale, screenPos);
        DrawFrontMuzzleFlashEffect(spriteBatch, RightTopGunMuzzlePos, flashAngle, TopGunMuzzleGlow, 1.15f * topGunScale, screenPos);

        spriteBatch.Draw(bodyTex, BodyPos - screenPos, bodyRect, drawColor, NPC.rotation, bodyOrigin, NPC.scale, SpriteEffects.None, 0f);

        spriteBatch.Draw(bodyGunTex, LeftBodyGunPivot - screenPos, bodyGunRect, drawColor, NPC.rotation, bodyGunOrigin, bodyGunScale, SpriteEffects.None, 0f);
        spriteBatch.Draw(bodyGunTex, RightBodyGunPivot - screenPos, bodyGunRect, drawColor, NPC.rotation, bodyGunOrigin, bodyGunScale, SpriteEffects.FlipHorizontally, 0f);
        DrawFrontMuzzleFlashEffect(spriteBatch, LeftBodyGunMuzzlePos, flashAngle, BodyGunMuzzleGlow, 1.15f * bodyGunScale, screenPos);
        DrawFrontMuzzleFlashEffect(spriteBatch, RightBodyGunMuzzlePos, flashAngle, BodyGunMuzzleGlow, 1.15f * bodyGunScale, screenPos);

        spriteBatch.Draw(frontTex, FrontGunPivot - screenPos, frontRect, drawColor, NPC.rotation, frontOrigin, frontScale, SpriteEffects.None, 0f);
        DrawFrontMuzzleFlashEffect(spriteBatch, FrontGunMuzzlePos, flashAngle, FrontGunMuzzleGlow, 1.15f * frontScale, screenPos);
    }

    private void DrawSidePass(
        SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor,
        Texture2D bodyTex, Texture2D frontTex, Texture2D bodyGunTex, Texture2D topGunTex,
        Rectangle bodyRect, Rectangle frontRect, Rectangle bodyGunRect, Rectangle topGunRect)
    {
        SpriteEffects staticEffects = TargetFacing == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        SpriteEffects gunEffects = TargetFacing == -1 ? SpriteEffects.None : SpriteEffects.FlipVertically;

        Vector2 bodyOrigin = bodyRect.Size() / 2f;
        Vector2 frontOrigin = new(TargetFacing == -1 ? frontRect.Width * 0.8f : frontRect.Width * 0.2f, frontRect.Height / 2f);
        Vector2 bodyGunOrigin = new(bodyGunRect.Width * 0.55f, bodyGunRect.Height / 2f);
        Vector2 topGunOrigin = new(topGunRect.Width * 0.65f, topGunRect.Height / 2f);

        float frontShootAngle = NPC.rotation + (TargetFacing == -1 ? MathHelper.Pi : 0f);

        spriteBatch.Draw(frontTex, FrontGunPivot - screenPos, frontRect, drawColor, NPC.rotation, frontOrigin, NPC.scale, staticEffects, 0f);
        spriteBatch.Draw(bodyTex, BodyPos - screenPos, bodyRect, drawColor, NPC.rotation, bodyOrigin, NPC.scale, staticEffects, 0f);
        DrawMuzzleFlashEffect(spriteBatch, FrontGunMuzzlePos, frontShootAngle, FrontGunMuzzleGlow, 1.35f * NPC.scale, NPC.velocity, screenPos);

        spriteBatch.Draw(bodyGunTex, BodyGunPivot - screenPos, bodyGunRect, drawColor, GunRotation, bodyGunOrigin, NPC.scale, gunEffects, 0f);
        DrawMuzzleFlashEffect(spriteBatch, BodyGunMuzzlePos, GunRotation + MathHelper.Pi, BodyGunMuzzleGlow, 1.35f * NPC.scale, NPC.velocity, screenPos);

        spriteBatch.Draw(topGunTex, TopGunPivot - screenPos, topGunRect, drawColor, TopGunRotation, topGunOrigin, NPC.scale, gunEffects, 0f);
        DrawMuzzleFlashEffect(spriteBatch, TopGunMuzzlePos, TopGunRotation + MathHelper.Pi, TopGunMuzzleGlow, 1.35f * NPC.scale, NPC.velocity, screenPos);
    }
    #endregion
}