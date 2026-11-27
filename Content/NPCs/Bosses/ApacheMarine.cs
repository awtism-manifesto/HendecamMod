using HendecamMod.Common.Systems.Particles;
using HendecamMod.Content.Projectiles.Enemies.ApacheBoss;
using HendecamMod.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.NPCs.Bosses;

public class DartshipMarine : ModNPC
{
    public static readonly Asset<Texture2D> marineTexture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/Dartship_Marine");
    public static readonly Asset<Texture2D> armTexture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/Dartship_Marine_Arm");
    public static readonly Asset<Texture2D> ropeTexture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/Dartship_Rope");

    public override string Texture => "HendecamMod/Content/NPCs/Bosses/Dartship_Marine";

    public static bool ExpertMode => Main.expertMode;
    public static bool MasterMode => Main.masterMode;
    public static bool LegendaryMode => (Main.getGoodWorld || Main.zenithWorld) && Main.masterMode;

    public int AttackCount = 3;
    public int ShotsPerBurst = 5;
    public float SweepAngleDegrees = 25f;
    public float MaxStayTime = 300f;
    public float SlideSpeed = 0.045f;
    public float AimTime = 16f;
    public float TelegraphTime = 16f;
    public float BurstTime = 20f;
    public float BurstCooldown = 23f;

    public int BossIndex => (int)NPC.ai[0];
    public ref float Timer => ref NPC.ai[1];
    public ref float SlideProgress => ref NPC.ai[2];
    public ref float LockedBaseArmRot => ref NPC.ai[3];

    public VerletChain RopeChain;
    public const int RopeSegments = 16;
    public const float MaxRopeLength = 200f;

    public int MarineFacing = -1;
    public Vector2 LastAnchorPos;
    public Vector2 ArmRecoil;
    public Vector2 ArmPivot;
    public Vector2 MuzzlePos;
    public float ArmRotation;
    public float TargetArmRotation;
    public float MuzzleGlow;

    private float burstTimer;
    private int burstsDone;
    private int shotsFired;
    private bool isRetracting;

    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 1;
        NPCID.Sets.MPAllowedEnemies[Type] = true;
        NPCID.Sets.DontDoHardmodeScaling[Type] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Shimmer] = true;
        NPCID.Sets.NPCBestiaryDrawModifiers hideModifier = new() { Hide = true };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, hideModifier);
    }

    public override void SetDefaults()
    {
        NPC.width = 24;
        NPC.height = 32;
        NPC.damage = 65;
        NPC.defense = 40;
        NPC.lifeMax = 40000;
        NPC.knockBackResist = 0f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.dontTakeDamage = true;
        NPC.aiStyle = -1;
    }

    public override void OnSpawn(IEntitySource source)
    {
        if (LegendaryMode)
        {
            AttackCount = 4;
            ShotsPerBurst = 7;
            SweepAngleDegrees = 32f;
            MaxStayTime = 340f;
            SlideSpeed = 0.06f;
            AimTime = 12f;
            TelegraphTime = 12f;
            BurstTime = 16f;
            BurstCooldown = 16f;
        }
        else if (MasterMode)
        {
            AttackCount = 4;
            ShotsPerBurst = 6;
            SweepAngleDegrees = 28f;
            MaxStayTime = 340f;
            SlideSpeed = 0.055f;
            AimTime = 14f;
            TelegraphTime = 14f;
            BurstTime = 18f;
            BurstCooldown = 18f;
        }
        else if (ExpertMode)
        {
            AttackCount = 3;
            ShotsPerBurst = 5;
            SweepAngleDegrees = 25f;
            MaxStayTime = 300f;
            SlideSpeed = 0.045f;
            AimTime = 16f;
            TelegraphTime = 16f;
            BurstTime = 20f;
            BurstCooldown = 23f;
        }
        else
        {
            AttackCount = 3;
            ShotsPerBurst = 4;
            SweepAngleDegrees = 22f;
            MaxStayTime = 260f;
            SlideSpeed = 0.04f;
            AimTime = 18f;
            TelegraphTime = 18f;
            BurstTime = 22f;
            BurstCooldown = 26f;
        }

        NPC.netUpdate = true;
    }
    public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
    {
        modifiers.FinalDamage *= 1.5f;
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write((sbyte)MarineFacing);
        writer.Write(AttackCount);
        writer.Write(ShotsPerBurst);
        writer.Write(SweepAngleDegrees);
        writer.Write(MaxStayTime);
        writer.Write(SlideSpeed);
        writer.Write(AimTime);
        writer.Write(TelegraphTime);
        writer.Write(BurstTime);
        writer.Write(BurstCooldown);
        writer.Write(burstTimer);
        writer.Write(burstsDone);
        writer.Write(shotsFired);
        writer.Write(isRetracting);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        MarineFacing = reader.ReadSByte();
        AttackCount = reader.ReadInt32();
        ShotsPerBurst = reader.ReadInt32();
        SweepAngleDegrees = reader.ReadSingle();
        MaxStayTime = reader.ReadSingle();
        SlideSpeed = reader.ReadSingle();
        AimTime = reader.ReadSingle();
        TelegraphTime = reader.ReadSingle();
        BurstTime = reader.ReadSingle();
        BurstCooldown = reader.ReadSingle();
        burstTimer = reader.ReadSingle();
        burstsDone = reader.ReadInt32();
        shotsFired = reader.ReadInt32();
        isRetracting = reader.ReadBoolean();
    }

    public override bool CheckActive() => false;

    public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => false;

    public void TriggerRetract()
    {
        if (!isRetracting)
        {
            isRetracting = true;
            NPC.netUpdate = true;
        }
    }

    private void SetFacing(int newFacing)
    {
        if (newFacing is not (-1 or 1))
            return;

        if (MarineFacing != newFacing)
        {
            MarineFacing = newFacing;
            ArmRotation = MathHelper.WrapAngle(MathHelper.Pi - ArmRotation);
        }

        NPC.spriteDirection = MarineFacing;
    }

    private float GetSweepAngleOffset(int shotIndex)
    {
        if (ShotsPerBurst <= 1)
            return 0f;

        float t = shotIndex / (float)(ShotsPerBurst - 1) - 0.5f;
        return t * MathHelper.ToRadians(SweepAngleDegrees) * -MarineFacing;
    }

    private void UpdateVerletRope(Vector2 startPos, Vector2 chopperVelocity)
    {
        if (Main.dedServ)
            return;

        float segLen = MaxRopeLength / RopeSegments;

        if (RopeChain?.allSticks == null)
        {
            RopeChain = PhysicsMethods.CreateVerletChain(
                RopeSegments,
                segLen,
                startPos,
                startPos + new Vector2(-chopperVelocity.X * 0.8f, 12f),
                pinStart: true,
                pinEnd: false
            );

            for (int i = 0; i < RopeChain.allSticks.Count; i++)
            {
                VerletPoint pt = RopeChain.allSticks[i].pointB;
                float factor = (i + 1) / (float)RopeSegments;
                pt.oldPos = pt.pos - new Vector2(-chopperVelocity.X * 0.25f * factor, 6f + 8f * factor);
            }

            LastAnchorPos = startPos;
        }

        Vector2 anchorDelta = startPos - LastAnchorPos;
        LastAnchorPos = startPos;

        RopeChain.UpdateStart(startPos);

        for (int i = 0; i < RopeChain.allSticks.Count; i++)
        {
            VerletStick stick = RopeChain.allSticks[i];
            stick.length = segLen;

            VerletPoint pt = stick.pointB;
            if (!pt.isVerletPinned)
            {
                pt.pos += anchorDelta * 0.22f;
                pt.oldPos += anchorDelta * 0.22f;

                Vector2 vel = (pt.pos - pt.oldPos) * 0.94f;
                pt.oldPos = pt.pos;

                float windSway = MathF.Sin(Main.GlobalTimeWrappedHourly * 2.8f + i * 0.25f) * 0.02f;
                pt.pos += vel + new Vector2(windSway, PhysicsMethods.gravity * 1.45f);
            }
        }

        for (int i = 0; i < 28; i++)
        {
            foreach (VerletStick stick in RopeChain.allSticks)
            {
                stick.Update();
            }
        }
    }

    private void GetPositionAndTangentAlongRope(Vector2 fallbackStart, float progress, out Vector2 pos, out Vector2 tangent)
    {
        if (RopeChain?.allSticks == null || RopeChain.allSticks.Count == 0)
        {
            pos = fallbackStart + new Vector2(0f, MathHelper.Clamp(progress, 0f, 1f) * MaxRopeLength);
            tangent = Vector2.UnitY;
            return;
        }

        float scaled = MathHelper.Clamp(progress, 0f, 1f) * RopeChain.allSticks.Count;
        int index = Math.Clamp((int)scaled, 0, RopeChain.allSticks.Count - 1);
        float localT = scaled - index;

        VerletStick stick = RopeChain.allSticks[index];
        pos = Vector2.Lerp(stick.pointA.pos, stick.pointB.pos, localT);
        tangent = (stick.pointB.pos - stick.pointA.pos).SafeNormalize(Vector2.UnitY);
    }

    private void ApplyRopeRecoil(Vector2 impulse)
    {
        if (Main.dedServ || RopeChain?.allSticks == null)
            return;

        int count = RopeChain.allSticks.Count;
        for (int i = 0; i < count; i++)
        {
            float weight = MathF.Pow((i + 1) / (float)count, 1.35f);
            RopeChain.allSticks[i].pointB.pos += impulse * weight;
        }
    }

    private void CleanupRope()
    {
        RopeChain?.Kill();
        RopeChain = null;
    }

    public override void AI()
    {
        if (BossIndex < 0 || BossIndex >= Main.maxNPCs)
        {
            CleanupRope();
            NPC.active = false;
            return;
        }

        NPC bossNPC = Main.npc[BossIndex];
        if (!bossNPC.active
            || bossNPC.type != ModContent.NPCType<ApacheElfShip>()
            || bossNPC.ModNPC is not ApacheElfShip boss
            || boss.AIState is not (ApacheElfShip.ActionState.SpecPops or ApacheElfShip.ActionState.Retreat or ApacheElfShip.ActionState.Death))
        {
            CleanupRope();
            NPC.active = false;
            return;
        }

        NPC.realLife = BossIndex;
        NPC.target = bossNPC.target;
        Player target = Main.player[NPC.target];

        bool forceRetreat = boss.AIState is ApacheElfShip.ActionState.Retreat or ApacheElfShip.ActionState.Death;
        if (forceRetreat || Timer >= MaxStayTime || burstsDone >= AttackCount)
            isRetracting = true;

        Timer++;

        if (!isRetracting)
        {
            SlideProgress = MathHelper.Clamp(SlideProgress + SlideSpeed, 0f, 1f);
        }
        else
        {
            float retractSpeed = forceRetreat ? MathF.Max(0.08f, SlideSpeed * 1.8f) : SlideSpeed;
            SlideProgress = MathHelper.Clamp(SlideProgress - retractSpeed, 0f, 1f);
        }

        bool fullyDown = SlideProgress >= 0.99f && !isRetracting;
        NPC.dontTakeDamage = SlideProgress < 0.5f || isRetracting;

        ArmRecoil = Vector2.Lerp(ArmRecoil, Vector2.Zero, 0.25f);
        MuzzleGlow = MathHelper.Max(0f, MuzzleGlow - 0.15f);

        UpdateVerletRope(boss.RopeAnchorPos, bossNPC.velocity);
        GetPositionAndTangentAlongRope(boss.RopeAnchorPos, SlideProgress, out Vector2 ropeAttachPos, out Vector2 ropeTangent);

        float ropeTilt = (ropeTangent.ToRotation() - MathHelper.PiOver2) * 0.5f;
        NPC.rotation = MathHelper.Lerp(NPC.rotation, MathHelper.Clamp(ropeTilt, -0.35f, 0.35f), 0.18f);

        float telegraphTick = AimTime;
        float fireStartTick = AimTime + TelegraphTime;
        float fireEndTick = fireStartTick + BurstTime;
        float cycleEndTick = fireEndTick + BurstCooldown;

        if (!fullyDown || burstTimer < telegraphTick)
            SetFacing(target.Center.X < NPC.Center.X ? -1 : 1);

        Vector2 harnessOffset = new Vector2(-2f * MarineFacing, -6f).RotatedBy(NPC.rotation);
        NPC.Center = ropeAttachPos - harnessOffset;
        NPC.velocity = Vector2.Zero;

        ArmPivot = NPC.Center + new Vector2(1f * MarineFacing, 0f).RotatedBy(NPC.rotation) + ArmRecoil;

        if (fullyDown)
        {
            burstTimer++;

            if (burstTimer < telegraphTick)
            {
                LockedBaseArmRot = (target.Center - ArmPivot).ToRotation() + MathHelper.Pi;
                TargetArmRotation = LockedBaseArmRot + GetSweepAngleOffset(0);

                if (burstTimer == telegraphTick - 1f)
                    NPC.netUpdate = true;
            }
            else if (burstTimer < fireStartTick)
            {
                TargetArmRotation = LockedBaseArmRot + GetSweepAngleOffset(0);
            }
            else if (burstTimer <= fireEndTick)
            {
                float burstT = BurstTime > 0f ? MathHelper.Clamp((burstTimer - fireStartTick) / BurstTime, 0f, 1f) : 1f;
                float sweepOffset = ShotsPerBurst > 1
                    ? (burstT - 0.5f) * MathHelper.ToRadians(SweepAngleDegrees) * -MarineFacing
                    : 0f;
                TargetArmRotation = LockedBaseArmRot + sweepOffset;
            }
        }
        else
        {
            float neutralAngle = MarineFacing == -1 ? 0f : MathHelper.Pi;
            TargetArmRotation = neutralAngle + NPC.rotation;
        }

        ArmRotation = Utils.AngleLerp(ArmRotation, TargetArmRotation, 0.25f);
        MuzzlePos = ArmPivot + new Vector2(-24f, -2f * MarineFacing).RotatedBy(ArmRotation);

        if (fullyDown)
        {
            if (burstTimer == telegraphTick)
            {
                SoundEngine.PlaySound(SoundID.Item15, MuzzlePos);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < ShotsPerBurst; i++)
                    {
                        float sweepArmRot = LockedBaseArmRot + GetSweepAngleOffset(i);
                        Vector2 sweepDir = new Vector2(-1f, 0f).RotatedBy(sweepArmRot);
                        Vector2 telegraphOrigin = ArmPivot + new Vector2(-24f, -2f * MarineFacing).RotatedBy(sweepArmRot);

                        Projectile.NewProjectile(
                            NPC.GetSource_FromAI(),
                            telegraphOrigin,
                            sweepDir,
                            ModContent.ProjectileType<DartshipTelegraph>(),
                            0,
                            0f,
                            Main.myPlayer,
                            0f,
                            0f,
                            TelegraphTime + 2f
                        );
                    }
                }
            }

            if (burstTimer >= fireStartTick && shotsFired < ShotsPerBurst)
            {
                float shotSpacing = ShotsPerBurst > 1 ? BurstTime / (ShotsPerBurst - 1) : 0f;

                while (shotsFired < ShotsPerBurst && burstTimer >= fireStartTick + shotsFired * shotSpacing)
                {
                    float shotArmRot = LockedBaseArmRot + GetSweepAngleOffset(shotsFired);
                    Vector2 shootDir = new Vector2(-1f, 0f).RotatedBy(shotArmRot);
                    Vector2 shotOrigin = ArmPivot + new Vector2(-24f, -2f * MarineFacing).RotatedBy(shotArmRot);

                    ArmRecoil = -shootDir * 4f;
                    ApplyRopeRecoil(-shootDir * 1.1f);

                    boss.BodyRecoilOffset += -shootDir * 0.45f;
                    boss.NPC.velocity += -shootDir * 0.06f;

                    MuzzleGlow = 1f;
                    SoundEngine.PlaySound(SoundID.Item11, shotOrigin);
                    Color color = Main.rand.NextFromList(Color.Orange, Color.OrangeRed, Color.SlateGray, Color.Gray);

                    ParticleSystem.SpawnParticle(
                    ParticleID.Gas,
                    MuzzlePos + Main.rand.NextVector2Circular(12f, 12f),
                    shootDir.RotatedByRandom(1.2f) * Main.rand.NextFloat(0.8f, 2.2f),
                    color,
                    0.9f,
                    Main.rand.NextFloat(0.55f, 0.85f),
                    data0: 0.03f);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile p = Projectile.NewProjectileDirect(
                            NPC.GetSource_FromAI(),
                            shotOrigin,
                            shootDir * 16.5f,
                            ModContent.ProjectileType<DartshipBullet>(),
                            boss.ProjectileDamage(NPC.damage / 3),
                            1f,
                            Main.myPlayer
                        );
                        p.hostile = true;
                        p.friendly = false;
                        p.tileCollide = false;
                    }

                    shotsFired++;
                }
            }

            if (burstTimer >= cycleEndTick && shotsFired >= ShotsPerBurst)
            {
                burstsDone++;
                burstTimer = 0f;
                shotsFired = 0;

                if (burstsDone >= AttackCount)
                    isRetracting = true;

                NPC.netUpdate = true;
            }
        }

        if (isRetracting && SlideProgress <= 0.01f)
        {
            CleanupRope();
            if (boss.marineWhoAmI == NPC.whoAmI)
                boss.marineWhoAmI = -1f;

            NPC.active = false;
            NPC.netUpdate = true;
        }
    }

    public override void OnKill()
    {
        CleanupRope();
    }

    public void DrawMarineAndRope(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        if (RopeChain?.allSticks != null)
        {
            Texture2D rTex = ropeTexture.Value;
            Vector2 rOrigin = rTex.Size() / 2f;

            float visibleSticks = !isRetracting
                ? RopeChain.allSticks.Count
                : Math.Max(1f, SlideProgress * RopeChain.allSticks.Count);

            int drawCount = Math.Clamp((int)MathF.Ceiling(visibleSticks), 0, RopeChain.allSticks.Count);
            for (int i = 0; i < drawCount; i++)
            {
                VerletStick stick = RopeChain.allSticks[i];
                Vector2 diff = stick.pointB.pos - stick.pointA.pos;
                float dist = diff.Length();
                if (dist <= 0.1f)
                    continue;

                Vector2 center = Vector2.Lerp(stick.pointA.pos, stick.pointB.pos, 0.5f);
                float angle = diff.ToRotation() - MathHelper.PiOver2;
                Color ropeLight = Lighting.GetColor(center.ToTileCoordinates());
                Vector2 scale = new(1f, (dist + 2f) / rTex.Height);

                spriteBatch.Draw(
                    rTex,
                    center - screenPos,
                    null,
                    ropeLight,
                    angle,
                    rOrigin,
                    scale,
                    SpriteEffects.None,
                    0f
                );
            }
        }

        SpriteEffects bodyEffects = MarineFacing == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        SpriteEffects armEffects = MarineFacing == -1 ? SpriteEffects.None : SpriteEffects.FlipVertically;

        Texture2D mTex = marineTexture.Value;
        Texture2D aTex = armTexture.Value;

        Vector2 bodyOrigin = mTex.Size() / 2f;
        Vector2 armOrigin = new(aTex.Width * 0.68f, aTex.Height * 0.56f);

        Vector2 fallbackAnchor = BossIndex >= 0 && BossIndex < Main.maxNPCs && Main.npc[BossIndex].ModNPC is ApacheElfShip boss
            ? boss.RopeAnchorPos
            : NPC.Center;
        GetPositionAndTangentAlongRope(fallbackAnchor, SlideProgress, out Vector2 ropeAttachPos, out _);
        Vector2 bodyDrawCenter = ropeAttachPos - new Vector2(-2f * MarineFacing, -6f).RotatedBy(NPC.rotation);
        Vector2 armDrawPivot = bodyDrawCenter + new Vector2(1f * MarineFacing, 0f).RotatedBy(NPC.rotation) + ArmRecoil;
        Vector2 muzzleDrawPos = armDrawPivot + new Vector2(-24f, -2f * MarineFacing).RotatedBy(ArmRotation);

        spriteBatch.Draw(
            mTex,
            bodyDrawCenter - screenPos,
            null,
            drawColor,
            NPC.rotation,
            bodyOrigin,
            NPC.scale,
            bodyEffects,
            0f
        );

        spriteBatch.Draw(
            aTex,
            armDrawPivot - screenPos,
            null,
            drawColor,
            ArmRotation,
            armOrigin,
            NPC.scale,
            armEffects,
            0f
        );

        ApacheElfShip.DrawMuzzleFlashEffect(spriteBatch, muzzleDrawPos, ArmRotation + MathHelper.Pi, MuzzleGlow, 0.9f * NPC.scale, Vector2.Zero, screenPos);
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) => false;
}