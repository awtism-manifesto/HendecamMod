using HendecamMod.Common.Systems.Particles;

namespace HendecamMod.Content.Particles;

public class TestParticle : ParticleType
{
    public override void OnSpawn(ref Particle particle)
    {

    }

    public override void Update(ref Particle particle)
    {
        particle.velocity.Y += 0.1f;
        particle.rotation += MathHelper.ToRadians(4f);
        if (particle.timer > 180)
        {
            Kill(ref particle);
        }

    }
}
