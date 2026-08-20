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
        particle.opacity -= 1 / 60f;
        particle.rotation += MathHelper.ToRadians(5);
        float risingSpeed = particle.data[0];
        particle.velocity.Y -= risingSpeed;
        particle.color = Color.Lerp(particle.color, Color.Black, 1 / 60f);
        if (risingSpeed == 0f)
        {
            particle.velocity *= 0.98f;
        }
        if (particle.opacity <= 0)
        {
            Kill(ref particle);
        }
    }
}
