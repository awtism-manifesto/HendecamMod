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

    public override string Texture => "HendecamMod/Content/NPCs/Bosses/ApacheElfShip";

    public float aiTimer0 { get => NPC.localAI[0]; set => NPC.localAI[0] = value; }
    public float aiTimer1 { get => NPC.localAI[1]; set => NPC.localAI[1] = value; }
    public float aiTimer2 { get => NPC.localAI[2]; set => NPC.localAI[2] = value; }
    public float attackCount { get => NPC.ai[2]; set => NPC.ai[2] = value; }
    public float marineWhoAmI { get => NPC.ai[3]; set => NPC.ai[3] = value; }

    public static bool ExpertMode => Main.expertMode;
    public static bool MasterMode => Main.masterMode;
    public static bool LegendaryMode => (Main.getGoodWorld || Main.zenithWorld) && Main.masterMode;

    public enum ActionState
    {
        Intro,
        Chase,
        Reposition,
        RazorRotor,
        DartshipBarrage,
        SpecPops,
        ChinookSupply,
        Retreat,
        Death
    }

    public ActionState AIState
    {
        get => (ActionState)NPC.ai[0];
        set => NPC.ai[0] = (float)value;
    }

    public MiscShaderData RotorShader = new MiscShaderData(Main.VertexPixelShaderRef, "MagicMissile")
        .UseProjectionMatrix(true)
        .UseImage0("Images/Extra_" + 201)
        .UseImage1("Images/Extra_" + 193)
        .UseImage2("Images/Extra_" + 252)
        .UseSaturation(-2.8f)
        .UseOpacity(2f);

    public static VertexStrip vertexStrip = new();
    public VertexStrip IntroTrailStrip = new();
    private readonly Vector2[] dashOldPositions = new Vector2[40];
    private readonly float[] dashOldRotations = new float[40];

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

    public int TargetFacing = -1;
    public int DesiredSide = -1;
    public float HoverTimer;

    public Vector2 BodyPos = Vector2.Zero;
    public Vector2 RotorPos = Vector2.Zero;
    public Vector2 TailRotorPos = Vector2.Zero;
    public Vector2 FrontGunPivot = Vector2.Zero;
    public Vector2 BodyGunPivot = Vector2.Zero;
    public Vector2 TopGunPivot = Vector2.Zero;
    public Vector2 FrontGunMuzzlePos = Vector2.Zero;
    public Vector2 BodyGunMuzzlePos = Vector2.Zero;
    public Vector2 TopGunMuzzlePos = Vector2.Zero;
    public Vector2 RopeAnchorPos = Vector2.Zero;

    public Vector2 BodyRecoilOffset = Vector2.Zero;
    public Vector2 FrontGunRecoil = Vector2.Zero;
    public Vector2 BodyGunRecoil = Vector2.Zero;
    public Vector2 TopGunRecoil = Vector2.Zero;
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

    public static int ProjectileDamage(int damage)
    {
        if (MasterMode)
            return (int)(damage / 3.5f);
        if (ExpertMode)
            return (int)(damage / 2.5f);
        return damage;
    }

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
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        aiTimer0 = reader.ReadSingle();
        aiTimer1 = reader.ReadSingle();
        aiTimer2 = reader.ReadSingle();
        TargetFacing = reader.ReadSByte();
        DesiredSide = reader.ReadSByte();
        doorGun = reader.ReadBoolean();
    }

    public override void OnSpawn(IEntitySource source)
    {
        aiTimer0 = 0f;
        aiTimer1 = 0f;
        aiTimer2 = 0f;
        attackCount = 0f;
        marineWhoAmI = -1f;
        RotorSpinFactor = 1f;
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
            TargetFacing = (player.Center.X < NPC.Center.X) ? -1 : 1;
            NPC.spriteDirection = TargetFacing;
            DesiredSide = -TargetFacing;
        }
        NPC.velocity = new Vector2(TargetFacing * 4f, 6f);
        NPC.netUpdate = true;
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AlpineTrophy>(), 10));
        LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
        var parameters = new DropOneByOne.Parameters
        {
            ChanceNumerator = 1,
            ChanceDenominator = 1,
            MinimumStackPerChunkBase = 4,
            MaximumStackPerChunkBase = 6,
            MinimumItemDropsCount = 40,
            MaximumItemDropsCount = 60,
        };
        notExpertRule.OnSuccess(new DropOneByOne(ModContent.ItemType<Fivenato>(), parameters));
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

    public override bool CheckDead()
    {
        if (AIState != ActionState.Death)
        {
            NPC.life = 1;
            NPC.dontTakeDamage = true;
            AIState = ActionState.Death;
            aiTimer0 = 0f;
            aiTimer1 = 0f;
            NPC.netUpdate = true;
            return false;
        }

        if (aiTimer1 >= 340f || (aiTimer1 > 75f && (NPC.collideY || NPC.collideX || NPC.velocity.Y == 0f)))
        {
            return true;
        }

        NPC.life = 1;
        return false;
    }

    public override void OnKill()
    {
        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("AGH!"), new Color(185, 105, 105));
        SoundEngine.PlaySound(SoundID.Item14, NPC.Center);

        if (!Main.dedServ)
        {
            Vector2[] crashPoints = [BodyPos, BodyGunPivot, TopGunPivot];
            foreach (Vector2 pt in crashPoints)
            {
                for (int i = 0; i < 20; i++)
                {
                    Color randColor = Main.rand.NextFromList(Color.Orange, Color.OrangeRed, Color.SlateGray, Color.Gray);
                    ParticleSystem.SpawnParticle(
                        ParticleID.Streak,
                        pt + Main.rand.NextVector2Circular(10f, 10f),
                        Vector2.UnitY.RotatedByRandom(3f) * Main.rand.NextFloat(1.5f, 3.8f),
                        randColor,
                        1f,
                        Main.rand.NextFloat(0.75f, 1.15f)
                    );

                    ParticleSystem.SpawnParticle(
                        ParticleID.Gas,
                        pt + Main.rand.NextVector2Circular(12f, 12f),
                        Vector2.UnitY.RotatedByRandom(1.3f) * Main.rand.NextFloat(0.8f, 2.2f),
                        randColor,
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

    public override bool CanHitPlayer(Player target, ref int cooldownSlot)
    {
        cooldownSlot = ImmunityCooldownID.Bosses;
        return AIState != ActionState.Intro && AIState != ActionState.Retreat && AIState != ActionState.Death;
    }

    private bool IsMarineActive()
    {
        int mIdx = (int)marineWhoAmI;
        return mIdx >= 0 && mIdx < Main.maxNPCs && Main.npc[mIdx].active && Main.npc[mIdx].type == ModContent.NPCType<DartshipMarine>();
    }

    private bool PlayerCheck(Player player)
    {
        if (!player.active || player.dead || Vector2.Distance(NPC.Center, player.Center) > 4000f)
        {
            NPC.TargetClosest();
            player = Main.player[NPC.target];
            if (!player.active || player.dead || Vector2.Distance(NPC.Center, player.Center) > 4000f)
            {
                if (AIState != ActionState.Retreat && AIState != ActionState.Death)
                {
                    AIState = ActionState.Retreat;
                    aiTimer0 = 0f;
                    aiTimer1 = 0f;
                    doorGun = false;
                    NPC.netUpdate = true;
                }
                return false;
            }
        }

        if (NPC.timeLeft < 600)
        {
            NPC.timeLeft = 600;
        }
        return true;
    }

    private void CheckSideSwitch(Player target)
    {
        if (DesiredSide == 0)
        {
            DesiredSide = (NPC.Center.X < target.Center.X) ? -1 : 1;
        }

        if (target.Center.X > NPC.Center.X)
        {
            DesiredSide = -DesiredSide;
        }
    }

    private void UpdateFacingAndGuns(Player target, bool trackBodyGun, bool trackTopGun)
    {
        float deltaX = target.Center.X - NPC.Center.X;
        if (MathF.Abs(deltaX) > 90f)
        {
            TargetFacing = (deltaX < 0f) ? -1 : 1;
        }
        NPC.spriteDirection = TargetFacing;

        float neutralAngle = (TargetFacing == -1) ? 0f : MathHelper.Pi;
        float defaultForwardAngle = neutralAngle + NPC.rotation;
        float distanceToPlayer = Vector2.Distance(target.Center, BodyPos);

        TargetGunRotation = (trackBodyGun && distanceToPlayer >= 80f)
            ? (target.Center - BodyGunPivot).ToRotation() + MathHelper.Pi
            : defaultForwardAngle;

        TargetTopGunRotation = (trackTopGun && distanceToPlayer >= 80f)
            ? (target.Center - TopGunPivot).ToRotation() + MathHelper.Pi
            : defaultForwardAngle;
    }

    private void HoverMoveTowards(Player target, float speedMultiplier = 1f, bool rotateBodyToVelocity = true)
    {
        if (DesiredSide == 0)
        {
            DesiredSide = (NPC.Center.X < target.Center.X) ? -1 : 1;
        }

        float hoverSideOffset = DesiredSide * 300f;
        float hoverHeightOffset = -210f + MathF.Sin(HoverTimer * 0.025f) * 18f;
        Vector2 desiredPos = target.Center + new Vector2(hoverSideOffset, hoverHeightOffset);

        Vector2 toDesired = desiredPos - NPC.Center;
        float distToDesired = toDesired.Length();
        float distToPlayer = Vector2.Distance(NPC.Center, target.Center);

        float maxSpeed = 6.5f * speedMultiplier;
        if (distToPlayer < 220f)
        {
            maxSpeed *= MathHelper.Clamp(distToPlayer / 220f, 0.25f, 1f);
        }

        Vector2 desiredVelocity = Vector2.Zero;
        if (distToDesired > 12f)
        {
            float speed = MathHelper.Clamp(distToDesired * 0.035f, 0.5f, maxSpeed);
            desiredVelocity = Vector2.Normalize(toDesired) * speed;
        }

        NPC.velocity = Vector2.Lerp(NPC.velocity, desiredVelocity, 0.045f);

        if (rotateBodyToVelocity)
        {
            NPC.rotation = MathHelper.Lerp(NPC.rotation, MathHelper.Clamp(NPC.velocity.X * 0.025f, -0.18f, 0.18f), 0.08f);
        }
    }

    private void UpdateTrailHistory()
    {
        if (dashOldPositions[39] == Vector2.Zero)
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

    private static void UpdateGunAnimation(bool active, ref int counter, ref int frame, int speed)
    {
        if (active)
        {
            if (++counter > speed)
            {
                counter = 0;
                frame = (frame + 1) % 3;
            }
        }
        else
        {
            frame = 0;
            counter = 0;
        }
    }

    private void UpdateKinematicsAndAnimations()
    {
        float targetRotorMult = (AIState == ActionState.RazorRotor) ? 1.1f : 1f;
        RotorSizeMultiplier = MathHelper.Lerp(RotorSizeMultiplier, targetRotorMult, 0.1f);

        float baseRotorSpeed = (AIState == ActionState.RazorRotor) ? 0.3f : 0.2f;
        float baseRotorArc = (AIState == ActionState.RazorRotor) ? 1.50f : 1.25f;

        RotorSpeed = baseRotorSpeed * RotorSpinFactor;
        TailRotorSpeed = 0.4f * RotorSpinFactor;
        RotorRadiusX = 72f * RotorSizeMultiplier;
        RotorRadiusY = 16f * RotorSizeMultiplier;
        RotorTrailArc = MathHelper.Max(0.25f, baseRotorArc * RotorSpinFactor);
        TailRotorTrailArc = MathHelper.Max(0.5f, 2.6f * RotorSpinFactor);
        RotorWidthMultiplier = 1.5f * RotorSizeMultiplier;

        BodyRecoilOffset = Vector2.Lerp(BodyRecoilOffset, Vector2.Zero, 0.1f);
        FrontGunRecoil = Vector2.Lerp(FrontGunRecoil, Vector2.Zero, 0.4f);
        BodyGunRecoil = Vector2.Lerp(BodyGunRecoil, Vector2.Zero, 0.4f);
        TopGunRecoil = Vector2.Lerp(TopGunRecoil, Vector2.Zero, 0.4f);

        FrontGunMuzzleGlow = MathHelper.Max(0f, FrontGunMuzzleGlow - 0.12f);
        BodyGunMuzzleGlow = MathHelper.Max(0f, BodyGunMuzzleGlow - 0.12f);
        TopGunMuzzleGlow = MathHelper.Max(0f, TopGunMuzzleGlow - 0.08f);

        BodyPos = NPC.Center + BodyRecoilOffset;

        Vector2 frontAnchor = new Vector2(66f * TargetFacing, 8f).RotatedBy(NPC.rotation);
        Vector2 bodyGunAnchor = new Vector2(-12f * TargetFacing, 8f).RotatedBy(NPC.rotation);
        Vector2 topGunAnchor = new Vector2(-40f * TargetFacing, -20f).RotatedBy(NPC.rotation);
        Vector2 rotorAnchor = new Vector2(RotorAnchorOffset.X * TargetFacing, RotorAnchorOffset.Y).RotatedBy(NPC.rotation);
        Vector2 tailRotorAnchor = new Vector2(TailRotorAnchorOffset.X * TargetFacing, TailRotorAnchorOffset.Y).RotatedBy(NPC.rotation);
        Vector2 ropeAnchor = new Vector2(12f * TargetFacing, 14f).RotatedBy(NPC.rotation);

        FrontGunPivot = BodyPos + frontAnchor + FrontGunRecoil.RotatedBy(NPC.rotation);
        BodyGunPivot = BodyPos + bodyGunAnchor + BodyGunRecoil;
        TopGunPivot = BodyPos + topGunAnchor + TopGunRecoil;
        RotorPos = BodyPos + rotorAnchor;
        TailRotorPos = BodyPos + tailRotorAnchor;
        RopeAnchorPos = BodyPos + ropeAnchor;

        GunRotation = Utils.AngleLerp(GunRotation, TargetGunRotation, GunRotationLerpSpeed);
        TopGunRotation = Utils.AngleLerp(TopGunRotation, TargetTopGunRotation, GunRotationLerpSpeed);

        FrontGunMuzzlePos = FrontGunPivot + new Vector2(20f * TargetFacing, 2f).RotatedBy(NPC.rotation);
        BodyGunMuzzlePos = BodyGunPivot + new Vector2(-34f, 0f).RotatedBy(GunRotation);
        TopGunMuzzlePos = TopGunPivot + new Vector2(-28f, -4f * TargetFacing).RotatedBy(TopGunRotation);

        if (AIState != ActionState.Death || aiTimer1 == 0f)
        {
            if (++bodyFrameCounter > bodyFrameSpeed)
            {
                bodyFrameCounter = 0;
                if (doorGun)
                {
                    if (bodyFrame < 4) bodyFrame++;
                }
                else
                {
                    if (bodyFrame > 0) bodyFrame--;
                }
            }
        }

        UpdateGunAnimation(frontGunActive, ref frontGunFrameCounter, ref frontGunFrame, gunFrameSpeed);
        UpdateGunAnimation(bodyGunActive, ref bodyGunFrameCounter, ref bodyGunFrame, gunFrameSpeed);
        UpdateGunAnimation(topGunActive, ref topGunFrameCounter, ref topGunFrame, gunFrameSpeed);

        RotorAngle = MathHelper.WrapAngle(RotorAngle + RotorSpeed * TargetFacing);
        TailRotorAngle = MathHelper.WrapAngle(TailRotorAngle + TailRotorSpeed * TargetFacing);

        for (int b = 0; b < RotorBladeCount; b++)
        {
            float bladeOffset = b * MathHelper.PiOver2;

            for (int i = 0; i < RotorTrailLength; i++)
            {
                float progress = i / (float)(RotorTrailLength - 1);

                float mainAngle = RotorAngle + bladeOffset - (progress * RotorTrailArc * TargetFacing);
                Vector2 mainLocalVec = new(MathF.Cos(mainAngle) * RotorRadiusX, MathF.Sin(mainAngle) * RotorRadiusY);
                Vector2 mainWorldVec = mainLocalVec.RotatedBy(NPC.rotation) * NPC.scale;
                RotorTrailPos[b][i] = RotorPos + mainWorldVec * RotorPosScale;
                RotorTrailRot[b][i] = mainWorldVec.ToRotation() + MathHelper.PiOver2;

                float tailAngle = TailRotorAngle + bladeOffset - (progress * TailRotorTrailArc * TargetFacing);
                Vector2 tailLocalVec = new(MathF.Cos(tailAngle) * TailRotorRadiusX, MathF.Sin(tailAngle) * TailRotorRadiusY);
                Vector2 tailWorldVec = tailLocalVec.RotatedBy(NPC.rotation) * NPC.scale;
                TailRotorTrailPos[b][i] = TailRotorPos + tailWorldVec * TailRotorPosScale;
                TailRotorTrailRot[b][i] = tailWorldVec.ToRotation() + MathHelper.PiOver2;
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

        switch (AIState)
        {
            case ActionState.Intro:
                if (PlayerCheck(player)) Intro(player);
                break;

            case ActionState.Chase:
                if (PlayerCheck(player)) Chase(player);
                break;

            case ActionState.Reposition:
                if (PlayerCheck(player)) Reposition(player);
                break;

            case ActionState.RazorRotor:
                if (PlayerCheck(player)) RazorRotor(player);
                break;

            case ActionState.DartshipBarrage:
                if (PlayerCheck(player)) DartshipBarrage(player);
                break;

            case ActionState.SpecPops:
                if (PlayerCheck(player)) SpecPops(player);
                break;

            case ActionState.ChinookSupply:
                if (PlayerCheck(player)) ChinookSupply(player);
                break;

            case ActionState.Retreat:
                Retreat();
                break;

            case ActionState.Death:
                Death();
                break;
        }

        UpdateKinematicsAndAnimations();
    }

    private void Intro(Player target)
    {
        aiTimer0++;
        NPC.dontTakeDamage = true;

        if (aiTimer0 == 1f)
        {
            TargetFacing = (target.Center.X < NPC.Center.X) ? -1 : 1;
            NPC.spriteDirection = TargetFacing;
            DesiredSide = -TargetFacing;
            GunRotation = (TargetFacing == -1) ? 0f : MathHelper.Pi;
            TopGunRotation = GunRotation;
        }

        UpdateFacingAndGuns(target, false, false);

        Vector2 hoverPos = target.Center + new Vector2(DesiredSide * 300f, -210f);

        if (aiTimer0 < 45f && NPC.Center.Y < target.Center.Y + 40f)
        {
            NPC.velocity.X = MathHelper.Lerp(NPC.velocity.X, TargetFacing * 6f, 0.08f);
            NPC.velocity.Y = Math.Min(22f, NPC.velocity.Y + 0.75f);

            float diveTilt = 0.55f * TargetFacing;
            NPC.rotation = Utils.AngleLerp(NPC.rotation, diveTilt, 0.15f);
        }
        else
        {
            Vector2 toHover = hoverPos - NPC.Center;
            float dist = toHover.Length();

            Vector2 pullUpVel = Vector2.Zero;
            if (dist > 12f)
            {
                float speed = MathHelper.Clamp(dist * 0.065f, 2f, 16f);
                pullUpVel = Vector2.Normalize(toHover) * speed;
            }

            NPC.velocity = Vector2.Lerp(NPC.velocity, pullUpVel, 0.08f);
            float pullUpTilt = MathHelper.Clamp(NPC.velocity.X * 0.025f + (NPC.velocity.Y * 0.015f * TargetFacing), -0.35f, 0.35f);
            NPC.rotation = Utils.AngleLerp(NPC.rotation, pullUpTilt, 0.12f);

            if (aiTimer0 >= 105f || (aiTimer0 >= 60f && dist < 50f))
            {
                NPC.dontTakeDamage = false;
                aiTimer0 = 0f;
                AIState = ActionState.Chase;
                NPC.netUpdate = true;
            }
        }
    }

    private void Chase(Player target)
    {
        if (aiTimer0 == 0f)
        {
            CheckSideSwitch(target);
        }

        doorGun = false;
        UpdateFacingAndGuns(target, false, false);
        HoverMoveTowards(target, 1f);

        aiTimer0++;
        if (aiTimer0 >= 150f)
        {
            aiTimer0 = 0f;
            attackCount = 0f;
            int pick = (int)(aiTimer2++ % 4);
            AIState = pick switch
            {
                0 => ActionState.DartshipBarrage,
                1 => ActionState.RazorRotor,
                2 => ActionState.SpecPops,
                _ => ActionState.ChinookSupply
            };
            NPC.netUpdate = true;
        }
    }

    private void Reposition(Player target)
    {
        doorGun = false;
        UpdateFacingAndGuns(target, false, false);
        aiTimer0++;

        if (DesiredSide == 0)
        {
            DesiredSide = -TargetFacing;
        }

        Vector2 swoopTarget = target.Center + new Vector2(DesiredSide * 340f, -240f + MathF.Cos(HoverTimer * 0.03f) * 20f);
        Vector2 dir = swoopTarget - NPC.Center;
        float dist = dir.Length();

        Vector2 targetVel = Vector2.Zero;
        if (dist > 16f)
        {
            float speed = MathHelper.Clamp(dist * 0.04f, 1f, 8f);
            targetVel = Vector2.Normalize(dir) * speed;
        }

        NPC.velocity = Vector2.Lerp(NPC.velocity, targetVel, 0.05f);
        NPC.rotation = MathHelper.Lerp(NPC.rotation, MathHelper.Clamp(NPC.velocity.X * 0.025f, -0.2f, 0.2f), 0.08f);

        if (aiTimer0 >= 150f || dist < 45f)
        {
            aiTimer0 = 0f;
            AIState = ActionState.Chase;
            NPC.netUpdate = true;
        }
    }

    private void RazorRotor(Player target)
    {
        if (aiTimer0 == 0f && attackCount == 0f)
        {
            CheckSideSwitch(target);
        }

        aiTimer0++;

        int sideToBoss = (target.Center.X < NPC.Center.X) ? -1 : 1;
        if (MathF.Abs(target.Center.X - NPC.Center.X) > 60f)
        {
            TargetFacing = sideToBoss;
        }
        NPC.spriteDirection = TargetFacing;

        Vector2 desiredPos = target.Center + new Vector2(-TargetFacing * 200f, -140f);
        Vector2 toDesired = desiredPos - NPC.Center;

        Vector2 targetVel = toDesired.SafeNormalize(Vector2.Zero) * MathHelper.Clamp(toDesired.Length() * 0.09f, 2f, 13.5f);
        NPC.velocity = Vector2.Lerp(NPC.velocity, targetVel, 0.1f);

        float slashSpeed = 20f;
        float flightTime = Vector2.Distance(RotorPos, target.Center) / slashSpeed;
        Vector2 interceptPos = new(target.Center.X + target.velocity.X * flightTime, target.Bottom.Y - 10f);
        float fireAngle = (interceptPos - RotorPos).ToRotation();

        float targetPlaneRot = (TargetFacing == -1) ? fireAngle + MathHelper.Pi : fireAngle;
        NPC.rotation = Utils.AngleLerp(NPC.rotation, targetPlaneRot, 0.18f);

        UpdateFacingAndGuns(target, false, false);

        if (aiTimer0 >= 30f)
        {
            aiTimer0 = 1f;
            Vector2 planeDir = new Vector2(TargetFacing, 0f).RotatedBy(NPC.rotation);
            SoundEngine.PlaySound(SoundID.Item71, RotorPos);
            BodyRecoilOffset = -planeDir * 4.5f;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(
                    NPC.GetSource_FromAI(),
                    RotorPos,
                    planeDir * slashSpeed,
                    ModContent.ProjectileType<RazorRotorSlash>(),
                    ProjectileDamage(NPC.damage / 2),
                    2f,
                    Main.myPlayer
                );
            }

            if (++attackCount >= 7f)
            {
                aiTimer0 = 0f;
                attackCount = 0f;
                DesiredSide = -TargetFacing;
                AIState = ActionState.Reposition;
                NPC.netUpdate = true;
            }
        }
    }

    private void DartshipBarrage(Player target)
    {
        if (aiTimer0 == 0f && attackCount == 0f)
        {
            CheckSideSwitch(target);
        }

        aiTimer0++;

        if (aiTimer0 >= 1f && aiTimer0 <= 120f)
        {
            UpdateFacingAndGuns(target, false, false);

            Vector2 desiredPos = new(target.Center.X + DesiredSide * 520f, target.Center.Y);
            Vector2 toDesired = desiredPos - NPC.Center;
            float dist = toDesired.Length();

            Vector2 desiredVelocity = Vector2.Zero;
            if (dist > 10f)
            {
                float speed = MathHelper.Clamp(dist * 0.08f, 1.5f, 12f);
                desiredVelocity = Vector2.Normalize(toDesired) * speed;
            }

            NPC.velocity = Vector2.Lerp(NPC.velocity, desiredVelocity, 0.08f);
            NPC.rotation = MathHelper.Lerp(NPC.rotation, MathHelper.Clamp(NPC.velocity.X * 0.02f, -0.12f, 0.12f), 0.1f);
        }

        if (aiTimer0 >= 130f && aiTimer0 <= 180f)
        {
            TargetFacing = -DesiredSide;
            NPC.spriteDirection = TargetFacing;
            UpdateFacingAndGuns(target, false, true);

            NPC.velocity *= 0.8f;
            if (NPC.velocity.Length() < 0.2f)
            {
                NPC.velocity = Vector2.Zero;
            }
            NPC.rotation = Utils.AngleLerp(NPC.rotation, 0f, 0.2f);

            if (aiTimer0 == 160f)
            {
                SoundEngine.PlaySound(SoundID.Item15, BodyGunMuzzlePos);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 forwardDir = new(TargetFacing, 0f);
                    Projectile.NewProjectile(
                        NPC.GetSource_FromAI(),
                        BodyGunMuzzlePos,
                        forwardDir,
                        ModContent.ProjectileType<DartshipTelegraph>(),
                        0,
                        0f,
                        Main.myPlayer,
                        0f,
                        0f,
                        30f
                    );
                }
            }
        }
        if (aiTimer0 >= 220f && aiTimer0 <= 300f)
        {
            TargetFacing = -DesiredSide;
            NPC.spriteDirection = TargetFacing;
            UpdateFacingAndGuns(target, false, true);

            float yDiff = target.Center.Y - NPC.Center.Y;
            float targetVelY = MathHelper.Clamp(yDiff * 0.02f, -1.5f, 1.5f);

            NPC.velocity = Vector2.Lerp(NPC.velocity, new Vector2(0f, targetVelY), 0.2f);
            NPC.rotation = Utils.AngleLerp(NPC.rotation, 0f, 0.15f);

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

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 bodyVel = bodyShootDir.RotatedByRandom(MathHelper.ToRadians(5f)) * 20.5f;
                    int bulletDamage = ProjectileDamage(NPC.damage / 3);

                    Projectile p = Projectile.NewProjectileDirect(
                        NPC.GetSource_FromAI(),
                        BodyGunMuzzlePos,
                        bodyVel,
                        ModContent.ProjectileType<DartshipBullet>(),
                        bulletDamage,
                        1f,
                        Main.myPlayer
                    );
                    p.hostile = true;
                    p.friendly = false;
                    p.tileCollide = false;
                }
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
                    Projectile p = Projectile.NewProjectileDirect(
                        NPC.GetSource_FromAI(),
                        TopGunMuzzlePos,
                        rocketDir * 12f,
                        ProjectileID.Missile,
                        ProjectileDamage(NPC.damage / 2),
                        2f,
                        Main.myPlayer
                    );
                    p.hostile = true;
                    p.friendly = false;
                }
            }
        }

        if (aiTimer0 > 310f)
        {
            aiTimer0 = 0f;
            DesiredSide = -DesiredSide;
            NPC.netUpdate = true;

            if (++attackCount >= 3f)
            {
                attackCount = 0f;
                AIState = ActionState.Reposition;
                NPC.netUpdate = true;
            }
        }
    }

    private void SpecPops(Player target)
    {
        if (aiTimer0 == 0f)
        {
            CheckSideSwitch(target);
        }

        aiTimer0++;

        bool marineActive = IsMarineActive();
        doorGun = aiTimer0 < 340f || marineActive;

        UpdateFacingAndGuns(target, false, false);

        Vector2 hoverPos = target.Center + new Vector2(DesiredSide * 260f, -260f + MathF.Sin(HoverTimer * 0.03f) * 14f);
        Vector2 toDesired = hoverPos - NPC.Center;
        float dist = toDesired.Length();

        Vector2 desiredVel = Vector2.Zero;
        if (dist > 10f)
        {
            float speed = MathHelper.Clamp(dist * 0.045f, 0.5f, 5.5f);
            desiredVel = Vector2.Normalize(toDesired) * speed;
        }

        NPC.velocity = Vector2.Lerp(NPC.velocity, desiredVel, 0.05f);
        NPC.rotation = MathHelper.Lerp(NPC.rotation, MathHelper.Clamp(NPC.velocity.X * 0.02f, -0.14f, 0.14f), 0.08f);

        if (aiTimer0 == 24f && Main.netMode != NetmodeID.MultiplayerClient)
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

        if (aiTimer0 >= 360f && !marineActive)
        {
            doorGun = false;
            marineWhoAmI = -1f;
            aiTimer0 = 0f;
            attackCount = 0f;
            DesiredSide = -DesiredSide;
            AIState = ActionState.Reposition;
            NPC.netUpdate = true;
        }
    }

    private void ChinookSupply(Player target)
    {
        aiTimer0++;

        int dashDir = -DesiredSide;
        float startX = target.Center.X + DesiredSide * 560f;
        float dashAltitudeY = target.Center.Y - 320f;

        if (aiTimer0 <= 95f)
        {
            doorGun = false;
            UpdateFacingAndGuns(target, false, false);

            Vector2 prepPos = new(startX, dashAltitudeY);
            Vector2 toPrep = prepPos - NPC.Center;
            float dist = toPrep.Length();

            Vector2 desiredVel = Vector2.Zero;
            if (dist > 12f)
            {
                float speed = MathHelper.Clamp(dist * 0.075f, 1.5f, 13f);
                desiredVel = Vector2.Normalize(toPrep) * speed;
            }

            NPC.velocity = Vector2.Lerp(NPC.velocity, desiredVel, 0.08f);
            NPC.rotation = MathHelper.Lerp(NPC.rotation, MathHelper.Clamp(NPC.velocity.X * 0.02f, -0.15f, 0.15f), 0.1f);

            bool onStartSide = (DesiredSide == -1) ? (NPC.Center.X < target.Center.X - 380f) : (NPC.Center.X > target.Center.X + 380f);
            if (aiTimer0 == 95f && (!onStartSide || Math.Abs(NPC.Center.Y - dashAltitudeY) > 90f))
            {
                aiTimer0 = 94f;
            }
        }
        else if (aiTimer0 <= 120f)
        {
            doorGun = true;
            TargetFacing = dashDir;
            NPC.spriteDirection = TargetFacing;

            float neutralAngle = (TargetFacing == -1) ? 0f : MathHelper.Pi;
            TargetGunRotation = neutralAngle + NPC.rotation;
            TargetTopGunRotation = neutralAngle + NPC.rotation;

            NPC.velocity *= 0.82f;
            NPC.rotation = Utils.AngleLerp(NPC.rotation, -dashDir * 0.12f, 0.15f);

            if (aiTimer0 == 120f)
            {
                SoundEngine.PlaySound(SoundID.Item89, NPC.Center);
            }
        }
        else if (aiTimer0 <= 176f)
        {
            doorGun = true;
            TargetFacing = dashDir;
            NPC.spriteDirection = TargetFacing;

            float neutralAngle = (TargetFacing == -1) ? 0f : MathHelper.Pi;
            TargetGunRotation = neutralAngle + NPC.rotation;
            TargetTopGunRotation = neutralAngle + NPC.rotation;

            float dashSpeed = 24f;
            float yAdjust = MathHelper.Clamp((dashAltitudeY - NPC.Center.Y) * 0.05f, -3f, 3f);
            NPC.velocity = Vector2.Lerp(NPC.velocity, new Vector2(dashDir * dashSpeed, yAdjust), 0.22f);
            NPC.rotation = Utils.AngleLerp(NPC.rotation, dashDir * 0.24f, 0.2f);

            if (aiTimer0 % 7f == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item42, RopeAnchorPos);
                BodyRecoilOffset += new Vector2(0f, -3f);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 crateVel = new(NPC.velocity.X * 0.18f, 3.5f);
                    Projectile.NewProjectile(
                        NPC.GetSource_FromAI(),
                        RopeAnchorPos,
                        crateVel,
                        ModContent.ProjectileType<ChinookSupplyCrate>(),
                        ProjectileDamage(NPC.damage / 2),
                        2f,
                        Main.myPlayer
                    );
                }
            }
        }
        else if (aiTimer0 <= 215f)
        {
            doorGun = false;
            UpdateFacingAndGuns(target, false, false);
            NPC.velocity *= 0.9f;
            NPC.rotation = Utils.AngleLerp(NPC.rotation, 0f, 0.12f);
        }
        else
        {
            aiTimer0 = 0f;
            DesiredSide = -DesiredSide;
            NPC.netUpdate = true;

            if (++attackCount >= 2f)
            {
                attackCount = 0f;
                AIState = ActionState.Reposition;
                NPC.netUpdate = true;
            }
        }
    }

    private static void SpawnGunExplosion(Vector2 pos)
    {
        SoundEngine.PlaySound(SoundID.Item14, pos);
        if (Main.dedServ) return;

        for (int i = 0; i < 6; i++)
        {
            Color randColor = Main.rand.NextFromList(Color.Orange, Color.OrangeRed, Color.SlateGray, Color.Gray);
            ParticleSystem.SpawnParticle(
                ParticleID.Streak,
                pos,
                Vector2.UnitY.RotatedByRandom(3f) * Main.rand.NextFloat(1.5f, 3.2f),
                randColor,
                1f,
                Main.rand.NextFloat(0.65f, 0.95f)
            );

            ParticleSystem.SpawnParticle(
                ParticleID.Gas,
                pos,
                Vector2.UnitY.RotatedByRandom(1.3f) * Main.rand.NextFloat(0.8f, 2f),
                randColor,
                0.9f,
                Main.rand.NextFloat(0.45f, 0.7f),
                data0: 0.03f
            );
        }
    }

    private void SpawnBurningGasTrail(Vector2 pos)
    {
        if (Main.dedServ) return;

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

    private void Retreat()
    {
        NPC.dontTakeDamage = true;

        if (IsMarineActive())
        {
            doorGun = true;
            NPC.velocity *= 0.9f;
            NPC.rotation = Utils.AngleLerp(NPC.rotation, 0f, 0.1f);
            return;
        }

        doorGun = false;
        aiTimer0++;

        float neutralAngle = (TargetFacing == -1) ? 0f : MathHelper.Pi;
        TargetGunRotation = neutralAngle + NPC.rotation;
        TargetTopGunRotation = neutralAngle + NPC.rotation;

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

    private void Death()
    {
        NPC.dontTakeDamage = true;

        if (IsMarineActive())
        {
            doorGun = true;
            NPC.velocity *= 0.88f;
            NPC.rotation = Utils.AngleLerp(NPC.rotation, 0f, 0.12f);
            return;
        }

        doorGun = false;
        aiTimer1++;

        float neutral = (TargetFacing == -1) ? 0f : MathHelper.Pi;
        float droopOffset = (TargetFacing == -1) ? -0.65f : 0.65f;

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
            NPC.rotation = Utils.AngleLerp(NPC.rotation, -0.1f * TargetFacing, 0.1f);

            TargetGunRotation = neutral + NPC.rotation + droopOffset;
            TargetTopGunRotation = neutral + NPC.rotation + (droopOffset * 1.15f);
            GunRotationLerpSpeed = 0.15f;

            RotorSpinFactor = MathHelper.Lerp(1f, 0.45f, aiTimer1 / 65f);
        }
        else
        {
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

            float fallNoseDown = (TargetFacing == -1) ? -1.15f : 1.15f;
            NPC.rotation = Utils.AngleLerp(NPC.rotation, fallNoseDown, 0.045f);

            TargetGunRotation = neutral + NPC.rotation + droopOffset;
            TargetTopGunRotation = neutral + NPC.rotation + droopOffset;

            if (NPC.collideY || NPC.collideX || NPC.velocity.Y == 0f || aiTimer1 >= 340f)
            {
                NPC.life = 0;
                NPC.HitEffect();
                NPC.checkDead();
            }
        }
    }

    private Color IntroStripColors(float progressOnStrip)
    {
        Color result = Color.Lerp(Color.Cyan, Color.White, progressOnStrip);
        result.A /= 2;
        return result * (1f - progressOnStrip);
    }

    private float IntroStripWidth(float progressOnStrip) => 28f * NPC.scale;

    private Color MainRotorStripColors(float progressOnStrip)
    {
        Color result = new(115, 130, 144, 255);
        result.A /= 3;
        return result * MathF.Pow(1f - progressOnStrip, RotorTrailPersistence);
    }

    private Color TailRotorStripColors(float progressOnStrip)
    {
        Color result = new(115, 130, 144, 255);
        result.A /= 3;
        return result * MathF.Pow(1f - progressOnStrip, TailRotorTrailPersistence);
    }

    private float MainRotorStripWidth(float progressOnStrip)
    {
        float bladeOffset = currentDrawingBlade * MathHelper.PiOver2;
        float sampleAngle = RotorAngle + bladeOffset - (progressOnStrip * RotorTrailArc * TargetFacing);
        Vector2 localVec = new(MathF.Cos(sampleAngle) * RotorRadiusX, MathF.Sin(sampleAngle) * RotorRadiusY);
        return localVec.Length() * RotorWidthMultiplier * NPC.scale;
    }

    private float TailRotorStripWidth(float progressOnStrip)
    {
        float bladeOffset = currentDrawingBlade * MathHelper.PiOver2;
        float sampleAngle = TailRotorAngle + bladeOffset - (progressOnStrip * TailRotorTrailArc * TargetFacing);
        Vector2 localVec = new(MathF.Cos(sampleAngle) * TailRotorRadiusX, MathF.Sin(sampleAngle) * TailRotorRadiusY);
        return localVec.Length() * TailRotorWidthMultiplier * NPC.scale;
    }

    public static void DrawMuzzleFlashEffect(SpriteBatch spriteBatch, Vector2 muzzleWorldPos, float shootAngle, float glowValue, float baseScale, Vector2 velocity, Vector2 screenPos)
    {
        if (glowValue <= 0f) return;

        float animProgress = 1f - glowValue;
        float scaleMultiplier = (animProgress < 0.2f) ? animProgress / 0.2f : (1f - animProgress) / 0.8f;
        scaleMultiplier = MathF.Sin(scaleMultiplier * MathHelper.PiOver2);

        Texture2D tex = muzzleTexture.Value;
        float scale = baseScale * scaleMultiplier;
        Vector2 flashOrigin = new(tex.Width / 2f, tex.Height);
        Vector2 stretch = new(1f, 1f + velocity.Length() * 0.04f);

        spriteBatch.Draw(
            tex,
            muzzleWorldPos - screenPos,
            null,
            new Color(255, 255, 255, 40),
            shootAngle + MathHelper.PiOver2,
            flashOrigin,
            stretch * scale,
            SpriteEffects.None,
            0f
        );
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        if (BodyPos == Vector2.Zero)
        {
            UpdateKinematicsAndAnimations();
        }

        bool drawDashTrail = AIState == ActionState.Intro || (AIState == ActionState.ChinookSupply && aiTimer0 > 120f && aiTimer0 <= 190f);
        if (drawDashTrail)
        {
            GameShaders.Misc["LightDisc"].Apply(null);
            IntroTrailStrip.PrepareStrip(dashOldPositions, dashOldRotations, IntroStripColors, IntroStripWidth, NPC.Size * 0.5f - screenPos, dashOldPositions.Length, true);
            IntroTrailStrip.DrawTrail();
            Main.pixelShader.CurrentTechnique.Passes[0].Apply();
        }

        SpriteEffects staticEffects = (TargetFacing == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        SpriteEffects aimingGunEffects = (TargetFacing == -1) ? SpriteEffects.None : SpriteEffects.FlipVertically;

        Rectangle bodyRect = bodyTexture.Value.Frame(1, BodyFrameCount, 0, bodyFrame);
        Rectangle frontGunRect = frontGunTexture.Value.Frame(1, 3, 0, frontGunFrame);
        Rectangle bodyGunRect = bodyGunTexture.Value.Frame(1, 3, 0, bodyGunFrame);
        Rectangle topGunRect = topGunTexture.Value.Frame(1, 3, 0, topGunFrame);

        Vector2 bodyOrigin = bodyRect.Size() / 2f;
        Vector2 frontGunOrigin = new((TargetFacing == -1) ? frontGunRect.Width * 0.8f : frontGunRect.Width * 0.2f, frontGunRect.Height / 2f);
        Vector2 bodyGunOrigin = new(bodyGunRect.Width * 0.55f, bodyGunRect.Height / 2f);
        Vector2 topGunOrigin = new(topGunRect.Width * 0.65f, topGunRect.Height / 2f);

        float frontShootAngle = NPC.rotation + ((TargetFacing == -1) ? MathHelper.Pi : 0f);
        float bodyShootAngle = GunRotation + MathHelper.Pi;
        float topShootAngle = TopGunRotation + MathHelper.Pi;

        RotorShader.Apply(null);

        for (int b = 0; b < RotorBladeCount; b++)
        {
            currentDrawingBlade = b;
            vertexStrip.PrepareStrip(RotorTrailPos[b], RotorTrailRot[b], MainRotorStripColors, MainRotorStripWidth, -screenPos, RotorTrailLength, true);
            vertexStrip.DrawTrail();
        }

        for (int b = 0; b < RotorBladeCount - 1; b += 2)
        {
            currentDrawingBlade = b;
            vertexStrip.PrepareStrip(TailRotorTrailPos[b], TailRotorTrailRot[b], TailRotorStripColors, TailRotorStripWidth, -screenPos, RotorTrailLength, true);
            vertexStrip.DrawTrail();
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

        spriteBatch.Draw(
            frontGunTexture.Value,
            FrontGunPivot - screenPos,
            frontGunRect,
            drawColor,
            NPC.rotation,
            frontGunOrigin,
            NPC.scale,
            staticEffects,
            0f
        );
        DrawMuzzleFlashEffect(spriteBatch, FrontGunMuzzlePos, frontShootAngle, FrontGunMuzzleGlow, 1.35f * NPC.scale, NPC.velocity, screenPos);

        spriteBatch.Draw(
            bodyTexture.Value,
            BodyPos - screenPos,
            bodyRect,
            drawColor,
            NPC.rotation,
            bodyOrigin,
            NPC.scale,
            staticEffects,
            0f
        );

        spriteBatch.Draw(
            bodyGunTexture.Value,
            BodyGunPivot - screenPos,
            bodyGunRect,
            drawColor,
            GunRotation,
            bodyGunOrigin,
            NPC.scale,
            aimingGunEffects,
            0f
        );
        DrawMuzzleFlashEffect(spriteBatch, BodyGunMuzzlePos, bodyShootAngle, BodyGunMuzzleGlow, 1.35f * NPC.scale, NPC.velocity, screenPos);

        spriteBatch.Draw(
            topGunTexture.Value,
            TopGunPivot - screenPos,
            topGunRect,
            drawColor,
            TopGunRotation,
            topGunOrigin,
            NPC.scale,
            aimingGunEffects,
            0f
        );
        DrawMuzzleFlashEffect(spriteBatch, TopGunMuzzlePos, topShootAngle, TopGunMuzzleGlow, 1.35f * NPC.scale, NPC.velocity, screenPos);

        return false;
    }
}