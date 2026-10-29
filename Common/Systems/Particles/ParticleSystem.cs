using HendecamMod.Common.Systems.Particles.Renderers;
using System.Collections.Generic;

namespace HendecamMod.Common.Systems.Particles;

public class ParticleSystem : ModSystem
{
    public static List<ParticleType> TypesByID { get; private set; } = new();

    public override void SetStaticDefaults()
    {
        InitializeTypesByID();
    }

    public static int RegisterParticle(ParticleType typeInstance)
    {
        int particleID = TypesByID.Count;
        TypesByID.Add(typeInstance);
        return particleID;
    }

    public static void InitializeTypesByID()
    {
        ParticleID.RegisterParticles();

        for (int i = 0; i < ParticleID.Count; i++)
            TypesByID[i].Load(ModContent.GetInstance<ParticleSystem>().Mod);
    }

    public static ref Particle SpawnParticle<T>(int type, Vector2 position, Vector2 velocity, Color color = default, float opacity = 1f, float scale = 1f, float data0 = 0f, float data1 = 0f, float data2 = 0f, float data3 = 0f) where T : ParticleRenderer
    {
        return ref ModContent.GetInstance<T>().SpawnParticle(type, position, velocity, color, opacity, scale, data0, data1, data2, data3);
    }

    public static ref Particle SpawnParticle(int type, Vector2 position, Vector2 velocity, Color color = default, float opacity = 1f, float scale = 1f, float data0 = 0f, float data1 = 0f, float data2 = 0f, float data3 = 0f)
    {
        return ref ModContent.GetInstance<AfterDustParticleRenderer>().SpawnParticle(type, position, velocity, color, opacity, scale, data0, data1, data2, data3);
    }
}