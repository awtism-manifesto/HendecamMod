using HendecamMod.Common.Systems.Particles;
using HendecamMod.Common.Utils;
namespace HendecamMod.Content.Particles;

public class FireParticle : ParticleType
{
    public override void OnSpawn(ref Particle particle)
    {
        particle.frame = Texture.RandomFrame(7);
    }

    public override void Update(ref Particle particle)
    { // Calls every frame the particle is active
        particle.opacity -= 1 / 30f;
        particle.position += particle.velocity;
        particle.rotation += MathHelper.ToRadians(5);
        particle.velocity *= 0.98f;
        particle.color = Color.Lerp(particle.color, Color.Transparent, 1 / 60f);
        if (particle.opacity <= 0)
        {
            Kill(ref particle);
        }
    }
}
