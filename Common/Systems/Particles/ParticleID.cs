using HendecamMod.Content.Particles;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HendecamMod.Common.Systems.Particles;

/// <summary>
/// Contains all particle ids which can be used when calling ParticleSystem.SpawnParticle().
/// Particle IDs have to be assigned manually in ParticleSystem.InitializeTypesByID().
/// </summary>
public abstract class ParticleID
{
    public static void RegisterParticles()
    {
        TestParticle = ParticleSystem.RegisterParticle(new TestParticle());
        Streak = ParticleSystem.RegisterParticle(new StreakParticle());
        Fire = ParticleSystem.RegisterParticle(new FireParticle());
    }

    public static int Count => ParticleSystem.TypesByID.Count;
    public static int TestParticle;
    public static int Streak;
    public static int Fire;
}
