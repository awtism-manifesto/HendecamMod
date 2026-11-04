using HendecamMod.Common.Systems.Particles;
using HendecamMod.Common.Utilities;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.NPCs.Bosses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles.Enemies.ApacheBoss;

public class DowndraftTelegraph : ModProjectile
{
    public override string Texture => HendecamMod.BlankTexture;

    public ref float AimAngle => ref Projectile.ai[0];
    public ref float BossIndex => ref Projectile.ai[1];
    public ref float MaxTime => ref Projectile.ai[2];

    public override void SetDefaults()
    {
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        Projectile.aiStyle = -1;
        Projectile.scale = 0.5f;
        Projectile.alpha = 0;
    }

    public override bool? CanDamage() => false;

    public override void AI()
    {
        if (MaxTime <= 0f) MaxTime = 20f;

        int bossIdx = (int)BossIndex;
        if (bossIdx >= 0 && bossIdx < Main.maxNPCs && Main.npc[bossIdx].active && Main.npc[bossIdx].ModNPC is ApacheElfShip boss)
        {
            Projectile.Center = boss.RotorPos + AimAngle.ToRotationVector2() * (50f * Projectile.scale);
        }

        Projectile.rotation = AimAngle;

        float maxScale = 2f;
        if (Projectile.scale < maxScale)
        {
            Projectile.scale += 0.125f;
        }
        else
        {
            Projectile.scale = maxScale;
            Projectile.alpha += 10;
        }
        if (Projectile.alpha > 255)
        {
            Projectile.Kill();
        }
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D glow = TextureAssets.Extra[ExtrasID.MartianProbeScanWave].Value;
        Rectangle glowrect = new(0, 0, glow.Width, glow.Height);
        Vector2 origin = glowrect.Size() / 2f;
        Color glowcolor = new Color(255, 255, 255, 60) * Projectile.Opacity;

        Main.EntitySpriteDraw(
            glow,
            Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
            glowrect,
            Projectile.GetAlpha(glowcolor),
            Projectile.rotation,
            origin,
            Projectile.scale,
            SpriteEffects.None,
            0
        );

        return false;
    }
}

public class DowndraftColumn : ModProjectile
{
    public override string Texture => HendecamMod.BlankTexture;

    public float MaxWindLength = 2200f;
    public float WindWidth = 150f;

    public ref float BossIndex => ref Projectile.ai[0];
    public ref float TotalDuration => ref Projectile.ai[1];
    public ref float Timer => ref Projectile.localAI[0];

    public float CurrentWindLength;

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.DrawScreenCheckFluff[Type] = 2400;
    }

    public override void SetDefaults()
    {
        Projectile.hostile = true;
        Projectile.friendly = false;
        Projectile.width = 32;
        Projectile.height = 32;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.hide = true;
        Projectile.timeLeft = 300;
    }

    public override void OnSpawn(IEntitySource source)
    {
        if (TotalDuration <= 0f) TotalDuration = 95f;
        Projectile.timeLeft = (int)TotalDuration;
    }

    private Rectangle GetWindColumnBounds()
    {
        int width = (int)WindWidth;
        int height = Math.Max(16, (int)CurrentWindLength);
        return new Rectangle(
            (int)(Projectile.Center.X - width * 0.5f),
            (int)Projectile.Center.Y,
            width,
            height
        );
    }

    public override void AI()
    {
        int bossIdx = (int)BossIndex;
        if (bossIdx < 0 || bossIdx >= Main.maxNPCs)
        {
            Projectile.Kill();
            return;
        }

        NPC bossNPC = Main.npc[bossIdx];
        if (!bossNPC.active || bossNPC.type != ModContent.NPCType<ApacheElfShip>() || bossNPC.ModNPC is not ApacheElfShip boss || boss.AIState != ApacheElfShip.ActionState.Downdraft)
        {
            Projectile.Kill();
            return;
        }

        Timer++;

        Projectile.Center = boss.NPC.Bottom;
        Projectile.velocity = Vector2.UnitY;
        Projectile.rotation = MathHelper.PiOver2;

        RaycastData data = Helpers.QuickRaycast(
            Projectile.Center,
            Projectile.velocity,
            point => Main.tile[point].HasTile && Main.tileSolidTop[Main.tile[point].TileType],
            MaxWindLength / 16f
        );
        CurrentWindLength = data.Length;

        if (Timer % 18f == 0f)
        {
            SoundEngine.PlaySound(SoundID.Item34, Projectile.Center);
        }

        Rectangle windBox = GetWindColumnBounds();

        if (!Main.dedServ)
        {
            int topLineCount = 5;
            for (int k = 0; k < topLineCount; k++)
            {
                float t = (k + Main.rand.NextFloat(0f, 1f)) / topLineCount - 0.5f;
                Vector2 linePos = Projectile.Center + new Vector2(t * WindWidth, Main.rand.NextFloat(-4f, 6f));
                Vector2 lineVel = new(Main.rand.NextFloat(-0.4f, 0.4f), Main.rand.NextFloat(-1f, 1f));

                Color lineColor = Color.Lerp(Color.LightCyan, Color.SlateGray, Main.rand.NextFloat(0.2f, 0.7f));
                lineColor.A = (byte)Main.rand.Next(85, 145);

                ParticleSystem.SpawnParticle(
                    ParticleID.Gas,
                    linePos,
                    lineVel,
                    lineColor,
                    Main.rand.NextFloat(0.55f, 0.9f),
                    Main.rand.NextFloat(0.85f, 1.35f),
                    data0: Main.rand.NextFloat(0.03f, 0.06f)
                );
            }

            for (int i = 0; i < 1; i++)
            {
                Vector2 spawnPos = Main.rand.NextVector2FromRectangle(windBox);
                float streakSpeed = Main.rand.NextFloat(12f, 18f);
                float swayX = Main.rand.NextFloat(-0.5f, 0.5f);
                Vector2 streakVel = new(swayX, streakSpeed);

                Color windColor = Color.Lerp(Color.LightCyan, Color.SlateGray, Main.rand.NextFloat(0.2f, 0.8f));
                windColor.A = (byte)Main.rand.Next(80, 150);

                ParticleSystem.SpawnParticle(
                    ParticleID.Streak,
                    spawnPos,
                    streakVel,
                    windColor,
                    Main.rand.NextFloat(0.75f, 1.15f),
                    Main.rand.NextFloat(0.75f, 1.25f)
                );
            }

            for (int x = 0; x < 1; x++)
            {
                Vector2 spawnPos = Main.rand.NextVector2FromRectangle(windBox);
                float gasSpeed = Main.rand.NextFloat(5f, 12f);
                Vector2 gasVel = new(0f, gasSpeed);

                Color windColor = Color.Lerp(Color.LightCyan, Color.SlateGray, Main.rand.NextFloat(0.25f, 0.75f));
                windColor.A = (byte)Main.rand.Next(70, 135);

                ParticleSystem.SpawnParticle(
                    ParticleID.Gas,
                    spawnPos,
                    gasVel,
                    windColor,
                    Main.rand.NextFloat(0.45f, 0.85f),
                    Main.rand.NextFloat(0.8f, 1.25f),
                    data0: Main.rand.NextFloat(0.025f, 0.055f)
                );
            }
        }

        for (int i = 0; i < Main.maxPlayers; i++)
        {
            Player p = Main.player[i];
            if (!p.active || p.dead) continue;

            if (p.Hitbox.Intersects(windBox))
            {
                p.velocity.Y = MathHelper.Clamp(p.velocity.Y + 2.5f, -12f, 36f);
                p.fallStart = (int)(p.position.Y / 16f);
                p.AddBuff(BuffID.WindPushed, 2);
                p.AddBuff(ModContent.BuffType<VertigoBuff>(), 600);
            }
        }
    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        return targetHitbox.Intersects(GetWindColumnBounds());
    }
}