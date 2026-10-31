using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles.Enemies.ApacheBoss;

public class ChinookSupplyCrate : ModProjectile
{
    public override string Texture => $"Terraria/Images/Item_{ItemID.WoodenCrate}";

    public override void SetDefaults()
    {
        Projectile.width = 28;
        Projectile.height = 28;
        Projectile.hostile = true;
        Projectile.friendly = false;
        Projectile.tileCollide = true;
        Projectile.ignoreWater = false;
        Projectile.penetrate = 1;
        Projectile.timeLeft = 360;
    }

    public override void AI()
    {
        Projectile.ai[0]++;
        float sway = MathF.Sin(Projectile.ai[0] * 0.08f + Projectile.whoAmI) * 0.12f;
        Projectile.velocity.X = MathHelper.Lerp(Projectile.velocity.X, sway * 12f, 0.04f);
        Projectile.velocity.Y = MathHelper.Clamp(Projectile.velocity.Y + 0.32f, -10f, 14f);
        Projectile.rotation = MathHelper.Lerp(Projectile.rotation, Projectile.velocity.X * 0.06f, 0.15f);

        if (Main.rand.NextBool(3))
        {
            Dust smoke = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, -1f, 120, default, 1.1f);
            smoke.noGravity = true;
            smoke.velocity *= 0.3f;
        }
    }

    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
        SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);

        for (int i = 0; i < 22; i++)
        {
            Dust smoke = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-6f, 1f), 100, default, 1.7f);
            smoke.noGravity = true;

            Dust fire = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-7f, 1f), 100, default, 2f);
            fire.noGravity = true;
        }

        for (int g = 0; g < 3; g++)
        {
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-4f, -1f)), Main.rand.Next(61, 64), 0.9f);
        }

        if (Main.netMode != NetmodeID.MultiplayerClient)
        {
            int shrapnelCount = 5;
            for (int i = 0; i < shrapnelCount; i++)
            {
                float t = (i / (float)(shrapnelCount - 1)) - 0.5f;
                float angle = -MathHelper.PiOver2 + (t * MathHelper.ToRadians(110f)) + Main.rand.NextFloat(-0.06f, 0.06f);
                Vector2 shrapnelVel = angle.ToRotationVector2() * Main.rand.NextFloat(9.5f, 12.5f);

                Projectile p = Projectile.NewProjectileDirect(
                Projectile.GetSource_FromThis(),
                    Projectile.Center - new Vector2(0f, 6f),
                    shrapnelVel,
                    ProjectileID.BulletHighVelocity,
                    Projectile.damage,
                    1.5f,
                    Main.myPlayer
                );
                p.hostile = true;
                p.friendly = false;
                p.tileCollide = false;
            }
        }
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D tex = TextureAssets.Projectile[Type].Value;
        Main.EntitySpriteDraw(
            tex,
            Projectile.Center - Main.screenPosition,
            null,
            lightColor,
            Projectile.rotation,
            tex.Size() / 2f,
            Projectile.scale,
            SpriteEffects.None,
            0
        );
        return false;
    }
}