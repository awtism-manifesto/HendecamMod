using HendecamMod.Common.Systems.Particles;
using HendecamMod.Common.Utilities;
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

public class DartshipReticle : ModProjectile
{
    public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.FlamingMace}";

    public ref float LockTime => ref Projectile.ai[0];
    public ref float TargetPlayer => ref Projectile.ai[1];
    public ref float Timer => ref Projectile.localAI[0];

    public float LockGlow;
    public float RingShrink;

    public override void SetDefaults()
    {
        Projectile.width = 32;
        Projectile.height = 32;
        Projectile.aiStyle = -1;
        Projectile.friendly = false;
        Projectile.hostile = false;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 180;
        Projectile.Opacity = 1f;
    }

    public override bool? CanDamage() => false;

    public override void OnSpawn(IEntitySource source)
    {
        if (LockTime <= 0f)
        {
            LockTime = 60f;
        }
        Projectile.timeLeft = (int)LockTime + 55;
    }

    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(Timer);
        writer.WriteVector2(Projectile.Center);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        Timer = reader.ReadSingle();
        Projectile.Center = reader.ReadVector2();
    }

    public override void AI()
    {
        int targetIdx = (int)MathHelper.Clamp(TargetPlayer, 0, Main.maxPlayers - 1);
        Player target = Main.player[targetIdx];

        if (!target.active || target.dead)
        {
            Projectile.Kill();
            return;
        }

        Timer++;
        LockGlow = MathHelper.Max(0f, LockGlow - 0.035f);
        RingShrink = MathHelper.Max(0f, RingShrink - 0.03f);

        if (Timer < LockTime)
        {
            if (Timer % 25f == 0f)
            {
                RingShrink = 1f;
            }

            Projectile.Center = Vector2.Lerp(Projectile.Center, target.Center, 0.3f);
        }
        else if (Timer == LockTime)
        {
            LockGlow = 1f;
            Projectile.velocity = Vector2.Zero;
            SoundEngine.PlaySound(SoundID.MaxMana, Projectile.Center);

            if (!Main.dedServ)
            {
                for (int i = 0; i < 15; i++)
                {
                    Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.RedTorch, Main.rand.NextVector2Circular(4f, 4f));
                    d.noGravity = true;
                    d.scale = 1.5f;
                }
            }

            Projectile.netUpdate = true;
        }
        else if (Timer > LockTime + 25f)
        {
            Projectile.Opacity = MathHelper.Max(0f, Projectile.Opacity - 0.06f);
        }
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D starTex = TextureAssets.Extra[ExtrasID.SharpTears].Value;
        Vector2 drawPos = Projectile.Center - Main.screenPosition;
        Vector2 origin = starTex.Size() / 2f;

        float pulse = 1f + MathF.Sin(Main.GlobalTimeWrappedHourly * 10f) * 0.08f;
        Color baseColor = new Color(255, 70, 40, 0) * Projectile.Opacity;

        for (int i = 0; i < 4; i++)
        {
            float angle = Projectile.rotation + i * MathHelper.PiOver2;
            Vector2 bracketOffset = angle.ToRotationVector2() * (22f * pulse);
            Main.EntitySpriteDraw(
                starTex,
                drawPos + bracketOffset,
                null,
                baseColor,
                angle + MathHelper.PiOver2,
                origin,
                new Vector2(0.45f, 0.85f) * Projectile.scale,
                SpriteEffects.None,
                0
            );
        }

        Main.EntitySpriteDraw(starTex, drawPos, null, baseColor, Projectile.rotation, origin, new Vector2(0.35f, 0.9f) * Projectile.scale * pulse, SpriteEffects.None, 0);
        Main.EntitySpriteDraw(starTex, drawPos, null, baseColor, Projectile.rotation + MathHelper.PiOver2, origin, new Vector2(0.35f, 0.9f) * Projectile.scale * pulse, SpriteEffects.None, 0);

        if (LockGlow > 0f)
        {
            float scale = 2.2f * Projectile.scale * MathF.Cos(MathHelper.PiOver2 * (1f - LockGlow));
            float opacity = Projectile.Opacity * MathF.Sqrt(LockGlow);
            Color glowColor = new Color(255, 184, 34, 0) * opacity;

            Main.EntitySpriteDraw(starTex, drawPos, null, glowColor, Projectile.rotation, origin, new Vector2(0.6f, 1.4f) * scale, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(starTex, drawPos, null, glowColor, Projectile.rotation + MathHelper.PiOver2, origin, new Vector2(0.6f, 1.4f) * scale, SpriteEffects.None, 0);
        }

        if (RingShrink > 0f)
        {
            float ringRadius = 56f * RingShrink * Projectile.scale;
            float opacity = Projectile.Opacity * MathF.Sqrt(RingShrink);
            Color ringColor = new Color(255, 184, 34, 0) * opacity;

            for (int i = 0; i < 8; i++)
            {
                float ringAngle = i * MathHelper.PiOver4 + Projectile.rotation;
                Vector2 segPos = drawPos + ringAngle.ToRotationVector2() * ringRadius;
                Main.EntitySpriteDraw(
                    starTex,
                    segPos,
                    null,
                    ringColor,
                    ringAngle + MathHelper.PiOver2,
                    origin,
                    new Vector2(0.3f, 0.65f) * Projectile.scale,
                    SpriteEffects.None,
                    0
                );
            }
        }

        return false;
    }
}
