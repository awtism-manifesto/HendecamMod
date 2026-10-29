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
        Circle = ParticleSystem.RegisterParticle(new CircleParticle());
        Glowy = ParticleSystem.RegisterParticle(new GlowyParticle());
        Gas = ParticleSystem.RegisterParticle(new GasParticle());
    }

    public static int Count => ParticleSystem.TypesByID.Count;
    public static int TestParticle;
    public static int Streak;

    /// <summary>
    /// data[0] - Rising speed (how fast the dust moves upward per tick, <b>subtracted</b> from the Y velocity)
    /// </summary>
    public static int Gas;

    /// <summary>
    /// data[0] - Minimum scale multiplier (0 => 0.5f)
    /// data[1] - Maximum scale multiplier (0 => 1f)
    /// </summary>
    public static int Circle;

    /// <summary>
    /// data[0] - Duration
    /// data[1] - Fade in duration
    /// data[2] - Fade out duration
    /// data[3] - Slowdown multiplier
    /// </summary>
    public static int Glowy;
}
