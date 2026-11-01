using HendecamMod.Content.Projectiles.Enemies.ApacheBoss;
using HendecamMod.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.NPCs.Bosses;

public class DartshipMarine : ModNPC
{
    public static readonly Asset<Texture2D> marineTexture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/Dartship_Marine");
    public static readonly Asset<Texture2D> armTexture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/Dartship_Marine_Arm");
    public static readonly Asset<Texture2D> ropeTexture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/Dartship_Rope");

    public override string Texture => "HendecamMod/Content/NPCs/Bosses/Dartship_Marine";

    public int BossIndex => (int)NPC.ai[0];
    public ref float Timer => ref NPC.ai[1];
    public ref float SlideProgress => ref NPC.ai[2];

    public VerletChain RopeChain;
    public const int RopeSegments = 16;
    public const float MaxRopeLength = 200f;

    public int MarineFacing = -1;
    public Vector2 LastAnchorPos = Vector2.Zero;
    public Vector2 ArmRecoil = Vector2.Zero;
    public Vector2 ArmPivot = Vector2.Zero;
    public Vector2 MuzzlePos = Vector2.Zero;
    public Vector2 LockedShootDir = Vector2.Zero;
    public float ArmRotation;
    public float TargetArmRotation;
    public float MuzzleGlow;

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
        NPC.aiStyle = -1;
    }

    public override bool CheckActive() => false;

    public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => false;

    private void UpdateVerletRope(Vector2 startPos, Vector2 chopperVelocity)
    {
        if (Main.dedServ) return;

        float segLen = MaxRopeLength / RopeSegments;

        if (RopeChain?.allSticks == null)
        {
            RopeChain = PhysicsMethods.CreateVerletChain(
                RopeSegments,
                segLen,
                startPos,
                startPos + new Vector2(-chopperVelocity.X * 1.5f, 12f),
                pinStart: true,
                pinEnd: false
            );

            for (int i = 0; i < RopeChain.allSticks.Count; i++)
            {
                VerletPoint pt = RopeChain.allSticks[i].pointB;
                float factor = (i + 1) / (float)RopeSegments;
                pt.oldPos = pt.pos - new Vector2(-chopperVelocity.X * 0.45f * factor, 8f + 10f * factor);
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
                pt.pos += anchorDelta * 0.35f;
                pt.oldPos += anchorDelta * 0.35f;

                Vector2 vel = (pt.pos - pt.oldPos) * 0.985f;
                pt.oldPos = pt.pos;

                float windSway = MathF.Sin(Main.GlobalTimeWrappedHourly * 3.5f + i * 0.25f) * 0.04f;
                pt.pos += vel + new Vector2(windSway, PhysicsMethods.gravity * 1.35f);
            }
        }

        for (int i = 0; i < 24; i++)
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
        if (Main.dedServ || RopeChain?.allSticks == null) return;

        int count = RopeChain.allSticks.Count;
        for (int i = 0; i < count; i++)
        {
            float weight = MathF.Pow((i + 1) / (float)count, 1.3f);
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
        if (!bossNPC.active || bossNPC.type != ModContent.NPCType<ApacheElfShip>() || bossNPC.ModNPC is not ApacheElfShip boss ||
            (boss.AIState != ApacheElfShip.ActionState.SpecPops && boss.AIState != ApacheElfShip.ActionState.Retreat && boss.AIState != ApacheElfShip.ActionState.Death))
        {
            CleanupRope();
            NPC.active = false;
            return;
        }

        NPC.realLife = BossIndex;
        NPC.target = bossNPC.target;
        Player target = Main.player[NPC.target];

        bool forceRetreat = boss.AIState == ApacheElfShip.ActionState.Retreat || boss.AIState == ApacheElfShip.ActionState.Death;
        if (forceRetreat && Timer < 250f)
        {
            Timer = 250f;
            NPC.netUpdate = true;
        }

        Timer++;

        if (Timer < 250f)
        {
            SlideProgress = MathHelper.Clamp(SlideProgress + 0.045f, 0f, 1f);
        }
        else
        {
            float retractSpeed = forceRetreat ? 0.08f : 0.045f;
            SlideProgress = MathHelper.Clamp(SlideProgress - retractSpeed, 0f, 1f);
        }

        bool fullyDown = SlideProgress >= 0.99f && Timer < 250f && !forceRetreat;
        NPC.dontTakeDamage = SlideProgress < 0.5f || forceRetreat;

        ArmRecoil = Vector2.Lerp(ArmRecoil, Vector2.Zero, 0.35f);
        MuzzleGlow = MathHelper.Max(0f, MuzzleGlow - 0.15f);

        UpdateVerletRope(boss.RopeAnchorPos, bossNPC.velocity);
        GetPositionAndTangentAlongRope(boss.RopeAnchorPos, SlideProgress, out Vector2 ropeAttachPos, out Vector2 ropeTangent);

        float ropeTilt = (ropeTangent.ToRotation() - MathHelper.PiOver2) * 0.7f;
        NPC.rotation = Utils.AngleLerp(NPC.rotation, MathHelper.Clamp(ropeTilt, -0.55f, 0.55f), 0.2f);

        int cycleTick = (int)(Timer % 80f);

        if (!fullyDown || cycleTick < 15 || LockedShootDir == Vector2.Zero)
        {
            MarineFacing = (target.Center.X < NPC.Center.X) ? -1 : 1;
            NPC.spriteDirection = MarineFacing;
        }

        Vector2 harnessOffset = new Vector2(-2f * MarineFacing, -6f).RotatedBy(NPC.rotation);
        NPC.Center = ropeAttachPos - harnessOffset;
        NPC.velocity = Vector2.Zero;

        ArmPivot = NPC.Center + new Vector2(1f * MarineFacing, 0f).RotatedBy(NPC.rotation) + ArmRecoil;

        if (fullyDown)
        {
            if (cycleTick < 15 || LockedShootDir == Vector2.Zero)
            {
                TargetArmRotation = (target.Center - ArmPivot).ToRotation() + MathHelper.Pi;
                LockedShootDir = new Vector2(-1f, 0f).RotatedBy(TargetArmRotation);
            }
        }
        else
        {
            float neutralAngle = (MarineFacing == -1) ? 0f : MathHelper.Pi;
            TargetArmRotation = neutralAngle + NPC.rotation;
            LockedShootDir = Vector2.Zero;
        }

        ArmRotation = Utils.AngleLerp(ArmRotation, TargetArmRotation, 0.22f);
        MuzzlePos = ArmPivot + new Vector2(-24f, -2f * MarineFacing).RotatedBy(ArmRotation);

        if (fullyDown)
        {
            if (cycleTick == 20)
            {
                SoundEngine.PlaySound(SoundID.Item15, MuzzlePos);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(
                        NPC.GetSource_FromAI(),
                        MuzzlePos,
                        LockedShootDir,
                        ModContent.ProjectileType<DartshipTelegraph>(),
                        0,
                        0f,
                        Main.myPlayer,
                        0f,
                        0f,
                        20f
                    );
                }
            }

            if (cycleTick >= 30 && cycleTick % 5 == 0)
            {
                Vector2 shootDir = LockedShootDir != Vector2.Zero
                    ? LockedShootDir
                    : new Vector2(-1f, 0f).RotatedBy(ArmRotation);

                ArmRecoil = -shootDir * 1.5f;
                ApplyRopeRecoil(-shootDir * 1.8f);

                boss.BodyRecoilOffset += -shootDir * 0.5f;
                boss.NPC.velocity += -shootDir * 0.1f;

                MuzzleGlow = 1f;
                SoundEngine.PlaySound(SoundID.Item11, MuzzlePos);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 bulletVel = shootDir.RotatedByRandom(MathHelper.ToRadians(2f)) * 16f;
                    Projectile p = Projectile.NewProjectileDirect(
                        NPC.GetSource_FromAI(),
                        MuzzlePos,
                        bulletVel,
                        ProjectileID.BulletHighVelocity,
                        ApacheElfShip.ProjectileDamage(NPC.damage / 3),
                        1f,
                        Main.myPlayer
                    );
                    p.hostile = true;
                    p.friendly = false;
                    p.tileCollide = false;
                }
            }
        }

        if (Timer >= 250f && SlideProgress <= 0.01f)
        {
            CleanupRope();
            if (boss.marineWhoAmI == NPC.whoAmI)
            {
                boss.marineWhoAmI = -1f;
            }
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

            float visibleSticks = (Timer < 250f)
                ? RopeChain.allSticks.Count
                : Math.Max(1f, SlideProgress * RopeChain.allSticks.Count);

            for (int i = 0; i < (int)Math.Ceiling(visibleSticks); i++)
            {
                VerletStick stick = RopeChain.allSticks[i];
                Vector2 diff = stick.pointB.pos - stick.pointA.pos;
                float dist = diff.Length();
                if (dist <= 0.1f) continue;

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

        SpriteEffects bodyEffects = (MarineFacing == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        SpriteEffects armEffects = (MarineFacing == -1) ? SpriteEffects.None : SpriteEffects.FlipVertically;

        Texture2D mTex = marineTexture.Value;
        Texture2D aTex = armTexture.Value;

        Vector2 bodyOrigin = mTex.Size() / 2f;
        Vector2 armOrigin = new(aTex.Width * 0.68f, aTex.Height * 0.56f);

        Vector2 fallbackAnchor = (BossIndex >= 0 && BossIndex < Main.maxNPCs && Main.npc[BossIndex].ModNPC is ApacheElfShip boss) ? boss.RopeAnchorPos : NPC.Center;
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

        float shootAngle = ArmRotation + MathHelper.Pi;
        ApacheElfShip.DrawMuzzleFlashEffect(spriteBatch, muzzleDrawPos, shootAngle, MuzzleGlow, 0.9f * NPC.scale, Vector2.Zero, screenPos);
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) => false;
}