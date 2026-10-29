using HendecamMod.Common.Systems.Particles;

namespace HendecamMod.Content.Particles;

public class CircleParticle : ParticleType
{
    public override void OnSpawn(ref Particle particle)
    {
        float minScaleMul = particle.data[0] == 0 ? 0.5f : particle.data[0];
        float maxScaleMul = particle.data[1] == 0 ? 1f : particle.data[1];
        particle.scale *= Main.rand.NextFloat(minScaleMul, maxScaleMul);
    }

    public override void Update(ref Particle particle)
    {
        particle.opacity -= 1 / 60f;

        if (particle.opacity <= 0)
            Kill(ref particle);
    }
}
