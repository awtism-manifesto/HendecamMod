using HendecamMod.Common.Systems.Assets;
using HendecamMod.Common.Systems.Particles;
using Microsoft.Xna.Framework.Graphics;

namespace HendecamMod.Content.Particles;

public class GlowyParticle : ParticleType
{
    public override string TexturePath => HendecamTextures.GlowBallTexturePath;

    public override void OnSpawn(ref Particle particle)
    {
        particle.opacity = 0f;
        particle.scale *= 2;
    }

    public override void Update(ref Particle particle)
    {
        float duration = particle.data[0];
        float fadeInDuration = particle.data[1];
        float fadeOutDuration = particle.data[2];
        float slowDownMul = particle.data[3];
        if (particle.timer < fadeInDuration)
            particle.opacity += 1f / fadeInDuration;
        else if (particle.timer > duration - fadeOutDuration)
            particle.opacity -= 1f / fadeOutDuration;
       
        particle.velocity *= slowDownMul;
        if (particle.timer > duration)
            Kill(ref particle);
    }

    public override void Draw(Particle particle)
    {
        Main.spriteBatch.Draw(
            Texture.Value,
            particle.position - Main.screenPosition,
            null,
            particle.color * particle.opacity,
            0f,
            Texture.Size() * 0.5f,
            particle.scale * 0.33f,
            SpriteEffects.None,
            0
            );
    }
}
