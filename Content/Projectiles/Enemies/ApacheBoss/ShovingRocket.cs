using HendecamMod.Content.NPCs.Bosses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles.Enemies.ApacheBoss;

public class ShovingRocket : ModProjectile
{
    public MiscShaderData Shader = new MiscShaderData(Main.VertexPixelShaderRef, "MagicMissile")
        .UseProjectionMatrix(true)
        .UseImage0("Images/Extra_191")
        .UseImage1("Images/Extra_194")
        .UseImage2("Images/Extra_190")
        .UseSaturation(-2.8f)
        .UseOpacity(2f);

    public static VertexStrip TrailStrip = new();

    public int BossIndex => (int)Projectile.ai[0];
    public ref float ReticleSpawnTick => ref Projectile.ai[1];
    public ref float LockDuration => ref Projectile.ai[2];

    public ref float Timer => ref Projectile.localAI[0];
    public int ReticleIdentity
    {
        get => (int)Projectile.localAI[1];
        set => Projectile.localAI[1] = value;
    }

    public Vector2 LockedTargetPos = Vector2.Zero;
    public float DeployProgress;
    public bool Launched;

    public float descendDuration = 20f;
    public float pullbackDuration = 12f;
    public float LaunchTick => ReticleSpawnTick + LockDuration;

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
    }

    public override void SetDefaults()
    {
        Projectile.width = 20;
        Projectile.height = 20;
        Projectile.aiStyle = -1;
        Projectile.friendly = false;
        Projectile.hostile = true;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 360;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.Opacity = 1f;
    }

    public override void OnSpawn(IEntitySource source)
    {
        ReticleIdentity = -1;
    }

    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(Timer);
        writer.Write(ReticleIdentity);
        writer.WriteVector2(LockedTargetPos);
        writer.Write(Launched);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        Timer = reader.ReadSingle();
        ReticleIdentity = reader.ReadInt32();
        LockedTargetPos = reader.ReadVector2();
        Launched = reader.ReadBoolean();
    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        float point = 0f;
        Vector2 dir = Projectile.rotation.ToRotationVector2();
        float halfLen = 25f * Projectile.scale;
        return Collision.CheckAABBvLineCollision(
            targetHitbox.TopLeft(),
            targetHitbox.Size(),
            Projectile.Center - dir * halfLen,
            Projectile.Center + dir * halfLen,
            20f * Projectile.scale,
            ref point
        );
    }

    private Projectile FindReticle()
    {
        if (ReticleIdentity < 0) return null;
        int retType = ModContent.ProjectileType<DartshipReticle>();
        for (int i = 0; i < Main.maxProjectiles; i++)
        {
            Projectile p = Main.projectile[i];
            if (p.active && p.type == retType && p.identity == ReticleIdentity)
            {
                return p;
            }
        }
        return null;
    }

    private void ResetTrailToCurrent()
    {
        for (int i = 0; i < Projectile.oldPos.Length; i++)
        {
            Projectile.oldPos[i] = Projectile.position;
            Projectile.oldRot[i] = Projectile.rotation;
        }
    }

    public override void AI()
    {
        if (ReticleSpawnTick <= 0f)
        {
            ReticleSpawnTick = 40f;
        }
        if (LockDuration <= 0f)
        {
            LockDuration = 40f;
        }

        descendDuration = MathF.Min(20f, ReticleSpawnTick * 0.5f);
        Timer++;

        if (!Launched)
        {
            if (BossIndex < 0 || BossIndex >= Main.maxNPCs)
            {
                Projectile.Kill();
                return;
            }

            NPC bossNPC = Main.npc[BossIndex];
            if (!bossNPC.active || bossNPC.type != ModContent.NPCType<ApacheElfShip>() || bossNPC.ModNPC is not ApacheElfShip boss ||
                boss.AIState != ApacheElfShip.ActionState.ShoverRocket)
            {
                Projectile.Kill();
                return;
            }

            Player target = Main.player[bossNPC.target];
            DeployProgress = MathHelper.Clamp(Timer / descendDuration, 0f, 1f);
            float smoothDeploy = MathF.Sin(DeployProgress * MathHelper.PiOver2);

            float hangOffsetY = MathHelper.Lerp(6f, 36f, smoothDeploy);
            Vector2 localHangOffset = new(4f * boss.TargetFacing, hangOffsetY);
            Projectile.Center = boss.BodyPos + localHangOffset.RotatedBy(bossNPC.rotation);
            Projectile.velocity = Vector2.Zero;

            float forwardRot = bossNPC.rotation + ((boss.TargetFacing == -1) ? MathHelper.Pi : 0f);
            Projectile.rotation = forwardRot;
            ResetTrailToCurrent();

            if (Timer == ReticleSpawnTick && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile reticle = Projectile.NewProjectileDirect(
                    Projectile.GetSource_FromAI(),
                    target.Center,
                    Vector2.Zero,
                    ModContent.ProjectileType<DartshipReticle>(),
                    0,
                    0f,
                    Main.myPlayer,
                    ai0: LockDuration,
                    ai1: target.whoAmI
                );
                ReticleIdentity = reticle.identity;
                Projectile.netUpdate = true;
            }

            Projectile activeReticle = FindReticle();
            if (activeReticle != null)
            {
                LockedTargetPos = activeReticle.Center;
            }
            else if (target.active && !target.dead && Timer < LaunchTick - 5f)
            {
                LockedTargetPos = target.Center;
            }

            if (Timer >= LaunchTick)
            {
                Launched = true;
                if (LockedTargetPos == Vector2.Zero && target.active)
                {
                    LockedTargetPos = target.Center;
                }

                Vector2 aimDir = (LockedTargetPos - Projectile.Center).SafeNormalize(forwardRot.ToRotationVector2());
                Projectile.rotation = aimDir.ToRotation();
                Projectile.velocity = -aimDir * 4.5f;

                boss.BodyRecoilOffset += -aimDir * 6f;
                Projectile.netUpdate = true;
            }
        }
        else
        {
            float launchElapsed = Timer - LaunchTick;
            Vector2 toTarget = LockedTargetPos - Projectile.Center;
            float distToTarget = toTarget.Length();
            Vector2 aimDir = (distToTarget > 8f) ? toTarget.SafeNormalize(Projectile.rotation.ToRotationVector2()) : Projectile.rotation.ToRotationVector2();

            if (launchElapsed <= pullbackDuration)
            {
                float pullbackT = launchElapsed / pullbackDuration;
                Projectile.rotation = aimDir.ToRotation();
                Projectile.velocity = aimDir * MathHelper.Lerp(-4.8f, 0f, MathF.Sin(pullbackT * MathHelper.PiOver2));
                ResetTrailToCurrent();

                if (launchElapsed == pullbackDuration)
                {
                    SoundEngine.PlaySound(SoundID.Item61, Projectile.Center);
                }
            }
            else
            {
                float accelTicks = launchElapsed - pullbackDuration;
                float currentSpeed = MathHelper.Clamp(4f + accelTicks * accelTicks * 0.28f, 4f, 42f);

                Projectile.velocity = aimDir * currentSpeed;
                Projectile.rotation = Projectile.velocity.ToRotation();

                if (!Main.dedServ && Main.rand.NextBool(2))
                {
                    Vector2 tailPos = Projectile.Center - Projectile.rotation.ToRotationVector2() * 18f;
                    Dust d = Dust.NewDustPerfect(tailPos, DustID.SolarFlare, -Projectile.velocity * 0.2f, 100, default, 1.5f);
                    d.noGravity = true;
                }

                if (distToTarget <= currentSpeed + 12f)
                {
                    Projectile.Center = LockedTargetPos;
                    Projectile.Kill();
                }
            }
        }
    }

    public override void OnKill(int timeLeft)
    {
        if (!Launched) return;

        SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

        if (Main.netMode != NetmodeID.MultiplayerClient)
        {
            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                Projectile.Center,
                Vector2.Zero,
                ModContent.ProjectileType<ShovingExplosion>(),
                Projectile.damage,
                14f,
                Main.myPlayer, 24f, 250f
            );
        }
    }

    private Color StripColors(float progressOnStrip)
    {
        Color color = Color.Lerp(new Color(255, 130, 20), new Color(20, 5, 0, 255), MathHelper.Clamp(progressOnStrip * 1.4f, 0f, 1f));
        color.A = (byte)(color.A * 0.65f);
        return color * (1f - progressOnStrip) * Projectile.Opacity;
    }

    private float StripWidth(float progressOnStrip)
    {
        return 46f * (1f - progressOnStrip * 0.45f) * Projectile.scale;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        if (Launched && Timer - LaunchTick > pullbackDuration)
        {
            Main.spriteBatch.End();

            Shader.Apply(null);
            Vector2 trailOffset = Projectile.Size * 0.5f - Projectile.rotation.ToRotationVector2() * 14f - Main.screenPosition;
            TrailStrip.PrepareStrip(Projectile.oldPos, Projectile.oldRot, StripColors, StripWidth, trailOffset, Projectile.oldPos.Length, true);
            TrailStrip.DrawTrail();
            Main.pixelShader.CurrentTechnique.Passes[0].Apply();

            Main.spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                Main.DefaultSamplerState,
                DepthStencilState.None,
                Main.Rasterizer,
                null,
                Main.GameViewMatrix.TransformationMatrix
            );
        }

        Texture2D rocketTex = TextureAssets.Projectile[Type].Value;
        Vector2 drawPosition = Projectile.Center - Main.screenPosition;
        Vector2 origin = rocketTex.Size() / 2f;

        Main.EntitySpriteDraw(
            rocketTex,
            drawPosition,
            null,
            lightColor * Projectile.Opacity,
            Projectile.rotation + MathHelper.PiOver2,
            origin,
            Projectile.scale,
            SpriteEffects.None,
            0
        );

        return false;
    }
}