using HendecamMod.Common.Systems.Particles;
using Microsoft.Xna.Framework.Graphics;

namespace HendecamMod.Content.Particles;

public class StreakParticle : ParticleType
{
    public override void OnSpawn(ref Particle particle)
    {
        if (particle.color == new Color(0, 0, 0, 0))
        {
            particle.color = Color.White;
        }
        particle.frame = new Rectangle(0, Main.rand.Next(0, 4) * 50, 50, 50);
        particle.scale *= 0.5f;
        particle.velocity *= 1;
    }

    public override void Update(ref Particle particle)
    { // Calls every frame the dust is active
        particle.position += particle.velocity;
        particle.rotation = particle.velocity.ToRotation() - MathHelper.PiOver2;
        particle.scale *= 0.97f;
        particle.velocity *= 0.94f;
        particle.color = Color.Lerp(particle.color, Color.White, 1 / 30f);
        float light = 0.35f * particle.scale;

        Lighting.AddLight(particle.position, light, light, light);

        if (particle.scale < 0.2f)
        {
            Kill(ref particle);
        }
    }

    public override void Draw(Particle particle)
    {
        Vector2 scale = new Vector2(particle.scale, particle.scale * 2);
        Main.spriteBatch.Draw(Texture.Value, particle.position - Main.screenPosition, particle.frame, particle.color, particle.rotation, new Vector2(25, 25), scale, SpriteEffects.None, 0f);
    }
}
