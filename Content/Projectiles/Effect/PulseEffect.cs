
using HendecamMod.Common.Systems.Assets;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Linq;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;

namespace HendecamMod.Content.Projectiles.Effect;

public class PulseEffect : ModProjectile
{
    public override string Texture => HendecamTextures.Empty100TexPath;

    int AITimer = 0;
    ref float Speed => ref Projectile.ai[0];
    ref float Scale => ref Projectile.ai[1];
    ref float ColorMult => ref Projectile.ai[2];

    public Color PulseColor { get; set; } = Color.White;
    public Entity EntityToFollow { get; set; } = null;
    int entityType = -1;
    int entityID = -1;

    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(PulseColor.R);
        writer.Write(PulseColor.G);
        writer.Write(PulseColor.B);
        writer.Write(PulseColor.A);
        writer.Write(entityType);
        writer.Write(entityID);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        PulseColor = new Color(reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        entityType = reader.ReadInt32();
        entityID = reader.ReadInt32();
        if (entityID < 0) return;
        switch (entityType)
        {
            case 1:
                EntityToFollow = Main.player[entityID];
                break;
            case 2:
                EntityToFollow = Main.projectile.FirstOrDefault(p => p.identity == entityID, null);
                break;
            case 3:
                EntityToFollow = Main.npc[entityID];
                break;
        }
    }

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        Main.projFrames[Type] = 1;
    }

    public override void SetDefaults()
    {
        Projectile.width = 120;
        Projectile.height = 120;
        Projectile.hostile = false;
        Projectile.friendly = false;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 600;
    }

    public override void AI()
    {
        if (AITimer == 0)
        {
            if (EntityToFollow != null)
            {
                if (EntityToFollow is Player)
                {
                    entityType = 1;
                    entityID = EntityToFollow.whoAmI;
                }
                else if (EntityToFollow is Projectile proj)
                {
                    entityType = 2;
                    entityID = proj.identity;
                }
                else if (EntityToFollow is NPC)
                {
                    entityType = 3;
                    entityID = EntityToFollow.whoAmI;
                }
            }
            Projectile.netUpdate = true;
        }
        if (EntityToFollow != null)
        {
            Projectile.Center = EntityToFollow.Center;
        }
        if (Scale == 0) Scale = 1;
        if (Speed == 0) Speed = 1;
        Projectile.velocity = Vector2.Zero;
        if (AITimer > 60 / Speed) Projectile.Kill();
        AITimer++;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = TextureAssets.Projectile[Type].Value;
        Vector2 drawPos = Projectile.Center - Main.screenPosition;
        var shader = GameShaders.Misc["HendecamMod:ShieldPulseShader"];
        Main.instance.GraphicsDevice.Textures[1] = HendecamTextures.NoiseTexture.Value;
        shader.Shader.Parameters["time"].SetValue(AITimer / 60f);
        shader.Shader.Parameters["alwaysVisible"].SetValue(false);
        shader.Shader.Parameters["speed"].SetValue(Speed);
        shader.Shader.Parameters["colorMultiplier"].SetValue(ColorMult);
        shader.Shader.Parameters["color"].SetValue(PulseColor.ToVector4());
        Main.spriteBatch.End();
        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, default, Main.Rasterizer, shader.Shader, Main.GameViewMatrix.TransformationMatrix);
        shader.Apply();
        Main.EntitySpriteDraw(texture, drawPos, null, Color.White, Projectile.rotation, texture.Size() * 0.5f, Scale, SpriteEffects.None, 0);
        Main.spriteBatch.End();
        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, default, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
        return false;
    }

    public override void PostDraw(Color lightColor)
    {
        Main.spriteBatch.End();
        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, default, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
    }
}
