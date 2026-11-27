using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles.Enemies.ApacheBoss
{
    //CREDITNOTE: Modified Fargo generic deathray code

    public class DartshipTelegraph : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.DrawScreenCheckFluff[Projectile.type] = 5000;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.alpha = 255;

            Projectile.hide = true;
            Projectile.scale = 1f;
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindProjectiles.Add(index);
        }

        public Color color = Color.Orange;

        public override bool? CanDamage()
        {
            return false;
        }

        private float aiTimer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        private float rayPosY//be accurate
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }
        private float maxTime//be accurate
        {
            get => Projectile.ai[2] == 0f ? 60f : Projectile.ai[2];
            set => Projectile.ai[2] = value;
        }
        private int drawLayers = 1;
        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
        }
        public override void AI()
        {
            float alphaModifier = 3;

            color = new Color(255, 255, 255, 0) * 0.75f;
            alphaModifier = 1;
            if (aiTimer < maxTime / 2)
                aiTimer = maxTime / 2;

            Projectile.position -= Projectile.velocity;
            Projectile.rotation = Projectile.velocity.ToRotation();

            if (alphaModifier >= 0)
            {
                Projectile.alpha = 255 - (int)(255 * Math.Sin(Math.PI / maxTime * aiTimer) * alphaModifier);
                if (Projectile.alpha < 0)
                    Projectile.alpha = 0;
            }
            if (++aiTimer > maxTime)
            {
                Projectile.Kill();
                return;
            }
            color.A = 0;
        }
        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projHitbox.Intersects(targetHitbox))
            {
                return true;
            }
            float num6 = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.rotation.ToRotationVector2() * 3000f, 16f * Projectile.scale, ref num6))
            {
                return true;
            }
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(228, 83, 38, 80) * Projectile.Opacity * (Main.mouseTextColor / 255f) * 0.9f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            Texture2D textureTip = TextureAssets.Extra[ExtrasID.SharpTears].Value;
            Rectangle frame2 = textureTip.Frame(1, Main.projFrames[Type], 0, Projectile.frame);
            int num156 = texture2D13.Height / Main.projFrames[Projectile.type];
            int y3 = num156 * Projectile.frame;
            Rectangle rectangle = new(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;

            int length = rayPosY == 0 ? 3000 : (int)rayPosY;
            Vector2 offset = Projectile.rotation.ToRotationVector2() * length / 2f;
            Vector2 position = Projectile.Center - Main.screenLastPosition + new Vector2(0f, Projectile.gfxOffY) + offset;
            const float resolutionCompensation = 128f / 24f;
            Rectangle destination = new((int)position.X, (int)position.Y, length, (int)(rectangle.Height * 0.25 / resolutionCompensation));

            Color drawColor = Projectile.GetAlpha(lightColor);
            Vector2 glowScale = new Vector2(1.25f, 0.15f);
            for (int j = 0; j < drawLayers; j++)
            {
                Main.EntitySpriteDraw(new DrawData(texture2D13, destination, new Rectangle?(rectangle), drawColor, Projectile.rotation, origin2, SpriteEffects.None, 0));
            }
            Main.EntitySpriteDraw(textureTip, Projectile.Center - Main.screenPosition, frame2,
            drawColor * 2, Projectile.rotation + MathHelper.PiOver4,
            new Vector2(textureTip.Width
            * 0.5f, textureTip.Height / Main.projFrames[Type] * 0.5f),
            glowScale * 1f, SpriteEffects.None, 0f);
            Main.EntitySpriteDraw(textureTip, Projectile.Center - Main.screenPosition, frame2,
            drawColor * 2, Projectile.rotation + MathHelper.PiOver4 + MathHelper.PiOver2,
            new Vector2(textureTip.Width
            * 0.5f, textureTip.Height / Main.projFrames[Type] * 0.5f),
            glowScale * 1f, SpriteEffects.None, 0f);
            return false;
        }
    }
}