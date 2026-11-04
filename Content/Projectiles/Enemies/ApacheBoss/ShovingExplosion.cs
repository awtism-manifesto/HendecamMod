using HendecamMod.Common.Systems.Particles;
using HendecamMod.Content.Projectiles;
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

public class ShovingExplosion : PerlinExplosion
{
    public override int Lifetime => 42;

    public override Vector2 ScaleRatio => new(1.5f, 1f);

    public float spawnGlow = 1f;
    public float glowRot;
    public Vector2 glowScale = new(1.8f, 1.8f);

    public bool AppliedShockwavePush
    {
        get => Projectile.localAI[0] == 1f;
        set => Projectile.localAI[0] = value ? 1f : 0f;
    }

    public override void SetDefaults()
    {
        Projectile.width = 48;
        Projectile.height = 48;
        Projectile.friendly = false;
        Projectile.hostile = true;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.penetrate = -1;
        Projectile.timeLeft = Lifetime;
        Projectile.Opacity = 1f;
    }

    public override void OnSpawn(IEntitySource source)
    {
        spawnGlow = 1f;
    }

    public override bool? CanDamage()
    {
        return spawnGlow <= 0f && Projectile.timeLeft > (int)(Lifetime * 0.35f);
    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        if (spawnGlow > 0f)
        {
            return false;
        }

        float renderedWidth = MathF.Min(Projectile.width, 512f) * ScaleRatio.X;
        float renderedHeight = MathF.Min(Projectile.height, 512f) * ScaleRatio.Y;

        float radiusX = renderedWidth * 0.42f;
        float radiusY = renderedHeight * 0.42f;
        if (radiusX <= 1f || radiusY <= 1f)
        {
            return false;
        }

        float closestX = MathHelper.Clamp(Projectile.Center.X, targetHitbox.Left, targetHitbox.Right);
        float closestY = MathHelper.Clamp(Projectile.Center.Y, targetHitbox.Top, targetHitbox.Bottom);

        float dx = (closestX - Projectile.Center.X) / radiusX;
        float dy = (closestY - Projectile.Center.Y) / radiusY;

        return (dx * dx + dy * dy) <= 1f;
    }

    public override Color GetCurrentExplosionColor(float pulseCompletionRatio)
    {
        return Color.Lerp(new Color(255, 165, 45), new Color(185, 35, 15), pulseCompletionRatio);
    }

    private void ForceShovePlayer(Player p, float maxRadius, float minForce, float maxForce)
    {
        Vector2 offset = p.Center - Projectile.Center;
        float dist = offset.Length();

        float falloff = MathHelper.Clamp(1f - (dist / maxRadius), 0f, 1f);
        float launchSpeed = MathHelper.Lerp(minForce, maxForce, MathF.Pow(falloff, 0.65f));

        Vector2 pushDir = offset.SafeNormalize(new Vector2(p.direction, -0.4f));
        if (MathF.Abs(pushDir.Y) < 0.25f)
        {
            pushDir.Y = -0.35f;
            pushDir.Normalize();
        }

        p.RemoveAllGrapplingHooks();
        if (p.mount.Active)
        {
            p.mount.Dismount(p);
        }

        p.dashDelay = 30;
        p.velocity = pushDir * launchSpeed;
        p.fallStart = (int)(p.position.Y / 16f);

        if (Main.netMode == NetmodeID.Server)
        {
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, p.whoAmI);
        }
    }

    public override void AI()
    {
        if (MaxRadius <= 0f)
        {
            MaxRadius = 250f;
        }

        if (spawnGlow > 0f)
        {
            Projectile.timeLeft = Lifetime;
            spawnGlow = MathHelper.Max(0f, spawnGlow - 0.05f);
            glowRot += 0.14f;
            return;
        }

        base.AI();

        if (!AppliedShockwavePush)
        {
            AppliedShockwavePush = true;
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            if (!Main.dedServ)
            {
                for (int i = 0; i < 20; i++)
                {
                    Color randColor = Main.rand.NextFromList(Color.Orange, Color.OrangeRed, Color.SlateGray, Color.Gray);
                    ParticleSystem.SpawnParticle(
                        ParticleID.Streak,
                        Projectile.Center,
                        Vector2.UnitY.RotatedByRandom(MathHelper.Pi) * Main.rand.NextFloat(3f, 8f),
                        randColor,
                        1f,
                        Main.rand.NextFloat(1.1f, 1.7f)
                    );
                }

                for (int i = 0; i < 28; i++)
                {
                    Color randColor = Main.rand.NextFromList(Color.Orange, Color.OrangeRed, Color.SlateGray, Color.Gray);
                    ParticleSystem.SpawnParticle(
                        ParticleID.Gas,
                        Projectile.Center,
                        Vector2.UnitY.RotatedByRandom(MathHelper.Pi) * Main.rand.NextFloat(1.5f, 5.5f),
                        randColor,
                        0.95f,
                        Main.rand.NextFloat(0.9f, 1.5f),
                        data0: 0.04f
                    );
                }
            }
        }
    }

    public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
    {
        modifiers.HitDirectionOverride = (Projectile.Center.X < target.Center.X) ? 1 : -1;
        modifiers.Knockback *= 0f;
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        target.AddBuff(BuffID.BrokenArmor, 360);
        ForceShovePlayer(target, 650f, 22f, 36f);
    }

    public override bool PreDraw(ref Color lightColor)
    {
        if (spawnGlow > 0f)
        {
            Texture2D effectTexture = TextureAssets.Extra[ExtrasID.SharpTears].Value;
            Vector2 drawPosition = Projectile.Center - Main.screenPosition;
            float scale = 2f * (float)Math.Cos(Math.PI / 2 * spawnGlow);
            float opacity = Projectile.Opacity * (float)Math.Sqrt(spawnGlow);
            Vector2 tearScale = new(0.2f, 1f);

            Main.EntitySpriteDraw(effectTexture, drawPosition + new Vector2(0, 0).RotatedBy(Projectile.rotation), null, new Color(255, 255, 255, 0) * opacity,
                glowRot, effectTexture.Size() / 2f, glowScale * scale * tearScale, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(effectTexture, drawPosition, null, new Color(255, 184, 34, 0) * opacity, glowRot + MathHelper.PiOver2,
                effectTexture.Size() / 2f, glowScale * scale * tearScale, SpriteEffects.None, 0);

            return false;
        }

        return base.PreDraw(ref lightColor);
    }
}