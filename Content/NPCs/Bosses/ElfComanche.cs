using HendecamMod.Content.Projectiles.Enemies.ApacheBoss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.NPCs.Bosses;

public class ElfComanche : ModNPC
{
    public static readonly Asset<Texture2D> bodyTexture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/ElfComanche");

    public override string Texture => "HendecamMod/Content/NPCs/Bosses/ElfComanche";

    public ref float OwnerWhoAmI => ref NPC.ai[0];
    public ref float FormationSlot => ref NPC.ai[1];
    public ref float CommandState => ref NPC.ai[2];
    public ref float HoverTimer => ref NPC.localAI[0];
    public ref float RetreatTimer => ref NPC.localAI[1];

    public int TargetFacing = -1;

    public Vector2 BodyPos;
    public Vector2 RotorPos;
    public Vector2 TailRotorPos;
    public Vector2 FrontGunPivot;
    public Vector2 FrontGunMuzzlePos;

    public Vector2 BodyRecoilOffset;
    public Vector2 FrontGunRecoil;
    public float FrontGunMuzzleGlow;

    public int gunFrameSpeed = 3;
    public int frontGunFrame;
    public int frontGunFrameCounter;
    public bool frontGunActive;

    public const int RotorBladeCount = 4;
    public const int RotorTrailLength = 24;
    public Vector2[][] RotorTrailPos = new Vector2[RotorBladeCount][];
    public float[][] RotorTrailRot = new float[RotorBladeCount][];
    public float RotorAngle;
    public float RotorSpeed = 0.2f;
    public float RotorRadiusX = 48f;
    public float RotorRadiusY = 11f;
    public float RotorTrailArc = 1.25f;
    public float RotorTrailPersistence = 1f;
    public float RotorWidthMultiplier = 1.5f;
    public float RotorPosScale = 0.55f;
    public float RotorSizeMultiplier = 1f;
    public float RotorSpinFactor = 1f;
    public Vector2 RotorAnchorOffset = new(-4f, -22f);

    public Vector2[][] TailRotorTrailPos = new Vector2[RotorBladeCount][];
    public float[][] TailRotorTrailRot = new float[RotorBladeCount][];
    public float TailRotorAngle;
    public float TailRotorSpeed = 0.4f;
    public float TailRotorRadiusX = 10f;
    public float TailRotorRadiusY = 10f;
    public float TailRotorTrailArc = 2.6f;
    public float TailRotorTrailPersistence = 0.5f;
    public float TailRotorWidthMultiplier = 1.2f;
    public float TailRotorPosScale = 0.75f;
    public Vector2 TailRotorAnchorOffset = new(-54f, -6f);

    private int currentDrawingBlade;
    private float entryTrailOpacity;
    private readonly Vector2[] dashOldPositions = new Vector2[40];
    private readonly float[] dashOldRotations = new float[40];
    private MiscShaderData rotorShader;

    public static VertexStrip vertexStrip = new();
    public VertexStrip EntryTrailStrip = new();

    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 1;
        NPCID.Sets.MPAllowedEnemies[Type] = true;
        NPCID.Sets.TrailCacheLength[Type] = 40;
        NPCID.Sets.TrailingMode[Type] = 2;
    }

    public override void SetDefaults()
    {
        NPC.damage = 50;
        NPC.defense = 25;
        NPC.lifeMax = 1000;
        NPC.width = 96;
        NPC.height = 44;
        NPC.knockBackResist = 0f;
        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.dontTakeDamage = true;
        NPC.chaseable = false;
        NPC.aiStyle = -1;

        for (int i = 0; i < RotorBladeCount; i++)
        {
            RotorTrailPos[i] = new Vector2[RotorTrailLength];
            RotorTrailRot[i] = new float[RotorTrailLength];
            TailRotorTrailPos[i] = new Vector2[RotorTrailLength];
            TailRotorTrailRot[i] = new float[RotorTrailLength];
        }
    }

    public override bool? CanBeHitByItem(Player player, Item item) => false;
    public override bool? CanBeHitByProjectile(Projectile projectile) => false;
    public override bool CanBeHitByNPC(NPC attacker) => false;
    public override bool CanHitPlayer(Player target, ref int cooldownSlot) => false;

    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write((sbyte)TargetFacing);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        TargetFacing = reader.ReadSByte();
    }

    public override void AI()
    {
        NPC.dontTakeDamage = true;
        HoverTimer++;
        frontGunActive = false;

        UpdateTrailHistory();

        bool hasOwner = TryGetOwner(out ApacheElfShip boss);
        bool shouldRetreat = !hasOwner
            || CommandState == 1f
            || boss.AIState is ApacheElfShip.ActionState.Death or ApacheElfShip.ActionState.Retreat;

        if (shouldRetreat)
        {
            UpdateRetreat();
        }
        else
        {
            UpdateEscort(boss);
        }

        UpdateKinematicsAndAnimations();
    }

    private bool TryGetOwner(out ApacheElfShip boss)
    {
        int ownerIdx = (int)OwnerWhoAmI;
        if (ownerIdx >= 0
            && ownerIdx < Main.maxNPCs
            && Main.npc[ownerIdx].active
            && Main.npc[ownerIdx].type == ModContent.NPCType<ApacheElfShip>()
            && Main.npc[ownerIdx].ModNPC is ApacheElfShip owner)
        {
            boss = owner;
            return true;
        }

        boss = null;
        return false;
    }

    private void SetFacing(int newFacing)
    {
        if (newFacing is not (-1 or 1))
            return;

        if (TargetFacing != newFacing)
        {
            TargetFacing = newFacing;
            NPC.rotation = MathHelper.Clamp(-NPC.rotation, -MathHelper.PiOver2, MathHelper.PiOver2);
        }

        NPC.spriteDirection = TargetFacing;
    }

    private void UpdateRetreat()
    {
        RetreatTimer++;
        NPC.velocity.X = MathHelper.Lerp(NPC.velocity.X, -TargetFacing * 15f, 0.06f);
        NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, -10f, 0.06f);
        NPC.rotation = MathHelper.Lerp(NPC.rotation, MathHelper.Clamp(NPC.velocity.X * 0.025f, -0.25f, 0.25f), 0.1f);

        if (RetreatTimer >= 90f)
            NPC.active = false;
    }

    private void UpdateEscort(ApacheElfShip boss)
    {
        if (NPC.timeLeft < 600)
            NPC.timeLeft = 600;

        SetFacing(boss.TargetFacing);

        Vector2 formationOffset = (int)FormationSlot switch
        {
            0 => new Vector2(-130f * TargetFacing, -105f),
            1 => new Vector2(-115f * TargetFacing, 100f),
            _ => new Vector2(-205f * TargetFacing, -5f)
        };

        float phase = FormationSlot * 2.1f;
        Vector2 hoverBob = new(
            MathF.Cos(HoverTimer * 0.03f + phase) * 10f,
            MathF.Sin(HoverTimer * 0.045f + phase) * 14f
        );

        Vector2 toDesired = boss.NPC.Center + formationOffset + hoverBob - NPC.Center;
        float dist = toDesired.Length();

        Vector2 desiredVel = dist > 6f
            ? (toDesired / dist) * MathHelper.Clamp(dist * 0.09f, 1.5f, 22f) + boss.NPC.velocity * 0.65f
            : boss.NPC.velocity;

        NPC.velocity = Vector2.Lerp(NPC.velocity, desiredVel, 0.1f);

        if (boss.AIState == ApacheElfShip.ActionState.ComancheCommander)
        {
            float targetRot = MathHelper.Clamp(boss.NPC.rotation, -MathHelper.PiOver2, MathHelper.PiOver2);
            NPC.rotation = MathHelper.Lerp(NPC.rotation, targetRot, 0.25f);
        }
        else
        {
            NPC.rotation = MathHelper.Lerp(NPC.rotation, MathHelper.Clamp(NPC.velocity.X * 0.024f, -0.25f, 0.25f), 0.1f);
        }

        if (CommandState != 2f)
            return;

        frontGunActive = true;

        int fireOffset = (int)FormationSlot * 3;
        if (((int)HoverTimer + fireOffset) % 10 != 0)
            return;

        Vector2 lookDir = new Vector2(TargetFacing, 0f).RotatedBy(NPC.rotation);
        FrontGunRecoil = new Vector2(-11f * TargetFacing, 0f);
        BodyRecoilOffset = -lookDir * 2f;
        FrontGunMuzzleGlow = 1f;
        SoundEngine.PlaySound(SoundID.Item11 with { Volume = 0.6f }, FrontGunPivot);
        SpawnBullet(boss, lookDir);
    }

    private void SpawnBullet(ApacheElfShip boss, Vector2 direction)
    {
        if (Main.netMode == NetmodeID.MultiplayerClient)
            return;

        Vector2 bulletVel = direction.RotatedByRandom(MathHelper.ToRadians(2.5f)) * 18.5f;
        Projectile p = Projectile.NewProjectileDirect(
            NPC.GetSource_FromAI(),
            FrontGunMuzzlePos,
            bulletVel,
            ModContent.ProjectileType<DartshipBullet>(),
            boss.ProjectileDamage(boss.NPC.damage / 3),
            1f,
            Main.myPlayer
        );
        p.hostile = true;
        p.friendly = false;
        p.tileCollide = false;
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
        RotorSpeed = 0.2f * RotorSpinFactor;
        TailRotorSpeed = 0.4f * RotorSpinFactor;

        RotorRadiusX = 48f * RotorSizeMultiplier;
        RotorRadiusY = 11f * RotorSizeMultiplier;
        RotorWidthMultiplier = 1.5f * RotorSizeMultiplier;

        RotorTrailArc = MathHelper.Max(0.25f, 1.25f * RotorSpinFactor);
        TailRotorTrailArc = MathHelper.Max(0.5f, 2.6f * RotorSpinFactor);

        BodyPos = NPC.Center + BodyRecoilOffset;

        Vector2 frontAnchor = (new Vector2(64f * TargetFacing, 4f) * NPC.scale).RotatedBy(NPC.rotation);
        Vector2 rotorAnchor = (new Vector2(RotorAnchorOffset.X * TargetFacing, RotorAnchorOffset.Y) * NPC.scale).RotatedBy(NPC.rotation);
        Vector2 tailRotorAnchor = (new Vector2(TailRotorAnchorOffset.X * TargetFacing, TailRotorAnchorOffset.Y) * NPC.scale).RotatedBy(NPC.rotation);

        FrontGunPivot = BodyPos + frontAnchor + (FrontGunRecoil * NPC.scale).RotatedBy(NPC.rotation);
        RotorPos = BodyPos + rotorAnchor;
        TailRotorPos = BodyPos + tailRotorAnchor;

        FrontGunMuzzlePos = FrontGunPivot + new Vector2(22f * TargetFacing, -2f).RotatedBy(NPC.rotation) * NPC.scale;

        BodyRecoilOffset = Vector2.Lerp(BodyRecoilOffset, Vector2.Zero, 0.14f);
        FrontGunRecoil = Vector2.Lerp(FrontGunRecoil, Vector2.Zero, 0.22f);
        FrontGunMuzzleGlow = MathHelper.Max(0f, FrontGunMuzzleGlow - 0.12f);

        entryTrailOpacity = MathF.Max(
            Utils.GetLerpValue(70f, 40f, HoverTimer, true),
            Utils.GetLerpValue(0f, 14f, RetreatTimer, true)
        );

        UpdateGunAnimation(frontGunActive, ref frontGunFrameCounter, ref frontGunFrame, gunFrameSpeed, 3);

        RotorAngle = MathHelper.WrapAngle(RotorAngle + RotorSpeed * TargetFacing);
        TailRotorAngle = MathHelper.WrapAngle(TailRotorAngle + TailRotorSpeed * TargetFacing);

        for (int b = 0; b < RotorBladeCount; b++)
        {
            float bladeOffset = b * MathHelper.PiOver2;

            for (int i = 0; i < RotorTrailLength; i++)
            {
                float progress = i / (float)(RotorTrailLength - 1);

                float mainAngle = RotorAngle + bladeOffset - progress * RotorTrailArc * TargetFacing;
                Vector2 mainWorldVec = new Vector2(MathF.Cos(mainAngle) * RotorRadiusX, MathF.Sin(mainAngle) * RotorRadiusY).RotatedBy(NPC.rotation) * NPC.scale;
                RotorTrailPos[b][i] = RotorPos + mainWorldVec * RotorPosScale;
                RotorTrailRot[b][i] = mainWorldVec.ToRotation() + MathHelper.PiOver2;

                float tailAngle = TailRotorAngle + bladeOffset - progress * TailRotorTrailArc * TargetFacing;
                Vector2 tailWorldVec = new Vector2(MathF.Cos(tailAngle) * TailRotorRadiusX, MathF.Sin(tailAngle) * TailRotorRadiusY).RotatedBy(NPC.rotation) * NPC.scale;
                TailRotorTrailPos[b][i] = TailRotorPos + tailWorldVec * TailRotorPosScale;
                TailRotorTrailRot[b][i] = tailWorldVec.ToRotation() + MathHelper.PiOver2;
            }
        }
    }

    private Color EntryStripColors(float progressOnStrip)
    {
        Color result = Color.Lerp(Color.LightCyan, Color.SlateGray, progressOnStrip);
        result.A /= 2;
        return result * (1f - progressOnStrip) * entryTrailOpacity;
    }

    private float EntryStripWidth(float progressOnStrip) => 18f * NPC.scale;

    private Color MainRotorStripColors(float progressOnStrip)
    {
        Color baseCol = new(115, 130, 144, 255);
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
        float sampleAngle = RotorAngle + currentDrawingBlade * MathHelper.PiOver2 - progressOnStrip * RotorTrailArc * TargetFacing;
        Vector2 localVec = new(MathF.Cos(sampleAngle) * RotorRadiusX, MathF.Sin(sampleAngle) * RotorRadiusY);
        return localVec.Length() * RotorWidthMultiplier * NPC.scale;
    }

    private float TailRotorStripWidth(float progressOnStrip)
    {
        float sampleAngle = TailRotorAngle + currentDrawingBlade * MathHelper.PiOver2 - progressOnStrip * TailRotorTrailArc * TargetFacing;
        Vector2 localVec = new(MathF.Cos(sampleAngle) * TailRotorRadiusX, MathF.Sin(sampleAngle) * TailRotorRadiusY);
        return localVec.Length() * TailRotorWidthMultiplier * NPC.scale;
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        if (BodyPos == Vector2.Zero)
            UpdateKinematicsAndAnimations();

        if (entryTrailOpacity > 0.01f)
        {
            GameShaders.Misc["LightDisc"].Apply(null);
            EntryTrailStrip.PrepareStrip(dashOldPositions, dashOldRotations, EntryStripColors, EntryStripWidth, NPC.Size * 0.5f - screenPos, dashOldPositions.Length, true);
            EntryTrailStrip.DrawTrail();
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

        for (int b = 0; b < RotorBladeCount - 1; b += 2)
        {
            currentDrawingBlade = b;
            vertexStrip.PrepareStrip(TailRotorTrailPos[b], TailRotorTrailRot[b], TailRotorStripColors, TailRotorStripWidth, -screenPos, RotorTrailLength, true);
            vertexStrip.DrawTrail();
        }

        spriteBatch.End();
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

        Texture2D activeBodyTex = bodyTexture.Value;
        Texture2D activeFrontGunTex = ApacheElfShip.frontGunTexture.Value;

        Rectangle bodyRect = activeBodyTex.Frame(1, 1, 0, 0);
        Rectangle frontGunRect = activeFrontGunTex.Frame(1, 3, 0, frontGunFrame % 3);

        SpriteEffects staticEffects = TargetFacing == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        Vector2 bodyOrigin = bodyRect.Size() / 2f;
        Vector2 frontGunOrigin = new(TargetFacing == -1 ? frontGunRect.Width * 0.8f : frontGunRect.Width * 0.2f, frontGunRect.Height / 2f);
        float frontShootAngle = NPC.rotation + (TargetFacing == -1 ? MathHelper.Pi : 0f);

        spriteBatch.Draw(activeFrontGunTex, FrontGunPivot - screenPos, frontGunRect, drawColor, NPC.rotation, frontGunOrigin, NPC.scale, staticEffects, 0f);
        spriteBatch.Draw(activeBodyTex, BodyPos - screenPos, bodyRect, drawColor, NPC.rotation, bodyOrigin, NPC.scale, staticEffects, 0f);
        ApacheElfShip.DrawMuzzleFlashEffect(spriteBatch, FrontGunMuzzlePos, frontShootAngle, FrontGunMuzzleGlow, 1.35f * NPC.scale, NPC.velocity, screenPos);

        return false;
    }
}