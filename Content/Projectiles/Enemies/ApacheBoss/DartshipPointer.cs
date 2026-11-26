using HendecamMod.Content.NPCs.Bosses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles.Enemies.ApacheBoss;

public class DartshipPointer : ModProjectile
{
    public ref float BossWhoAmI => ref Projectile.ai[0];
    public ref float TargetPlayer => ref Projectile.ai[1];
    public ref float Duration => ref Projectile.ai[2];
    public ref float Timer => ref Projectile.localAI[0];

    private Vector2 aimOffset;
    private int retargetTimer;

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Type] = 8;
        ProjectileID.Sets.TrailingMode[Type] = 2;
    }

    public override void SetDefaults()
    {
        Projectile.width = 24;
        Projectile.height = 24;
        Projectile.hostile = false;
        Projectile.friendly = false;
        Projectile.damage = 0;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.timeLeft = 600;
    }

    public override bool? CanDamage() => false;

    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.WriteVector2(aimOffset);
        writer.Write(retargetTimer);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        aimOffset = reader.ReadVector2();
        retargetTimer = reader.ReadInt32();
    }

    public override void AI()
    {
        Timer++;

        int bossIdx = (int)BossWhoAmI;
        bool validBoss = bossIdx >= 0
            && bossIdx < Main.maxNPCs
            && Main.npc[bossIdx].active
            && Main.npc[bossIdx].type == ModContent.NPCType<ApacheElfShip>()
            && Main.npc[bossIdx].ModNPC is ApacheElfShip boss
            && boss.AIState == ApacheElfShip.ActionState.ComancheCommander;

        if (!validBoss || (Duration > 0f && Timer >= Duration))
        {
            Projectile.Kill();
            return;
        }

        int playerIdx = (int)TargetPlayer;
        if (playerIdx < 0 || playerIdx >= Main.maxPlayers || !Main.player[playerIdx].active || Main.player[playerIdx].dead)
        {
            Projectile.Kill();
            return;
        }

        Player target = Main.player[playerIdx];

        if (--retargetTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
        {
            retargetTimer = Main.rand.Next(22, 38);
            float radius = Main.rand.NextFloat(70f, 165f);
            aimOffset = Main.rand.NextVector2CircularEdge(radius, radius * 0.85f);
            Projectile.netUpdate = true;
        }

        Vector2 continuousWeave = new Vector2(
                    MathF.Cos(Timer * 0.065f) * 34f,
                    MathF.Sin(Timer * 0.09f) * 26f
                );

        Vector2 sloppyGoal = target.Center + aimOffset + continuousWeave;
        Vector2 toGoal = sloppyGoal - Projectile.Center;
        float dist = toGoal.Length();

        float distT = Utils.GetLerpValue(100f, 350f, dist, true);
        float speedCurve = distT * distT * 0.45f + distT * 0.55f;
        float desiredSpeed = MathHelper.Lerp(1.8f, 26f, speedCurve);
        float accelLerp = MathHelper.Lerp(0.055f, 0.14f, distT);

        Vector2 desiredVel = dist > 4f
            ? (toGoal / dist) * desiredSpeed
            : Vector2.UnitX.RotatedBy(Timer * 0.1f) * 1.8f;

        Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVel, accelLerp);

        float targetRot = MathHelper.Clamp(Projectile.velocity.X * 0.014f, -0.25f, 0.25f);
        Projectile.rotation = MathHelper.Lerp(Projectile.rotation, targetRot, 0.18f);

        Lighting.AddLight(Projectile.Center, 0.7f, 0.38f, 0.08f);
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D tex = TextureAssets.Projectile[Type].Value;
        Vector2 origin = new Vector2(tex.Width * 0.34f, tex.Height * 0.16f);

        float fadeIn = Utils.GetLerpValue(0f, 10f, Timer, true);
        float fadeOut = Duration > 0f ? Utils.GetLerpValue(Duration, Duration - 15f, Timer, true) : 1f;
        float opacity = fadeIn * fadeOut;

        for (int i = Projectile.oldPos.Length - 1; i >= 0; i--)
        {
            if (Projectile.oldPos[i] == Vector2.Zero)
                continue;

            float progress = 1f - (i / (float)Projectile.oldPos.Length);
            Vector2 drawPos = Projectile.oldPos[i] + Projectile.Size * 0.5f - Main.screenPosition;
            Color trailColor = Color.Lerp(new Color(255, 130, 30), Color.White, progress * 0.4f) * (progress * 0.42f * opacity);
            float trailScale = Projectile.scale * MathHelper.Lerp(0.75f, 0.96f, progress);

            Main.EntitySpriteDraw(
                tex,
                drawPos,
                null,
                trailColor,
                Projectile.oldRot[i],
                origin,
                trailScale,
                SpriteEffects.None,
                0
            );
        }

        Main.EntitySpriteDraw(
            tex,
            Projectile.Center - Main.screenPosition,
            null,
            Color.White * opacity,
            Projectile.rotation,
            origin,
            Projectile.scale,
            SpriteEffects.None,
            0
        );

        return false;
    }
}